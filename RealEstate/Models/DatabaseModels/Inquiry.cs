using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Models.DatabaseModels
{
    [Table("tbInquiries")]
    [PrimaryKey("Id")]
    public class Inquiry
    {
        public int Id { get; set; }
        public int IdOffer { get; set; }
        public int? IdUser { get; set; }
        public bool Status { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string? AdditionalInformation { get; set; }
        public DateTime DateTimeSent { get; set; } = DateTime.UtcNow;
    }
}
