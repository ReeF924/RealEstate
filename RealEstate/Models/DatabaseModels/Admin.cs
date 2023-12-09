using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Models.DatabaseModels
{
    [Table("tbAdmins")]
    public class Admin : LoginUser
    {
        public bool IsAdmin { get; set; }

        public Admin(int id, string username, string password, string name, string surname, string email, string phoneNumber, bool isAdmin) : base(id, username, password, name, surname, email, phoneNumber)
        {
            this.IsAdmin = isAdmin;
        }
    }
}
