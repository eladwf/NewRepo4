using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using GeekQuiz.Models;
using Microsoft.AspNet.Http.Internal;
using System.Security.Claims;
using Microsoft.Data.Entity;
using Microsoft.AspNet.Identity;



namespace GeekQuiz.Services
{
   
    public class TriviaService
    {
        private TriviaDbContext Db;

        public TriviaService(TriviaDbContext _db)
        {
            this.Db = _db;

        }
      
        public  void CreateRoom(Room r ,string id,string username)
        {

            r.AdminName = username;
            UserAtrr usr = Db.UserAtrr.Where(o => o.AppUserID == id).First();
            foreach (var x in r.Players)
            {
                if (x==usr)
                {
                    return;
                }
            }
            r.Players.Add(usr);
            r.IsActive = true;
            r.ClassRoom = false;
            usr.IsPlaying = true;

            usr.RoomScore = 0;
            Db.Room.Add(r);
            Db.SaveChanges();
            foreach (var x in Db.TriviaQuestions.Where(q => (q.Cat == r.Catgory)&&(q.roomID==0))
    .Take(r.NumOfQuestion)
    .Select(d => d.Id)
    .ToList())
            {
                r.QuestionsArr.Add(new Qlist(x, r.RoomID));
            }
            usr._RoomID = r.RoomID;
            Score score = new Score(usr.UserName, r.RoomID);
            score.score = 0;
            r.score.Add(score);
            Db.SaveChanges();



        }

        public void CreateClassRoom(Room r, string id, string username)
        {

            r.AdminName = username;
            UserAtrr usr = Db.UserAtrr.Where(o => o.AppUserID == id).First();
            r.IsActive = false;
            r.Catgory = Room.Catrgory.General;
            r.ClassRoom = true;
            usr.IsPlaying = true;
            Db.Room.Add(r);
            Db.SaveChanges();

        }

        public void AddClassQuestions(Room r)
        {
            if (r.QuestionsArr.Count > 0)
                return;
            foreach (var x in Db.TriviaQuestions.Where(q => q.roomID == r.RoomID)
.Take(r.NumOfQuestion)
.Select(d => d.Id)
.ToList())
            {
                r.QuestionsArr.Add(new Qlist(x, r.RoomID));
            }
            Db.SaveChanges();

        }




        public UserAtrr getUser(string id)
        {
            UserAtrr usr = Db.UserAtrr.Where(o => o.AppUserID == id).First();
            return usr;
        }
        public Room GetRoom(string id)
        {

            UserAtrr usr = getUser(id);


            List<Room> rooms = Db.Room.ToList();
            Room room = null;
            foreach (var r in rooms)
            {
                if (r.Players.Contains(usr))
                {
                    room = r;
                    break;
                }
            }
            if (room == null)
                return null;

            room.Players = Db.UserAtrr.Where(o => o._RoomID == room.RoomID).ToList();
            room.QuestionsArr=Db.Qlist.Where(o => o._RoomID == room.RoomID).ToList();
            room.score = Db.Score.Where(o => o._RoomID == room.RoomID).ToList();
            return room;
        }

        public void AddUser(string id, string name)
        {
            if (Db.UserAtrr.Any(o => o.AppUserID == id))

            {
                return;
            }

            UserAtrr usr = new UserAtrr(id,name);

            Db.UserAtrr.Add(usr);

            Db.SaveChanges();

            return;
        }

        public void EnterToRoom(Room room,string id)
        {

            room.Players = Db.UserAtrr.Where(o => o._RoomID == room.RoomID).ToList();
            UserAtrr usr = Db.UserAtrr.Where(o => o.AppUserID == id).First();
            usr.QuestionID = Db.TriviaQuestions.Select(o => o.Id).First();
            Db.SaveChanges();
            if (room.Players.Contains(usr) || room.Players.Count >= 2)
                return ;

            Score score = new Score(usr.UserName,room.RoomID);
            score.score = 0;
            usr.RoomScore = 0;
            room.score.Add(score);
            usr.IsPlaying = true;
            usr._RoomID = room.RoomID;
          
            room.Players.Add(usr);
            Db.Room.Update(room);
            Db.SaveChanges();
        }


        public void RemoveUser(string id)
        {

            UserAtrr usr = Db.UserAtrr.Where(o => o.AppUserID == id).First();

           
            usr.IsPlaying = false;
            Room r = GetRoom(id);
            if (r.Players.Count == 0)
                r.IsActive = false;
            r.Players.Remove(usr);
            usr._RoomID = -1;
           
           
            Db.SaveChanges();
        }

        internal void EnterToClassRoom(Room room, string id)
        {
            room.Players = Db.UserAtrr.Where(o => o._RoomID == room.RoomID).ToList();
            UserAtrr usr = Db.UserAtrr.Where(o => o.AppUserID == id).First();
            usr.QuestionID = Db.TriviaQuestions.Select(o => o.Id).First();
            Db.SaveChanges();
            if (room.Players.Contains(usr) )
                return;

            Score score = new Score(usr.UserName,room.RoomID);
            score.score = 0;
            usr.RoomScore = 0;
            room.score.Add(score);
            usr.IsPlaying = true;
            usr._RoomID = room.RoomID;

            room.Players.Add(usr);
            Db.Room.Update(room);
            Db.SaveChanges();
        }

        public bool CheckIfRoomIsFull(string id)
        {
            Room room = GetRoom(id);
            if (room == null)
                return false; 
            if (room.Players.Count==2)
                return true;

            return false;
        }

    }
}
