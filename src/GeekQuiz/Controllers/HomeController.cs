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
using GeekQuiz.Services;


namespace GeekQuiz.Controllers
{


    [Microsoft.AspNet.Authorization.Authorize]


    public class HomeController : Controller
    {


        private TriviaDbContext context;
        private TriviaService Ts;


        public HomeController(TriviaDbContext context)
        {
            this.context = context;

            Ts = new TriviaService(context);
        }



        [HttpPost]
        public ActionResult Rules(Room r)
        {

            if(r.IsLocked==false)
            ModelState.Remove("Password");

            if (ModelState.IsValid)

            {

               
                Ts.CreateRoom(r, User.GetUserId(), User.GetUserName());

                
                return RedirectToAction("Room");
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            
            return View(r);
        }


        [HttpPost]
        public ActionResult ClassRules(Room r)
        {

            var add = Request.Form["submitbutton1"].Count;

            var start = Request.Form["submitbutton2"].Count;
            if (r.IsLocked == false)
                ModelState.Remove("Password");

            if (ModelState.IsValid)

            {
                
                Ts.CreateClassRoom(r, User.GetUserId(), User.GetUserName());


                if (add > 0)
                {

                    return RedirectToAction("AddingQuestions");

                }
                if (start > 0)
                {

                    return RedirectToAction("ClassRoom");


                }

                return View(r);

            }
            else
            {
                return View(r);
            }


           

        }




        //called from angular http object
        [HttpGet]
        public Room getRoom()
        {
            return Ts.GetRoom(User.GetUserId());
        }

        public IActionResult Room()

        {

            Room r = getRoom();

            if (r==null)
            {
                ViewData["Action"] = "Lobby";

               
                return RedirectToAction("Lobby");

            }

            return View(r);
        }


        public IActionResult Index()
        {
            string id = User.GetUserId();
            string name = User.GetUserName();
            Ts.AddUser(id,name);

            return View();
        }
        [HttpGet]
        public IActionResult Summary() {

            string id = User.GetUserId();
            Room r = Ts.GetRoom(id);
            UserAtrr usr = context.UserAtrr.Where(o => o.AppUserID == id).First();
            usr.GlobalScore += usr.RoomScore;
            return View(r);
        }



        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
        public IActionResult Rules()
        {
            return View();
        }
        public IActionResult ClassRules()
        {
            return View();
        }

        public ActionResult Lobby()
        {
            var rooms = context.Room.Where(r => r.ClassRoom != true).ToList();
            return View(rooms);
        }
        public ActionResult Score()
        {
            var players = context.UserAtrr.ToList();
            return View(players);
        }



        public ActionResult ClassLobby()
        {
            var rooms = context.Room.Where(r=>r.ClassRoom == true).ToList();
            return View(rooms);
        }

        public IActionResult EnterRoomNoPass(Room room)
        {


            string id = User.GetUserId();

            Ts.EnterToRoom(room, id);
            
            return RedirectToAction("Room");
        }

        public IActionResult EnterClassRoomNoPass(Room room)
        {


            string id = User.GetUserId();
            
            Ts.EnterToClassRoom(room, id);
            if (room.AdminName == User.GetUserName())
                return View("Teacher", room);
            return RedirectToAction("ClassRoom");
        }


        [HttpGet]
        public JsonResult EnterRoom(int roomID, string pass)
        {
            string id = User.GetUserId();
            Room room = context.Room.Where(o => o.RoomID == roomID).First();
            if (pass == PasswordHasher.Decrypt(room.Password))
            {
                Ts.EnterToRoom(room, id);
                return Json("ddd");
            }
            return Json("wrong password");
        }

        public IActionResult ExitGame()
        {
            var id = User.GetUserId();
            Ts.RemoveUser(id);
            
            return RedirectToAction("Lobby");

        }

        public IActionResult StartGame()
        {

            if (Ts.CheckIfRoomIsFull(User.GetUserId()))

                {

                ViewData["RoomEmpty"] = false;

                return View();
                }
            ViewData["RoomEmpty"] = true;
           
            if (ViewData["Action"] != null)
            {
                if (ViewData["Action"].Equals("Lobby"))
                {
                    
                    return RedirectToAction("Lobby");
                }
            }
            return RedirectToAction("Room");

        }
        public IActionResult StartMultiPlayerGame()
        {
            Room room = getRoom();

                room.IsActive = true;
                Ts.AddClassQuestions(room);
                context.SaveChanges();
            if (room.AdminName == User.GetUserName())
            {
                return View("Teacher",room);
            }
            return View();

        }



        public ActionResult AddingQuestions(Question data)
        {
            if (data.Title == null)
                return View();

            TriviaQuestion q = new TriviaQuestion();
            q.Title = data.Title;
            Room room = context.Room.Where(r => (r.AdminName == User.GetUserName()) && (r.ClassRoom)).First();
            q.Options = data.options;
            q.Cat = Models.Room.Catrgory.General;
            q.roomID = room.RoomID;
            context.TriviaQuestions.Add(q);
            context.SaveChanges();
            ModelState.Clear();
            return View();
        }

        [HttpGet]
        public JsonResult CheckRoom()
        {
            var roomstate = false;
            if (Ts.CheckIfRoomIsFull(User.GetUserId()))
            {
                ViewData["RoomEmpty"] = false;
                roomstate = true;
            }
            else

            {
                ViewData["RoomEmpty"] = true;
            roomstate =false;
            }
            return Json(roomstate);
        }


        public IActionResult ClassRoom()
        {
            Room r = getRoom();

            if (r == null)
            {
                ViewData["Action"] = "Lobby";

                
                return RedirectToAction("ClassLobby");

            }

            return View(r);
            
        }
        [HttpGet]
        public JsonResult UserList()
        {
            var cl = new List<String>();

            Room r = Ts.GetRoom(User.GetUserId());
            cl = context.UserAtrr.Where(o => o._RoomID == r.RoomID).Select(u => u.UserName).ToList();


            return Json(cl.ToList());
        }
        public IActionResult Teacher()
        {
            Room room = context.Room.Where(r => r.AdminName == User.GetUserName()).First();
            return View(room);
        }
    }


}
