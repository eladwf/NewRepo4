using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GeekQuiz.Models
{
    public class Qlist
    {
        public Qlist(int iddd,int RoomID)
        {
            Value = iddd;
            _RoomID = RoomID;
        }
        public Qlist()
        {  }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int Value { get; set; }
        public int _RoomID { get; set; }
    }
}
