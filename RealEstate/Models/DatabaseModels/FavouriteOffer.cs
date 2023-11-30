using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Models.DatabaseModels
{
    [Table("tbFavouriteOffers")]
    [PrimaryKey("IdUser", "IdOffer")]
    public class FavouriteOffer
    {
        public int IdUser { get; set; }
        public int IdOffer { get; set; }
    }
}
