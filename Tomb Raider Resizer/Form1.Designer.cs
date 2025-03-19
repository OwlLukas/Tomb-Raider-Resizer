namespace Tomb_Raider_Resizer
{
    partial class Form1
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
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.lbl_gameSelect = new System.Windows.Forms.Label();
            this.cb_gameList = new System.Windows.Forms.ComboBox();
            this.lblResolution = new System.Windows.Forms.Label();
            this.tB_w = new System.Windows.Forms.TextBox();
            this.tb_Y = new System.Windows.Forms.TextBox();
            this.lblwidth = new System.Windows.Forms.Label();
            this.lblheight = new System.Windows.Forms.Label();
            this.lblWindowOptions = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_processstatus = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblDocking = new System.Windows.Forms.Label();
            this.cb_dockPosition = new System.Windows.Forms.ComboBox();
            this.lvlnote = new System.Windows.Forms.Label();
            this.lblMonitor = new System.Windows.Forms.Label();
            this.cb_monitor = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_gameSelect
            // 
            this.lbl_gameSelect.AutoSize = true;
            this.lbl_gameSelect.Location = new System.Drawing.Point(11, 9);
            this.lbl_gameSelect.Name = "lbl_gameSelect";
            this.lbl_gameSelect.Size = new System.Drawing.Size(97, 16);
            this.lbl_gameSelect.TabIndex = 1;
            this.lbl_gameSelect.Text = "Choose Game:";
            // 
            // cb_gameList
            // 
            this.cb_gameList.FormattingEnabled = true;
            this.cb_gameList.Location = new System.Drawing.Point(135, 6);
            this.cb_gameList.Name = "cb_gameList";
            this.cb_gameList.Size = new System.Drawing.Size(477, 24);
            this.cb_gameList.TabIndex = 2;
            // 
            // lblResolution
            // 
            this.lblResolution.AutoSize = true;
            this.lblResolution.Location = new System.Drawing.Point(11, 76);
            this.lblResolution.Name = "lblResolution";
            this.lblResolution.Size = new System.Drawing.Size(74, 16);
            this.lblResolution.TabIndex = 3;
            this.lblResolution.Text = "Resolution:";
            // 
            // tB_w
            // 
            this.tB_w.Location = new System.Drawing.Point(182, 73);
            this.tB_w.Name = "tB_w";
            this.tB_w.Size = new System.Drawing.Size(68, 22);
            this.tB_w.TabIndex = 4;
            // 
            // tb_Y
            // 
            this.tb_Y.Location = new System.Drawing.Point(311, 73);
            this.tb_Y.Name = "tb_Y";
            this.tb_Y.Size = new System.Drawing.Size(68, 22);
            this.tb_Y.TabIndex = 5;
            // 
            // lblwidth
            // 
            this.lblwidth.AutoSize = true;
            this.lblwidth.Location = new System.Drawing.Point(132, 76);
            this.lblwidth.Name = "lblwidth";
            this.lblwidth.Size = new System.Drawing.Size(44, 16);
            this.lblwidth.TabIndex = 6;
            this.lblwidth.Text = "Width:";
            // 
            // lblheight
            // 
            this.lblheight.AutoSize = true;
            this.lblheight.Location = new System.Drawing.Point(256, 76);
            this.lblheight.Name = "lblheight";
            this.lblheight.Size = new System.Drawing.Size(49, 16);
            this.lblheight.TabIndex = 7;
            this.lblheight.Text = "Height:";
            // 
            // lblWindowOptions
            // 
            this.lblWindowOptions.AutoSize = true;
            this.lblWindowOptions.Location = new System.Drawing.Point(11, 107);
            this.lblWindowOptions.Name = "lblWindowOptions";
            this.lblWindowOptions.Size = new System.Drawing.Size(107, 16);
            this.lblWindowOptions.TabIndex = 10;
            this.lblWindowOptions.Text = "Window Options:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(212, 278);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(180, 43);
            this.button1.TabIndex = 11;
            this.button1.Text = "Resize";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(128, 107);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(169, 20);
            this.checkBox1.TabIndex = 15;
            this.checkBox1.Text = "Force Windowed Mode";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(303, 108);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(174, 20);
            this.checkBox2.TabIndex = 16;
            this.checkBox2.Text = "Remove Window Frame";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 16);
            this.label5.TabIndex = 17;
            this.label5.Text = "Process Status:";
            // 
            // lbl_processstatus
            // 
            this.lbl_processstatus.AutoSize = true;
            this.lbl_processstatus.Location = new System.Drawing.Point(132, 44);
            this.lbl_processstatus.Name = "lbl_processstatus";
            this.lbl_processstatus.Size = new System.Drawing.Size(143, 16);
            this.lbl_processstatus.TabIndex = 18;
            this.lbl_processstatus.Text = "Scanning for Process...";
            // 
            // panel1
            // 
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lbl_gameSelect);
            this.panel1.Controls.Add(this.lbl_processstatus);
            this.panel1.Controls.Add(this.cb_gameList);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(635, 76);
            this.panel1.TabIndex = 19;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lblDocking);
            this.panel2.Controls.Add(this.lblResolution);
            this.panel2.Controls.Add(this.cb_dockPosition);
            this.panel2.Controls.Add(this.lvlnote);
            this.panel2.Controls.Add(this.lblMonitor);
            this.panel2.Controls.Add(this.cb_monitor);
            this.panel2.Controls.Add(this.tB_w);
            this.panel2.Controls.Add(this.checkBox2);
            this.panel2.Controls.Add(this.tb_Y);
            this.panel2.Controls.Add(this.checkBox1);
            this.panel2.Controls.Add(this.lblwidth);
            this.panel2.Controls.Add(this.lblheight);
            this.panel2.Controls.Add(this.lblWindowOptions);
            this.panel2.Location = new System.Drawing.Point(12, 94);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(635, 178);
            this.panel2.TabIndex = 20;
            // 
            // lblDocking
            // 
            this.lblDocking.AutoSize = true;
            this.lblDocking.Location = new System.Drawing.Point(11, 41);
            this.lblDocking.Name = "lblDocking";
            this.lblDocking.Size = new System.Drawing.Size(111, 16);
            this.lblDocking.TabIndex = 25;
            this.lblDocking.Text = "Docking Position:";
            // 
            // cb_dockPosition
            // 
            this.cb_dockPosition.FormattingEnabled = true;
            this.cb_dockPosition.Location = new System.Drawing.Point(135, 38);
            this.cb_dockPosition.Name = "cb_dockPosition";
            this.cb_dockPosition.Size = new System.Drawing.Size(163, 24);
            this.cb_dockPosition.TabIndex = 23;
            // 
            // lvlnote
            // 
            this.lvlnote.AutoSize = true;
            this.lvlnote.Location = new System.Drawing.Point(11, 142);
            this.lvlnote.Name = "lvlnote";
            this.lvlnote.Size = new System.Drawing.Size(482, 16);
            this.lvlnote.TabIndex = 24;
            this.lvlnote.Text = "Note: You can still use ALT+Enter to manually toggle full screen or window mode.";
            // 
            // lblMonitor
            // 
            this.lblMonitor.AutoSize = true;
            this.lblMonitor.Location = new System.Drawing.Point(11, 11);
            this.lblMonitor.Name = "lblMonitor";
            this.lblMonitor.Size = new System.Drawing.Size(54, 16);
            this.lblMonitor.TabIndex = 22;
            this.lblMonitor.Text = "Monitor:";
            // 
            // cb_monitor
            // 
            this.cb_monitor.FormattingEnabled = true;
            this.cb_monitor.Location = new System.Drawing.Point(135, 8);
            this.cb_monitor.Name = "cb_monitor";
            this.cb_monitor.Size = new System.Drawing.Size(324, 24);
            this.cb_monitor.TabIndex = 21;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 328);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Tomb Raider Resizer";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lbl_gameSelect;
        private System.Windows.Forms.ComboBox cb_gameList;
        private System.Windows.Forms.Label lblResolution;
        private System.Windows.Forms.TextBox tB_w;
        private System.Windows.Forms.TextBox tb_Y;
        private System.Windows.Forms.Label lblwidth;
        private System.Windows.Forms.Label lblheight;
        private System.Windows.Forms.Label lblWindowOptions;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbl_processstatus;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cb_monitor;
        private System.Windows.Forms.Label lblMonitor;
        private System.Windows.Forms.ComboBox cb_dockPosition;
        private System.Windows.Forms.Label lvlnote;
        private System.Windows.Forms.Label lblDocking;
    }
}

