using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using AspiranturaSqlite.Models;
using AspiranturaSqlite.Models.ViewModels;

namespace AspiranturaSqlite.Data
{
    public class AspiranturaContext : DbContext
    {
        public AspiranturaContext(DbContextOptions<AspiranturaContext> options) : base(options)
        {
        }

        public DbSet<Speciality> Specialities { get; set; }
        public DbSet<Knowledge> Knowledges { get; set; }
        public DbSet<Aspirant> Aspirants { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<AspirantOrder> AspirantOrders { get; set; }
        public DbSet<StatusType> Statuses { get; set; }
        public DbSet<OrderType> Ordertypes { get; set; }
        
        // Для того, чтобы таблицы были в единственном числе
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Speciality>().ToTable("Speciality");
            modelBuilder.Entity<Knowledge>().ToTable("Knowledge");
            modelBuilder.Entity<Aspirant>().ToTable("Aspirant");
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<StatusType>().ToTable("StatusType");
            modelBuilder.Entity<OrderType>().ToTable("OrderType");

            // табл связи многие ко многим
            modelBuilder.Entity<AspirantOrder>().ToTable("AspirantOrder");
            modelBuilder.Entity<AspirantOrder>().HasKey(c => new { c.AspirantId, c.OrderId });          // настраивает составной первичный ключ

        }
        
        // Для того, чтобы таблицы были в единственном числе
      //  public DbSet<AspiranturaSqlite.Models.ViewModels.OrderVM> OrderVM { get; set; }
        
        // Для того, чтобы таблицы были в единственном числе
      //  public DbSet<AspiranturaSqlite.Models.ViewModels.AssignedAspirantData> AssignedAspirantData { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "D://aspirantura.db" };
        //    var connectionString = connectionStringBuilder.ToString();
        //    var connection = new SqliteConnection(connectionString);

        //    optionsBuilder.UseSqlite(connection);
        //}
    }
}
