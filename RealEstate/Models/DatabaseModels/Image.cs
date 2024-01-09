using System.ComponentModel.DataAnnotations;
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
        [StringLength(36)]
        public string PathName { get; set; }
        public bool IsMainImage { get; set; }
    }
}
