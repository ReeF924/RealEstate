using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Models.DatabaseModels
{
    [Table("tbAdmins")]
    public class Admin : LoginUser
    {
        public bool IsAdmin { get; set; }
    }
}
