using Org.BouncyCastle.Tls.Crypto;
using RealEstate.Models.DatabaseModels;
namespace RealEstate.Models.ViewModels
{
	public class OfferEdit : Offer
	{
        //public List<KeyValuePair<KeyValuePair<int, string>, KeyValuePair<int, string>?>> Parameters { get; set; }
        public List<ParameterView> Parameters { get; set; }

        public OfferEdit(Offer offer, List<ParameterView> parameters)
        {
            this.Id = offer.Id;
            this.IdBroker = offer.IdBroker;
            this.Name = offer.Name;
            this.Description = offer.Description;
            this.ShortDescription = offer.ShortDescription;
            this.Price = offer.Price;
            this.Category = offer.Category;
            this.Price = offer.Price;
            this.Location = offer.Location;

            this.Parameters = parameters;

            //this.Parameters = new();

            //parameters.ForEach(param =>
            //{
            //    OfferParameter? offerParameter = param.Parameter;

            //    KeyValuePair<int, string>? offerKVP = offerParameter == null ? null : new KeyValuePair<int, string>(offerParameter.Id, offerParameter.Value);

            //    this.Parameters.Add(new(new(param.Id, param.Value), offerKVP));
            //});
        }

        public OfferEdit()
        {
            
        }
    }
}
