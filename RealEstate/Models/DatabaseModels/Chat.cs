using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Models.DatabaseModels
{
    [Table("Chats")]
    [PrimaryKey("Id")]
    public class Chat
    {
        public int Id { get; set; }
        public int IdUser1 { get; set; }
        public int IdUser2 { get; set; }
        public int? IdInquiry { get; set; }
        public string? Subject { get; set; }
        public int? IdLastMessage { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
