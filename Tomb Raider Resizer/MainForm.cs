using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            cmbDockPosition.SelectedIndex = 4; // Default to Center.
            cmbDockPosition.SelectedIndexChanged += cmbDockPosition_SelectedIndexChanged;
        }

        /// <summary>
        /// Updates the enabled/disabled states of controls based on input validity, process status, and display mode.
        /// </summary>
        private void UpdateControlStates()
        {
            if (rbFullscreen.Checked)
            {
                // In fullscreen mode, width and height are not needed.
                txtWidth.Enabled = false;
                txtHeight.Enabled = false;
                // Force docking to Center and disable docking combobox.
                cmbDockPosition.SelectedIndex = 4;
                cmbDockPosition.Enabled = false;
                // Disable the "Remove Window Frame" checkbox.
                chkRemoveFrame.Enabled = false;
                // The resize button is enabled as soon as a process is found.
                btnResize.Enabled = isProcessFound;
            }
            else
            {
                // In non-fullscreen mode, validate width and height.
                txtWidth.Enabled = isProcessFound;
                txtHeight.Enabled = isProcessFound;
                cmbDockPosition.Enabled = isProcessFound;
                // Re-enable the "Remove Window Frame" checkbox.
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
                // Disable docking options and force Center.
                cmbDockPosition.SelectedIndex = 4;
                cmbDockPosition.Enabled = false;
                // Disable the "Remove Window Frame" checkbox.
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
        /// Timer tick event that checks for the process.
        /// </summary>
        private void ProcessCheckTimer_Tick(object sender, EventArgs e)
        {
            if (!(cmbGameList.SelectedItem is GameInfo game))
                return;

            Task.Run(() =>
            {
                bool processFound = game.ProcessNames.Any(procName => Process.GetProcessesByName(procName).Length > 0);
                this.BeginInvoke((Action)(() =>
                {
                    isProcessFound = processFound;
                    lblProcessStatus.Text = processFound ? "Process Found!" : "Not Connected!";
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
        /// Moves the game window to the selected monitor.
        /// </summary>
        private void cmbMonitor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbFullscreen.Checked)
            {
                // Im Fullscreen-Modus: Passe das Fenster an den neuen Monitor an.
                int monitorIndex = cmbMonitor.SelectedIndex;
                Rectangle bounds = Screen.AllScreens[monitorIndex].Bounds;
                if (cmbGameList.SelectedItem is GameInfo selectedGame)
                {
                    string processName = selectedGame.ProcessNames.FirstOrDefault();
                    if (!string.IsNullOrEmpty(processName))
                    {
                        bool removeFrame = chkRemoveFrame.Checked;
                        // Aktualisiere das Fenster im Borderless-Fullscreen-Modus auf dem neuen Monitor.
                        ResizeHelper.BorderlessFullscreenWindow(processName, removeFrame, false, bounds);
                    }
                }
            }
            else
            {
                // Normaler Modus: Verschiebe das Fenster in den Arbeitsbereich des neuen Monitors.
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
        /// In Fullscreen mode, executes the same action as Alt+Enter.
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
                // Fullscreen: Emulate Alt+Enter behavior using Borderless Fullscreen.
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
