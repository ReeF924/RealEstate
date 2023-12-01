using Microsoft.EntityFrameworkCore;

namespace RealEstate.Models.DatabaseModels
{
    [PrimaryKey("Id")]
    public class Admin
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? ProfilePicturePath { get; set; }
    }
}
