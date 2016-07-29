using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekQuiz.Models
{
    public class Question
    {
        public string Title{get;set;}

        public List<TriviaOption> options { get; set; }

        


    }
}
