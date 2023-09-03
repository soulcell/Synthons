using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Reporting.WinForms;
using Synthons.Domain;

namespace Synthons.WPF.Reporting.Reports;
public class SaleReport
{
    public static void Load(LocalReport report, List<Sale> sales, DateTime startDate, DateTime endDate, Product? product)
    {
        var items = sales.Select(s => new ViSalesJournal
        {
            SaleId = s.SaleId,
            Employee = s.Employee.FullName,
            PaymentDate = s.PaymentDate,
            TotalDue = s.TotalDue
        }).ToArray();

        var parameters = new List<ReportParameter> {
            new ReportParameter("StartDate", startDate.ToString()),
            new ReportParameter("EndDate", endDate.ToString())
        };

        if (product != null) parameters.Add(new ReportParameter("Product", product.Name));

        using var fs = new FileStream("Reports/SaleReport.rdlc", FileMode.Open);
        report.LoadReportDefinition(fs);
        report.DataSources.Clear();
        report.DataSources.Add(new ReportDataSource("SalesDataSet", items));
        report.SetParameters(parameters);
    }
}
