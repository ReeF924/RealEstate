using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace RealEstate.Models.DatabaseModels
{
    [Table("tbImages")]
    [PrimaryKey("Id")]
    public class Image
    {
        public int Id { get; set; }
        public int IdOffer { get; set; }
        public string ImagePath { get; set; }
    }
}
