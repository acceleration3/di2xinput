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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label2 = new System.Windows.Forms.Label();
            this.configCombo = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.searchMethod = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.targetCombo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.watcherStatus = new System.Windows.Forms.Label();
            this.convertGrid = new System.Windows.Forms.DataGridView();
            this.colXI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deviceList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.controllerSelect = new System.Windows.Forms.TabControl();
            this.controllerTab1 = new System.Windows.Forms.TabPage();
            this.controllerTab2 = new System.Windows.Forms.TabPage();
            this.controllerTab3 = new System.Windows.Forms.TabPage();
            this.controllerTab4 = new System.Windows.Forms.TabPage();
            this.groupBox1.SuspendLayout();
            this.searchMethod.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.convertGrid)).BeginInit();
            this.controllerSelect.SuspendLayout();
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
            // configCombo
            // 
            this.configCombo.FormattingEnabled = true;
            this.configCombo.Location = new System.Drawing.Point(87, 12);
            this.configCombo.Name = "configCombo";
            this.configCombo.Size = new System.Drawing.Size(212, 21);
            this.configCombo.TabIndex = 6;
            this.configCombo.DropDown += new System.EventHandler(this.configCombo_DropDown);
            this.configCombo.SelectedIndexChanged += new System.EventHandler(this.configCombo_SelectedIndexChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(305, 11);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(44, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.searchMethod);
            this.groupBox1.Controls.Add(this.convertGrid);
            this.groupBox1.Controls.Add(this.deviceList);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.controllerSelect);
            this.groupBox1.Location = new System.Drawing.Point(12, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(337, 445);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // searchMethod
            // 
            this.searchMethod.Controls.Add(this.tabPage1);
            this.searchMethod.Controls.Add(this.tabPage2);
            this.searchMethod.Location = new System.Drawing.Point(6, 19);
            this.searchMethod.Name = "searchMethod";
            this.searchMethod.SelectedIndex = 0;
            this.searchMethod.Size = new System.Drawing.Size(324, 78);
            this.searchMethod.TabIndex = 12;
            this.searchMethod.SelectedIndexChanged += new System.EventHandler(this.searchMethod_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.targetCombo);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(316, 52);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Single Process";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // targetCombo
            // 
            this.targetCombo.FormattingEnabled = true;
            this.targetCombo.Location = new System.Drawing.Point(94, 13);
            this.targetCombo.Name = "targetCombo";
            this.targetCombo.Size = new System.Drawing.Size(216, 21);
            this.targetCombo.TabIndex = 11;
            this.targetCombo.DropDown += new System.EventHandler(this.targetCombo_DropDown);
            this.targetCombo.SelectedIndexChanged += new System.EventHandler(this.targetCombo_SelectedIndexChanged);
            this.targetCombo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.targetCombo_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Target Process:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.watcherStatus);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(316, 52);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Process Watcher";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // watcherStatus
            // 
            this.watcherStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.watcherStatus.Location = new System.Drawing.Point(3, 3);
            this.watcherStatus.Name = "watcherStatus";
            this.watcherStatus.Size = new System.Drawing.Size(310, 46);
            this.watcherStatus.TabIndex = 0;
            this.watcherStatus.Text = "Looking for XInput compatible processes...";
            this.watcherStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // convertGrid
            // 
            this.convertGrid.AllowUserToAddRows = false;
            this.convertGrid.AllowUserToDeleteRows = false;
            this.convertGrid.AllowUserToResizeColumns = false;
            this.convertGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.convertGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.convertGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.convertGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.convertGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colXI,
            this.colDI});
            this.convertGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.convertGrid.Location = new System.Drawing.Point(10, 173);
            this.convertGrid.MultiSelect = false;
            this.convertGrid.Name = "convertGrid";
            this.convertGrid.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.convertGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.convertGrid.RowHeadersVisible = false;
            this.convertGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.convertGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.convertGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.convertGrid.ShowCellErrors = false;
            this.convertGrid.ShowCellToolTips = false;
            this.convertGrid.ShowEditingIcon = false;
            this.convertGrid.ShowRowErrors = false;
            this.convertGrid.Size = new System.Drawing.Size(321, 259);
            this.convertGrid.TabIndex = 8;
            this.convertGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.convertGrid_CellClick);
            // 
            // colXI
            // 
            this.colXI.HeaderText = "XBox Button";
            this.colXI.Name = "colXI";
            this.colXI.ReadOnly = true;
            this.colXI.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colXI.Width = 140;
            // 
            // colDI
            // 
            this.colDI.HeaderText = "DirectInput Mapping";
            this.colDI.Name = "colDI";
            this.colDI.ReadOnly = true;
            this.colDI.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colDI.Width = 176;
            // 
            // deviceList
            // 
            this.deviceList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.deviceList.FormattingEnabled = true;
            this.deviceList.Items.AddRange(new object[] {
            "None"});
            this.deviceList.Location = new System.Drawing.Point(115, 146);
            this.deviceList.Name = "deviceList";
            this.deviceList.Size = new System.Drawing.Size(215, 21);
            this.deviceList.TabIndex = 7;
            this.deviceList.DropDown += new System.EventHandler(this.deviceList_DropDown);
            this.deviceList.SelectedIndexChanged += new System.EventHandler(this.deviceList_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 149);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "DirectInput Device:";
            // 
            // controllerSelect
            // 
            this.controllerSelect.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.controllerSelect.Controls.Add(this.controllerTab1);
            this.controllerSelect.Controls.Add(this.controllerTab2);
            this.controllerSelect.Controls.Add(this.controllerTab3);
            this.controllerSelect.Controls.Add(this.controllerTab4);
            this.controllerSelect.HotTrack = true;
            this.controllerSelect.Location = new System.Drawing.Point(31, 111);
            this.controllerSelect.Name = "controllerSelect";
            this.controllerSelect.SelectedIndex = 0;
            this.controllerSelect.Size = new System.Drawing.Size(274, 21);
            this.controllerSelect.TabIndex = 5;
            this.controllerSelect.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selecting);
            // 
            // controllerTab1
            // 
            this.controllerTab1.Location = new System.Drawing.Point(4, 25);
            this.controllerTab1.Name = "controllerTab1";
            this.controllerTab1.Padding = new System.Windows.Forms.Padding(3);
            this.controllerTab1.Size = new System.Drawing.Size(266, 0);
            this.controllerTab1.TabIndex = 1;
            this.controllerTab1.Text = "Controller 1";
            this.controllerTab1.UseVisualStyleBackColor = true;
            // 
            // controllerTab2
            // 
            this.controllerTab2.Location = new System.Drawing.Point(4, 25);
            this.controllerTab2.Name = "controllerTab2";
            this.controllerTab2.Size = new System.Drawing.Size(266, 0);
            this.controllerTab2.TabIndex = 2;
            this.controllerTab2.Text = "Controller 2";
            this.controllerTab2.UseVisualStyleBackColor = true;
            // 
            // controllerTab3
            // 
            this.controllerTab3.Location = new System.Drawing.Point(4, 25);
            this.controllerTab3.Name = "controllerTab3";
            this.controllerTab3.Size = new System.Drawing.Size(266, 0);
            this.controllerTab3.TabIndex = 3;
            this.controllerTab3.Text = "Controller 3";
            this.controllerTab3.UseVisualStyleBackColor = true;
            // 
            // controllerTab4
            // 
            this.controllerTab4.Location = new System.Drawing.Point(4, 25);
            this.controllerTab4.Name = "controllerTab4";
            this.controllerTab4.Size = new System.Drawing.Size(266, 0);
            this.controllerTab4.TabIndex = 4;
            this.controllerTab4.Text = "Controller 4";
            this.controllerTab4.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 496);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.configCombo);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "DirectInput to XInput Converter";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.searchMethod.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.convertGrid)).EndInit();
            this.controllerSelect.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox configCombo;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView convertGrid;
        private System.Windows.Forms.ComboBox deviceList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl controllerSelect;
        private System.Windows.Forms.TabPage controllerTab1;
        private System.Windows.Forms.TabPage controllerTab2;
        private System.Windows.Forms.TabPage controllerTab3;
        private System.Windows.Forms.TabPage controllerTab4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox targetCombo;
        private System.Windows.Forms.TabControl searchMethod;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label watcherStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colXI;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDI;
    }
}

