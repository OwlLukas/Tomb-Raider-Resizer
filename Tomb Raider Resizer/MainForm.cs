using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using Tomb_Raider_Resizer.Properties;
using Tomb_Raider_Resizer;
using TombRaiderResizer;

namespace Tomb_Raider_Resizer
{
    public partial class MainForm : Form
    {
        private List<GameInfo> games = new List<GameInfo>();
        private Timer processCheckTimer;
        private bool isProcessFound = false;
        // Debounce counter for process check (only after several failures status switches)
        private int notFoundCounter = 0;
        private const int failureThreshold = 5;

        public MainForm()
        {
            InitializeComponent();
            ApplySystemTheme();

            // Use fixed dialog style so user cannot resize the form manually.
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Set default display mode and resolution mode.
            rbForceWindowed.Checked = true;
            rbRes169.Checked = true;

            InitializeGameList();
            InitializeMonitorComboBox();
            InitializeDockPositionComboBox();
            FillResolutionCombos();
            AttachResolutionRadioEvents();

            // Custom resolution text boxes update control states on change.
            txtWidth.TextChanged += (s, e) => UpdateControlStates();
            txtHeight.TextChanged += (s, e) => UpdateControlStates();

            rbFullscreen.CheckedChanged += DisplayModeChanged;
            rbForceWindowed.CheckedChanged += DisplayModeChanged;

            // Adjust docking position automatically when the monitor changes.
            cmbDockPosition.SelectedIndexChanged += cmbDockPosition_SelectedIndexChanged;

            // Update resolution controls based on the selected resolution mode.
            UpdateResolutionControls();

            btnResize.Enabled = false;
            UpdateControlStates();

            // Check the initially selected game and set LL_AoDInfo accordingly.
            if (cmbGameList.SelectedItem is GameInfo initialGame)
            {
                if (initialGame.Title.Contains("Angel of Darkness"))
                {
                    LL_AoDInfo.Text = "Note: Make sure to set the game to windowed in TRAODSCU. Only resizing available.";
                }
                else
                {
                    LL_AoDInfo.Text = "Note: For the proper experience, please be sure set your display scaling to 100%";
                }
                LL_AoDInfo.Links.Clear();
                // Underline only "Note: " (6 characters)
                LL_AoDInfo.Links.Add(0, 6, null);
                LL_AoDInfo.LinkColor = LL_AoDInfo.ForeColor;
                LL_AoDInfo.ActiveLinkColor = LL_AoDInfo.ForeColor;
                LL_AoDInfo.VisitedLinkColor = LL_AoDInfo.ForeColor;

                lblProcessStatus.Text = "Searching...";
                SetProcessStatusIcon(Tomb_Raider_Resizer.Properties.Resources.loadingAnimated);
                StartProcessCheck(initialGame);
            }
        }

        #region Update & Display Methods

        private void UpdateControlStates()
        {
            // If Fullscreen Mode is activated, disable all resolution controls.
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
                if (rbResCustom.Checked)
                {
                    txtWidth.Enabled = isProcessFound;
                    txtHeight.Enabled = isProcessFound;
                    cmb169.Enabled = false;
                    cmb43.Enabled = false;
                    bool validInputs = int.TryParse(txtWidth.Text.Trim(), out _) &&
                                       int.TryParse(txtHeight.Text.Trim(), out _);
                    btnResize.Enabled = validInputs && isProcessFound;
                }
                else if (rbRes169.Checked)
                {
                    txtWidth.Enabled = false;
                    txtHeight.Enabled = false;
                    cmb169.Enabled = isProcessFound;
                    cmb43.Enabled = false;
                    btnResize.Enabled = isProcessFound && cmb169.SelectedItem != null;
                }
                else if (rbRes43.Checked)
                {
                    txtWidth.Enabled = false;
                    txtHeight.Enabled = false;
                    cmb169.Enabled = false;
                    cmb43.Enabled = isProcessFound;
                    btnResize.Enabled = isProcessFound && cmb43.SelectedItem != null;
                }
                rbResCustom.Enabled = isProcessFound;
                rbRes169.Enabled = isProcessFound;
                rbRes43.Enabled = isProcessFound;
                cmbDockPosition.Enabled = isProcessFound;
                chkRemoveFrame.Enabled = isProcessFound;
            }
            cmbMonitor.Enabled = isProcessFound;
            rbForceWindowed.Enabled = isProcessFound;
            rbFullscreen.Enabled = isProcessFound;

            lblMonitor.Enabled = isProcessFound;
            lblDocking.Enabled = isProcessFound;
            lblWidth.Enabled = isProcessFound;
            lblHeight.Enabled = isProcessFound;

