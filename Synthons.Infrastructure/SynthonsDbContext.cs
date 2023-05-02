using Microsoft.EntityFrameworkCore;
using Synthons.Domain;

namespace Synthons.Infrastructure;

public partial class SynthonsDbContext : DbContext
{
    public SynthonsDbContext()
    {
    }

    public SynthonsDbContext(DbContextOptions<SynthonsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductPrice> ProductPrices { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<SaleProduct> SaleProducts { get; set; }

    public virtual DbSet<SaleService> SaleServices { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<ServicePrice> ServicePrices { get; set; }

    public virtual DbSet<ViProductSoldTotal> ViProductSoldTotals { get; set; }

    public virtual DbSet<ViSalesJournal> ViSalesJournals { get; set; }

    public virtual DbSet<ViServiceSoldTotal> ViServiceSoldTotals { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=SYNTHONS;Integrated Security=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64B84C84628C");

            entity.ToTable("Customer");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.BirthDate).HasColumnType("date");
            entity.Property(e => e.EmailAddress).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.MiddleName).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(12)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04FF13A8191F1");

            entity.ToTable("Employee");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.BirthDate).HasColumnType("date");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.MiddleName).HasMaxLength(50);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__B40CC6EDD5A65582");

            entity.ToTable("Product");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Description).HasMaxLength(1);
            entity.Property(e => e.Manufacturer).HasMaxLength(1);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<ProductPrice>(entity =>
        {
            entity.HasKey(e => e.ProductPriceId).HasName("PK__ProductP__92B9430F71475EC9");

            entity.ToTable("ProductPrice");

            entity.Property(e => e.ProductPriceId).HasColumnName("ProductPriceID");
            entity.Property(e => e.AssignmentDate).HasPrecision(2);
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductPrices)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__ProductPr__Produ__4EA8A765");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.SaleId).HasName("PK__Sale__1EE3C41FFE9015DA");

            entity.ToTable("Sale");

            entity.Property(e => e.SaleId).HasColumnName("SaleID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.OrderDate).HasPrecision(2);
            entity.Property(e => e.PaymentDate).HasPrecision(2);
            entity.Property(e => e.TotalDue).HasColumnType("money");

            entity.HasOne(d => d.Customer).WithMany(p => p.Sales)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Sale__CustomerID__4707859D");

            entity.HasOne(d => d.Employee).WithMany(p => p.Sales)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Sale__EmployeeID__47FBA9D6");
        });

        modelBuilder.Entity<SaleProduct>(entity =>
        {
            entity.HasKey(e => e.SaleProductId).HasName("PK__SaleProd__43130B8EC8508EE0");

            entity.ToTable("SaleProduct", tb =>
                {
                    tb.HasTrigger("TR_SaleProduct_PreventModifyingSalesAfterPaid");
                    tb.HasTrigger("TR_SaleProduct_TotalPriceUpdate");
                });

            entity.Property(e => e.SaleProductId).HasColumnName("SaleProductID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.SaleId).HasColumnName("SaleID");
            entity.Property(e => e.TotalPrice)
                .HasComputedColumnSql("([UnitPrice]*[Qty])", false)
                .HasColumnType("money");
            entity.Property(e => e.UnitPrice).HasColumnType("money");

            entity.HasOne(d => d.Product).WithMany(p => p.SaleProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SaleProdu__Produ__5555A4F4");

            entity.HasOne(d => d.Sale).WithMany(p => p.SaleProducts)
                .HasForeignKey(d => d.SaleId)
                .HasConstraintName("FK__SaleProdu__SaleI__546180BB");
        });

        modelBuilder.Entity<SaleService>(entity =>
        {
            entity.HasKey(e => e.SaleServiceId).HasName("PK__SaleServ__8793A467A966CFF6");

            entity.ToTable("SaleService", tb =>
                {
                    tb.HasTrigger("TR_SaleService_PreventModifyingSalesAfterPaid");
                    tb.HasTrigger("TR_SaleService_TotalPriceUpdate");
                });

            entity.Property(e => e.SaleServiceId).HasColumnName("SaleServiceID");
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.SaleId).HasColumnName("SaleID");
            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

            entity.HasOne(d => d.Sale).WithMany(p => p.SaleServices)
                .HasForeignKey(d => d.SaleId)
                .HasConstraintName("FK__SaleServi__SaleI__5832119F");

            entity.HasOne(d => d.Service).WithMany(p => p.SaleServices)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SaleServi__Servi__592635D8");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Service__C51BB0EA9D0FB5F5");

            entity.ToTable("Service");

            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
            entity.Property(e => e.Description).HasMaxLength(1);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<ServicePrice>(entity =>
        {
            entity.HasKey(e => e.ServicePriceId).HasName("PK__ServiceP__E8178A8F09E58F35");

            entity.ToTable("ServicePrice");

            entity.Property(e => e.ServicePriceId).HasColumnName("ServicePriceID");
            entity.Property(e => e.AssignmentDate).HasPrecision(2);
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

            entity.HasOne(d => d.Service).WithMany(p => p.ServicePrices)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK__ServicePr__Servi__51851410");
        });

        modelBuilder.Entity<ViProductSoldTotal>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VI_ProductSoldTotal");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.TotalPrice).HasColumnType("money");
        });

        modelBuilder.Entity<ViSalesJournal>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VI_SalesJournal");

            entity.Property(e => e.Customer).HasMaxLength(101);
            entity.Property(e => e.Employee).HasMaxLength(101);
            entity.Property(e => e.PaymentDate).HasPrecision(2);
            entity.Property(e => e.SaleId).HasColumnName("SaleID");
            entity.Property(e => e.TotalDue).HasColumnType("money");
        });

        modelBuilder.Entity<ViServiceSoldTotal>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VI_ServiceSoldTotal");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
            entity.Property(e => e.TotalPrice).HasColumnType("money");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
