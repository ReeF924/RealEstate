using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Models.DatabaseModels
{
    [Table("tbOfferParameters")]
    public class OfferParameter
    {
        public int Id { get; set; }
        public int IdLabel { get; set; }
        public int IdOffer { get; set; }
        public string Value { get; set; }

    }
}
