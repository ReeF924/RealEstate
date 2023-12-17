using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace RealEstate.Models.DatabaseModels
{
    [Table("Images")]
    [PrimaryKey("Id")]
    public class Image
    {
        public int Id { get; set; }
        public int IdOffer { get; set; }
    }
}
