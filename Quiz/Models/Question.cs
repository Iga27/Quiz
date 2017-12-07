using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quiz.Models
{
    public class Question
    {
        public int QuestionId { get; set; }

        public string QuestionText { get; set; }

       // public string AdditionalField { get; set; }

        // public Answer Answer { get; set; }

        public ICollection<Option> Options { get; set; }

         public Question()
        {
            Options = new List<Option>();
        } 
    }
}