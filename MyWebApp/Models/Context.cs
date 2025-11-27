using Microsoft.EntityFrameworkCore;

namespace MyWebApp.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }
        public DbSet<Watch> WatchesTable { get; set; }
        public DbSet<User> UsersTable { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ---------------- UsersTable Seed ----------------
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = Guid.Parse("bf9f6fbf-df81-45df-b037-82eaff7aa4d6"),
                    UserName = "admin",
                    UserEmail = "admin@gmail.com",
                    UserPassword = "admin2627",
                    IsOnline = false
                },
                new User
                {
                    UserId = Guid.Parse("f30379bd-ee19-4582-a4ed-976841d89390"),
                    UserName = "Furkan",
                    UserEmail = "mfurkantelli@gmail.com",
                    UserPassword = "26278367",
                    IsOnline = false
                }
            );

            // ---------------- WatchesTable Seed ----------------
            modelBuilder.Entity<Watch>().HasData(
                new Watch
                {
                    Id = Guid.Parse("843f7c4d-e699-491b-a3e9-045b51a25c70"),
                    WatchName = "Silver Index Fluted Yellow Gold Leather 18238",
                    WatchBrand = "Rolex",
                    Price = 12525m,
                    Img = "https://d2j6dbq0eux0bg.cloudfront.net/images/38270005/4079824799.jpg"
                },
                new Watch
                {
                    Id = Guid.Parse("60c5951c-1c33-4184-8808-07e2a396d1dc"),
                    WatchName = "26315ST Silver & Blue Dial Mens Watch Box Papers",
                    WatchBrand = "Audemars Piguet",
                    Price = 49999m,
                    Img = "https://img.chrono24.com/images/uhren/42214924-sc9belurrolnws8n76a18bwe-ExtraLarge.jpg"
                },
                new Watch
                {
                    Id = Guid.Parse("b720e0ed-5b5d-4aaf-97dd-5d6defb83b1a"),
                    WatchName = "Sport Daytona 16518 Full set Pre-owned 1997",
                    WatchBrand = "Rolex",
                    Price = 28000m,
                    Img = "https://s3.us-east-1.amazonaws.com/ISHOWIMAGES/ROLEX+V7/wp/model_gallery_assets_portrait/Slide4_portrait/m126506-0001.webp"
                },
            new Watch
            {
                Id = Guid.Parse("2e0592bd-a5a4-485e-992d-9985ac0e3b57"),
                WatchName = "Thin",
                WatchBrand = "A. Lange & Söhne",
                Price = 15999m,
                Img = "https://img.chrono24.com/images/uhren/43258204-pk3j42yayatax2wodailsezm-ExtraLarge.jpg"
            },
            new Watch
            {
                Id = Guid.Parse("01706492-d963-4c7f-a069-9d80f42e203f"),
                WatchName = "Seamaster Diver 300M",
                WatchBrand = "Omega",
                Price = 337m,
                Img = "https://www.omegawatches.com/media/catalog/product/o/m/omega-seamaster-diver-300m-co-axial-master-chronometer-42-mm-21032422001005-2b286f.png?w=700"
            },
            new Watch
            {
                Id = Guid.Parse("5a2ec16c-d2a3-4023-a8bc-aaf12f6b0017"),
                WatchName = "Triple Calendar Moon Phase Indicates week day month date and accurate moon phase",
                WatchBrand = "Ernest Borel",
                Price = 2450m,
                Img = "https://img.chrono24.com/images/uhren/42798797-ultc6577p1gbjwgwgy79ogrh-ExtraLarge.jpg"
            },
            new Watch
            {
                Id = Guid.Parse("3f2b06b8-aebb-4866-a194-c93bd381f11b"),
                WatchName = "Welder WRC407 45 mm Erkek Kol Saati",
                WatchBrand = "WELDER",
                Price = 8770m,
                Img = "https://cdn.saatvesaat.com.tr/media/catalog/product/b/5/b5a02869a055070e6ed199509d879f19a4dd08cbeb488683f231fe5148fda3e0.jpeg"
            },
            new Watch
            {
                Id = Guid.Parse("58d1f733-7a74-4ec8-afcc-d67139db2846"),
                WatchName = "De Cartier Large Silver Dial Stainless Steel WSSA0018 4072",
                WatchBrand = "Cartier",
                Price = 6895m,
                Img = "https://i.ebayimg.com/images/g/IwEAAeSwQaJoyv-a/s-l1200.jpg"
            });
        }

    }

}
