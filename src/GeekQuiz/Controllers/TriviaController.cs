using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using GeekQuiz.Models;
using Microsoft.AspNet.Authorization;
using Microsoft.Data.Entity;
using System.Security.Claims;
using GeekQuiz.Services;
// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GeekQuiz.Controllers
{
   [Produces("application/json")]
   [Route("api/[controller]")]
    [Authorize]
    public class TriviaController : Controller
    {
        private TriviaDbContext context;
        private TriviaService ts;
       
        public TriviaController(TriviaDbContext context)
        {
            ts = new TriviaService(context);
            this.context = context;
        }

        // GET: api/Trivia
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = User.Identity.Name;
            string str = User.GetUserId();
            Room room = ts.GetRoom(str);
            var Userattr=await context.UserAtrr.Where(user => user.AppUserID == str).FirstAsync();
            Userattr.Answered = false;
            Userattr.StartTime = DateTime.UtcNow;

            TriviaQuestion nextQuestion =
                await this.NextQuestionAsync(userId,room,Userattr);
            Userattr.QuestionID = nextQuestion.Id;
            if (nextQuestion == null)
            {
                return HttpNotFound();
            }        
            context.Update(Userattr);
            context.SaveChanges();
            return Ok(nextQuestion);
        }

        // POST: api/Trivia
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TriviaAnswer answer)
        {
            string UserID = User.GetUserId();
            Room room = ts.GetRoom(UserID);
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            var Userattr = await context.UserAtrr.Where(user => user.AppUserID == UserID).FirstAsync();

            Userattr.EndTime = DateTime.UtcNow;
            TimeSpan sub = Userattr.EndTime - Userattr.StartTime;
            Userattr.Answered = true;
            float time=sub.Milliseconds;
           
            float score = (1 / time)*100000;
            Score scr = room.score.Where(o=>o.name==Userattr.UserName).First();
            var isCorrect = await this.StoreAsync(answer, room, Userattr);
            if (isCorrect)
            {
                scr.score += (int)score;
                Userattr.RoomScore += (int)score;
                Userattr.GlobalScore += (int)score;
                context.Update(scr);
            }

            context.Update(Userattr);
            context.SaveChanges();
            return this.CreatedAtAction("Get", new { }, isCorrect);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }

            base.Dispose(disposing);
        }

        private async Task<TriviaQuestion> NextQuestionAsync(string userId, Room room, UserAtrr usr)
        {
            var lastQuestionId = usr.QuestionID;

            int nextQuestionId;
            if (lastQuestionId == 0)
            {
                nextQuestionId = room.QuestionsArr[0].Value;
                return await this.context.TriviaQuestions.Where(o => o.Id==nextQuestionId).Include(q => q.Options).FirstOrDefaultAsync(q => q.Id == nextQuestionId);
            }
            nextQuestionId = room.QuestionsArr.Where(p=>p.Value>lastQuestionId).Select(q => q.Value).First();
            var final = await this.context.TriviaQuestions.Where(o => o.Id==nextQuestionId).Include(q => q.Options).FirstOrDefaultAsync(q => q.Id == nextQuestionId);
            
            return final;
        }

        private async Task<TriviaQuestion> NextClassQuestionAsync(string userId, Room room, UserAtrr usr)
        {
            var lastQuestionId = usr.QuestionID;

            int nextQuestionId;
            if (lastQuestionId == 0)
            {
                nextQuestionId = room.QuestionsArr[0].Value;
                return await this.context.TriviaQuestions.Where(o => o.Id == nextQuestionId).Include(q => q.Options).FirstOrDefaultAsync(q => q.Id == nextQuestionId);
            }
            nextQuestionId = room.QuestionsArr.Where(p => p.Value > lastQuestionId).Select(q => q.Value).First();
            var final = await this.context.TriviaQuestions.Where(o => o.Id == nextQuestionId).Include(q => q.Options).FirstOrDefaultAsync(q => q.Id == nextQuestionId);

            return final;
        }


        private async Task<bool> StoreAsync(TriviaAnswer answer,Room room, UserAtrr usr)
        {
            

            if (answer.UserID == "wrong")
            {
                answer.UserID = User.Identity.Name;
                answer.RoomID = room.RoomID;
                var Option = await this.context.TriviaOptions.FirstOrDefaultAsync(o =>
                    o.Id == answer.OptionId
                    && o.QuestionId == answer.QuestionId);
                if (Option != null)
                {
                    answer.RoomID = room.RoomID;
                    answer.TriviaOption = Option;
                    this.context.TriviaAnswers.Add(answer);

                    await this.context.SaveChangesAsync();
                }
                return false;

            }


            answer.UserID = User.Identity.Name;
            answer.RoomID = room.RoomID;
            var selectedOption = await this.context.TriviaOptions.FirstOrDefaultAsync(o =>
                o.Id == answer.OptionId
                && o.QuestionId == answer.QuestionId);
            if (selectedOption != null)
            {
                answer.TriviaOption = selectedOption;
                this.context.TriviaAnswers.Add(answer); 
               
                await this.context.SaveChangesAsync();
            }

            return selectedOption.IsCorrect;
        }
    }
}
