﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Owin;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(ChatMini.App_Start.Startup))]
namespace ChatMini.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}