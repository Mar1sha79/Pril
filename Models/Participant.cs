using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Приложение.Models
{
    [Table("Participants")] // Имя таблицы в нижнем регистре
    public class Participant
    {
        [Key]
        [Column("id")] // Связываем с 'id' в PostgreSQL
        public int Id { get; set; }

        [Required]
        [Column("fullname")] // Связываем с 'fullname'
        public string FullName { get; set; } = string.Empty;

        [Column("birthdate")] // Исправляет ошибку column p.BirthDate does not exist
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [Column("teamname")] // Связываем с 'teamname'
        public string? TeamName { get; set; }
    }
}