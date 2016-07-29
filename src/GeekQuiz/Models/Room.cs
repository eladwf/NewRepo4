using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GeekQuiz.Models
{
    public class Room
    {
        public enum Catrgory {
            General=1,
            History=2,
        }
        [Required(ErrorMessage = "Please enter a password")]
        [Display(Name = "Password:")]

        
        public string Password {
            get { return Passwordstored; } set {  Passwordstored= Services.PasswordHasher.Encrypt(value); }
            
        }
       

        public string AdminName { get; set; }

        [ DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoomID { get; set; }
        
        [Display(Name = "number of question")]
        public int NumOfQuestion { get; set; }

        [Display(Name = "time per qustion")]
        public int Timeperquestion { get; set; }
        [Required(ErrorMessage = "Please enter a Room name")]
        [Display(Name = "Title:")]
        public string Title{get;set;}

        public virtual List<UserAtrr> Players { get; set; }

       public List<Score> score { get; set; }
        public int Lastquestionid { get; set; }


        public bool Active { get; set; }


        public List<Qlist> QuestionsArr { get; set; }
    public Catrgory Catgory { get; set; }
        protected virtual string Passwordstored { get;set; }

        public bool IsActive { get; set; }

        public bool IsLocked { get; set; }


        public bool ClassRoom { get; set; }


        public Room()
        {
            Players = new List<UserAtrr>();
            QuestionsArr = new List<Qlist>();
            score = new List<Score>();
            Timeperquestion = 20;
            NumOfQuestion = 40;
            
        }
      

    }
}
