using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jukebox.Controllers
{
    public class LoginController : WebApiController
    {
        public LoginController() : base()
        {
        }

        [Route(HttpVerbs.Post, "/login")]
        public int GetTestResponse()
        {
            return -1;
        }
    }
}
