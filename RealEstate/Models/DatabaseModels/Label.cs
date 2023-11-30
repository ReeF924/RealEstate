using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Models.DatabaseModels
{
    [Table("tbLabels")]
    [PrimaryKey("Id")]
    public class Label
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}
