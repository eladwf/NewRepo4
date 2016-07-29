using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekQuiz.wwwroot.SignalRChat
{
    public class ChatHub
    {

        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(name, message);
        }


    }
}
