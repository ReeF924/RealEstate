using RealEstate.Models.DatabaseModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Models.ViewModels
{
    public class OfferList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Location { get; set; }
        public string? Broker { get; set; }
        public string Category { get; set; } // f - flats, l - luxury,  h - houses, c - cottages
        public int OffersMade { get; set; }
        public OfferList(Offer offer, string broker, int offersMade)
        {
            this.Id = offer.Id;
            this.Name = offer.Name;
            this.Price = offer.Price;
            this.Location = offer.Location;
            this.Broker = broker;

            switch (offer.Category)
            {
                case 'f':
                    this.Category = "Flat";
                    break;
                case 'l':
                    this.Category = "Luxury";
                    break;
                case 'h':
                    this.Category = "House";
                    break;
                case 'c':
                    this.Category = "Cottage";
                    break;
            }
            this.OffersMade = offersMade;
        }
    }
}
