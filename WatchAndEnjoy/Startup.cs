﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WatchAndEnjoy.Startup))]
namespace WatchAndEnjoy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
