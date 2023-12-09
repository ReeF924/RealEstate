using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Models.DatabaseModels
{
    [Table("tbUsers")]
    public class User : LoginUser
    {
        public User(int id, string username, string password, string name, string surname, string email, string phoneNumber) : base(id, username, password, name, surname, email, phoneNumber)
        {
        }
        public User() : base()
        {
            
        }
    }
}
