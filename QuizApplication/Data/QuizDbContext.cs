using Microsoft.EntityFrameworkCore;
using System;

namespace QuizApplication.Data
{
    public class QuizDbContext : DbContext
    {
        public QuizDbContext(DbContextOptions<QuizDbContext> options)
            : base(options)
        {
        }
        public DbSet<Quiz> Quizes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<AnswerOption> AnswerOptions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Question>()
                .HasOne(q => q.Quiz)
                .WithMany(e => e.Questions)
                .HasForeignKey(q => q.QuizId);

            builder.Entity<Question>()
                .Property(q => q.QuestionType)
                .HasConversion(
                qtype => qtype.ToString(), //convert TO exp: enum(in code) to string(in db)
                qtype => (QuestionType)Enum.Parse(typeof(QuestionType), qtype)); //convert FROM exp: string(in db) to enum(in code)


            builder.Entity<AnswerOption>()
                .HasOne(o => o.Question)
                .WithMany(q => q.AnswerOptions)
                .HasForeignKey(o => o.QuestionId);            
        }
    }
}
