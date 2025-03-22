namespace Tomb_Raider_Resizer
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ToolTip toolTip1;

        // Display Mode / Window Options (in eigenem Panel)
        private System.Windows.Forms.RadioButton rbForceWindowed;
        private System.Windows.Forms.RadioButton rbFullscreen;
        private System.Windows.Forms.ComboBox cmbDockPosition;
        private System.Windows.Forms.Label lblDocking;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.Label lblMonitor;
        private System.Windows.Forms.ComboBox cmbMonitor;

        // Game Selection Group
        private System.Windows.Forms.Label lblGameSelect;
        private System.Windows.Forms.ComboBox cmbGameList;
        private System.Windows.Forms.Label lblProcessStatusLabel;
        private System.Windows.Forms.Label lblProcessStatus;
        private System.Windows.Forms.PictureBox pbProcess;

        // Auflösungs-Controls (Radiobuttons, TextBoxes, ComboBoxen)
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

        // Panels zur Gruppierung
        private System.Windows.Forms.Panel pnlGameSelection;
        private System.Windows.Forms.Panel pnlMonitorOptions;
        private System.Windows.Forms.Panel pnlResolutionOptions;
        private System.Windows.Forms.Panel pnlWindowOptions;

        // Header-Labels für die Panels
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
            this.pbProcess = new System.Windows.Forms.PictureBox();
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
            this.lblNote = new System.Windows.Forms.Label();
            this.lblMonitor = new System.Windows.Forms.Label();
            this.cmbMonitor = new System.Windows.Forms.ComboBox();
            this.btnResize = new System.Windows.Forms.Button();
            this.lblGameSelectionHeader = new System.Windows.Forms.Label();
            this.lblMonitorOptionsHeader = new System.Windows.Forms.Label();
            this.lblResolutionOptionsHeader = new System.Windows.Forms.Label();
            this.lblWindowOptionsHeader = new System.Windows.Forms.Label();
            this.pnlGameSelection = new System.Windows.Forms.Panel();
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
            this.lblGameSelect.Size = new System.Drawing.Size(89, 15);
            this.lblGameSelect.TabIndex = 1;
            this.lblGameSelect.Text = "Choose Game:";
            this.toolTip1.SetToolTip(this.lblGameSelect, "Select a game.");
            // 
            // cmbGameList
            // 
            this.cmbGameList.FormattingEnabled = true;
            this.cmbGameList.Location = new System.Drawing.Point(108, 23);
            this.cmbGameList.Name = "cmbGameList";
            this.cmbGameList.Size = new System.Drawing.Size(359, 21);
            this.cmbGameList.TabIndex = 2;
            this.toolTip1.SetToolTip(this.cmbGameList, "Choose a game from the list.");
            // 
            // lblProcessStatusLabel
            // 
            this.lblProcessStatusLabel.AutoSize = true;
            this.lblProcessStatusLabel.Location = new System.Drawing.Point(6, 52);
            this.lblProcessStatusLabel.Name = "lblProcessStatusLabel";
            this.lblProcessStatusLabel.Size = new System.Drawing.Size(91, 15);
            this.lblProcessStatusLabel.TabIndex = 17;
            this.lblProcessStatusLabel.Text = "Process Status:";
            this.toolTip1.SetToolTip(this.lblProcessStatusLabel, "Process status.");
            // 
            // lblProcessStatus
            // 
            this.lblProcessStatus.AutoSize = true;
            this.lblProcessStatus.Location = new System.Drawing.Point(105, 52);
            this.lblProcessStatus.Name = "lblProcessStatus";
            this.lblProcessStatus.Size = new System.Drawing.Size(132, 15);
            this.lblProcessStatus.TabIndex = 18;
            this.lblProcessStatus.Text = "Scanning for Process...";
            this.toolTip1.SetToolTip(this.lblProcessStatus, "Current process status.");
            // 
            // pbProcess
            // 
            this.pbProcess.Location = new System.Drawing.Point(215, 46);
            this.pbProcess.Name = "pbProcess";
            this.pbProcess.Size = new System.Drawing.Size(35, 25);
            this.pbProcess.TabIndex = 26;
            this.pbProcess.TabStop = false;
            this.toolTip1.SetToolTip(this.pbProcess, "Process status icon.");
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(105, 76);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(54, 20);
            this.txtWidth.TabIndex = 4;
            this.toolTip1.SetToolTip(this.txtWidth, "Width in pixels.");
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(189, 77);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(54, 20);
            this.txtHeight.TabIndex = 5;
            this.toolTip1.SetToolTip(this.txtHeight, "Height in pixels.");
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(74, 80);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(21, 15);
            this.lblWidth.TabIndex = 6;
            this.lblWidth.Text = "W:";
            this.toolTip1.SetToolTip(this.lblWidth, "Width (px).");
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(165, 80);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(19, 15);
            this.lblHeight.TabIndex = 7;
            this.lblHeight.Text = "H:";
            this.toolTip1.SetToolTip(this.lblHeight, "Height (px).");
            // 
            // rbResCustom
            // 
            this.rbResCustom.AutoSize = true;
            this.rbResCustom.Location = new System.Drawing.Point(4, 77);
            this.rbResCustom.Name = "rbResCustom";
            this.rbResCustom.Size = new System.Drawing.Size(70, 19);
            this.rbResCustom.TabIndex = 30;
            this.rbResCustom.TabStop = true;
            this.rbResCustom.Text = "Custom";
            this.toolTip1.SetToolTip(this.rbResCustom, "Use custom resolution.");
            this.rbResCustom.UseVisualStyleBackColor = true;
            // 
            // rbRes169
            // 
            this.rbRes169.AutoSize = true;
            this.rbRes169.Location = new System.Drawing.Point(4, 55);
            this.rbRes169.Name = "rbRes169";
            this.rbRes169.Size = new System.Drawing.Size(52, 19);
            this.rbRes169.TabIndex = 31;
            this.rbRes169.TabStop = true;
            this.rbRes169.Text = "16:9";
            this.toolTip1.SetToolTip(this.rbRes169, "Use 16:9 resolutions.");
            this.rbRes169.UseVisualStyleBackColor = true;
            // 
            // rbRes43
            // 
            this.rbRes43.AutoSize = true;
            this.rbRes43.Location = new System.Drawing.Point(4, 28);
            this.rbRes43.Name = "rbRes43";
            this.rbRes43.Size = new System.Drawing.Size(45, 19);
            this.rbRes43.TabIndex = 32;
            this.rbRes43.TabStop = true;
            this.rbRes43.Text = "4:3";
            this.toolTip1.SetToolTip(this.rbRes43, "Use 4:3 resolutions.");
            this.rbRes43.UseVisualStyleBackColor = true;
            // 
            // cmb169
            // 
            this.cmb169.FormattingEnabled = true;
            this.cmb169.Location = new System.Drawing.Point(103, 51);
            this.cmb169.Name = "cmb169";
            this.cmb169.Size = new System.Drawing.Size(121, 21);
            this.cmb169.TabIndex = 26;
            this.toolTip1.SetToolTip(this.cmb169, "Select a 16:9 resolution.");
            // 
            // cmb43
            // 
            this.cmb43.FormattingEnabled = true;
            this.cmb43.Location = new System.Drawing.Point(103, 24);
            this.cmb43.Name = "cmb43";
            this.cmb43.Size = new System.Drawing.Size(121, 21);
            this.cmb43.TabIndex = 27;
            this.toolTip1.SetToolTip(this.cmb43, "Select a 4:3 resolution.");
            // 
            // rbForceWindowed
            // 
            this.rbForceWindowed.AutoSize = true;
            this.rbForceWindowed.Location = new System.Drawing.Point(6, 25);
            this.rbForceWindowed.Name = "rbForceWindowed";
            this.rbForceWindowed.Size = new System.Drawing.Size(107, 19);
            this.rbForceWindowed.TabIndex = 0;
            this.rbForceWindowed.TabStop = true;
            this.rbForceWindowed.Text = "Window Mode";
            this.toolTip1.SetToolTip(this.rbForceWindowed, "Force windowed mode.");
            this.rbForceWindowed.UseVisualStyleBackColor = true;
            // 
            // rbFullscreen
            // 
            this.rbFullscreen.AutoSize = true;
            this.rbFullscreen.Location = new System.Drawing.Point(163, 25);
            this.rbFullscreen.Name = "rbFullscreen";
            this.rbFullscreen.Size = new System.Drawing.Size(120, 19);
            this.rbFullscreen.TabIndex = 1;
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
            this.chkRemoveFrame.Size = new System.Drawing.Size(161, 19);
            this.chkRemoveFrame.TabIndex = 16;
            this.chkRemoveFrame.Text = "Remove Window Frame";
            this.toolTip1.SetToolTip(this.chkRemoveFrame, "Remove window border.");
            this.chkRemoveFrame.UseVisualStyleBackColor = true;
            // 
            // lblDocking
            // 
            this.lblDocking.AutoSize = true;
            this.lblDocking.Location = new System.Drawing.Point(3, 48);
            this.lblDocking.Name = "lblDocking";
            this.lblDocking.Size = new System.Drawing.Size(102, 15);
            this.lblDocking.TabIndex = 25;
            this.lblDocking.Text = "Docking Position:";
            this.toolTip1.SetToolTip(this.lblDocking, "Select docking position.");
            // 
            // cmbDockPosition
            // 
            this.cmbDockPosition.FormattingEnabled = true;
            this.cmbDockPosition.Location = new System.Drawing.Point(105, 45);
            this.cmbDockPosition.Name = "cmbDockPosition";
            this.cmbDockPosition.Size = new System.Drawing.Size(114, 21);
            this.cmbDockPosition.TabIndex = 23;
            this.toolTip1.SetToolTip(this.cmbDockPosition, "Choose window docking position.");
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Location = new System.Drawing.Point(3, 81);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(446, 15);
            this.lblNote.TabIndex = 24;
            this.lblNote.Text = "Note: You can still use ALT+Enter to manually toggle full screen or window mode.";
            this.toolTip1.SetToolTip(this.lblNote, "Tip: Use ALT+Enter to toggle full screen.");
            // 
            // lblMonitor
            // 
            this.lblMonitor.AutoSize = true;
            this.lblMonitor.Location = new System.Drawing.Point(3, 24);
            this.lblMonitor.Name = "lblMonitor";
            this.lblMonitor.Size = new System.Drawing.Size(52, 15);
            this.lblMonitor.TabIndex = 22;
            this.lblMonitor.Text = "Monitor:";
            this.toolTip1.SetToolTip(this.lblMonitor, "Select monitor.");
            // 
            // cmbMonitor
            // 
            this.cmbMonitor.FormattingEnabled = true;
            this.cmbMonitor.Location = new System.Drawing.Point(105, 21);
            this.cmbMonitor.Name = "cmbMonitor";
            this.cmbMonitor.Size = new System.Drawing.Size(244, 21);
            this.cmbMonitor.TabIndex = 21;
            this.toolTip1.SetToolTip(this.cmbMonitor, "Choose target monitor.");
            // 
            // btnResize
            // 
            this.btnResize.Location = new System.Drawing.Point(173, 402);
            this.btnResize.Name = "btnResize";
            this.btnResize.Size = new System.Drawing.Size(135, 35);
            this.btnResize.TabIndex = 11;
            this.btnResize.Text = "Resize";
            this.toolTip1.SetToolTip(this.btnResize, "Apply changes.");
            this.btnResize.UseVisualStyleBackColor = true;
            this.btnResize.Click += new System.EventHandler(this.btnResize_Click);
            // 
            // lblGameSelectionHeader
            // 
            this.lblGameSelectionHeader.AutoSize = true;
            this.lblGameSelectionHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblGameSelectionHeader.Location = new System.Drawing.Point(3, 0);
            this.lblGameSelectionHeader.Name = "lblGameSelectionHeader";
            this.lblGameSelectionHeader.Size = new System.Drawing.Size(121, 17);
            this.lblGameSelectionHeader.TabIndex = 0;
            this.lblGameSelectionHeader.Text = "Process Details";
            // 
            // lblMonitorOptionsHeader
            // 
            this.lblMonitorOptionsHeader.AutoSize = true;
            this.lblMonitorOptionsHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblMonitorOptionsHeader.Location = new System.Drawing.Point(3, 0);
            this.lblMonitorOptionsHeader.Name = "lblMonitorOptionsHeader";
            this.lblMonitorOptionsHeader.Size = new System.Drawing.Size(117, 17);
            this.lblMonitorOptionsHeader.TabIndex = 0;
            this.lblMonitorOptionsHeader.Text = "Monitor Details";
            // 
            // lblResolutionOptionsHeader
            // 
            this.lblResolutionOptionsHeader.AutoSize = true;
            this.lblResolutionOptionsHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblResolutionOptionsHeader.Location = new System.Drawing.Point(3, 2);
            this.lblResolutionOptionsHeader.Name = "lblResolutionOptionsHeader";
            this.lblResolutionOptionsHeader.Size = new System.Drawing.Size(140, 17);
            this.lblResolutionOptionsHeader.TabIndex = 0;
            this.lblResolutionOptionsHeader.Text = "Resolution Details";
            // 
            // lblWindowOptionsHeader
            // 
            this.lblWindowOptionsHeader.AutoSize = true;
            this.lblWindowOptionsHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblWindowOptionsHeader.Location = new System.Drawing.Point(3, 0);
            this.lblWindowOptionsHeader.Name = "lblWindowOptionsHeader";
            this.lblWindowOptionsHeader.Size = new System.Drawing.Size(118, 17);
            this.lblWindowOptionsHeader.TabIndex = 0;
            this.lblWindowOptionsHeader.Text = "Window Details";
            // 
            // pnlGameSelection
            // 
            this.pnlGameSelection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGameSelection.Controls.Add(this.pbProcess);
            this.pnlGameSelection.Controls.Add(this.lblGameSelect);
            this.pnlGameSelection.Controls.Add(this.lblProcessStatus);
            this.pnlGameSelection.Controls.Add(this.cmbGameList);
            this.pnlGameSelection.Controls.Add(this.lblProcessStatusLabel);
            this.pnlGameSelection.Controls.Add(this.lblGameSelectionHeader);
            this.pnlGameSelection.Location = new System.Drawing.Point(9, 10);
            this.pnlGameSelection.Name = "pnlGameSelection";
            this.pnlGameSelection.Size = new System.Drawing.Size(495, 76);
            this.pnlGameSelection.TabIndex = 19;
            // 
            // pnlMonitorOptions
            // 
            this.pnlMonitorOptions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMonitorOptions.Controls.Add(this.lblDocking);
            this.pnlMonitorOptions.Controls.Add(this.cmbDockPosition);
            this.pnlMonitorOptions.Controls.Add(this.lblMonitor);
            this.pnlMonitorOptions.Controls.Add(this.cmbMonitor);
            this.pnlMonitorOptions.Controls.Add(this.lblMonitorOptionsHeader);
            this.pnlMonitorOptions.Location = new System.Drawing.Point(9, 92);
            this.pnlMonitorOptions.Name = "pnlMonitorOptions";
            this.pnlMonitorOptions.Size = new System.Drawing.Size(495, 76);
            this.pnlMonitorOptions.TabIndex = 20;
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
            this.pnlResolutionOptions.Location = new System.Drawing.Point(9, 174);
            this.pnlResolutionOptions.Name = "pnlResolutionOptions";
            this.pnlResolutionOptions.Size = new System.Drawing.Size(495, 110);
            this.pnlResolutionOptions.TabIndex = 23;
            // 
            // pnlWindowOptions
            // 
            this.pnlWindowOptions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlWindowOptions.Controls.Add(this.rbForceWindowed);
            this.pnlWindowOptions.Controls.Add(this.rbFullscreen);
            this.pnlWindowOptions.Controls.Add(this.chkRemoveFrame);
            this.pnlWindowOptions.Controls.Add(this.lblNote);
            this.pnlWindowOptions.Controls.Add(this.lblWindowOptionsHeader);
            this.pnlWindowOptions.Location = new System.Drawing.Point(9, 290);
            this.pnlWindowOptions.Name = "pnlWindowOptions";
            this.pnlWindowOptions.Size = new System.Drawing.Size(495, 106);
            this.pnlWindowOptions.TabIndex = 22;
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(510, 440);
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
    }
}