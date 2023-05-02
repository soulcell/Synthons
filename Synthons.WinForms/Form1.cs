using Microsoft.EntityFrameworkCore;
using Synthons.Domain;
using Synthons.Infrastructure;

namespace Synthons.WinForms;

public partial class Form1 : Form
{
    private List<Employee> Employees
    {
        get; set;
    }

    private readonly SynthonsDbContext dbContext;
    public Form1(SynthonsDbContext dbContext)
    {
        InitializeComponent();
        this.dbContext = dbContext;
    }

    private void Form1_Load(object sender, EventArgs e)
    {

    }

    private void buttonCreate_Click(object sender, EventArgs e)
    {
        using FormCreateEmployee form = new FormCreateEmployee();
        if (form.ShowDialog() == DialogResult.OK)
        {
            var employee = form.Employee;

            dbContext.Employees.Add(employee);

            dbContext.SaveChanges();

            toolStripStatusLabel1.Text = $"Добавлен сотрудник с ID: {employee.EmployeeId}";
        }
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {

        dbContext.Dispose();
    }

    private void buttonRead_Click(object sender, EventArgs e)
    {
        dataGridViewEmployees.DataSource = dbContext.Employees.ToList();
        textBoxEmployeeCount.Text = getEmployeeCount().ToString();
    }

    private int getEmployeeCount()
    {
        return dbContext.Employees.Count();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        dataGridView1.DataSource = dbContext.Sales.Include(x => x.Employee).Include(x => x.Customer)
            .Select(x => new
            {
                Id = x.SaleId,
                Customer = $"{x.Customer.LastName} {x.Customer.FirstName} {x.Customer.MiddleName}",
                Employee = $"{x.Employee.LastName} {x.Employee.FirstName} {x.Employee.MiddleName}",
                Date = x.OrderDate,
                TotalPrice = x.TotalDue
            }).ToList();
    }
}