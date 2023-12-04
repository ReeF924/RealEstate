using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Models.DatabaseModels
{
    [Table("tbUsers")]
    [PrimaryKey("Id")]
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3)]
        [RegularExpression(@"^([a-zA-Z]{1,4}\. )?([a-zA-Z]+ ?){0,2}([a-zA-Z]+)$")]
        public string Name { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3)]
        [RegularExpression(@"^([a-zA-Z]+)$")]
        public string Surname { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Email { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 7)]
        [RegularExpression(@"^(\+[0-9]{1,3})?([0-9]+)$")]
        public string PhoneNumber { get; set; }
    }
}
