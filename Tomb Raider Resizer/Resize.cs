using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace TombRaiderResizer
{
    /// <summary>
    /// Provides methods to resize, reposition, and modify window styles using Win32 API calls.
    /// </summary>
    public static class ResizeHelper
    {
        #region Win32 API Imports

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr", SetLastError = true)]
        private static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong", SetLastError = true)]
        private static extern IntPtr SetWindowLong32(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        private static IntPtr SetWindowLongPtrCompat(IntPtr hWnd, int nIndex, IntPtr dwNewLong) =>
            IntPtr.Size == 8 ? SetWindowLongPtr64(hWnd, nIndex, dwNewLong) : SetWindowLong32(hWnd, nIndex, dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
            int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll")]
        public static extern bool MoveWindow(IntPtr hwnd, int x, int y, int width, int height, bool repaint);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);

        [DllImport("user32.dll")]
        static extern bool RedrawWindow(IntPtr hWnd, IntPtr lprcUpdate, IntPtr hrgnUpdate, uint flags);

        [DllImport("user32.dll")]
        static extern bool UpdateWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern bool InvalidateRect(IntPtr hWnd, IntPtr lpRect, bool bErase);

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        // Import SetForegroundWindow to bring the window to the foreground.
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        // Structure for window rectangle.
        private struct RECT
        {
            public int Left, Top, Right, Bottom;
        }

        #endregion

        #region Constants

        const int SW_SHOWNORMAL = 1;
        const int GWL_STYLE = -16;
        const int GWL_EXSTYLE = -20;
        static readonly IntPtr WS_OVERLAPPEDWINDOW = new IntPtr(0x00CF0000);
        const int WS_CAPTION = 0x00C00000;
        const int WS_THICKFRAME = 0x00040000;
        const int WS_EX_APPWINDOW = 0x00040000;

        const int SWP_NOMOVE = 0x0002;
        const int SWP_NOSIZE = 0x0001;
        const int SWP_NOZORDER = 0x0004;
        const int SWP_FRAMECHANGED = 0x0020;
        const int SWP_SHOWWINDOW = 0x0040;

        const uint RDW_INVALIDATE = 0x0001;
        const uint RDW_ERASE = 0x0004;
        const uint RDW_ALLCHILDREN = 0x0080;
        const uint RDW_UPDATENOW = 0x0100;

        #endregion

        /// <summary>
        /// Docking positions for window placement.
        /// </summary>
        public enum DockPosition
        {
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight,
            Center
        }

        /// <summary>
        /// Resizes and repositions the window for the given process.
        /// </summary>
        public static void ResizeWindow(string processName, int width, int height, bool removeFrame, bool forceWindowed)
        {
            var process = Process.GetProcessesByName(processName).FirstOrDefault();
            if (process == null)
                return;

            IntPtr hwnd = process.MainWindowHandle;
            if (hwnd == IntPtr.Zero)
                return;

            if (forceWindowed)
            {
                SetWindowLongPtrCompat(hwnd, GWL_STYLE, WS_OVERLAPPEDWINDOW);
                int exStyle = GetWindowLong(hwnd, GWL_EXSTYLE) | WS_EX_APPWINDOW;
                SetWindowLongPtrCompat(hwnd, GWL_EXSTYLE, new IntPtr(exStyle));
                ShowWindow(hwnd, SW_SHOWNORMAL);
                Thread.Sleep(100);
            }

            if (removeFrame)
            {
                int style = GetWindowLong(hwnd, GWL_STYLE);
                style &= ~(WS_CAPTION | WS_THICKFRAME);
                SetWindowLongPtrCompat(hwnd, GWL_STYLE, new IntPtr(style));

                int exStyle = GetWindowLong(hwnd, GWL_EXSTYLE) | WS_EX_APPWINDOW;
                SetWindowLongPtrCompat(hwnd, GWL_EXSTYLE, new IntPtr(exStyle));
                Thread.Sleep(100);
            }

            MoveWindow(hwnd, 0, 0, width, height, true);
            SetWindowPos(hwnd, IntPtr.Zero, 0, 0, 0, 0,
                SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_FRAMECHANGED);
            RefreshWindow(hwnd);

            Thread.Sleep(50);
            MoveWindow(hwnd, 0, 0, width + 1, height + 1, true);
            Thread.Sleep(20);
            MoveWindow(hwnd, 0, 0, width, height, true);
        }

        /// <summary>
        /// Moves the window of the specified process to the center of the given area.
        /// </summary>
        public static void MoveWindowToMonitor(string processName, Rectangle area)
        {
            var process = Process.GetProcessesByName(processName).FirstOrDefault();
            if (process == null)
                return;

            IntPtr hwnd = process.MainWindowHandle;
            if (hwnd == IntPtr.Zero)
                return;

            if (!GetWindowRect(hwnd, out RECT rect))
                return;
            int windowWidth = rect.Right - rect.Left;
            int windowHeight = rect.Bottom - rect.Top;

            int newX = area.Left + (area.Width - windowWidth) / 2;
            int newY = area.Top + (area.Height - windowHeight) / 2;
            MoveWindow(hwnd, newX, newY, windowWidth, windowHeight, true);
        }

        /// <summary>
        /// Docks the window to a specific position within the given area.
        /// </summary>
        public static void DockWindowToMonitor(string processName, Rectangle area, DockPosition dockPos)
        {
            var process = Process.GetProcessesByName(processName).FirstOrDefault();
            if (process == null)
                return;

            IntPtr hwnd = process.MainWindowHandle;
            if (hwnd == IntPtr.Zero)
                return;

            if (!GetWindowRect(hwnd, out RECT rect))
                return;
            int windowWidth = rect.Right - rect.Left;
            int windowHeight = rect.Bottom - rect.Top;

            int newX = 0, newY = 0;
            switch (dockPos)
            {
                case DockPosition.TopLeft:
                    newX = area.Left;
                    newY = area.Top;
                    break;
                case DockPosition.TopRight:
                    newX = area.Right - windowWidth;
                    newY = area.Top;
                    break;
                case DockPosition.BottomLeft:
                    newX = area.Left;
                    newY = area.Bottom - windowHeight;
                    break;
                case DockPosition.BottomRight:
                    newX = area.Right - windowWidth;
                    newY = area.Bottom - windowHeight;
                    break;
                case DockPosition.Center:
                default:
                    newX = area.Left + (area.Width - windowWidth) / 2;
                    newY = area.Top + (area.Height - windowHeight) / 2;
                    break;
            }

            MoveWindow(hwnd, newX, newY, windowWidth, windowHeight, true);
            SetWindowPos(hwnd, IntPtr.Zero, newX, newY, 0, 0,
                SWP_NOSIZE | SWP_NOZORDER | SWP_FRAMECHANGED);
        }

        /// <summary>
        /// Resizes the window and then docks it to the desired position within the given area.
        /// </summary>
        public static void ResizeWindow(string processName, int width, int height, bool removeFrame, bool forceWindowed, Rectangle area, DockPosition dockPos)
        {
            var process = Process.GetProcessesByName(processName).FirstOrDefault();
            if (process == null)
                return;

            IntPtr hwnd = process.MainWindowHandle;
            if (hwnd == IntPtr.Zero)
                return;

            if (forceWindowed)
            {
                SetWindowLongPtrCompat(hwnd, GWL_STYLE, WS_OVERLAPPEDWINDOW);
                int exStyle = GetWindowLong(hwnd, GWL_EXSTYLE) | WS_EX_APPWINDOW;
                SetWindowLongPtrCompat(hwnd, GWL_EXSTYLE, new IntPtr(exStyle));
                ShowWindow(hwnd, SW_SHOWNORMAL);
                Thread.Sleep(100);
            }

            if (removeFrame)
            {
                int style = GetWindowLong(hwnd, GWL_STYLE) & ~(WS_CAPTION | WS_THICKFRAME);
                SetWindowLongPtrCompat(hwnd, GWL_STYLE, new IntPtr(style));
                int exStyle = GetWindowLong(hwnd, GWL_EXSTYLE) | WS_EX_APPWINDOW;
                SetWindowLongPtrCompat(hwnd, GWL_EXSTYLE, new IntPtr(exStyle));
                Thread.Sleep(100);
            }

            int newX, newY;
            switch (dockPos)
            {
                case DockPosition.TopLeft:
                    newX = area.Left;
                    newY = area.Top;
                    break;
                case DockPosition.TopRight:
                    newX = area.Right - width;
                    newY = area.Top;
                    break;
                case DockPosition.BottomLeft:
                    newX = area.Left;
                    newY = area.Bottom - height;
                    break;
                case DockPosition.BottomRight:
                    newX = area.Right - width;
                    newY = area.Bottom - height;
                    break;
                case DockPosition.Center:
                default:
                    newX = area.Left + (area.Width - width) / 2;
                    newY = area.Top + (area.Height - height) / 2;
                    break;
            }

            MoveWindow(hwnd, newX, newY, width, height, true);
            SetWindowPos(hwnd, IntPtr.Zero, 0, 0, 0, 0,
                SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_FRAMECHANGED);
            RefreshWindow(hwnd);

            Thread.Sleep(50);
            MoveWindow(hwnd, newX, newY, width + 1, height + 1, true);
            Thread.Sleep(20);
            MoveWindow(hwnd, newX, newY, width, height, true);
        }

        /// <summary>
        /// Sets the window to borderless fullscreen mode.
        /// This method configures the window to cover the entire monitor (using Screen.Bounds),
        /// sets the window style to WS_POPUP (removing borders and title bar),
        /// and positions the window without forcing it topmost so that andere Fenster bei Fokuswechsel 
        /// darübergebracht werden können.
        /// </summary>
        /// <param name="processName">Name of the process.</param>
        /// <param name="removeFrame">Ignored in fullscreen mode.</param>
        /// <param name="forceWindowed">Ignored in fullscreen mode.</param>
        /// <param name="bounds">The full bounds of the target monitor (use Screen.AllScreens[…].Bounds).</param>
        public static void BorderlessFullscreenWindow(string processName, bool removeFrame, bool forceWindowed, Rectangle bounds)
        {
            var process = Process.GetProcessesByName(processName).FirstOrDefault();
            if (process == null)
                return;

            IntPtr hwnd = process.MainWindowHandle;
            if (hwnd == IntPtr.Zero)
                return;

            // Set window style to WS_POPUP (no borders, no title bar)
            const int WS_POPUP = unchecked((int)0x80000000);
            SetWindowLongPtrCompat(hwnd, GWL_STYLE, new IntPtr(WS_POPUP));

            // Remove any topmost flag so that other windows can be placed above when not active.
            int exStyle = GetWindowLong(hwnd, GWL_EXSTYLE) & ~0x00000008; // Remove WS_EX_TOPMOST (0x00000008)
            exStyle |= WS_EX_APPWINDOW;
            SetWindowLongPtrCompat(hwnd, GWL_EXSTYLE, new IntPtr(exStyle));

            // Show the window.
            ShowWindow(hwnd, SW_SHOWNORMAL);
            Thread.Sleep(100);

            // Set window bounds to cover the entire monitor (including Taskbar area).
            int x = bounds.Left;
            int y = bounds.Top;
            int width = bounds.Width;
            int height = bounds.Height;

            // Use HWND_NOTOPMOST (-2) so the window is not forced above all others.
            IntPtr HWND_NOTOPMOST = new IntPtr(-2);
            SetWindowPos(hwnd, HWND_NOTOPMOST, x, y, width, height, SWP_SHOWWINDOW | SWP_FRAMECHANGED);
            RefreshWindow(hwnd);

            // Bring the window to the foreground.
            SetForegroundWindow(hwnd);

            Thread.Sleep(50);
            MoveWindow(hwnd, x, y, width + 1, height + 1, true);
            Thread.Sleep(20);
            MoveWindow(hwnd, x, y, width, height, true);
        }

        /// <summary>
        /// Forces the window to refresh its display.
        /// </summary>
        /// <param name="hwnd">Handle of the window.</param>
        private static void RefreshWindow(IntPtr hwnd)
        {
            InvalidateRect(hwnd, IntPtr.Zero, true);
            UpdateWindow(hwnd);
            RedrawWindow(hwnd, IntPtr.Zero, IntPtr.Zero, RDW_INVALIDATE | RDW_ERASE | RDW_ALLCHILDREN | RDW_UPDATENOW);
        }

        #region Display Settings API

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct DEVMODE
        {
            private const int CCHDEVICENAME = 32;
            private const int CCHFORMNAME = 32;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHDEVICENAME)]
            public string dmDeviceName;
            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;
            public int dmFields;
            public int dmPositionX;
            public int dmPositionY;
            public int dmDisplayOrientation;
            public int dmDisplayFixedOutput;
            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHFORMNAME)]
            public string dmFormName;
            public short dmLogPixels;
            public int dmBitsPerPel;
            public int dmPelsWidth;
            public int dmPelsHeight;
            public int dmDisplayFlags;
            public int dmDisplayFrequency;
            public int dmICMMethod;
            public int dmICMIntent;
            public int dmMediaType;
            public int dmDitherType;
            public int dmReserved1;
            public int dmReserved2;
            public int dmPanningWidth;
            public int dmPanningHeight;
        }

        #endregion
    }
}
