using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RealEstate.Models.DatabaseModels
{
    [PrimaryKey("Id")]
    public class LoginUser
    {
        public int Id { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 3)]
        [RegularExpression(@"^([^;""\\\/\!]+)$")]
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
        [JsonConstructor]
        public LoginUser(string username, string password, string name, string surname, string email, string phoneNumber)
        {
            Username = username;
            Password = password;
            Name = name;
            Surname = surname;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public LoginUser(int id, string username, string password, string name, string surname, string email, string phoneNumber) : this(username, password, name, surname, email, phoneNumber)
        {
            Id = id;
        }
        public LoginUser()
        {

        }
    }
}
