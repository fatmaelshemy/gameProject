using gameProject.Models;

namespace gameProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Games> Games { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<GameDevice> GameDevices { get; set; }
        public DbSet<Devices> Devices { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Category>()
                .HasData(new Category[]
                {
                    new Category {Id =1,Name ="Sports"},
                    new Category {Id =2,Name ="Action"},
                    new Category {Id =3,Name ="Adventure"},
                    new Category {Id =4,Name ="Racing"},
                    new Category {Id =5,Name ="Fighting"},
                    new Category {Id =6,Name ="Film"},

                });
            modelBuilder.Entity<Devices>()
                .HasData(new Devices[]
                {
                    new Devices {Id=1,Name="PlayStation",Icon="bi bi-playstation"},
                    new Devices {Id=2,Name="xbox",Icon="bi bi-xbox"},
                    new Devices {Id=3,Name="Nintendo Switch",Icon="bi bi-nintendo-switch"},
                    new Devices {Id=4,Name="pc",Icon="bi bi-pc-display"},
                });
            modelBuilder.Entity<GameDevice>()
                .HasKey(e => new { e.DeviceId, e.GameId });
            base.OnModelCreating(modelBuilder);
        }

    }
}
