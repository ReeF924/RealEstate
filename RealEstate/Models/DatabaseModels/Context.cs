using Microsoft.EntityFrameworkCore;

namespace RealEstate.Models.DatabaseModels
{
    public class Context :DbContext
    {
        public DbSet<Image>? Images { get; set; }
        public DbSet<Parameter>? Labels { get; set; }
        public DbSet<Admin>? Admins { get; set; }
        public DbSet<FavouriteOffer>? FavouriteOffers { get; set; }
        public DbSet<Inquiry> Inquiries { get; set; }
        public DbSet<Offer>? Offers { get; set; }
        public DbSet<OfferParameter>? OfferParameters { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=mysqlstudenti.litv.sssvt.cz;database=4b1_veseckylukas_db1;user=veseckylukas;password=123456;SslMode=none");
            //optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
