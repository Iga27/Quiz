using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Quiz.Models
{
    public class Answer
    {
        /*[Key]
        [ForeignKey("Option")]
        public int OptionId { get; set; }
        [JsonIgnore]
        public Option Option { get; set; }


         
        [ForeignKey("Question")]
        public int QuestionId { get; set; }
        [JsonIgnore]
        public Question Question { get; set; }*/
         
        public int Id { get; set; }

 

        public int OptionId { get; set; }

        public int QuestionId { get; set; }

        [JsonIgnore]
        public virtual Option Option { get; set; }
    }
}