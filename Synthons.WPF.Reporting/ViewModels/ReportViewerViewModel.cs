using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Microsoft.Reporting.WinForms;
using MvvmDialogs;
using Synthons.Domain;
using Synthons.Infrastructure;
using Synthons.WPF.Reporting.Reports;

namespace Synthons.WPF.Reporting.ViewModels;
public partial class ReportViewerViewModel : ObservableObject, IModalDialogViewModel
{
    [ObservableProperty]
    private bool? dialogResult;

    [ObservableProperty]
    private WindowsFormsHost viewer;

    [ObservableProperty]
    private ReportViewer reportViewer;

    [ObservableProperty]
    private DateTime startDate = DateTime.Today.AddDays(-7);

    [ObservableProperty]
    private DateTime endDate = DateTime.Today;

    [ObservableProperty]
    private List<Product> products = new List<Product>();

    [ObservableProperty]
    private Product? selectedProduct;

    public List<Sale> Sales { get; set; } = new List<Sale>();

    private readonly SynthonsDbContext context;

    public ReportViewerViewModel(SynthonsDbContext context)
    {
        this.context = context;

        WindowsFormsHost windowsFormsHost = new WindowsFormsHost();
        ReportViewer = new ReportViewer();
        windowsFormsHost.Child = ReportViewer;

        Viewer = windowsFormsHost;
    }

    [RelayCommand]
    private void Loaded()
    {
        RefreshReport();
    }

    partial void OnStartDateChanged(DateTime value) => RefreshReport();

    partial void OnEndDateChanged(DateTime value) => RefreshReport();

    partial void OnSelectedProductChanged(Product? value) => RefreshReport();

    private async Task RefreshSalesAsync()
    {
        Products = await context.Products.ToListAsync();

        Sales = await context.Sales
            .Include(x => x.Employee)
            .Include(x => x.Customer)
            //.Where(x => x.PaymentDate.HasValue)
            .Where(x => x.OrderDate >= StartDate && x.OrderDate <= EndDate)
            .ToListAsync();
    }

    private void RefreshSales()
    {
        Products = context.Products.ToList();

        Sales = context.Sales
            .Include(x => x.Employee)
            .Include(x => x.Customer)
            .Include(x => x.SaleProducts).ThenInclude(sp => sp.Product)
            .Where(x => x.PaymentDate.HasValue)
            .Where(x => x.PaymentDate >= StartDate && x.PaymentDate <= EndDate)
            .Where(x => (SelectedProduct == null || x.SaleProducts.Any(sp => sp.Product == SelectedProduct)))
            .OrderBy(x => x.PaymentDate)
            .ToList();
    }

    private void RefreshReport()
    {
        RefreshSales();
        SaleReport.Load(ReportViewer.LocalReport, Sales.ToList(), StartDate, EndDate, SelectedProduct);
        ReportViewer.RefreshReport();
    }
}
