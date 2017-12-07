using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Quiz.Models
{
    public class QuizContext : DbContext
    {
        public QuizContext() : base("DbQuizConnection")
        {
           // Database.SetInitializer(new DbInitializer());
        }

        
        public DbSet<Question> Questions { get; set; }

        public DbSet<Option> Options { get; set; }

        public DbSet<Answer> Answers { get; set; }
    }
}