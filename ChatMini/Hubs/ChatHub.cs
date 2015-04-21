using ChatMini.Models;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatMini.Hubs
{
    public class ChatHub : Hub
    {
        private static List<User> Users = new List<User>();

        /// <summary>
        /// Отправка сообщений
        /// </summary>
        public void Send(string name, string message)
        {
            Clients.All.addMessage(name, message);
        }

        //Подключение нового пользователя
        public void Connect(string userName)
        {
            var id = Context.ConnectionId;

            if (!Users.Any(x => x.ConnectionId == id))
            {
                Users.Add(new User { ConnectionId = id, Name = userName });

                //посылаем сообщение текущему пользователю
                Clients.Caller.onConnected(id, userName, Users);

                //посылаем сообщения всем, кроме текущего
                Clients.AllExcept(id).onNewUserConnected(id, userName);
            }
        }


        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            //var connectionId = Context.ConnectionId;
            var item = Users.FirstOrDefault(v => v.ConnectionId == Context.ConnectionId);

            if (item != null)
            {
                Users.Remove(item);
                Clients.All.onUserDisconnected(item.ConnectionId, item.Name);
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}