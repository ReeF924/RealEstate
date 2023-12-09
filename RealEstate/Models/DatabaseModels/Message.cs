using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Models.DatabaseModels
{
    [Table("tbMessages")]
    [PrimaryKey("Id")]
    public class Message
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdAdmin { get; set; }
        public int? IdInquiry { get; set; }
        public string Text { get; set; }
        public DateTime DateTimeSentUtc { get; set; }
    }
}
