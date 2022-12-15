using System.Data.SqlTypes;
using Infrastructure.Ef.DbEntities;
using Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Ef;

public class TakeAndDashContext : DbContext
{
    private readonly IConnectionStringProvider _connectionStringProvider;

    public TakeAndDashContext(IConnectionStringProvider connectionStringProvider)
    {
        _connectionStringProvider = connectionStringProvider;
    }

    public DbSet<DbArticle> Articles { get; set; }
    public DbSet<DbBrand> Brands { get; set; }

    public DbSet<DbCategory> Categories { get; set; }

    public DbSet<DbArticleFamilies> ArticleFamilies { get; set; }

    public DbSet<DbFamily> Families { get; set; }
    
    public DbSet<DbOrderContent> OrderContents { get; set; }
    
    public DbSet<DbOrderHistoryContent> OrderHistoryContents { get; set; }
    
    public DbSet<DbOrders> Orders { get; set; }
    
    public DbSet<DbOrdersHistory> OrdersHistories { get; set; }
    
    public DbSet<DbUser> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlServer(_connectionStringProvider.Get("db"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DbArticle>(entity =>
        {
            entity.ToTable("article");
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Id).HasColumnName("Id");
            entity.Property(a => a.Nametag).HasColumnName("nametag");
            entity.Property(a => a.Price).HasColumnName("price");
            entity.Property(a => a.PricingType).HasColumnName("pricingtype");
            entity.Property(a => a.Stock).HasColumnName("stock");
            entity.Property(a => a.IdCategory).HasColumnName("idcategory");
            entity.Property(a => a.IdBrand).HasColumnName("idbrand");
        });
        
        modelBuilder.Entity<DbBrand>(entity =>
        {
            entity.ToTable("brand");
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Id).HasColumnName("Id");
            entity.Property(a => a.Name).HasColumnName("name");
        });
        
        modelBuilder.Entity<DbArticleFamilies>(entity =>
        {
            entity.ToTable("article_families");
            entity.HasKey(o => o.IdFamily);
            entity.HasKey(o => o.IdArticle);
            entity.Property(a => a.IdArticle).HasColumnName("id_article");
            entity.Property(a => a.IdFamily).HasColumnName("id_family");
        });
        
        modelBuilder.Entity<DbCategory>(entity =>
        {
            entity.ToTable("category");
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id).HasColumnName("id");
            entity.Property(c => c.Name).HasColumnName("name");
        });
        
        modelBuilder.Entity<DbFamily>(entity =>
        {
            entity.ToTable("families");
            entity.HasKey(f => f.Id);
            entity.Property(f => f.Id).HasColumnName("id");
            entity.Property(f => f.FamilyName).HasColumnName("family_name");
        });
        
        modelBuilder.Entity<DbOrderContent>(entity =>
        {
            entity.ToTable("order_content");
            entity.Property(o => o.Quantity).HasColumnName("quantity");
            entity.Property(o => o.Prepared).HasColumnName("prepared");
            entity.HasKey(o => o.IdOrder);
            entity.HasKey(o => o.IdArticle);
            entity.Property(o => o.IdOrder).HasColumnName("idorder");
            entity.Property(o => o.IdArticle).HasColumnName("idarticle");
        });
        
        modelBuilder.Entity<DbOrderHistoryContent>(entity =>
        {
            entity.ToTable("order_history_content");
            entity.HasKey(o => o.IdOrder);
            entity.HasKey(o => o.IdArticle);
            entity.Property(o => o.Quantity).HasColumnName("quantity");
            entity.Property(o => o.IdOrder).HasColumnName("idorder");
            entity.Property(o => o.IdArticle).HasColumnName("idarticle");
        });
        
        modelBuilder.Entity<DbOrders>(entity =>
        {
            entity.ToTable("orders");
            entity.HasKey(o => o.Id);
            entity.Property(o => o.Id).HasColumnName("Id");
            entity.Property(o => o.CreationDate).HasColumnName("creationdate");
            entity.Property(o => o.TakeDateTime).HasColumnName("takedatetime");
            entity.Property(o => o.IdUser).HasColumnName("iduser");
        });
        
        modelBuilder.Entity<DbOrdersHistory>(entity =>
        {
            entity.ToTable("orders_history");
            entity.HasKey(o => o.Id);
            entity.Property(o => o.Id).HasColumnName("Id");
            entity.Property(o => o.CreationDate).HasColumnName("creationdate");
            entity.Property(o => o.TakeDateTime).HasColumnName("takendatetime");
            entity.Property(o => o.IdUser).HasColumnName("iduser");
        });
        
        modelBuilder.Entity<DbUser>(entity =>
        {
            entity.ToTable("users");
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Id).HasColumnName("Id");
            entity.Property(u => u.Lastname).HasColumnName("lastname");
            entity.Property(u => u.Surname).HasColumnName("surname");
            entity.Property(u => u.Email).HasColumnName("email");
            entity.Property(u => u.Birthdate).HasColumnName("birthdate");
            entity.Property(u => u.Permission).HasColumnName("permission");
            entity.Property(u => u.Hsh).HasColumnName("hsh");
            entity.Property(u => u.Salt).HasColumnName("salt");
        });
    }
}