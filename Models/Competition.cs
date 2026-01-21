using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Приложение.Models
{
    [Table("competitions")] 
    public class Competition
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("date")]
        public DateTime Date { get; set; }

        [Column("location")]
        public string? Location { get; set; }
    }
}