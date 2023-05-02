namespace Synthons.WinForms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabControl1 = new TabControl();
            tabPageEmployees = new TabPage();
            textBoxEmployeeCount = new TextBox();
            label1 = new Label();
            buttonCreate = new Button();
            buttonRead = new Button();
            dataGridViewEmployees = new DataGridView();
            tabPage1 = new TabPage();
            button1 = new Button();
            dataGridView1 = new DataGridView();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            tabControl1.SuspendLayout();
            tabPageEmployees.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewEmployees).BeginInit();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPageEmployees);
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(800, 507);
            tabControl1.TabIndex = 0;
            // 
            // tabPageEmployees
            // 
            tabPageEmployees.Controls.Add(textBoxEmployeeCount);
            tabPageEmployees.Controls.Add(label1);
            tabPageEmployees.Controls.Add(buttonCreate);
            tabPageEmployees.Controls.Add(buttonRead);
            tabPageEmployees.Controls.Add(dataGridViewEmployees);
            tabPageEmployees.Location = new Point(4, 29);
            tabPageEmployees.Name = "tabPageEmployees";
            tabPageEmployees.Size = new Size(792, 474);
            tabPageEmployees.TabIndex = 2;
            tabPageEmployees.Text = "Сотрудники";
            tabPageEmployees.UseVisualStyleBackColor = true;
            // 
            // textBoxEmployeeCount
            // 
            textBoxEmployeeCount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            textBoxEmployeeCount.Location = new Point(654, 131);
            textBoxEmployeeCount.Name = "textBoxEmployeeCount";
            textBoxEmployeeCount.ReadOnly = true;
            textBoxEmployeeCount.Size = new Size(44, 27);
            textBoxEmployeeCount.TabIndex = 4;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(587, 133);
            label1.Name = "label1";
            label1.Size = new Size(61, 20);
            label1.TabIndex = 3;
            label1.Text = "Кол-во:";
            // 
            // buttonCreate
            // 
            buttonCreate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonCreate.Location = new Point(587, 63);
            buttonCreate.Name = "buttonCreate";
            buttonCreate.Size = new Size(205, 53);
            buttonCreate.TabIndex = 2;
            buttonCreate.Text = "Добавить";
            buttonCreate.UseVisualStyleBackColor = true;
            buttonCreate.Click += buttonCreate_Click;
            // 
            // buttonRead
            // 
            buttonRead.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonRead.Location = new Point(587, 3);
            buttonRead.Name = "buttonRead";
            buttonRead.Size = new Size(205, 53);
            buttonRead.TabIndex = 1;
            buttonRead.Text = "Читать";
            buttonRead.UseVisualStyleBackColor = true;
            buttonRead.Click += buttonRead_Click;
            // 
            // dataGridViewEmployees
            // 
            dataGridViewEmployees.AllowUserToAddRows = false;
            dataGridViewEmployees.AllowUserToDeleteRows = false;
            dataGridViewEmployees.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewEmployees.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewEmployees.Location = new Point(3, 3);
            dataGridViewEmployees.Name = "dataGridViewEmployees";
            dataGridViewEmployees.ReadOnly = true;
            dataGridViewEmployees.RowHeadersWidth = 51;
            dataGridViewEmployees.RowTemplate.Height = 29;
            dataGridViewEmployees.Size = new Size(578, 411);
            dataGridViewEmployees.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(button1);
            tabPage1.Controls.Add(dataGridView1);
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3, 3, 3, 3);
            tabPage1.Size = new Size(792, 474);
            tabPage1.TabIndex = 3;
            tabPage1.Text = "Журнал заказов";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.Location = new Point(587, 3);
            button1.Name = "button1";
            button1.Size = new Size(205, 53);
            button1.TabIndex = 2;
            button1.Text = "Читать";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(3, 3);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new Size(578, 411);
            dataGridView1.TabIndex = 1;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 485);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(800, 22);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(0, 16);
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 507);
            Controls.Add(statusStrip1);
            Controls.Add(tabControl1);
            Name = "Form1";
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            tabControl1.ResumeLayout(false);
            tabPageEmployees.ResumeLayout(false);
            tabPageEmployees.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewEmployees).EndInit();
            tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPageEmployees;
        private DataGridView dataGridViewEmployees;
        private Button buttonCreate;
        private Button buttonRead;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private TextBox textBoxEmployeeCount;
        private Label label1;
        private TabPage tabPage1;
        private Button button1;
        private DataGridView dataGridView1;
    }
}