using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Quiz.Models
{
    public class Option
    {
         public int OptionId { get; set; }

         public string OptionText { get; set; }

         [ForeignKey("Question")]
         public int  QuestionId { get; set; }

         [JsonIgnore] //we need this attribute but i don't know why
         public virtual Question Question { get; set; }

         public bool IsCorrect { get; set; }

         

        //public Answer Answer { get; set; } //doesnt need this property in one-to-many in main class,
        //because Answer also have another foreign key for Option
        
    }
}