            // Specific adjustments for "Tomb Raider The Angel of Darkness"
            if (cmbGameList.SelectedItem is GameInfo selectedGame &&
                selectedGame.Title.Contains("Angel of Darkness"))
            {
                // Disable Fullscreen radiobutton and monitor combobox.
                rbFullscreen.Enabled = false;
                cmbMonitor.Enabled = false;

                // If Fullscreen was accidentally selected, switch back to Window Mode.
                if (rbFullscreen.Checked)
                {
                    rbForceWindowed.Checked = true;
                }
            }
        }

        private void DisplayModeChanged(object sender, EventArgs e)
        {
            // Simply update control states when the display mode changes.
            UpdateControlStates();
        }

        #endregion

        #region Resolution Combo Handling

        private void FillResolutionCombos()
        {
            int minWidth = 640;
            int maxWidth = 5000;

            cmb169.Items.Clear();
            for (int width = minWidth; width <= maxWidth; width += 16)
            {
                if ((width * 9) % 16 == 0)
                {
                    int height = width * 9 / 16;
                    cmb169.Items.Add($"{width} x {height}");
                }
            }
            // Default select "1280 x 720" if available.
            int index1280 = -1;
            for (int i = 0; i < cmb169.Items.Count; i++)
            {
                if (cmb169.Items[i].ToString() == "1280 x 720")
                {
                    index1280 = i;
                    break;
                }
            }
            if (index1280 >= 0)
                cmb169.SelectedIndex = index1280;
            else if (cmb169.Items.Count > 0)
                cmb169.SelectedIndex = 0;

            cmb43.Items.Clear();
            for (int width = minWidth; width <= maxWidth; width += 4)
            {
                if ((width * 3) % 4 == 0)
                {
                    int height = width * 3 / 4;
                    cmb43.Items.Add($"{width} x {height}");
                }
            }
            if (cmb43.Items.Count > 0)
                cmb43.SelectedIndex = 0;
        }

        private void AttachResolutionRadioEvents()
        {
            rbResCustom.CheckedChanged += (s, e) => UpdateResolutionControls();
            rbRes169.CheckedChanged += (s, e) => UpdateResolutionControls();
            rbRes43.CheckedChanged += (s, e) => UpdateResolutionControls();
        }

        private void UpdateResolutionControls()
        {
            if (rbResCustom.Checked)
            {
                txtWidth.Enabled = true;
                txtHeight.Enabled = true;
                cmb169.Enabled = false;
                cmb43.Enabled = false;
            }
            else if (rbRes169.Checked)
            {
                txtWidth.Enabled = false;
                txtHeight.Enabled = false;
                cmb169.Enabled = isProcessFound;
                cmb43.Enabled = false;
            }
            else if (rbRes43.Checked)
            {
                txtWidth.Enabled = false;
                txtHeight.Enabled = false;
                cmb169.Enabled = false;
                cmb43.Enabled = isProcessFound;
            }
            UpdateControlStates();
        }

        #endregion

        #region Theme & Process Check

        private bool IsLightTheme()
        {
            object registryValue = Registry.GetValue(
                @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize",
                "AppsUseLightTheme", 1);
            return (registryValue is int value) ? (value == 1) : true;
        }

        private void ApplySystemTheme()
        {
            if (IsLightTheme())
            {
                this.BackColor = Color.White;
                this.ForeColor = Color.Black;
                btnResize.BackColor = SystemColors.Control;
                btnResize.ForeColor = SystemColors.ControlText;
            }
            else
            {
                this.BackColor = Color.FromArgb(45, 45, 48);
                this.ForeColor = Color.White;
                // In Dark Mode, use a darker gray for the Resize button.
                btnResize.BackColor = Color.FromArgb(63, 63, 70);
                btnResize.ForeColor = Color.White;
            }
            // Update colors for controls (except ComboBoxes)
            UpdateControlColors(this.Controls);

            // Update LL_AoDInfo link colors to match current mode.
            if (LL_AoDInfo != null)
            {
                LL_AoDInfo.LinkColor = LL_AoDInfo.ForeColor;
                LL_AoDInfo.ActiveLinkColor = LL_AoDInfo.ForeColor;
                LL_AoDInfo.VisitedLinkColor = LL_AoDInfo.ForeColor;
            }
        }

        private void UpdateControlColors(Control.ControlCollection controls)
        {
            foreach (Control ctrl in controls)
            {
                if (!(ctrl is ComboBox))
                {
                    if (ctrl is Label ||
                        ctrl is RadioButton ||
                        ctrl is CheckBox ||
                        ctrl is LinkLabel ||
                        ctrl is Button)
                    {
                        ctrl.ForeColor = this.ForeColor;
                    }
                }
                if (ctrl.HasChildren)
                {
                    UpdateControlColors(ctrl.Controls);
                }
            }
        }

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

