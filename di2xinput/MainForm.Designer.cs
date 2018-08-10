namespace di2xinput
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label2 = new System.Windows.Forms.Label();
            this.ConfigCombo = new System.Windows.Forms.ComboBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.MainTabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.AssignButton = new System.Windows.Forms.Button();
            this.DeviceCombo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ControllerPicture = new System.Windows.Forms.PictureBox();
            this.MappingGrid = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SingleRadio = new System.Windows.Forms.RadioButton();
            this.ProcessCombo = new System.Windows.Forms.ComboBox();
            this.WatcherRadio = new System.Windows.Forms.RadioButton();
            this.MainTabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ControllerPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MappingGrid)).BeginInit();
            this.tabPage5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Configuration:";
            // 
            // ConfigCombo
            // 
            this.ConfigCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigCombo.FormattingEnabled = true;
            this.ConfigCombo.Location = new System.Drawing.Point(87, 12);
            this.ConfigCombo.Name = "ConfigCombo";
            this.ConfigCombo.Size = new System.Drawing.Size(477, 21);
            this.ConfigCombo.TabIndex = 6;
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveButton.Location = new System.Drawing.Point(570, 10);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(44, 23);
            this.SaveButton.TabIndex = 7;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            // 
            // MainTabs
            // 
            this.MainTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainTabs.Controls.Add(this.tabPage1);
            this.MainTabs.Controls.Add(this.tabPage2);
            this.MainTabs.Controls.Add(this.tabPage3);
            this.MainTabs.Controls.Add(this.tabPage4);
            this.MainTabs.Controls.Add(this.tabPage5);
            this.MainTabs.Location = new System.Drawing.Point(12, 48);
            this.MainTabs.Name = "MainTabs";
            this.MainTabs.SelectedIndex = 0;
            this.MainTabs.Size = new System.Drawing.Size(602, 410);
            this.MainTabs.TabIndex = 8;
            this.MainTabs.SelectedIndexChanged += new System.EventHandler(this.MainTabs_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.AssignButton);
            this.tabPage1.Controls.Add(this.DeviceCombo);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.ControllerPicture);
            this.tabPage1.Controls.Add(this.MappingGrid);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(594, 384);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Controller 1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // AssignButton
            // 
            this.AssignButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AssignButton.Location = new System.Drawing.Point(346, 354);
            this.AssignButton.Name = "AssignButton";
            this.AssignButton.Size = new System.Drawing.Size(240, 23);
            this.AssignButton.TabIndex = 6;
            this.AssignButton.Text = "Assign all buttons";
            this.AssignButton.UseVisualStyleBackColor = true;
            // 
            // DeviceCombo
            // 
            this.DeviceCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DeviceCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DeviceCombo.FormattingEnabled = true;
            this.DeviceCombo.Items.AddRange(new object[] {
            "None",
            "Keyboard"});
            this.DeviceCombo.Location = new System.Drawing.Point(96, 6);
            this.DeviceCombo.Name = "DeviceCombo";
            this.DeviceCombo.Size = new System.Drawing.Size(490, 21);
            this.DeviceCombo.TabIndex = 5;
            this.DeviceCombo.DropDown += new System.EventHandler(this.DeviceCombo_DropDown);
            this.DeviceCombo.SelectedIndexChanged += new System.EventHandler(this.DeviceCombo_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Mapped device:";
            // 
            // ControllerPicture
            // 
            this.ControllerPicture.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ControllerPicture.Image = global::di2xinput.Properties.Resources.Xbox_Controller;
            this.ControllerPicture.Location = new System.Drawing.Point(6, 33);
            this.ControllerPicture.Name = "ControllerPicture";
            this.ControllerPicture.Size = new System.Drawing.Size(332, 344);
            this.ControllerPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ControllerPicture.TabIndex = 3;
            this.ControllerPicture.TabStop = false;
            // 
            // MappingGrid
            // 
            this.MappingGrid.AllowUserToAddRows = false;
            this.MappingGrid.AllowUserToDeleteRows = false;
            this.MappingGrid.AllowUserToResizeColumns = false;
            this.MappingGrid.AllowUserToResizeRows = false;
            this.MappingGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MappingGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MappingGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.MappingGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.MappingGrid.Location = new System.Drawing.Point(346, 33);
            this.MappingGrid.MultiSelect = false;
            this.MappingGrid.Name = "MappingGrid";
            this.MappingGrid.ReadOnly = true;
            this.MappingGrid.RowHeadersVisible = false;
            this.MappingGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.MappingGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.MappingGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.MappingGrid.ShowCellErrors = false;
            this.MappingGrid.ShowCellToolTips = false;
            this.MappingGrid.ShowEditingIcon = false;
            this.MappingGrid.ShowRowErrors = false;
            this.MappingGrid.Size = new System.Drawing.Size(240, 315);
            this.MappingGrid.TabIndex = 0;
            this.MappingGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MappingGrid_CellClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Binding";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Mapping";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 120;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(594, 384);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Controller 2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(594, 384);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Controller 3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(594, 384);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Controller 4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.groupBox1);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(594, 384);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Settings";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Xbox_Controller (1).png");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.SingleRadio);
            this.groupBox1.Controls.Add(this.ProcessCombo);
            this.groupBox1.Controls.Add(this.WatcherRadio);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(294, 92);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search Method";
            // 
            // SingleRadio
            // 
            this.SingleRadio.AutoSize = true;
            this.SingleRadio.Checked = true;
            this.SingleRadio.Location = new System.Drawing.Point(6, 32);
            this.SingleRadio.Name = "SingleRadio";
            this.SingleRadio.Size = new System.Drawing.Size(94, 17);
            this.SingleRadio.TabIndex = 0;
            this.SingleRadio.TabStop = true;
            this.SingleRadio.Text = "Single process";
            this.SingleRadio.UseVisualStyleBackColor = true;
            this.SingleRadio.CheckedChanged += new System.EventHandler(this.SingleRadio_CheckedChanged);
            // 
            // ProcessCombo
            // 
            this.ProcessCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ProcessCombo.FormattingEnabled = true;
            this.ProcessCombo.Location = new System.Drawing.Point(106, 31);
            this.ProcessCombo.Name = "ProcessCombo";
            this.ProcessCombo.Size = new System.Drawing.Size(176, 21);
            this.ProcessCombo.TabIndex = 2;
            this.ProcessCombo.DropDown += new System.EventHandler(this.ProcessCombo_DropDown);
            this.ProcessCombo.SelectedIndexChanged += new System.EventHandler(this.ProcessCombo_SelectedIndexChanged);
            // 
            // WatcherRadio
            // 
            this.WatcherRadio.AutoSize = true;
            this.WatcherRadio.Location = new System.Drawing.Point(6, 58);
            this.WatcherRadio.Name = "WatcherRadio";
            this.WatcherRadio.Size = new System.Drawing.Size(107, 17);
            this.WatcherRadio.TabIndex = 1;
            this.WatcherRadio.Text = "Process Watcher";
            this.WatcherRadio.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 468);
            this.Controls.Add(this.MainTabs);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.ConfigCombo);
            this.Controls.Add(this.label2);
            this.MinimumSize = new System.Drawing.Size(638, 507);
            this.Name = "MainForm";
            this.Text = "DirectInput to XInput Converter";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MainTabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ControllerPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MappingGrid)).EndInit();
            this.tabPage5.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ConfigCombo;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.TabControl MainTabs;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView MappingGrid;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.PictureBox ControllerPicture;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ComboBox DeviceCombo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button AssignButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton SingleRadio;
        private System.Windows.Forms.ComboBox ProcessCombo;
        private System.Windows.Forms.RadioButton WatcherRadio;
    }
}

