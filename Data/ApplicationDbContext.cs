using Microsoft.EntityFrameworkCore;
using Приложение.Models;

namespace Приложение.Data
{
    /// <summary>
    /// Контекст базы данных для управления соревнованиями.
    /// Обеспечивает связь моделей C# с таблицами PostgreSQL.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            // Решение ошибки DateTime Kind=Unspecified (image_bb1598.png)
            // Это позволяет PostgreSQL принимать обычные даты без принудительного UTC
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        // Ваши основные таблицы (Требование: 3 связных таблицы)
        public DbSet<Competition> competitions { get; set; }
        public DbSet<Participant> participants { get; set; }
        public DbSet<Result> results { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Решение ошибки "relation Competitions does not exist" (image_c50af8.png)
            // Принудительно связываем код с таблицами с большой буквы из pgAdmin
            modelBuilder.Entity<Competition>().ToTable("competitions");
            modelBuilder.Entity<Participant>().ToTable("participants");
            modelBuilder.Entity<Result>().ToTable("results");

            // Настройка связей один-ко-многим для таблицы Результатов
            modelBuilder.Entity<Result>()
                .HasOne(r => r.Competition)
                .WithMany()
                .HasForeignKey(r => r.CompetitionId);

            modelBuilder.Entity<Result>()
                .HasOne(r => r.Participant)
                .WithMany()
                .HasForeignKey(r => r.ParticipantId);
        }
    }
}