        private void SetProcessStatusIcon(System.Drawing.Image img)
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

        private void StartProcessCheck(GameInfo game)
        {
            processCheckTimer?.Stop();
            processCheckTimer?.Dispose();

            processCheckTimer = new Timer { Interval = 1000 };
            processCheckTimer.Tick += ProcessCheckTimer_Tick;
            processCheckTimer.Start();
        }

        private void ProcessCheckTimer_Tick(object sender, EventArgs e)
        {
            if (!(cmbGameList.SelectedItem is GameInfo game))
                return;

            Task.Run(() =>
            {
                bool processFound = game.ProcessNames.Any(procName => Process.GetProcessesByName(procName).Length > 0);
                this.BeginInvoke((Action)(() =>
                {
                    if (processFound)
                    {
                        notFoundCounter = 0;
                        if (!isProcessFound)
                        {
                            isProcessFound = true;
                            lblProcessStatus.Text = "Process Found!";
                            SetProcessStatusIcon(Tomb_Raider_Resizer.Properties.Resources.greenCheckmark);
                            // When process is found, default to 16:9 resolution.
                            rbRes169.Checked = true;
                        }
                    }
                    else
                    {
                        notFoundCounter++;
                        if (notFoundCounter >= failureThreshold && isProcessFound)
                        {
                            isProcessFound = false;
                            lblProcessStatus.Text = "Searching...";
                            SetProcessStatusIcon(Tomb_Raider_Resizer.Properties.Resources.loadingAnimated);
                        }
                    }
                    UpdateControlStates();
                }));
            });
        }

        #endregion

        #region Initialization of Lists and Controls

        private void InitializeGameList()
        {
            games.Add(new GameInfo("Tomb Raider I-III Starring Lara Croft", "tomb123"));
            games.Add(new GameInfo("Tomb Raider IV-VI Remastered", "tomb456"));
            games.Add(new GameInfo("Tomb Raider The Angel of Darkness (2003)", "TRAOD", "TRAOD_P3", "TRAOD_P4"));

            cmbGameList.DataSource = games;
            cmbGameList.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGameList.SelectedIndexChanged += cmbGameList_SelectedIndexChanged;
        }

        private void InitializeMonitorComboBox()
        {
            cmbMonitor.Items.Clear();
            foreach (Screen screen in Screen.AllScreens)
            {
                cmbMonitor.Items.Add($"{screen.DeviceName} ({screen.Bounds.Width}x{screen.Bounds.Height})");
            }
            if (cmbMonitor.Items.Count > 0)
                cmbMonitor.SelectedIndex = 0;
            cmbMonitor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMonitor.SelectedIndexChanged += cmbMonitor_SelectedIndexChanged;
        }

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

        #endregion

        #region Event Handlers

