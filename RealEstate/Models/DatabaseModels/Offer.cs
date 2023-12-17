using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RealEstate.Models.DatabaseModels
{
    [Table("Offers")]
    [PrimaryKey("Id")]
    public class Offer
    {
        public int Id { get; set; }
        public int? IdBroker { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string? ShortDescription { get; set; }
        public char Category { get; set; } // f - flats, l - luxury,  h - houses, c - cottages
        public char EnergyClass { get; set; }
        [ForeignKey("IdOffer")]
        public List<Image> Images { get; set; } = new();
        [ForeignKey("IdOffer")]
        public List<OfferParameter> OfferParameters { get; set; } = new();
    }
}
