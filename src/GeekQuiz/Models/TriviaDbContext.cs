using Microsoft.Data.Entity;

namespace GeekQuiz.Models
{
    public class TriviaDbContext : DbContext
    {
        private static bool _created = false;

        public TriviaDbContext()
        {
            if (!_created)
            {
                _created = true;
                Database.EnsureCreated();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TriviaOption>()
                .HasKey(o => new { o.QuestionId, o.Id });

            builder.Entity<TriviaAnswer>()
                .HasOne(a => a.TriviaOption)
                .WithMany()
                .HasForeignKey(a => new { a.QuestionId, a.OptionId });

            builder.Entity<TriviaQuestion>()
                .HasMany(q => q.Options)
                .WithOne(o => o.TriviaQuestion);

            builder.Entity<Room>().HasKey(r => new { r.RoomID });
     //     builder.Entity<Room>().HasOne(r => r.Players).WithMany();
          builder.Entity<UserAtrr>().HasKey(o=>new { o.UserID });
                
        }
        
        public DbSet<TriviaQuestion> TriviaQuestions { get; set; }

        public DbSet<TriviaOption> TriviaOptions { get; set; }

        public DbSet<TriviaAnswer> TriviaAnswers { get; set; }

        public DbSet<Room> Room { get; set; }
        public DbSet<UserAtrr> UserAtrr { get; set; }
        public DbSet<Qlist> Qlist { get; set; }

        public DbSet<Score> Score { get; set; }

    }
}
