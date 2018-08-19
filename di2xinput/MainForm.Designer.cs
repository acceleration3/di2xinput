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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.ProgramTab = new System.Windows.Forms.TabPage();
            this.RemoveButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.ProgramGrid = new System.Windows.Forms.DataGridView();
            this.ProfileTab = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.ProfileCombo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.MainTabs = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.DeviceCombo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ControllerPicture = new System.Windows.Forms.PictureBox();
            this.MappingGrid = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.SettingsTab = new System.Windows.Forms.TabPage();
            this.openProgramDialog = new System.Windows.Forms.OpenFileDialog();
            this.IconColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.PathColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VersionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProfileColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.tabControl1.SuspendLayout();
            this.ProgramTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProgramGrid)).BeginInit();
            this.ProfileTab.SuspendLayout();
            this.MainTabs.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ControllerPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MappingGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.ProgramTab);
            this.tabControl1.Controls.Add(this.ProfileTab);
            this.tabControl1.Controls.Add(this.SettingsTab);
            this.tabControl1.Location = new System.Drawing.Point(8, 9);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(569, 346);
            this.tabControl1.TabIndex = 0;
            // 
            // ProgramTab
            // 
            this.ProgramTab.Controls.Add(this.RemoveButton);
            this.ProgramTab.Controls.Add(this.AddButton);
            this.ProgramTab.Controls.Add(this.ProgramGrid);
            this.ProgramTab.Location = new System.Drawing.Point(4, 22);
            this.ProgramTab.Name = "ProgramTab";
            this.ProgramTab.Padding = new System.Windows.Forms.Padding(3);
            this.ProgramTab.Size = new System.Drawing.Size(561, 320);
            this.ProgramTab.TabIndex = 0;
            this.ProgramTab.Text = "Program List";
            this.ProgramTab.UseVisualStyleBackColor = true;
            // 
            // RemoveButton
            // 
            this.RemoveButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.RemoveButton.Location = new System.Drawing.Point(283, 289);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(103, 23);
            this.RemoveButton.TabIndex = 2;
            this.RemoveButton.Text = "Remove program";
            this.RemoveButton.UseVisualStyleBackColor = true;
            this.RemoveButton.Click += new System.EventHandler(this.RemoveButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.AddButton.Location = new System.Drawing.Point(174, 289);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(103, 23);
            this.AddButton.TabIndex = 1;
            this.AddButton.Text = "Add program";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // ProgramGrid
            // 
            this.ProgramGrid.AllowUserToAddRows = false;
            this.ProgramGrid.AllowUserToDeleteRows = false;
            this.ProgramGrid.AllowUserToResizeRows = false;
            this.ProgramGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgramGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.ProgramGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ProgramGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IconColumn,
            this.PathColumn,
            this.VersionColumn,
            this.ProfileColumn});
            this.ProgramGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.ProgramGrid.Location = new System.Drawing.Point(4, 3);
            this.ProgramGrid.Name = "ProgramGrid";
            this.ProgramGrid.RowHeadersVisible = false;
            this.ProgramGrid.ShowCellErrors = false;
            this.ProgramGrid.ShowCellToolTips = false;
            this.ProgramGrid.ShowEditingIcon = false;
            this.ProgramGrid.ShowRowErrors = false;
            this.ProgramGrid.Size = new System.Drawing.Size(552, 280);
            this.ProgramGrid.TabIndex = 0;
            // 
            // ProfileTab
            // 
            this.ProfileTab.Controls.Add(this.button1);
            this.ProfileTab.Controls.Add(this.ProfileCombo);
            this.ProfileTab.Controls.Add(this.label2);
            this.ProfileTab.Controls.Add(this.MainTabs);
            this.ProfileTab.Location = new System.Drawing.Point(4, 22);
            this.ProfileTab.Name = "ProfileTab";
            this.ProfileTab.Padding = new System.Windows.Forms.Padding(3);
            this.ProfileTab.Size = new System.Drawing.Size(561, 320);
            this.ProfileTab.TabIndex = 1;
            this.ProfileTab.Text = "Edit Profile";
            this.ProfileTab.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(485, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(67, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // ProfileCombo
            // 
            this.ProfileCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProfileCombo.FormattingEnabled = true;
            this.ProfileCombo.Location = new System.Drawing.Point(86, 10);
            this.ProfileCombo.Name = "ProfileCombo";
            this.ProfileCombo.Size = new System.Drawing.Size(393, 21);
            this.ProfileCombo.TabIndex = 11;
            this.ProfileCombo.DropDown += new System.EventHandler(this.ProfileCombo_DropDown);
            this.ProfileCombo.SelectedIndexChanged += new System.EventHandler(this.ProfileCombo_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Profile Name:";
            // 
            // MainTabs
            // 
            this.MainTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainTabs.Controls.Add(this.tabPage3);
            this.MainTabs.Controls.Add(this.tabPage4);
            this.MainTabs.Controls.Add(this.tabPage5);
            this.MainTabs.Controls.Add(this.tabPage6);
            this.MainTabs.Location = new System.Drawing.Point(8, 38);
            this.MainTabs.Name = "MainTabs";
            this.MainTabs.SelectedIndex = 0;
            this.MainTabs.Size = new System.Drawing.Size(545, 274);
            this.MainTabs.TabIndex = 9;
            this.MainTabs.SelectedIndexChanged += new System.EventHandler(this.MainTabs_SelectedIndexChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.DeviceCombo);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Controls.Add(this.ControllerPicture);
            this.tabPage3.Controls.Add(this.MappingGrid);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(537, 248);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Controller 1";
            this.tabPage3.UseVisualStyleBackColor = true;
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
            this.DeviceCombo.Size = new System.Drawing.Size(433, 21);
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
            this.ControllerPicture.Size = new System.Drawing.Size(275, 208);
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
            this.MappingGrid.Location = new System.Drawing.Point(289, 33);
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
            this.MappingGrid.Size = new System.Drawing.Size(240, 208);
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
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(537, 248);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Controller 2";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(537, 248);
            this.tabPage5.TabIndex = 2;
            this.tabPage5.Text = "Controller 3";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage6
            // 
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(537, 248);
            this.tabPage6.TabIndex = 3;
            this.tabPage6.Text = "Controller 4";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // SettingsTab
            // 
            this.SettingsTab.Location = new System.Drawing.Point(4, 22);
            this.SettingsTab.Name = "SettingsTab";
            this.SettingsTab.Padding = new System.Windows.Forms.Padding(3);
            this.SettingsTab.Size = new System.Drawing.Size(561, 320);
            this.SettingsTab.TabIndex = 2;
            this.SettingsTab.Text = "Settings";
            this.SettingsTab.UseVisualStyleBackColor = true;
            // 
            // openProgramDialog
            // 
            this.openProgramDialog.Filter = "Executable File | *.exe";
            // 
            // IconColumn
            // 
            this.IconColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.IconColumn.FillWeight = 20F;
            this.IconColumn.HeaderText = "Icon";
            this.IconColumn.Name = "IconColumn";
            this.IconColumn.ReadOnly = true;
            this.IconColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.IconColumn.Width = 32;
            // 
            // PathColumn
            // 
            this.PathColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.PathColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.PathColumn.FillWeight = 80F;
            this.PathColumn.HeaderText = "Path";
            this.PathColumn.Name = "PathColumn";
            this.PathColumn.ReadOnly = true;
            this.PathColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PathColumn.Width = 277;
            // 
            // VersionColumn
            // 
            this.VersionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.VersionColumn.HeaderText = "XI Version";
            this.VersionColumn.Name = "VersionColumn";
            this.VersionColumn.ReadOnly = true;
            this.VersionColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.VersionColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.VersionColumn.Width = 90;
            // 
            // ProfileColumn
            // 
            this.ProfileColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ProfileColumn.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.ProfileColumn.FillWeight = 20F;
            this.ProfileColumn.HeaderText = "Profile";
            this.ProfileColumn.Name = "ProfileColumn";
            this.ProfileColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ProfileColumn.Width = 150;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.tabControl1);
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "MainForm";
            this.Text = "DirectInput to XInput Converter";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.ProgramTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ProgramGrid)).EndInit();
            this.ProfileTab.ResumeLayout(false);
            this.ProfileTab.PerformLayout();
            this.MainTabs.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ControllerPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MappingGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage ProgramTab;
        private System.Windows.Forms.TabPage ProfileTab;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox ProfileCombo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl MainTabs;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ComboBox DeviceCombo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox ControllerPicture;
        private System.Windows.Forms.DataGridView MappingGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.DataGridView ProgramGrid;
        private System.Windows.Forms.Button RemoveButton;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.TabPage SettingsTab;
        private System.Windows.Forms.OpenFileDialog openProgramDialog;
        private System.Windows.Forms.DataGridViewImageColumn IconColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PathColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn VersionColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn ProfileColumn;
    }
}