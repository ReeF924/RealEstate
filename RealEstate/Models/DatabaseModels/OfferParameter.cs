using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Models.DatabaseModels
{
    [Table("OfferParameters")]
    [PrimaryKey("Id")]
    public class OfferParameter
    {
        public int Id { get; set; }
        public int IdParameter { get; set; }
        public int IdOffer { get; set; }
        public string Value { get; set; }

    }
}
