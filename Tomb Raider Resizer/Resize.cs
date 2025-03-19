using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace Tomb_Raider_Resizer
{
    class Resize
    {
        // Für 64-Bit: SetWindowLongPtr; für 32-Bit: SetWindowLong
        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr", SetLastError = true)]
        private static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong", SetLastError = true)]
        private static extern IntPtr SetWindowLong32(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        // Kompatible Methode, die je nach Plattform die richtige Funktion aufruft
        private static IntPtr SetWindowLongPtrCompat(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
        {
            return IntPtr.Size == 8
                ? SetWindowLongPtr64(hWnd, nIndex, dwNewLong)
                : SetWindowLong32(hWnd, nIndex, dwNewLong);
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
            int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll")]
        public static extern bool MoveWindow(IntPtr hwnd, int x, int y, int width, int height, bool repaint);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);

        // RedrawWindow für komplettes Neuzeichnen
        [DllImport("user32.dll")]
        static extern bool RedrawWindow(IntPtr hWnd, IntPtr lprcUpdate, IntPtr hrgnUpdate, uint flags);

        // UpdateWindow, um sofort ein WM_PAINT zu erzwingen
        [DllImport("user32.dll")]
        static extern bool UpdateWindow(IntPtr hWnd);

        // InvalidateRect, um das Fenster zum Neuzeichnen zu markieren
        [DllImport("user32.dll")]
        static extern bool InvalidateRect(IntPtr hWnd, IntPtr lpRect, bool bErase);

        // P/Invoke: GetWindowRect, um die aktuelle Fensterposition und Größe zu ermitteln
        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        // Struktur für Fensterrechteck
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        public enum DockPosition
        {
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight,
            Center
        }


        // Konstanten für die Fenster-Manipulation
        const int SW_SHOWNORMAL = 1;
        const int GWL_STYLE = -16;
        const int GWL_EXSTYLE = -20;
        static readonly IntPtr WS_OVERLAPPEDWINDOW = new IntPtr(0x00CF0000); // Standard-Windowed-Stil
        const int WS_CAPTION = 0x00C00000;    // Titelleiste
        const int WS_THICKFRAME = 0x00040000; // Größenändern-Rahmen
        const int WS_EX_APPWINDOW = 0x00040000; // Sorgt dafür, dass das Fenster in der Taskleiste bleibt

        const int SWP_NOMOVE = 0x0002;
        const int SWP_NOSIZE = 0x0001;
        const int SWP_NOZORDER = 0x0004;
        const int SWP_FRAMECHANGED = 0x0020;

        // Flags für RedrawWindow
        const uint RDW_INVALIDATE = 0x0001;
        const uint RDW_ERASE = 0x0004;
        const uint RDW_ALLCHILDREN = 0x0080;
        const uint RDW_UPDATENOW = 0x0100;

        public static void ResizeWindow(string ProcessName, int x, int y, bool RemoveFrame, bool ForceWindowed)
        {
            // Prozess anhand des Namens suchen
            var gameProcess = Process.GetProcessesByName(ProcessName).FirstOrDefault();
            if (gameProcess == null)
            {
                return;
            }

            // Fensterhandle ermitteln
            IntPtr hwnd = gameProcess.MainWindowHandle;
            if (hwnd == IntPtr.Zero)
            {
                return;
            }

            // Falls der Windowed-Modus erzwungen werden soll:
            if (ForceWindowed)
            {
                // Setze den Fensterstil auf den Standard-Windowed-Stil
                SetWindowLongPtrCompat(hwnd, GWL_STYLE, WS_OVERLAPPEDWINDOW);

                // Setze den Extended Style, damit das Fenster in der Taskleiste bleibt
                int exStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
                exStyle |= WS_EX_APPWINDOW;
                SetWindowLongPtrCompat(hwnd, GWL_EXSTYLE, new IntPtr(exStyle));

                // Mache das Fenster sichtbar
                ShowWindow(hwnd, SW_SHOWNORMAL);

                // Kurze Pause, damit der Prozess die Änderung verarbeiten kann
                Thread.Sleep(100);
            }

            // Falls der Rahmen entfernt werden soll:
            if (RemoveFrame)
            {
                int style = GetWindowLong(hwnd, GWL_STYLE);
                // Entferne die Bits für Titelleiste und dicken Rahmen
                style &= ~(WS_CAPTION | WS_THICKFRAME);
                SetWindowLongPtrCompat(hwnd, GWL_STYLE, new IntPtr(style));

                // Optional: Auch hier den Extended Style anpassen, falls nötig
                int exStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
                exStyle |= WS_EX_APPWINDOW;
                SetWindowLongPtrCompat(hwnd, GWL_EXSTYLE, new IntPtr(exStyle));

                Thread.Sleep(100);
            }

            // Fenstergröße anpassen
            MoveWindow(hwnd, 0, 0, x, y, true);

            // Änderungen übernehmen
            SetWindowPos(hwnd, IntPtr.Zero, 0, 0, 0, 0,
                SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_FRAMECHANGED);

            // Versuche, das Fenster zur Neuzeichnung zu zwingen
            InvalidateRect(hwnd, IntPtr.Zero, true);
            UpdateWindow(hwnd);
            RedrawWindow(hwnd, IntPtr.Zero, IntPtr.Zero, RDW_INVALIDATE | RDW_ERASE | RDW_ALLCHILDREN | RDW_UPDATENOW);

            // --- Flicker-Trick: Simuliere eine minimale Größenänderung ---
            Thread.Sleep(50);
            MoveWindow(hwnd, 0, 0, x + 1, y + 1, true);
            Thread.Sleep(20);
            MoveWindow(hwnd, 0, 0, x, y, true);
        }

        /// <summary>
        /// Verschiebt das Fenster des angegebenen Prozesses (über seinen Namen) in den Arbeitsbereich des übergebenen Monitors.
        /// Das Fenster wird im Arbeitsbereich (WorkingArea) des Monitors zentriert.
        /// </summary>
        /// <param name="ProcessName">Name des Prozesses</param>
        /// <param name="workingArea">Der Arbeitsbereich (WorkingArea) des Zielmonitors</param>
        public static void MoveWindowToMonitor(string ProcessName, Rectangle workingArea)
        {
            var gameProcess = Process.GetProcessesByName(ProcessName).FirstOrDefault();
            if (gameProcess == null)
                return;

            IntPtr hwnd = gameProcess.MainWindowHandle;
            if (hwnd == IntPtr.Zero)
                return;

            // Ermittele die aktuelle Fenstergröße
            if (!GetWindowRect(hwnd, out RECT rect))
                return;
            int windowWidth = rect.Right - rect.Left;
            int windowHeight = rect.Bottom - rect.Top;

            // Berechne neue, zentrierte Position im Arbeitsbereich
            int newX = workingArea.Left + (workingArea.Width - windowWidth) / 2;
            int newY = workingArea.Top + (workingArea.Height - windowHeight) / 2;

            // Verschiebe das Fenster
            MoveWindow(hwnd, newX, newY, windowWidth, windowHeight, true);
        }

        /// <summary>
        /// Verschiebt das Fenster des angegebenen Prozesses in den Arbeitsbereich (WorkingArea) des Zielmonitors
        /// und dockt es an die gewünschte Position.
        /// </summary>
        /// <param name="ProcessName">Name des Prozesses</param>
        /// <param name="workingArea">Arbeitsbereich des Zielmonitors</param>
        /// <param name="dockPos">Gewünschte Dock-Position</param>
        public static void DockWindowToMonitor(string ProcessName, Rectangle workingArea, DockPosition dockPos)
        {
            var gameProcess = Process.GetProcessesByName(ProcessName).FirstOrDefault();
            if (gameProcess == null)
                return;

            IntPtr hwnd = gameProcess.MainWindowHandle;
            if (hwnd == IntPtr.Zero)
                return;

            // Ermittele die aktuelle Fenstergröße
            if (!GetWindowRect(hwnd, out RECT rect))
                return;
            int windowWidth = rect.Right - rect.Left;
            int windowHeight = rect.Bottom - rect.Top;

            int newX = 0, newY = 0;
            switch (dockPos)
            {
                case DockPosition.TopLeft:
                    newX = workingArea.Left;
                    newY = workingArea.Top;
                    break;
                case DockPosition.TopRight:
                    newX = workingArea.Right - windowWidth;
                    newY = workingArea.Top;
                    break;
                case DockPosition.BottomLeft:
                    newX = workingArea.Left;
                    newY = workingArea.Bottom - windowHeight;
                    break;
                case DockPosition.BottomRight:
                    newX = workingArea.Right - windowWidth;
                    newY = workingArea.Bottom - windowHeight;
                    break;
                case DockPosition.Center:
                default:
                    newX = workingArea.Left + (workingArea.Width - windowWidth) / 2;
                    newY = workingArea.Top + (workingArea.Height - windowHeight) / 2;
                    break;
            }

            // Fenster verschieben
            MoveWindow(hwnd, newX, newY, windowWidth, windowHeight, true);
            SetWindowPos(hwnd, IntPtr.Zero, newX, newY, 0, 0,
                SWP_NOSIZE | SWP_NOZORDER | SWP_FRAMECHANGED);
        }

        public static void ResizeWindow(string ProcessName, int x, int y, bool RemoveFrame, bool ForceWindowed, Rectangle workingArea, DockPosition dockPos)
        {
            // (Vorherige Logik: Fenster vorbereiten, z.B. Windowed-Mode erzwingen, Rahmen entfernen etc.)
            var gameProcess = Process.GetProcessesByName(ProcessName).FirstOrDefault();
            if (gameProcess == null)
                return;

            IntPtr hwnd = gameProcess.MainWindowHandle;
            if (hwnd == IntPtr.Zero)
                return;

            if (ForceWindowed)
            {
                SetWindowLongPtrCompat(hwnd, GWL_STYLE, WS_OVERLAPPEDWINDOW);
                int exStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
                exStyle |= WS_EX_APPWINDOW;
                SetWindowLongPtrCompat(hwnd, GWL_EXSTYLE, new IntPtr(exStyle));
                ShowWindow(hwnd, SW_SHOWNORMAL);
                Thread.Sleep(100);
            }

            if (RemoveFrame)
            {
                int style = GetWindowLong(hwnd, GWL_STYLE);
                style &= ~(WS_CAPTION | WS_THICKFRAME);
                SetWindowLongPtrCompat(hwnd, GWL_STYLE, new IntPtr(style));
                int exStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
                exStyle |= WS_EX_APPWINDOW;
                SetWindowLongPtrCompat(hwnd, GWL_EXSTYLE, new IntPtr(exStyle));
                Thread.Sleep(100);
            }

            // Neue Position basierend auf der gewünschten Docking-Position berechnen:
            int newX, newY;
            switch (dockPos)
            {
                case DockPosition.TopLeft:
                    newX = workingArea.Left;
                    newY = workingArea.Top;
                    break;
                case DockPosition.TopRight:
                    newX = workingArea.Right - x;
                    newY = workingArea.Top;
                    break;
                case DockPosition.BottomLeft:
                    newX = workingArea.Left;
                    newY = workingArea.Bottom - y;
                    break;
                case DockPosition.BottomRight:
                    newX = workingArea.Right - x;
                    newY = workingArea.Bottom - y;
                    break;
                case DockPosition.Center:
                default:
                    newX = workingArea.Left + (workingArea.Width - x) / 2;
                    newY = workingArea.Top + (workingArea.Height - y) / 2;
                    break;
            }

            // Fenstergröße und Position anpassen
            MoveWindow(hwnd, newX, newY, x, y, true);

            // Änderungen übernehmen
            SetWindowPos(hwnd, IntPtr.Zero, 0, 0, 0, 0,
                SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_FRAMECHANGED);
            InvalidateRect(hwnd, IntPtr.Zero, true);
            UpdateWindow(hwnd);
            RedrawWindow(hwnd, IntPtr.Zero, IntPtr.Zero, RDW_INVALIDATE | RDW_ERASE | RDW_ALLCHILDREN | RDW_UPDATENOW);

            // --- Flicker-Trick: minimale Größenänderung simulieren ---
            Thread.Sleep(50);
            MoveWindow(hwnd, newX, newY, x + 1, y + 1, true);
            Thread.Sleep(20);
            MoveWindow(hwnd, newX, newY, x, y, true);
        }



    }
}
