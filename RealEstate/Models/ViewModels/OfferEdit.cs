using Org.BouncyCastle.Tls.Crypto;
using RealEstate.Models.DatabaseModels;
namespace RealEstate.Models.ViewModels
{
	public class OfferEdit : Offer
	{
        public List<ParameterView> Parameters { get; set; }
        public List<ImageEdit> ImageEdits { get; set; }

        public OfferEdit(Offer offer, List<ParameterView> parameters)
        {
            this.Id = offer.Id;
            this.IdBroker = offer.IdBroker;
            this.Name = offer.Name;
            this.Description = offer.Description;
            this.ShortDescription = offer.ShortDescription;
            this.Price = offer.Price;
            this.Category = offer.Category;
            this.Location = offer.Location;
            this.EnergyClass = offer.EnergyClass;
            this.Area = offer.Area;
            this.Price = offer.Price;
            this.Location = offer.Location;
            this.Region = offer.Region;
            this.Parameters = parameters;

            List<ImageEdit> images = new();

            offer.Images.ForEach(image =>
            {
                images.Add(new() { Name = image.PathName, ToRemove = false, IsMainImage = image.IsMainImage});
            });
            this.ImageEdits = images;
        }

        public OfferEdit()
        {

        }
    }
}
