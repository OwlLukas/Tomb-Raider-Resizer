namespace TombRaiderResizer
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lblGameSelect = new System.Windows.Forms.Label();
            this.cmbGameList = new System.Windows.Forms.ComboBox();
            this.lblResolution = new System.Windows.Forms.Label();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.lblWidth = new System.Windows.Forms.Label();
            this.lblHeight = new System.Windows.Forms.Label();
            this.lblWindowOptions = new System.Windows.Forms.Label();
            this.btnResize = new System.Windows.Forms.Button();
            this.chkRemoveFrame = new System.Windows.Forms.CheckBox();
            this.lblProcessStatusLabel = new System.Windows.Forms.Label();
            this.lblProcessStatus = new System.Windows.Forms.Label();
            this.pnlGameSelection = new System.Windows.Forms.Panel();
            this.pnlOptions = new System.Windows.Forms.Panel();
            this.lblDocking = new System.Windows.Forms.Label();
            this.cmbDockPosition = new System.Windows.Forms.ComboBox();
            this.lblNote = new System.Windows.Forms.Label();
            this.lblMonitor = new System.Windows.Forms.Label();
            this.cmbMonitor = new System.Windows.Forms.ComboBox();
            this.rbForceWindowed = new System.Windows.Forms.RadioButton();
            this.rbFullscreen = new System.Windows.Forms.RadioButton();
            this.lblWindowExtras = new System.Windows.Forms.Label();
            this.pnlGameSelection.SuspendLayout();
            this.pnlOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblGameSelect
            // 
            this.lblGameSelect.AutoSize = true;
            this.lblGameSelect.Location = new System.Drawing.Point(8, 7);
            this.lblGameSelect.Name = "lblGameSelect";
            this.lblGameSelect.Size = new System.Drawing.Size(89, 15);
            this.lblGameSelect.TabIndex = 1;
            this.lblGameSelect.Text = "Choose Game:";
            // 
            // cmbGameList
            // 
            this.cmbGameList.FormattingEnabled = true;
            this.cmbGameList.Location = new System.Drawing.Point(101, 5);
            this.cmbGameList.Name = "cmbGameList";
            this.cmbGameList.Size = new System.Drawing.Size(359, 21);
            this.cmbGameList.TabIndex = 2;
            // 
            // lblResolution
            // 
            this.lblResolution.AutoSize = true;
            this.lblResolution.Location = new System.Drawing.Point(8, 62);
            this.lblResolution.Name = "lblResolution";
            this.lblResolution.Size = new System.Drawing.Size(69, 15);
            this.lblResolution.TabIndex = 3;
            this.lblResolution.Text = "Resolution:";
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(136, 59);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(52, 20);
            this.txtWidth.TabIndex = 4;
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(233, 59);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(52, 20);
            this.txtHeight.TabIndex = 5;
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(99, 62);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(41, 15);
            this.lblWidth.TabIndex = 6;
            this.lblWidth.Text = "Width:";
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(192, 62);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(46, 15);
            this.lblHeight.TabIndex = 7;
            this.lblHeight.Text = "Height:";
            // 
            // lblWindowOptions
            // 
            this.lblWindowOptions.AutoSize = true;
            this.lblWindowOptions.Location = new System.Drawing.Point(8, 87);
            this.lblWindowOptions.Name = "lblWindowOptions";
            this.lblWindowOptions.Size = new System.Drawing.Size(99, 15);
            this.lblWindowOptions.TabIndex = 10;
            this.lblWindowOptions.Text = "Window Options:";
            // 
            // btnResize
            // 
            this.btnResize.Location = new System.Drawing.Point(160, 274);
            this.btnResize.Name = "btnResize";
            this.btnResize.Size = new System.Drawing.Size(135, 35);
            this.btnResize.TabIndex = 11;
            this.btnResize.Text = "Resize";
            this.btnResize.UseVisualStyleBackColor = true;
            this.btnResize.Click += new System.EventHandler(this.btnResize_Click);
            // 
            // chkRemoveFrame
            // 
            this.chkRemoveFrame.AutoSize = true;
            this.chkRemoveFrame.Location = new System.Drawing.Point(113, 114);
            this.chkRemoveFrame.Name = "chkRemoveFrame";
            this.chkRemoveFrame.Size = new System.Drawing.Size(161, 19);
            this.chkRemoveFrame.TabIndex = 16;
            this.chkRemoveFrame.Text = "Remove Window Frame";
            this.chkRemoveFrame.UseVisualStyleBackColor = true;
            // 
            // lblProcessStatusLabel
            // 
            this.lblProcessStatusLabel.AutoSize = true;
            this.lblProcessStatusLabel.Location = new System.Drawing.Point(8, 36);
            this.lblProcessStatusLabel.Name = "lblProcessStatusLabel";
            this.lblProcessStatusLabel.Size = new System.Drawing.Size(91, 15);
            this.lblProcessStatusLabel.TabIndex = 17;
            this.lblProcessStatusLabel.Text = "Process Status:";
            // 
            // lblProcessStatus
            // 
            this.lblProcessStatus.AutoSize = true;
            this.lblProcessStatus.Location = new System.Drawing.Point(99, 36);
            this.lblProcessStatus.Name = "lblProcessStatus";
            this.lblProcessStatus.Size = new System.Drawing.Size(132, 15);
            this.lblProcessStatus.TabIndex = 18;
            this.lblProcessStatus.Text = "Scanning for Process...";
            // 
            // pnlGameSelection
            // 
            this.pnlGameSelection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGameSelection.Controls.Add(this.lblGameSelect);
            this.pnlGameSelection.Controls.Add(this.lblProcessStatus);
            this.pnlGameSelection.Controls.Add(this.cmbGameList);
            this.pnlGameSelection.Controls.Add(this.lblProcessStatusLabel);
            this.pnlGameSelection.Location = new System.Drawing.Point(9, 10);
            this.pnlGameSelection.Name = "pnlGameSelection";
            this.pnlGameSelection.Size = new System.Drawing.Size(525, 62);
            this.pnlGameSelection.TabIndex = 19;
            // 
            // pnlOptions
            // 
            this.pnlOptions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlOptions.Controls.Add(this.lblWindowExtras);
            this.pnlOptions.Controls.Add(this.rbForceWindowed);
            this.pnlOptions.Controls.Add(this.lblDocking);
            this.pnlOptions.Controls.Add(this.rbFullscreen);
            this.pnlOptions.Controls.Add(this.lblResolution);
            this.pnlOptions.Controls.Add(this.cmbDockPosition);
            this.pnlOptions.Controls.Add(this.lblNote);
            this.pnlOptions.Controls.Add(this.lblMonitor);
            this.pnlOptions.Controls.Add(this.cmbMonitor);
            this.pnlOptions.Controls.Add(this.txtWidth);
            this.pnlOptions.Controls.Add(this.chkRemoveFrame);
            this.pnlOptions.Controls.Add(this.txtHeight);
            this.pnlOptions.Controls.Add(this.lblWidth);
            this.pnlOptions.Controls.Add(this.lblHeight);
            this.pnlOptions.Controls.Add(this.lblWindowOptions);
            this.pnlOptions.Location = new System.Drawing.Point(9, 76);
            this.pnlOptions.Name = "pnlOptions";
            this.pnlOptions.Size = new System.Drawing.Size(525, 192);
            this.pnlOptions.TabIndex = 20;
            // 
            // lblDocking
            // 
            this.lblDocking.AutoSize = true;
            this.lblDocking.Location = new System.Drawing.Point(8, 33);
            this.lblDocking.Name = "lblDocking";
            this.lblDocking.Size = new System.Drawing.Size(102, 15);
            this.lblDocking.TabIndex = 25;
            this.lblDocking.Text = "Docking Position:";
            // 
            // cmbDockPosition
            // 
            this.cmbDockPosition.FormattingEnabled = true;
            this.cmbDockPosition.Location = new System.Drawing.Point(101, 31);
            this.cmbDockPosition.Name = "cmbDockPosition";
            this.cmbDockPosition.Size = new System.Drawing.Size(123, 21);
            this.cmbDockPosition.TabIndex = 23;
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Location = new System.Drawing.Point(8, 158);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(446, 15);
            this.lblNote.TabIndex = 24;
            this.lblNote.Text = "Note: You can still use ALT+Enter to manually toggle full screen or window mode.";
            // 
            // lblMonitor
            // 
            this.lblMonitor.AutoSize = true;
            this.lblMonitor.Location = new System.Drawing.Point(8, 9);
            this.lblMonitor.Name = "lblMonitor";
            this.lblMonitor.Size = new System.Drawing.Size(52, 15);
            this.lblMonitor.TabIndex = 22;
            this.lblMonitor.Text = "Monitor:";
            // 
            // cmbMonitor
            // 
            this.cmbMonitor.FormattingEnabled = true;
            this.cmbMonitor.Location = new System.Drawing.Point(101, 6);
            this.cmbMonitor.Name = "cmbMonitor";
            this.cmbMonitor.Size = new System.Drawing.Size(244, 21);
            this.cmbMonitor.TabIndex = 21;
            // 
            // rbForceWindowed
            // 
            this.rbForceWindowed.AutoSize = true;
            this.rbForceWindowed.Location = new System.Drawing.Point(113, 87);
            this.rbForceWindowed.Name = "rbForceWindowed";
            this.rbForceWindowed.Size = new System.Drawing.Size(155, 19);
            this.rbForceWindowed.TabIndex = 0;
            this.rbForceWindowed.TabStop = true;
            this.rbForceWindowed.Text = "Force Windowed Mode";
            this.rbForceWindowed.UseVisualStyleBackColor = true;
            // 
            // rbFullscreen
            // 
            this.rbFullscreen.AutoSize = true;
            this.rbFullscreen.Location = new System.Drawing.Point(287, 87);
            this.rbFullscreen.Name = "rbFullscreen";
            this.rbFullscreen.Size = new System.Drawing.Size(85, 19);
            this.rbFullscreen.TabIndex = 1;
            this.rbFullscreen.TabStop = true;
            this.rbFullscreen.Text = "Fullscreen";
            this.rbFullscreen.UseVisualStyleBackColor = true;
            // 
            // lblWindowExtras
            // 
            this.lblWindowExtras.AutoSize = true;
            this.lblWindowExtras.Location = new System.Drawing.Point(8, 116);
            this.lblWindowExtras.Name = "lblWindowExtras";
            this.lblWindowExtras.Size = new System.Drawing.Size(91, 15);
            this.lblWindowExtras.TabIndex = 21;
            this.lblWindowExtras.Text = "Window Extras:";
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(546, 317);
            this.Controls.Add(this.pnlOptions);
            this.Controls.Add(this.pnlGameSelection);
            this.Controls.Add(this.btnResize);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Tomb Raider Resizer";
            this.pnlGameSelection.ResumeLayout(false);
            this.pnlGameSelection.PerformLayout();
            this.pnlOptions.ResumeLayout(false);
            this.pnlOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblGameSelect;
        private System.Windows.Forms.ComboBox cmbGameList;
        private System.Windows.Forms.Label lblResolution;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.Label lblWindowOptions;
        private System.Windows.Forms.Button btnResize;
        private System.Windows.Forms.CheckBox chkRemoveFrame;
        private System.Windows.Forms.Label lblProcessStatusLabel;
        private System.Windows.Forms.Label lblProcessStatus;
        private System.Windows.Forms.Panel pnlGameSelection;
        private System.Windows.Forms.Panel pnlOptions;
        private System.Windows.Forms.ComboBox cmbMonitor;
        private System.Windows.Forms.Label lblMonitor;
        private System.Windows.Forms.ComboBox cmbDockPosition;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.Label lblDocking;
        private System.Windows.Forms.RadioButton rbForceWindowed;
        private System.Windows.Forms.RadioButton rbFullscreen;
        private System.Windows.Forms.Label lblWindowExtras;
    }
}
