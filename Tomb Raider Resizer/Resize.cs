using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace Tomb_Raider_Resizer
{
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

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

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
        const int WS_BORDER = 0x00800000;
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

        public enum DockPosition
        {
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight,
            Center
        }

        /// <summary>
        /// Ändert die Fenstergröße und -position des angegebenen Prozesses.
        /// Wird "Remove Window Frame" aktiviert, wird der Fensterstil explizit auf WS_POPUP gesetzt und das Fenster mehrfach neu gezeichnet.
        /// Diese Logik wird nun einheitlich für alle Auflösungsmodi (Custom, 16:9, 4:3) angewandt.
        /// </summary>
        public static void ResizeWindow(string processName, int width, int height, bool removeFrame, bool forceWindowed, Rectangle area, DockPosition dockPos, bool skipFlicker = false)
        {
            var process = Process.GetProcessesByName(processName).FirstOrDefault();
            if (process == null)
                return;
            IntPtr hwnd = process.MainWindowHandle;
            if (hwnd == IntPtr.Zero)
                return;

            // Setze den Fensterstil je nach Modus und ob der Rahmen entfernt werden soll.
            if (forceWindowed)
            {
                if (removeFrame)
                {
                    // Entferne Rahmen: WS_CAPTION, WS_THICKFRAME und WS_BORDER
                    int style = GetWindowLong(hwnd, GWL_STYLE) & ~(WS_CAPTION | WS_THICKFRAME | WS_BORDER);
                    SetWindowLongPtrCompat(hwnd, GWL_STYLE, new IntPtr(style));
                    int exStyle = GetWindowLong(hwnd, GWL_EXSTYLE) | WS_EX_APPWINDOW;
                    SetWindowLongPtrCompat(hwnd, GWL_EXSTYLE, new IntPtr(exStyle));
                    Thread.Sleep(100);
                }
                else
                {
                    SetWindowLongPtrCompat(hwnd, GWL_STYLE, WS_OVERLAPPEDWINDOW);
                    int exStyle = GetWindowLong(hwnd, GWL_EXSTYLE) | WS_EX_APPWINDOW;
                    SetWindowLongPtrCompat(hwnd, GWL_EXSTYLE, new IntPtr(exStyle));
                    ShowWindow(hwnd, SW_SHOWNORMAL);
                    Thread.Sleep(100);
                }
            }
            else
            {
                if (!removeFrame)
                {
                    SetWindowLongPtrCompat(hwnd, GWL_STYLE, WS_OVERLAPPEDWINDOW);
                    int exStyle = GetWindowLong(hwnd, GWL_EXSTYLE) | WS_EX_APPWINDOW;
                    SetWindowLongPtrCompat(hwnd, GWL_EXSTYLE, new IntPtr(exStyle));
                    ShowWindow(hwnd, SW_SHOWNORMAL);
                    Thread.Sleep(100);
                }
                else
                {
                    int style = GetWindowLong(hwnd, GWL_STYLE) & ~(WS_CAPTION | WS_THICKFRAME | WS_BORDER);
                    SetWindowLongPtrCompat(hwnd, GWL_STYLE, new IntPtr(style));
                    int exStyle = GetWindowLong(hwnd, GWL_EXSTYLE) | WS_EX_APPWINDOW;
                    SetWindowLongPtrCompat(hwnd, GWL_EXSTYLE, new IntPtr(exStyle));
                    Thread.Sleep(100);
                }
            }

            // Berechne die neue Position basierend auf der gewählten Docking-Position.
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

            // Setze Fenstergröße und -position.
            MoveWindow(hwnd, newX, newY, width, height, true);
            SetWindowPos(hwnd, IntPtr.Zero, 0, 0, 0, 0,
                SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_FRAMECHANGED);
            RefreshWindow(hwnd);

            if (!skipFlicker)
            {
                Thread.Sleep(50);
                MoveWindow(hwnd, newX, newY, width + 1, height + 1, true);
                Thread.Sleep(20);
                MoveWindow(hwnd, newX, newY, width, height, true);
            }

            // Wenn "Remove Window Frame" aktiviert ist, setze nun den WS_POPUP-Stil und aktualisiere das Fenster
            // – dies entspricht exakt der Logik, die bei 16:9 und 4:3 funktioniert.
            if (removeFrame)
            {
                SetWindowLongPtrCompat(hwnd, GWL_STYLE, new IntPtr(unchecked((int)0x80000000))); // WS_POPUP
                SetWindowPos(hwnd, IntPtr.Zero, newX, newY, width, height, SWP_SHOWWINDOW | SWP_FRAMECHANGED);
                RefreshWindow(hwnd);
                SetForegroundWindow(hwnd);
                Thread.Sleep(50);
                MoveWindow(hwnd, newX, newY, width + 1, height + 1, true);
                Thread.Sleep(20);
                MoveWindow(hwnd, newX, newY, width, height, true);
            }
        }

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
            int newX, newY;
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

        public static void BorderlessFullscreenWindow(string processName, bool removeFrame, bool forceWindowed, Rectangle bounds)
        {
            var process = Process.GetProcessesByName(processName).FirstOrDefault();
            if (process == null)
                return;
            IntPtr hwnd = process.MainWindowHandle;
            if (hwnd == IntPtr.Zero)
                return;

            // Für Fullscreen immer rahmenlos: WS_POPUP
            SetWindowLongPtrCompat(hwnd, GWL_STYLE, new IntPtr(unchecked((int)0x80000000))); // WS_POPUP
            int exStyle = GetWindowLong(hwnd, GWL_EXSTYLE) & ~0x00000008;
            exStyle |= WS_EX_APPWINDOW;
            SetWindowLongPtrCompat(hwnd, GWL_EXSTYLE, new IntPtr(exStyle));
            ShowWindow(hwnd, SW_SHOWNORMAL);
            Thread.Sleep(100);
            int x = bounds.Left;
            int y = bounds.Top;
            int width = bounds.Width;
            int height = bounds.Height;
            IntPtr HWND_NOTOPMOST = new IntPtr(-2);
            SetWindowPos(hwnd, HWND_NOTOPMOST, x, y, width, height, SWP_SHOWWINDOW | SWP_FRAMECHANGED);
            RefreshWindow(hwnd);
            SetForegroundWindow(hwnd);
            Thread.Sleep(50);
            MoveWindow(hwnd, x, y, width + 1, height + 1, true);
            Thread.Sleep(20);
            MoveWindow(hwnd, x, y, width, height, true);
        }

        private static void RefreshWindow(IntPtr hwnd)
        {
            InvalidateRect(hwnd, IntPtr.Zero, true);
            UpdateWindow(hwnd);
            RedrawWindow(hwnd, IntPtr.Zero, IntPtr.Zero,
                RDW_INVALIDATE | RDW_ERASE | RDW_ALLCHILDREN | RDW_UPDATENOW);
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
