namespace Synthons.Lab3;

partial class FormCreateEmployee
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
        buttonCreate = new Button();
        buttonCancel = new Button();
        textBoxLName = new TextBox();
        label1 = new Label();
        label2 = new Label();
        textBoxFName = new TextBox();
        label3 = new Label();
        textBox1 = new TextBox();
        label4 = new Label();
        dateTimePicker1 = new DateTimePicker();
        SuspendLayout();
        // 
        // buttonCreate
        // 
        buttonCreate.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        buttonCreate.DialogResult = DialogResult.OK;
        buttonCreate.Location = new Point(12, 268);
        buttonCreate.Name = "buttonCreate";
        buttonCreate.Size = new Size(195, 36);
        buttonCreate.TabIndex = 0;
        buttonCreate.Text = "Создать";
        buttonCreate.UseVisualStyleBackColor = true;
        buttonCreate.Click += buttonCreate_Click;
        // 
        // buttonCancel
        // 
        buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        buttonCancel.DialogResult = DialogResult.Cancel;
        buttonCancel.Location = new Point(213, 268);
        buttonCancel.Name = "buttonCancel";
        buttonCancel.Size = new Size(189, 36);
        buttonCancel.TabIndex = 1;
        buttonCancel.Text = "Отмена";
        buttonCancel.UseVisualStyleBackColor = true;
        // 
        // textBoxLName
        // 
        textBoxLName.Location = new Point(12, 40);
        textBoxLName.Name = "textBoxLName";
        textBoxLName.Size = new Size(390, 27);
        textBoxLName.TabIndex = 2;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(12, 17);
        label1.Name = "label1";
        label1.Size = new Size(73, 20);
        label1.TabIndex = 3;
        label1.Text = "Фамилия";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(12, 70);
        label2.Name = "label2";
        label2.Size = new Size(39, 20);
        label2.TabIndex = 5;
        label2.Text = "Имя";
        // 
        // textBoxFName
        // 
        textBoxFName.Location = new Point(12, 93);
        textBoxFName.Name = "textBoxFName";
        textBoxFName.Size = new Size(390, 27);
        textBoxFName.TabIndex = 4;
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Location = new Point(12, 123);
        label3.Name = "label3";
        label3.Size = new Size(72, 20);
        label3.TabIndex = 7;
        label3.Text = "Отчество";
        // 
        // textBox1
        // 
        textBox1.Location = new Point(12, 146);
        textBox1.Name = "textBox1";
        textBox1.Size = new Size(390, 27);
        textBox1.TabIndex = 6;
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Location = new Point(12, 176);
        label4.Name = "label4";
        label4.Size = new Size(116, 20);
        label4.TabIndex = 8;
        label4.Text = "Дата рождения";
        // 
        // dateTimePicker1
        // 
        dateTimePicker1.Location = new Point(12, 199);
        dateTimePicker1.MaxDate = new DateTime(2020, 12, 31, 0, 0, 0, 0);
        dateTimePicker1.MinDate = new DateTime(1900, 1, 1, 0, 0, 0, 0);
        dateTimePicker1.Name = "dateTimePicker1";
        dateTimePicker1.Size = new Size(390, 27);
        dateTimePicker1.TabIndex = 9;
        dateTimePicker1.Value = new DateTime(2020, 12, 31, 0, 0, 0, 0);
        // 
        // FormCreateEmployee
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(414, 316);
        Controls.Add(dateTimePicker1);
        Controls.Add(label4);
        Controls.Add(label3);
        Controls.Add(textBox1);
        Controls.Add(label2);
        Controls.Add(textBoxFName);
        Controls.Add(label1);
        Controls.Add(textBoxLName);
        Controls.Add(buttonCancel);
        Controls.Add(buttonCreate);
        MaximumSize = new Size(432, 363);
        MinimumSize = new Size(432, 363);
        Name = "FormCreateEmployee";
        Text = "Создать работника";
        Load += FormCreateEmployee_Load;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Button buttonCreate;
    private Button buttonCancel;
    private TextBox textBoxLName;
    private Label label1;
    private Label label2;
    private TextBox textBoxFName;
    private Label label3;
    private TextBox textBox1;
    private Label label4;
    private DateTimePicker dateTimePicker1;
}