using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using Tomb_Raider_Resizer.Properties;

namespace TombRaiderResizer
{
    /// <summary>
    /// Main form for the Tomb Raider Resizer application.
    /// </summary>
    public partial class MainForm : Form
    {
        private List<GameInfo> games = new List<GameInfo>();
        private Timer processCheckTimer;
        private bool isProcessFound = false;

        public MainForm()
        {
            InitializeComponent();
            ApplySystemTheme(); // Passe das Thema beim Start an.

            // Set a fixed dialog style for a cleaner UI.
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Auto-select "Forced Windowed Mode" at startup.
            rbForceWindowed.Checked = true;

            InitializeGameList();
            InitializeMonitorComboBox();
            InitializeDockPositionComboBox();

            // Attach events to update control states when text changes.
            txtWidth.TextChanged += (s, e) => UpdateControlStates();
            txtHeight.TextChanged += (s, e) => UpdateControlStates();

            // Attach event handler for display mode change (radio buttons).
            rbFullscreen.CheckedChanged += DisplayModeChanged;
            rbForceWindowed.CheckedChanged += DisplayModeChanged;

            // Disable the resize button until valid inputs and process detection.
            btnResize.Enabled = false;
            UpdateControlStates();

            // Start checking the process status for the initially selected game.
            if (cmbGameList.SelectedItem is GameInfo initialGame)
            {
                StartProcessCheck(initialGame);
            }
        }

        /// <summary>
        /// Prüft anhand der Registry, ob Windows im hellen Modus ist.
        /// Gibt true zurück, wenn "AppsUseLightTheme" auf 1 steht (hell), ansonsten false (dunkel).
        /// </summary>
        private bool IsLightTheme()
        {
            object registryValue = Registry.GetValue(
                @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize",
                "AppsUseLightTheme", 1);
            if (registryValue is int value)
            {
                return value == 1;
            }
            return true;
        }

        /// <summary>
        /// Passt die Farben des Formulars und seiner Controls entsprechend dem Systemthema an.
        /// </summary>
        private void ApplySystemTheme()
        {
            if (IsLightTheme())
            {
                // Heller Modus
                this.BackColor = Color.White;
                this.ForeColor = Color.Black;
                // Weitere Anpassungen, z. B. Panel-Farben, können hier ergänzt werden.
            }
            else
            {
                // Dunkler Modus – typisches Windows-Dunkelgrau
                this.BackColor = Color.FromArgb(45, 45, 48);
                this.ForeColor = Color.White;
            }
        }

        /// <summary>
        /// Überschreibt WndProc, um auf WM_SETTINGCHANGE zu reagieren (Thema-Wechsel).
        /// </summary>
        /// <param name="m">Das Nachrichtenobjekt.</param>
        protected override void WndProc(ref Message m)
        {
            const int WM_SETTINGCHANGE = 0x001A;
            if (m.Msg == WM_SETTINGCHANGE)
            {
                string param = Marshal.PtrToStringUni(m.LParam);
                if (!string.IsNullOrEmpty(param) &&
                    (param.Contains("AppsUseLightTheme") || param.Contains("ImmersiveColorSet")))
                {
                    ApplySystemTheme();
                }
            }
            base.WndProc(ref m);
        }

        /// <summary>
        /// Sets the process status icon in the PictureBox using resources.
        /// Skalierte das grüne Icon auf 24x24 Pixel.
        /// </summary>
        private void SetProcessStatusIcon(Image img)
        {
            if (img == Tomb_Raider_Resizer.Properties.Resources.greenCheckmark)
            {
                Bitmap resized = new Bitmap(img, new Size(24, 24));
                pbProcess.Image = resized;
            }
            else
            {
                pbProcess.Image = img;
            }
        }

        /// <summary>
        /// Populates the game list.
        /// </summary>
        private void InitializeGameList()
        {
            games.Add(new GameInfo("Tomb Raider I-III Starring Lara Croft", "tomb123"));
            games.Add(new GameInfo("Tomb Raider IV-VI Remastered", "tomb456"));
            games.Add(new GameInfo("Tomb Raider The Angel of Darkness (2003)", "TRAOD", "TRAOD_P3", "TRAOD_P4"));

            cmbGameList.DataSource = games;
            cmbGameList.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGameList.SelectedIndexChanged += cmbGameList_SelectedIndexChanged;
        }

