namespace Synthons.Lab3;

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
        dataGridView1 = new DataGridView();
        dataGridView2 = new DataGridView();
        button1 = new Button();
        button2 = new Button();
        button3 = new Button();
        ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
        SuspendLayout();
        // 
        // dataGridView1
        // 
        dataGridView1.AllowUserToAddRows = false;
        dataGridView1.AllowUserToDeleteRows = false;
        dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridView1.Location = new Point(12, 59);
        dataGridView1.Name = "dataGridView1";
        dataGridView1.ReadOnly = true;
        dataGridView1.RowHeadersWidth = 51;
        dataGridView1.Size = new Size(776, 153);
        dataGridView1.TabIndex = 0;
        // 
        // dataGridView2
        // 
        dataGridView2.AllowUserToAddRows = false;
        dataGridView2.AllowUserToDeleteRows = false;
        dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridView2.Location = new Point(12, 255);
        dataGridView2.Name = "dataGridView2";
        dataGridView2.ReadOnly = true;
        dataGridView2.RowHeadersWidth = 51;
        dataGridView2.Size = new Size(776, 183);
        dataGridView2.TabIndex = 1;
        // 
        // button1
        // 
        button1.Location = new Point(12, 24);
        button1.Name = "button1";
        button1.Size = new Size(380, 29);
        button1.TabIndex = 2;
        button1.Text = "Добавить в базу данных";
        button1.UseVisualStyleBackColor = true;
        button1.Click += button1_Click;
        // 
        // button2
        // 
        button2.Location = new Point(12, 220);
        button2.Name = "button2";
        button2.Size = new Size(380, 29);
        button2.TabIndex = 3;
        button2.Text = "Добавить в DataSet";
        button2.UseVisualStyleBackColor = true;
        button2.Click += button2_Click;
        // 
        // button3
        // 
        button3.Location = new Point(398, 24);
        button3.Name = "button3";
        button3.Size = new Size(390, 29);
        button3.TabIndex = 4;
        button3.Text = "Читать";
        button3.UseVisualStyleBackColor = true;
        button3.Click += button3_Click;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(button3);
        Controls.Add(button2);
        Controls.Add(button1);
        Controls.Add(dataGridView2);
        Controls.Add(dataGridView1);
        Name = "Form1";
        Text = "Form1";
        Load += Form1_Load;
        ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
        ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private DataGridView dataGridView1;
    private DataGridView dataGridView2;
    private Button button1;
    private Button button2;
    private Button button3;
}
