using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RealEstate.Models.DatabaseModels
{
    [Table("tbMessages")]
    [PrimaryKey("Id")]
    public class Message
    {
        public int Id { get; set; }
        public int IdSender { get; set; }
        public int IdRecipient { get; set; }
        public int? IdInquiry { get; set; }
        public string Text { get; set; }
        public DateTime DateTimeSentUtc { get; set; }
        [JsonIgnore]
        public User? OtherUser { get; set; }
    }
}
