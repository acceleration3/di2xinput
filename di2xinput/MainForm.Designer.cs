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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label2 = new System.Windows.Forms.Label();
            this.configCombo = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.convertGrid = new System.Windows.Forms.DataGridView();
            this.deviceList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.controllerSelect = new System.Windows.Forms.TabControl();
            this.controllerTab1 = new System.Windows.Forms.TabPage();
            this.controllerTab2 = new System.Windows.Forms.TabPage();
            this.controllerTab3 = new System.Windows.Forms.TabPage();
            this.controllerTab4 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.targetCombo = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.colXI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.convertGrid)).BeginInit();
            this.controllerSelect.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
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
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(305, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(44, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tabControl1);
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
            // convertGrid
            // 
            this.convertGrid.AllowUserToAddRows = false;
            this.convertGrid.AllowUserToDeleteRows = false;
            this.convertGrid.AllowUserToResizeColumns = false;
            this.convertGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            this.convertGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.convertGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.convertGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.convertGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colXI,
            this.colDI});
            this.convertGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.convertGrid.Location = new System.Drawing.Point(10, 173);
            this.convertGrid.MultiSelect = false;
            this.convertGrid.Name = "convertGrid";
            this.convertGrid.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.convertGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
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
            this.controllerTab1.Size = new System.Drawing.Size(302, 0);
            this.controllerTab1.TabIndex = 1;
            this.controllerTab1.Text = "Controller 1";
            this.controllerTab1.UseVisualStyleBackColor = true;
            // 
            // controllerTab2
            // 
            this.controllerTab2.Location = new System.Drawing.Point(4, 25);
            this.controllerTab2.Name = "controllerTab2";
            this.controllerTab2.Size = new System.Drawing.Size(302, 0);
            this.controllerTab2.TabIndex = 2;
            this.controllerTab2.Text = "Controller 2";
            this.controllerTab2.UseVisualStyleBackColor = true;
            // 
            // controllerTab3
            // 
            this.controllerTab3.Location = new System.Drawing.Point(4, 25);
            this.controllerTab3.Name = "controllerTab3";
            this.controllerTab3.Size = new System.Drawing.Size(302, 0);
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Target Process:";
            // 
            // targetCombo
            // 
            this.targetCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.targetCombo.FormattingEnabled = true;
            this.targetCombo.Location = new System.Drawing.Point(94, 13);
            this.targetCombo.Name = "targetCombo";
            this.targetCombo.Size = new System.Drawing.Size(216, 21);
            this.targetCombo.TabIndex = 11;
            this.targetCombo.DropDown += new System.EventHandler(this.targetCombo_DropDown);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(6, 19);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(324, 78);
            this.tabControl1.TabIndex = 12;
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
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(316, 52);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Process Watcher";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(310, 46);
            this.label4.TabIndex = 0;
            this.label4.Text = "Looking for XInput compatible processes...";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 496);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
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
            ((System.ComponentModel.ISupportInitialize)(this.convertGrid)).EndInit();
            this.controllerSelect.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox configCombo;
        private System.Windows.Forms.Button button1;
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
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn colXI;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDI;
    }
}

