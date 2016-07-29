using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.Data.Entity;
using Microsoft.AspNet.Authorization;
using GeekQuiz.Models;
using Microsoft.AspNet.Http.Internal;
using Microsoft.AspNet.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekQuiz.Models
{
    public class UserAtrr
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
        public string AppUserID { get; set; }
        public string UserName { get; set; }
        public bool CratedGame { get; set; }
        public bool IsPlaying { get; set; }
        public bool Answered { get; set; }
        public DateTime  StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int RoomScore { get; set; }
        public int QuestionID { get; set; }
        public int GlobalScore { get; set; }

        

        public int _RoomID { get; set; }
        public UserAtrr(string ID, string name)
        {
            AppUserID = ID;
            UserName = name;
            IsPlaying = false;
            CratedGame = true;
            GlobalScore = 0;

        }



        public UserAtrr() { }


    }
}       
