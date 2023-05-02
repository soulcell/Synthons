using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Synthons.Lab3;

public partial class Form1 : Form
{
    SqlDataAdapter adapter;
    DataSet dataSet;
    DataSet virtualSet = new DataSet();
    SqlConnection connection = new SqlConnection("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=SYNTHONS;Integrated Security=True;");
    SqlCommandBuilder commandBuilder;
    public Form1()
    {
        InitializeComponent();

        
        adapter = new SqlDataAdapter("SELECT * FROM Employee", connection);
        dataSet = new DataSet();
        adapter.Fill(dataSet);
        commandBuilder = new SqlCommandBuilder(adapter);

        DataTable empTable = virtualSet.Tables.Add("Employees");
        DataColumn empID = empTable.Columns.Add("EmployeeID", typeof(Int32));
        empID.AutoIncrement = true;
        empID.AutoIncrementSeed = 200;
        empID.AutoIncrementStep = 3;
        empTable.Columns.Add("LName", typeof(string));
        empTable.Columns.Add("FName", typeof(string));
        empTable.Columns.Add("MName", typeof(string));


        empTable.Rows.Add(new object[] { "0","LName1", "Fname1", "MName1" });

    }

    private void Form1_Load(object sender, EventArgs e)
    {
        dataGridView1.DataSource = dataSet.Tables[0];
        dataGridView2.DataSource = virtualSet.Tables[0];
    }

    private void button2_Click(object sender, EventArgs e)
    {
        using FormCreateEmployee form = new FormCreateEmployee();
        if (form.ShowDialog() == DialogResult.OK)
        {
            var employee = form.Employee;
            virtualSet.Tables[0].Rows.Add(new object[] { "0", employee.LastName, employee.FirstName, employee.MiddleName });
            dataGridView2.DataSource = virtualSet.Tables[0];
        }
    }

    private void button1_Click(object sender, EventArgs e)
    {
        using FormCreateEmployee form = new FormCreateEmployee();
        if (form.ShowDialog() == DialogResult.OK)
        {
            var employee = form.Employee;
            dataSet.Tables[0].Rows.Add("0", employee.FirstName, employee.LastName, employee.MiddleName, employee.BirthDate);
        }
        adapter.Update(dataSet);
    }

    private void button3_Click(object sender, EventArgs e)
    {
        adapter = new SqlDataAdapter("SELECT * FROM Employee", connection);
        dataSet = new DataSet();
        adapter.Fill(dataSet);
        dataGridView1.DataSource = dataSet.Tables[0];
    }
}