        /// <summary>
        /// Populates the monitor selection combo box.
        /// </summary>
        private void InitializeMonitorComboBox()
        {
            cmbMonitor.Items.Clear();
            foreach (Screen screen in Screen.AllScreens)
            {
                cmbMonitor.Items.Add($"{screen.DeviceName} ({screen.Bounds.Width}x{screen.Bounds.Height})");
            }
            if (cmbMonitor.Items.Count > 0)
            {
                cmbMonitor.SelectedIndex = 0;
            }
            cmbMonitor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMonitor.SelectedIndexChanged += cmbMonitor_SelectedIndexChanged;
        }

        /// <summary>
        /// Populates the docking position combo box.
        /// </summary>
        private void InitializeDockPositionComboBox()
        {
            cmbDockPosition.Items.Clear();
            cmbDockPosition.Items.Add("Top Left");
            cmbDockPosition.Items.Add("Top Right");
            cmbDockPosition.Items.Add("Bottom Left");
            cmbDockPosition.Items.Add("Bottom Right");
            cmbDockPosition.Items.Add("Center");
            cmbDockPosition.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDockPosition.SelectedIndex = 4;
            cmbDockPosition.SelectedIndexChanged += cmbDockPosition_SelectedIndexChanged;
        }

        /// <summary>
        /// Updates the enabled/disabled states of controls based on input validity, process status, and display mode.
        /// </summary>
        private void UpdateControlStates()
        {
            if (rbFullscreen.Checked)
            {
                txtWidth.Enabled = false;
                txtHeight.Enabled = false;
                cmbDockPosition.SelectedIndex = 4;
                cmbDockPosition.Enabled = false;
                chkRemoveFrame.Enabled = false;
                btnResize.Enabled = isProcessFound;
            }
            else
            {
                txtWidth.Enabled = isProcessFound;
                txtHeight.Enabled = isProcessFound;
                cmbDockPosition.Enabled = isProcessFound;
                chkRemoveFrame.Enabled = isProcessFound;
                bool validInputs = int.TryParse(txtWidth.Text, out _) && int.TryParse(txtHeight.Text, out _);
                btnResize.Enabled = validInputs && isProcessFound;
            }

            cmbMonitor.Enabled = isProcessFound;
            rbForceWindowed.Enabled = isProcessFound;
            rbFullscreen.Enabled = isProcessFound;
            lblMonitor.Enabled = isProcessFound;
            lblDocking.Enabled = isProcessFound;
            lblResolution.Enabled = isProcessFound;
            lblWindowOptions.Enabled = isProcessFound;
            lblNote.Enabled = isProcessFound;
            lblWidth.Enabled = isProcessFound;
            lblHeight.Enabled = isProcessFound;
            lblWindowExtras.Enabled = isProcessFound;
        }

        /// <summary>
        /// Called when the display mode radio buttons change.
        /// In Fullscreen mode, clears and disables the width/height fields, sets docking to Center, and disables the "Remove Window Frame" checkbox.
        /// </summary>
        private void DisplayModeChanged(object sender, EventArgs e)
        {
            if (rbFullscreen.Checked)
            {
                txtWidth.Text = "";
                txtHeight.Text = "";
                txtWidth.Enabled = false;
                txtHeight.Enabled = false;
                cmbDockPosition.SelectedIndex = 4;
                cmbDockPosition.Enabled = false;
                chkRemoveFrame.Enabled = false;
            }
            else
            {
                txtWidth.Enabled = isProcessFound;
                txtHeight.Enabled = isProcessFound;
                cmbDockPosition.Enabled = isProcessFound;
                chkRemoveFrame.Enabled = isProcessFound;
            }
            UpdateControlStates();
        }

        /// <summary>
        /// Starts a timer to periodically check if the process for the selected game is running.
        /// </summary>
        private void StartProcessCheck(GameInfo game)
        {
            processCheckTimer?.Stop();
            processCheckTimer?.Dispose();

            processCheckTimer = new Timer { Interval = 1000 };
            processCheckTimer.Tick += ProcessCheckTimer_Tick;
            processCheckTimer.Start();
        }

        /// <summary>
        /// Timer tick event that checks for the process and updates the status icon.
        /// Zeigt solange "Searching..." an, bis der Prozess gefunden wird.
        /// </summary>
        private void ProcessCheckTimer_Tick(object sender, EventArgs e)
        {
            if (!(cmbGameList.SelectedItem is GameInfo game))
                return;

            // Zeige während der Suche das Lade-GIF.
            SetProcessStatusIcon(Tomb_Raider_Resizer.Properties.Resources.loadingAnimated);

            Task.Run(() =>
            {
                bool processFound = game.ProcessNames.Any(procName => Process.GetProcessesByName(procName).Length > 0);
                this.BeginInvoke((Action)(() =>
                {
                    if (processFound)
                    {
                        isProcessFound = true;
                        lblProcessStatus.Text = "Process Found!";
                        SetProcessStatusIcon(Tomb_Raider_Resizer.Properties.Resources.greenCheckmark);
                    }
                    else
                    {
                        isProcessFound = false;
                        lblProcessStatus.Text = "Searching...";
                        SetProcessStatusIcon(Tomb_Raider_Resizer.Properties.Resources.loadingAnimated);
                    }
                    UpdateControlStates();
                }));
            });
        }

