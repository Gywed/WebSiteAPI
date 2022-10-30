using Infrastructure.Ef.DbEntities;
using Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Ef;

public class TakeAndDashContext : DbContext
{
    private readonly IConnectionStringProvider _connectionStringProvider;

    protected TakeAndDashContext(IConnectionStringProvider connectionStringProvider)
    {
        _connectionStringProvider = connectionStringProvider;
    }

    public DbSet<DbArticle> Articles { get; set; }

    public DbSet<DbCategory> Categories { get; set; }

    public DbSet<DbArticleFamilies> ArticleFamilies { get; set; }

    public DbSet<DbFamilies> Families { get; set; }
    
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
            entity.HasKey(a => a.id);
            entity.Property(a => a.id).HasColumnName("id");
            entity.Property(a => a.nametag).HasColumnName("nametag");
            entity.Property(a => a.price).HasColumnName("price");
            entity.Property(a => a.pricingtype).HasColumnName("pricingtype");
            entity.Property(a => a.stock).HasColumnName("stock");
            entity.Property(a => a.idcategory).HasColumnName("idcategory");
        });
        
        modelBuilder.Entity<DbArticleFamilies>(entity =>
        {
            entity.ToTable("article_families");
            entity.Property(a => a.idarticle).HasColumnName("idarticle");
            entity.Property(a => a.idfamily).HasColumnName("idfamily");
        });
        
        modelBuilder.Entity<DbCategory>(entity =>
        {
            entity.ToTable("category");
            entity.HasKey(c => c.id);
            entity.Property(c => c.id).HasColumnName("id");
            entity.Property(c => c.name).HasColumnName("name");
        });
        
        modelBuilder.Entity<DbFamilies>(entity =>
        {
            entity.ToTable("families");
            entity.HasKey(f => f.id);
            entity.Property(f => f.id).HasColumnName("id");
            entity.Property(f => f.family_name).HasColumnName("family_name");
        });
        
        modelBuilder.Entity<DbOrderContent>(entity =>
        {
            entity.ToTable("order_content");
            entity.Property(o => o.quantity).HasColumnName("quantity");
            entity.Property(o => o.idorder).HasColumnName("idorder");
            entity.Property(o => o.idarticle).HasColumnName("idarticle");
        });
        
        modelBuilder.Entity<DbOrderHistoryContent>(entity =>
        {
            entity.ToTable("order_history_content");
            entity.Property(o => o.quantity).HasColumnName("quantity");
            entity.Property(o => o.idorder).HasColumnName("idorder");
            entity.Property(o => o.idarticle).HasColumnName("idarticle");
        });
        
        modelBuilder.Entity<DbOrders>(entity =>
        {
            entity.ToTable("orders");
            entity.HasKey(o => o.id);
            entity.Property(o => o.id).HasColumnName("id");
            entity.Property(o => o.creationDate).HasColumnName("creationdate");
            entity.Property(o => o.iduser).HasColumnName("iduser");
        });
        
        modelBuilder.Entity<DbOrdersHistory>(entity =>
        {
            entity.ToTable("orders_history");
            entity.HasKey(o => o.id);
            entity.Property(o => o.id).HasColumnName("id");
            entity.Property(o => o.creationDate).HasColumnName("creationdate");
            entity.Property(o => o.iduser).HasColumnName("iduser");
        });
        
        modelBuilder.Entity<DbUser>(entity =>
        {
            entity.ToTable("users");
            entity.HasKey(u => u.id);
            entity.Property(u => u.id).HasColumnName("id");
            entity.Property(u => u.lastname).HasColumnName("lastname");
            entity.Property(u => u.surname).HasColumnName("surname");
            entity.Property(u => u.email).HasColumnName("email");
            entity.Property(u => u.age).HasColumnName("age");
            entity.Property(u => u.permission).HasColumnName("permission");
            entity.Property(u => u.hsh).HasColumnName("hsh");
            entity.Property(u => u.salt).HasColumnName("salt");
        });
    }
}