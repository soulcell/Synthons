using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Synthons.Domain;

namespace Synthons.WinForms;
public partial class FormCreateEmployee : Form
{
    public Employee Employee
    {
        get; private set;
    }

    public FormCreateEmployee()
    {
        InitializeComponent();
    }

    private void FormCreateEmployee_Load(object sender, EventArgs e)
    {
        dateTimePicker1.MaxDate = DateTime.Now.AddYears(-16);
    }

    private void buttonCreate_Click(object sender, EventArgs e)
    {
        Employee = new Employee()
        {
            LastName = textBoxLName.Text,
            FirstName = textBoxFName.Text,
            MiddleName = textBox1.Text,
            BirthDate = dateTimePicker1.Value.Date
        };
    }
}
