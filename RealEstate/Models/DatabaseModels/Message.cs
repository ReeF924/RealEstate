using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RealEstate.Models.DatabaseModels
{
    [Table("Messages")]
    [PrimaryKey("Id")]
    public class Message
    {
        public int Id { get; set; }
        public int IdChat { get; set; }
        public int IdUser { get; set; }
        public string Text { get; set; }
        public bool IsRead { get; set; }
        public DateTime DateTimeSentUtc { get; set; }
    }
}
