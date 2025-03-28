namespace Tomb_Raider_Resizer
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ToolTip toolTip1;

        // Display Mode / Window Options
        private System.Windows.Forms.RadioButton rbForceWindowed;
        private System.Windows.Forms.RadioButton rbFullscreen;
        private System.Windows.Forms.ComboBox cmbDockPosition;
        private System.Windows.Forms.Label lblDocking;
        private System.Windows.Forms.Label lblMonitor;
        private System.Windows.Forms.ComboBox cmbMonitor;

        // Game Selection Group
        private System.Windows.Forms.Label lblGameSelect;
        private System.Windows.Forms.ComboBox cmbGameList;
        private System.Windows.Forms.Label lblProcessStatusLabel;
        private System.Windows.Forms.Label lblProcessStatus;
        private System.Windows.Forms.PictureBox pbProcess;

        // Resolution Controls
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.RadioButton rbResCustom;
        private System.Windows.Forms.RadioButton rbRes169;
        private System.Windows.Forms.RadioButton rbRes43;
        private System.Windows.Forms.ComboBox cmb169;
        private System.Windows.Forms.ComboBox cmb43;

        private System.Windows.Forms.Button btnResize;
        private System.Windows.Forms.CheckBox chkRemoveFrame;

        // Panels
        private System.Windows.Forms.Panel pnlGameSelection;
        private System.Windows.Forms.Panel pnlMonitorOptions;
        private System.Windows.Forms.Panel pnlResolutionOptions;
        private System.Windows.Forms.Panel pnlWindowOptions;

        // Header Labels
        private System.Windows.Forms.Label lblGameSelectionHeader;
        private System.Windows.Forms.Label lblMonitorOptionsHeader;
        private System.Windows.Forms.Label lblResolutionOptionsHeader;
        private System.Windows.Forms.Label lblWindowOptionsHeader;

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lblGameSelect = new System.Windows.Forms.Label();
            this.cmbGameList = new System.Windows.Forms.ComboBox();
            this.lblProcessStatusLabel = new System.Windows.Forms.Label();
            this.lblProcessStatus = new System.Windows.Forms.Label();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.lblWidth = new System.Windows.Forms.Label();
            this.lblHeight = new System.Windows.Forms.Label();
            this.rbResCustom = new System.Windows.Forms.RadioButton();
            this.rbRes169 = new System.Windows.Forms.RadioButton();
            this.rbRes43 = new System.Windows.Forms.RadioButton();
            this.cmb169 = new System.Windows.Forms.ComboBox();
            this.cmb43 = new System.Windows.Forms.ComboBox();
            this.rbForceWindowed = new System.Windows.Forms.RadioButton();
            this.rbFullscreen = new System.Windows.Forms.RadioButton();
            this.chkRemoveFrame = new System.Windows.Forms.CheckBox();
            this.lblDocking = new System.Windows.Forms.Label();
            this.cmbDockPosition = new System.Windows.Forms.ComboBox();
            this.lblMonitor = new System.Windows.Forms.Label();
            this.cmbMonitor = new System.Windows.Forms.ComboBox();
            this.btnResize = new System.Windows.Forms.Button();
            this.pbProcess = new System.Windows.Forms.PictureBox();
            this.lblGameSelectionHeader = new System.Windows.Forms.Label();
            this.lblMonitorOptionsHeader = new System.Windows.Forms.Label();
            this.lblResolutionOptionsHeader = new System.Windows.Forms.Label();
            this.lblWindowOptionsHeader = new System.Windows.Forms.Label();
            this.pnlGameSelection = new System.Windows.Forms.Panel();
            this.LL_AoDInfo = new System.Windows.Forms.LinkLabel();
            this.pnlMonitorOptions = new System.Windows.Forms.Panel();
            this.pnlResolutionOptions = new System.Windows.Forms.Panel();
            this.pnlWindowOptions = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pbProcess)).BeginInit();
            this.pnlGameSelection.SuspendLayout();
            this.pnlMonitorOptions.SuspendLayout();
            this.pnlResolutionOptions.SuspendLayout();
            this.pnlWindowOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblGameSelect
            // 
            this.lblGameSelect.AutoSize = true;
            this.lblGameSelect.Location = new System.Drawing.Point(6, 26);
            this.lblGameSelect.Name = "lblGameSelect";
            this.lblGameSelect.Size = new System.Drawing.Size(97, 16);
            this.lblGameSelect.TabIndex = 100;
            this.lblGameSelect.Text = "Choose Game:";
            this.toolTip1.SetToolTip(this.lblGameSelect, "Select a game.");
            // 
            // cmbGameList
            // 
            this.cmbGameList.FormattingEnabled = true;
            this.cmbGameList.Location = new System.Drawing.Point(108, 23);
            this.cmbGameList.Name = "cmbGameList";
            this.cmbGameList.Size = new System.Drawing.Size(359, 24);
            this.cmbGameList.TabIndex = 0;
            this.toolTip1.SetToolTip(this.cmbGameList, "Choose a game from the list.");
            // 
            // lblProcessStatusLabel
            // 
            this.lblProcessStatusLabel.AutoSize = true;
            this.lblProcessStatusLabel.Location = new System.Drawing.Point(6, 52);
            this.lblProcessStatusLabel.Name = "lblProcessStatusLabel";
            this.lblProcessStatusLabel.Size = new System.Drawing.Size(100, 16);
            this.lblProcessStatusLabel.TabIndex = 101;
            this.lblProcessStatusLabel.Text = "Process Status:";
            this.toolTip1.SetToolTip(this.lblProcessStatusLabel, "Shows the process status.");
            // 
            // lblProcessStatus
            // 
            this.lblProcessStatus.AutoSize = true;
            this.lblProcessStatus.Location = new System.Drawing.Point(105, 52);
            this.lblProcessStatus.Name = "lblProcessStatus";
            this.lblProcessStatus.Size = new System.Drawing.Size(143, 16);
            this.lblProcessStatus.TabIndex = 102;
            this.lblProcessStatus.Text = "Scanning for Process...";
            this.toolTip1.SetToolTip(this.lblProcessStatus, "Current process status.");
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(103, 76);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(54, 22);
            this.txtWidth.TabIndex = 8;
            this.toolTip1.SetToolTip(this.txtWidth, "Enter the desired width in pixels.");
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(187, 77);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(54, 22);
            this.txtHeight.TabIndex = 9;
            this.toolTip1.SetToolTip(this.txtHeight, "Enter the desired height in pixels.");
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(78, 80);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(23, 16);
            this.lblWidth.TabIndex = 103;
            this.lblWidth.Text = "W:";
            this.toolTip1.SetToolTip(this.lblWidth, "Width (px).");
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(163, 80);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(20, 16);
            this.lblHeight.TabIndex = 104;
            this.lblHeight.Text = "H:";
            this.toolTip1.SetToolTip(this.lblHeight, "Height (px).");
            // 
            // rbResCustom
            // 
            this.rbResCustom.AutoSize = true;
            this.rbResCustom.Location = new System.Drawing.Point(6, 78);
            this.rbResCustom.Name = "rbResCustom";
            this.rbResCustom.Size = new System.Drawing.Size(73, 20);
            this.rbResCustom.TabIndex = 7;
            this.rbResCustom.TabStop = true;
            this.rbResCustom.Text = "Custom";
            this.toolTip1.SetToolTip(this.rbResCustom, "Select custom resolution input.");
            this.rbResCustom.UseVisualStyleBackColor = true;
            // 
            // rbRes169
            // 
            this.rbRes169.AutoSize = true;
            this.rbRes169.Location = new System.Drawing.Point(5, 51);
            this.rbRes169.Name = "rbRes169";
            this.rbRes169.Size = new System.Drawing.Size(52, 20);
            this.rbRes169.TabIndex = 5;
            this.rbRes169.TabStop = true;
            this.rbRes169.Text = "16:9";
            this.toolTip1.SetToolTip(this.rbRes169, "Select 16:9 resolution mode.");
            this.rbRes169.UseVisualStyleBackColor = true;
            // 
            // rbRes43
            // 
            this.rbRes43.AutoSize = true;
            this.rbRes43.Location = new System.Drawing.Point(4, 28);
            this.rbRes43.Name = "rbRes43";
            this.rbRes43.Size = new System.Drawing.Size(45, 20);
            this.rbRes43.TabIndex = 3;
            this.rbRes43.TabStop = true;
            this.rbRes43.Text = "4:3";
            this.toolTip1.SetToolTip(this.rbRes43, "Select 4:3 resolution mode.");
            this.rbRes43.UseVisualStyleBackColor = true;
            // 
            // cmb169
            // 
            this.cmb169.FormattingEnabled = true;
            this.cmb169.Location = new System.Drawing.Point(103, 51);
            this.cmb169.Name = "cmb169";
            this.cmb169.Size = new System.Drawing.Size(121, 24);
            this.cmb169.TabIndex = 6;
            this.toolTip1.SetToolTip(this.cmb169, "Choose a 16:9 resolution from the list.");
            // 
            // cmb43
            // 
            this.cmb43.FormattingEnabled = true;
            this.cmb43.Location = new System.Drawing.Point(103, 24);
            this.cmb43.Name = "cmb43";
            this.cmb43.Size = new System.Drawing.Size(121, 24);
            this.cmb43.TabIndex = 4;
            this.toolTip1.SetToolTip(this.cmb43, "Choose a 4:3 resolution from the list.");
            // 
            // rbForceWindowed
            // 
            this.rbForceWindowed.AutoSize = true;
            this.rbForceWindowed.Location = new System.Drawing.Point(6, 25);
            this.rbForceWindowed.Name = "rbForceWindowed";
            this.rbForceWindowed.Size = new System.Drawing.Size(114, 20);
            this.rbForceWindowed.TabIndex = 10;
            this.rbForceWindowed.TabStop = true;
            this.rbForceWindowed.Text = "Window Mode";
            this.toolTip1.SetToolTip(this.rbForceWindowed, "Force the application to run in window mode.");
            this.rbForceWindowed.UseVisualStyleBackColor = true;
            // 
            // rbFullscreen
            // 
            this.rbFullscreen.AutoSize = true;
            this.rbFullscreen.Location = new System.Drawing.Point(126, 25);
            this.rbFullscreen.Name = "rbFullscreen";
            this.rbFullscreen.Size = new System.Drawing.Size(128, 20);
            this.rbFullscreen.TabIndex = 11;
            this.rbFullscreen.TabStop = true;
            this.rbFullscreen.Text = "Fullscreen Mode";
            this.toolTip1.SetToolTip(this.rbFullscreen, "Enable fullscreen mode.");
            this.rbFullscreen.UseVisualStyleBackColor = true;
            // 
            // chkRemoveFrame
            // 
            this.chkRemoveFrame.AutoSize = true;
            this.chkRemoveFrame.Location = new System.Drawing.Point(6, 49);
            this.chkRemoveFrame.Name = "chkRemoveFrame";
            this.chkRemoveFrame.Size = new System.Drawing.Size(174, 20);
            this.chkRemoveFrame.TabIndex = 12;
            this.chkRemoveFrame.Text = "Remove Window Frame";
            this.toolTip1.SetToolTip(this.chkRemoveFrame, "Remove the window border for a frameless look.");
            this.chkRemoveFrame.UseVisualStyleBackColor = true;
            // 
            // lblDocking
            // 
            this.lblDocking.AutoSize = true;
            this.lblDocking.Location = new System.Drawing.Point(3, 48);
            this.lblDocking.Name = "lblDocking";
            this.lblDocking.Size = new System.Drawing.Size(111, 16);
            this.lblDocking.TabIndex = 105;
            this.lblDocking.Text = "Docking Position:";
            this.toolTip1.SetToolTip(this.lblDocking, "Select the desired docking position for the window.");
            // 
            // cmbDockPosition
            // 
            this.cmbDockPosition.FormattingEnabled = true;
            this.cmbDockPosition.Location = new System.Drawing.Point(105, 45);
            this.cmbDockPosition.Name = "cmbDockPosition";
            this.cmbDockPosition.Size = new System.Drawing.Size(114, 24);
            this.cmbDockPosition.TabIndex = 2;
            this.toolTip1.SetToolTip(this.cmbDockPosition, "Choose the docking position.");
            // 
            // lblMonitor
            // 
            this.lblMonitor.AutoSize = true;
            this.lblMonitor.Location = new System.Drawing.Point(3, 24);
            this.lblMonitor.Name = "lblMonitor";
            this.lblMonitor.Size = new System.Drawing.Size(54, 16);
            this.lblMonitor.TabIndex = 106;
            this.lblMonitor.Text = "Monitor:";
            this.toolTip1.SetToolTip(this.lblMonitor, "Select the target monitor.");
            // 
            // cmbMonitor
            // 
            this.cmbMonitor.FormattingEnabled = true;
            this.cmbMonitor.Location = new System.Drawing.Point(105, 21);
            this.cmbMonitor.Name = "cmbMonitor";
            this.cmbMonitor.Size = new System.Drawing.Size(244, 24);
            this.cmbMonitor.TabIndex = 1;
            this.toolTip1.SetToolTip(this.cmbMonitor, "Choose the monitor on which to display the window.");
            // 
            // btnResize
            // 
            this.btnResize.Location = new System.Drawing.Point(176, 389);
            this.btnResize.Name = "btnResize";
            this.btnResize.Size = new System.Drawing.Size(135, 35);
            this.btnResize.TabIndex = 13;
            this.btnResize.Text = "Resize";
            this.toolTip1.SetToolTip(this.btnResize, "Apply the selected changes.");
            this.btnResize.UseVisualStyleBackColor = true;
            this.btnResize.Click += new System.EventHandler(this.btnResize_Click);
            // 
            // pbProcess
            // 
            this.pbProcess.Location = new System.Drawing.Point(215, 46);
            this.pbProcess.Name = "pbProcess";
            this.pbProcess.Size = new System.Drawing.Size(35, 25);
            this.pbProcess.TabIndex = 107;
            this.pbProcess.TabStop = false;
            this.toolTip1.SetToolTip(this.pbProcess, "Process status icon.");
            // 
            // lblGameSelectionHeader
            // 
            this.lblGameSelectionHeader.AutoSize = true;
            this.lblGameSelectionHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblGameSelectionHeader.Location = new System.Drawing.Point(3, 0);
            this.lblGameSelectionHeader.Name = "lblGameSelectionHeader";
            this.lblGameSelectionHeader.Size = new System.Drawing.Size(121, 17);
            this.lblGameSelectionHeader.TabIndex = 108;
            this.lblGameSelectionHeader.Text = "Process Details";
            this.toolTip1.SetToolTip(this.lblGameSelectionHeader, "Game selection and process details.");
            // 
            // lblMonitorOptionsHeader
            // 
            this.lblMonitorOptionsHeader.AutoSize = true;
            this.lblMonitorOptionsHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblMonitorOptionsHeader.Location = new System.Drawing.Point(3, 0);
            this.lblMonitorOptionsHeader.Name = "lblMonitorOptionsHeader";
            this.lblMonitorOptionsHeader.Size = new System.Drawing.Size(117, 17);
            this.lblMonitorOptionsHeader.TabIndex = 109;
            this.lblMonitorOptionsHeader.Text = "Monitor Details";
            this.toolTip1.SetToolTip(this.lblMonitorOptionsHeader, "Monitor and docking options.");
            // 
            // lblResolutionOptionsHeader
            // 
            this.lblResolutionOptionsHeader.AutoSize = true;
            this.lblResolutionOptionsHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblResolutionOptionsHeader.Location = new System.Drawing.Point(3, 2);
            this.lblResolutionOptionsHeader.Name = "lblResolutionOptionsHeader";
            this.lblResolutionOptionsHeader.Size = new System.Drawing.Size(140, 17);
            this.lblResolutionOptionsHeader.TabIndex = 110;
            this.lblResolutionOptionsHeader.Text = "Resolution Details";
            this.toolTip1.SetToolTip(this.lblResolutionOptionsHeader, "Select the desired resolution settings.");
            // 
            // lblWindowOptionsHeader
            // 
            this.lblWindowOptionsHeader.AutoSize = true;
            this.lblWindowOptionsHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblWindowOptionsHeader.Location = new System.Drawing.Point(3, 0);
            this.lblWindowOptionsHeader.Name = "lblWindowOptionsHeader";
            this.lblWindowOptionsHeader.Size = new System.Drawing.Size(118, 17);
            this.lblWindowOptionsHeader.TabIndex = 111;
            this.lblWindowOptionsHeader.Text = "Window Details";
            this.toolTip1.SetToolTip(this.lblWindowOptionsHeader, "Configure window mode and border options.");
            // 
            // pnlGameSelection
            // 
            this.pnlGameSelection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGameSelection.Controls.Add(this.LL_AoDInfo);
            this.pnlGameSelection.Controls.Add(this.pbProcess);
            this.pnlGameSelection.Controls.Add(this.lblGameSelect);
            this.pnlGameSelection.Controls.Add(this.lblProcessStatus);
            this.pnlGameSelection.Controls.Add(this.cmbGameList);
            this.pnlGameSelection.Controls.Add(this.lblProcessStatusLabel);
            this.pnlGameSelection.Controls.Add(this.lblGameSelectionHeader);
            this.pnlGameSelection.Location = new System.Drawing.Point(9, 10);
            this.pnlGameSelection.Name = "pnlGameSelection";
            this.pnlGameSelection.Size = new System.Drawing.Size(495, 96);
            this.pnlGameSelection.TabIndex = 112;
            this.toolTip1.SetToolTip(this.pnlGameSelection, "Panel for game selection and process status.");
            // 
            // LL_AoDInfo
            // 
            this.LL_AoDInfo.AutoSize = true;
            this.LL_AoDInfo.Location = new System.Drawing.Point(6, 73);
            this.LL_AoDInfo.Name = "LL_AoDInfo";
            this.LL_AoDInfo.Size = new System.Drawing.Size(485, 16);
            this.LL_AoDInfo.TabIndex = 200;
            this.LL_AoDInfo.TabStop = true;
            this.LL_AoDInfo.Text = "Note: For the proper experience, please be sure set your display scaling to 100%";
            this.toolTip1.SetToolTip(this.LL_AoDInfo, "Important note regarding game settings.");
            // 
            // pnlMonitorOptions
            // 
            this.pnlMonitorOptions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMonitorOptions.Controls.Add(this.lblDocking);
            this.pnlMonitorOptions.Controls.Add(this.cmbDockPosition);
            this.pnlMonitorOptions.Controls.Add(this.lblMonitor);
            this.pnlMonitorOptions.Controls.Add(this.cmbMonitor);
            this.pnlMonitorOptions.Controls.Add(this.lblMonitorOptionsHeader);
            this.pnlMonitorOptions.Location = new System.Drawing.Point(9, 112);
            this.pnlMonitorOptions.Name = "pnlMonitorOptions";
            this.pnlMonitorOptions.Size = new System.Drawing.Size(495, 76);
            this.pnlMonitorOptions.TabIndex = 113;
            this.toolTip1.SetToolTip(this.pnlMonitorOptions, "Panel for monitor selection and docking position.");
            // 
            // pnlResolutionOptions
            // 
            this.pnlResolutionOptions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlResolutionOptions.Controls.Add(this.rbRes43);
            this.pnlResolutionOptions.Controls.Add(this.rbRes169);
            this.pnlResolutionOptions.Controls.Add(this.rbResCustom);
            this.pnlResolutionOptions.Controls.Add(this.lblWidth);
            this.pnlResolutionOptions.Controls.Add(this.cmb43);
            this.pnlResolutionOptions.Controls.Add(this.txtHeight);
            this.pnlResolutionOptions.Controls.Add(this.cmb169);
            this.pnlResolutionOptions.Controls.Add(this.txtWidth);
            this.pnlResolutionOptions.Controls.Add(this.lblHeight);
            this.pnlResolutionOptions.Controls.Add(this.lblResolutionOptionsHeader);
            this.pnlResolutionOptions.Location = new System.Drawing.Point(9, 194);
            this.pnlResolutionOptions.Name = "pnlResolutionOptions";
            this.pnlResolutionOptions.Size = new System.Drawing.Size(495, 110);
            this.pnlResolutionOptions.TabIndex = 114;
            this.toolTip1.SetToolTip(this.pnlResolutionOptions, "Panel for selecting resolution options.");
            // 
            // pnlWindowOptions
            // 
            this.pnlWindowOptions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlWindowOptions.Controls.Add(this.rbForceWindowed);
            this.pnlWindowOptions.Controls.Add(this.rbFullscreen);
            this.pnlWindowOptions.Controls.Add(this.chkRemoveFrame);
            this.pnlWindowOptions.Controls.Add(this.lblWindowOptionsHeader);
            this.pnlWindowOptions.Location = new System.Drawing.Point(9, 310);
            this.pnlWindowOptions.Name = "pnlWindowOptions";
            this.pnlWindowOptions.Size = new System.Drawing.Size(495, 73);
            this.pnlWindowOptions.TabIndex = 115;
            this.toolTip1.SetToolTip(this.pnlWindowOptions, "Panel for window mode and border options.");
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(510, 428);
            this.Controls.Add(this.pnlResolutionOptions);
            this.Controls.Add(this.pnlWindowOptions);
            this.Controls.Add(this.pnlMonitorOptions);
            this.Controls.Add(this.pnlGameSelection);
            this.Controls.Add(this.btnResize);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Tomb Raider Resizer";
            ((System.ComponentModel.ISupportInitialize)(this.pbProcess)).EndInit();
            this.pnlGameSelection.ResumeLayout(false);
            this.pnlGameSelection.PerformLayout();
            this.pnlMonitorOptions.ResumeLayout(false);
            this.pnlMonitorOptions.PerformLayout();
            this.pnlResolutionOptions.ResumeLayout(false);
            this.pnlResolutionOptions.PerformLayout();
            this.pnlWindowOptions.ResumeLayout(false);
            this.pnlWindowOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.LinkLabel LL_AoDInfo;
    }
}
