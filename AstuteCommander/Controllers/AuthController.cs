using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;

namespace AstuteCommander.Controllers
{
    public class AuthController : AstuteController
    {
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger, IMemoryCache memCache, IAppSettings settings, IHostingEnvironment env) : base(memCache, settings, env)
        {
            _logger = logger;
        }

        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Logout()
        {            
            HttpContext.Session.Remove(Session.USER);
            HttpContext.Session.Remove(Session.TOKEN);
            HttpContext.Session.Remove(Session.VSTS);
            HttpContext.Session.Remove(Session.GIT);
            HttpContext.Session.Remove(Session.PROJECTS);
            HttpContext.Session.Remove(Session.LOGGEDIN);
            HttpContext.Session.Remove(Session.SELECTED_PROJ);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Login(Models.LoginVM model)
        {
            try
            {
                if (true) // ModelState.IsValid
                {
                    if (LogIn(model.UserName, model.Password))
                    {
                        bool isLoggedIn = true;

                        try
                        {
                            // Set the session stuff up
                            HttpContext.Session.SetString(Session.USER, model.UserName);              //"sstraley1"
                            HttpContext.Session.SetString(Session.TOKEN, model.PersonalAccessToken);  //"sksf7bfglyb5n65wm7csutpnrmxg4kxwdz7b66v6asazp56pgt7q"


//                            var root = _env.ContentRootPath + "\\App_data\\" + HttpContext.Session.GetString(Session.USER);
                            var root = @"C:\vsts";

                            var bash = _env.ContentRootPath + "\\App_data\\Git";
//                            var bash = @"C:\Program Files\Git\usr\bin";                            

                            HttpContext.Session.SetString(Session.VSTS, root );       //"C:\vsts"
                            HttpContext.Session.SetString(Session.GIT, bash);     //"C:\Program Files\Git\usr\bin\sh.exe"

                            // find LOCAL VSTS Folder and if not, then make
                            if (!System.IO.Directory.Exists(HttpContext.Session.GetString(Session.VSTS)))
                            {
                                System.IO.Directory.CreateDirectory(HttpContext.Session.GetString(Session.VSTS));
                            }

                            // find GIT exe file
                            if (!System.IO.File.Exists(HttpContext.Session.GetString(Session.GIT) + @"\sh.exe"))
                            {
                                isLoggedIn = false;
                                // maybe throw an error
                            }

                            // Need this to set stating organization name
                            Classes.VSTS.Vsts.SetOrg(_settings.Setting.GetSection("Organization").Value);
                            var tempVsts = new Classes.VSTS.Vsts(HttpContext.Session.GetString(Session.TOKEN));
                            var projects = tempVsts.GetProjects();


                            if (projects != null)
                                HttpContext.Session.SetComplexData(Session.PROJECTS, projects);

                        }
                        catch (System.Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine(ex.Message);
                            isLoggedIn = false;
                        }

                        HttpContext.Session.SetInt32(Session.LOGGEDIN, isLoggedIn ? 0 : 1);
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                return View();
            }
        }

        private bool LogIn(string userName, string password)
        {
            return true;
        }
    }
}