        /// <summary>
        /// Handles the game selection change event.
        /// </summary>
        private void cmbGameList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbGameList.SelectedItem is GameInfo selectedGame)
            {
                StartProcessCheck(selectedGame);
            }
        }

        /// <summary>
        /// Handles the monitor selection change event.
        /// If fullscreen is active, adjusts the window to the new monitor bounds; otherwise, moves the window.
        /// </summary>
        private void cmbMonitor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbFullscreen.Checked)
            {
                int monitorIndex = cmbMonitor.SelectedIndex;
                Rectangle bounds = Screen.AllScreens[monitorIndex].Bounds;
                if (cmbGameList.SelectedItem is GameInfo selectedGame)
                {
                    string processName = selectedGame.ProcessNames.FirstOrDefault();
                    if (!string.IsNullOrEmpty(processName))
                    {
                        bool removeFrame = chkRemoveFrame.Checked;
                        ResizeHelper.BorderlessFullscreenWindow(processName, removeFrame, false, bounds);
                    }
                }
            }
            else
            {
                MoveGameWindowToSelectedMonitor();
            }
        }

        /// <summary>
        /// Moves the game window to the monitor selected in cmbMonitor.
        /// </summary>
        private void MoveGameWindowToSelectedMonitor()
        {
            int monitorIndex = cmbMonitor.SelectedIndex;
            if (monitorIndex < 0 || monitorIndex >= Screen.AllScreens.Length)
                return;

            Screen selectedScreen = Screen.AllScreens[monitorIndex];
            Rectangle workingArea = selectedScreen.WorkingArea;

            if (cmbGameList.SelectedItem is GameInfo selectedGame)
            {
                string processName = selectedGame.ProcessNames.FirstOrDefault();
                if (!string.IsNullOrEmpty(processName))
                {
                    ResizeHelper.MoveWindowToMonitor(processName, workingArea);
                }
            }
        }

        /// <summary>
        /// Handles the docking position selection change event.
        /// </summary>
        private void cmbDockPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            int monitorIndex = cmbMonitor.SelectedIndex;
            if (monitorIndex < 0 || monitorIndex >= Screen.AllScreens.Length)
                return;

            Rectangle workingArea = Screen.AllScreens[monitorIndex].WorkingArea;
            if (cmbGameList.SelectedItem is GameInfo selectedGame)
            {
                string processName = selectedGame.ProcessNames.FirstOrDefault();
                if (!string.IsNullOrEmpty(processName))
                {
                    ResizeHelper.DockWindowToMonitor(processName, workingArea, (ResizeHelper.DockPosition)cmbDockPosition.SelectedIndex);
                }
            }
        }

        /// <summary>
        /// Handles the click event for the resize button.
        /// In Fullscreen mode, executes the same action as Alt+Enter (Borderless Fullscreen).
        /// In non-fullscreen mode, performs the standard resize with docking.
        /// </summary>
        private void btnResize_Click(object sender, EventArgs e)
        {
            if (!(cmbGameList.SelectedItem is GameInfo selectedGame))
                return;

            int monitorIndex = cmbMonitor.SelectedIndex;
            if (monitorIndex < 0 || monitorIndex >= Screen.AllScreens.Length)
                return;

            Rectangle workingArea = Screen.AllScreens[monitorIndex].WorkingArea;
            bool removeFrame = chkRemoveFrame.Checked;

            if (rbFullscreen.Checked)
            {
                Rectangle bounds = Screen.AllScreens[monitorIndex].Bounds;
                ResizeHelper.BorderlessFullscreenWindow(selectedGame.ProcessNames.FirstOrDefault(), removeFrame, false, bounds);
            }
            else
            {
                if (!int.TryParse(txtWidth.Text, out int width) ||
                    !int.TryParse(txtHeight.Text, out int height))
                    return;

                bool forceWindowed = rbForceWindowed.Checked;
                var dockPos = (ResizeHelper.DockPosition)cmbDockPosition.SelectedIndex;
                ResizeHelper.ResizeWindow(selectedGame.ProcessNames.FirstOrDefault(), width, height, removeFrame, forceWindowed, workingArea, dockPos);
            }
        }
    }
}
