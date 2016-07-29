using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GeekQuiz.Models
{
    public class Score
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string name { get; set; }
        public int _RoomID { get; set; }
        public int score { get; set; }

        public Score (string name, int id)
        {
            this.name = name;
            this._RoomID = id;


        }
        public Score()
        { }
    }
}
