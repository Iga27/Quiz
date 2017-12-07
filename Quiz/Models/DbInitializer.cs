using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Quiz.Models
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<QuizContext>
    {

        protected override void Seed(QuizContext context)  
        {
            context.Questions.Add(new Question
            { //Id???
                QuestionText = "В каком году основан город Брест?",
                Options = new Option[] { new Option { OptionText = "1018", IsCorrect = false }, new Option { OptionText = "1019", IsCorrect = true }, new Option { OptionText = "1020", IsCorrect = false } }
            });

            context.Questions.Add(new Question
            { //Id???
                QuestionText = "Как звали Гоголя ?",
                Options = new Option[] { new Option { OptionText = "Василий",IsCorrect=false }, new Option { OptionText = "Николай",IsCorrect=true }, new Option { OptionText = "Андрей", IsCorrect = false } }
            });
            context.SaveChanges();
            base.Seed(context);
        }
    }
}