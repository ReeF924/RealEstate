using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Models.DatabaseModels
{
    [Table("Parameters")]
    [PrimaryKey("Id")]
    public class Parameter
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}
