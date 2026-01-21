using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Приложение.Models
{
    [Table("results")]
    public class Result
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("competitionid")]
        public int CompetitionId { get; set; }
        public Competition? Competition { get; set; }

        [Column("participantid")]
        public int ParticipantId { get; set; }
        public Participant? Participant { get; set; }

        [Column("score")]
        public decimal Score { get; set; }

        [Column("rank")]
        public int Rank { get; set; }
    }
}