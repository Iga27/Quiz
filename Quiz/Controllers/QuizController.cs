using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Quiz.Models;

namespace Quiz.Controllers
{
    public class QuizController : ApiController
    {
        private QuizContext db = new QuizContext();

        // GET: api/Quiz
        /* public IQueryable<Question> GetQuestions()
         {
             return db.Questions;
         }*/

        private async Task<Question> NextQuestionAsync()
        {
            var random = new Random();

            return await db.Questions.OrderBy(x=>x.QuestionId).Skip(random.Next(0, QuestionsCount())).Include(x=>x.Options).FirstOrDefaultAsync();

        }

        // GET: api/Quiz/
        //[Queryable] //for OData
        [ResponseType(typeof(Question))]
        public async Task<IHttpActionResult> GetQuestion() //without id(random)
        {
            Question question =await NextQuestionAsync();
            if (question == null)
            {
                return NotFound();
            }

            return Ok(question);
        }

        // PUT: api/Quiz/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutQuestion(int id, Question question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

             if (id != question.QuestionId)
            {
                return BadRequest();
            }
           
            db.Entry(question).State = EntityState.Modified;

            try
            {
                 await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Quiz/answer
        [ResponseType(typeof(Answer))]
        [ActionName("answer")]
        [HttpPost]
        public async Task<IHttpActionResult> PostAnswer([FromBody]Answer answer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isCorrect = await AnswerAsync(answer);
            return this.Ok<bool>(isCorrect);
        }

        private async Task<bool> AnswerAsync(Answer answer)
        {
            db.Answers.Add(answer);
            await db.SaveChangesAsync();

            var selectedOption =await db.Options.FirstOrDefaultAsync(o => o.OptionId == answer.OptionId && o.QuestionId == answer.QuestionId);//only selected(not true or false)
            return selectedOption.IsCorrect;
        }

        //api/quiz/question/
           [ActionName("question")]
          [ResponseType(typeof(Question))]
          [HttpPost]
          public IHttpActionResult QuestionPost( Question question)
          {
              if (!ModelState.IsValid)
              {
                  return BadRequest(ModelState);
              }

              var questionDb=db.Questions.Add(question);
              db.SaveChangesAsync();

            // return CreatedAtRoute("ControllerAndAction", new { id = question.QuestionId }, question);
            return this.Ok<Question>(question);
        } 

        

    // DELETE: api/Quiz/5
    [HttpDelete]
        [ResponseType(typeof(Question))]
        public async Task<IHttpActionResult> DeleteQuestion(int id)
        {
            Question question = await db.Questions.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }

            db.Questions.Remove(question);
            await db.SaveChangesAsync();

            return Ok(question);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool QuestionExists(int id)
        {
            return db.Questions.Count(e => e.QuestionId == id) > 0;
        }

        private int QuestionsCount()
        {
            return db.Questions.Count();
        }
    }
}