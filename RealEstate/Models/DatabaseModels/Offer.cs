using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Models.DatabaseModels
{
    [Table("tbOffers")]
    [PrimaryKey("Id")]
    public class Offer
    {
        public int Id { get; set; }
        public int IdSeller { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string? ShortDescription { get; set; }
        public string Category { get; set; }
    }
}