        private void cmbGameList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbGameList.SelectedItem is GameInfo selectedGame)
            {
                notFoundCounter = 0;
                isProcessFound = false;
                lblProcessStatus.Text = "Searching...";
                SetProcessStatusIcon(Tomb_Raider_Resizer.Properties.Resources.loadingAnimated);
                StartProcessCheck(selectedGame);

                if (selectedGame.Title.Contains("Angel of Darkness"))
                {
                    rbForceWindowed.Checked = true;
                    LL_AoDInfo.Text = "Note: You need to set the window mode in TRAODSCU. Only window resizing is available.";
                }
                else
                {
                    LL_AoDInfo.Text = "Note: For the proper experience, please be sure set your display scaling to 100%.";
                }
                LL_AoDInfo.Links.Clear();
                // Underline only "Note: " (6 characters)
                LL_AoDInfo.Links.Add(0, 6, null);
                LL_AoDInfo.LinkColor = LL_AoDInfo.ForeColor;
                LL_AoDInfo.ActiveLinkColor = LL_AoDInfo.ForeColor;
                LL_AoDInfo.VisitedLinkColor = LL_AoDInfo.ForeColor;
                UpdateControlStates();
            }
        }

        private void cmbMonitor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbFullscreen.Checked)
            {
                int monitorIndex = cmbMonitor.SelectedIndex;
                Rectangle bounds = Screen.AllScreens[monitorIndex].Bounds;
                if (cmbGameList.SelectedItem is GameInfo selectedGame)
                {
                    string processName = GetActiveProcessName(selectedGame);
                    if (!string.IsNullOrEmpty(processName))
                    {
                        bool removeFrame = chkRemoveFrame.Checked;
                        ResizeHelper.BorderlessFullscreenWindow(processName, removeFrame, false, bounds);
                    }
                }
            }
            else
            {
                // When the monitor changes, also consider the current docking position.
                MoveGameWindowToSelectedMonitor();
            }
        }

        private void MoveGameWindowToSelectedMonitor()
        {
            int monitorIndex = cmbMonitor.SelectedIndex;
            if (monitorIndex < 0 || monitorIndex >= Screen.AllScreens.Length)
                return;
            Screen selectedScreen = Screen.AllScreens[monitorIndex];
            Rectangle workingArea = selectedScreen.WorkingArea;
            if (cmbGameList.SelectedItem is GameInfo selectedGame)
            {
                string processName = GetActiveProcessName(selectedGame);
                if (!string.IsNullOrEmpty(processName))
                {
                    var dockPos = (ResizeHelper.DockPosition)cmbDockPosition.SelectedIndex;
                    // Reposition window according to docking position.
                    ResizeHelper.DockWindowToMonitor(processName, workingArea, dockPos);
                }
            }
        }

        private void cmbDockPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Automatically adjust the docking position.
            if (!isProcessFound) return;
            if (rbFullscreen.Checked) return;
            if (!(cmbGameList.SelectedItem is GameInfo selectedGame)) return;
            int monitorIndex = cmbMonitor.SelectedIndex;
            if (monitorIndex < 0 || monitorIndex >= Screen.AllScreens.Length)
                return;
            Rectangle workingArea = Screen.AllScreens[monitorIndex].WorkingArea;
            var dockPos = (ResizeHelper.DockPosition)cmbDockPosition.SelectedIndex;
            // Move window to new docking position.
            ResizeHelper.DockWindowToMonitor(GetActiveProcessName(selectedGame), workingArea, dockPos);
        }

        private void btnResize_Click(object sender, EventArgs e)
        {
            if (!(cmbGameList.SelectedItem is GameInfo selectedGame))
                return;

            int monitorIndex = cmbMonitor.SelectedIndex;
            if (monitorIndex < 0 || monitorIndex >= Screen.AllScreens.Length)
                return;

            Rectangle workingArea = Screen.AllScreens[monitorIndex].WorkingArea;
            bool removeFrame = chkRemoveFrame.Checked;
            int width = 0, height = 0;

            // If Fullscreen is selected, execute the fullscreen branch.
            if (rbFullscreen.Checked)
            {
                Rectangle bounds = Screen.AllScreens[monitorIndex].Bounds;
                ResizeHelper.BorderlessFullscreenWindow(
                    GetActiveProcessName(selectedGame),
                    removeFrame,
                    false,
                    bounds);
            }
            else if (rbResCustom.Checked)
            {
                if (!int.TryParse(txtWidth.Text.Trim(), out width) ||
                    !int.TryParse(txtHeight.Text.Trim(), out height))
                    return;
                ResizeHelper.ResizeWindow(
                    GetActiveProcessName(selectedGame),
                    width,
                    height,
                    removeFrame,
                    rbForceWindowed.Checked,
                    workingArea,
                    (ResizeHelper.DockPosition)cmbDockPosition.SelectedIndex,
                    true);
            }
            else if (rbRes169.Checked)
            {
                string res = cmb169.SelectedItem.ToString();
                var parts = res.Split('x');
                if (parts.Length == 2)
                {
                    if (!int.TryParse(parts[0].Trim(), out width) ||
                        !int.TryParse(parts[1].Trim(), out height))
                        return;
                }
                bool forceWindowed = rbForceWindowed.Checked;
                var dockPos = (ResizeHelper.DockPosition)cmbDockPosition.SelectedIndex;
                ResizeHelper.ResizeWindow(
                    GetActiveProcessName(selectedGame),
                    width,
                    height,
                    removeFrame,
                    forceWindowed,
                    workingArea,
                    dockPos);
            }
            else if (rbRes43.Checked)
            {
                string res = cmb43.SelectedItem.ToString();
                var parts = res.Split('x');
                if (parts.Length == 2)
                {
                    if (!int.TryParse(parts[0].Trim(), out width) ||
                        !int.TryParse(parts[1].Trim(), out height))
                        return;
                }
                bool forceWindowed = rbForceWindowed.Checked;
                var dockPos = (ResizeHelper.DockPosition)cmbDockPosition.SelectedIndex;
                ResizeHelper.ResizeWindow(
                    GetActiveProcessName(selectedGame),
                    width,
                    height,
                    removeFrame,
                    forceWindowed,
                    workingArea,
                    dockPos);
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Iterates through the process names stored in GameInfo and returns the first that is running.
        /// Otherwise, returns null.
        /// </summary>
        private string GetActiveProcessName(GameInfo game)
        {
            foreach (var procName in game.ProcessNames)
            {
                if (Process.GetProcessesByName(procName).Length > 0)
                    return procName;
            }
            return null;
        }

        #endregion
    }
}
