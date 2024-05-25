using Sem3Final.Models.Entities;
using Sem3Final.Models.ModelsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Sem3Final.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpGet]

        public ActionResult PageLogin(string returnUrl)
        {
            UserView modelUser = new UserView();
            try
            {

                if (User.IsInRole("USER"))
                {
                    string r = returnUrl;
                    if (r.Equals("/Admin/Index"))
                    {
                        return RedirectToAction("Page404", "Home");
                    }
                }

                //if (this.Request.IsAuthenticated)
                //{
                //    return this.RedirectToLocal(returnUrl);
                //}
                //return View();
            }
            catch (Exception)
            {
                return null;
            }
            return this.View(modelUser);
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            try
            {
                if (Url.IsLocalUrl(returnUrl))
                {

                    if (returnUrl == "/Admin/Index")
                    {


                        return RedirectToAction("Index", "Admin");
                    }
                    if (returnUrl == "/Home/Index")
                    {
                        return RedirectToAction("Index", "Home");

                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return this.RedirectToAction("LogOut", "Home");
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult LogOut()
        {
            var ctx = Request.GetOwinContext();
            var authenticationManger = ctx.Authentication;
            authenticationManger.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return RedirectToAction("PageLogin", "Home");
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult PageLogin1(string returnUrl)
        {
            // Xử lý logic xác thực người dùng
            string username = Request.Params["Email"];
            string password = Request.Params["Pass"];
            try
            {
                dbSem3Entities product = new dbSem3Entities();
                //if (ModelState.IsValid)
                if (username != null && password != null)
                {
                    var loginInfo = product.Members.Where(x => x.email == username && x.password == password)
                        .ToList();

                    var loginInfoAdmin = product.Admins.Where(y => y.email == username && y.password == password)
                        .ToList();
                    if (loginInfo != null && loginInfo.Count() > 0)
                    {
                        var logindetails = loginInfo.First();
                        string role1 = "USER";
                        HttpContext.Session["AccountName"] = logindetails.email;
                        HttpContext.Session["ImageAccount"] = logindetails.images;
                        HttpContext.Session["IdAccountUser"] = logindetails.id;
                        this.SignInUser(logindetails.email, false, role1);

                        returnUrl = "/Home/Index";
                        return this.RedirectToLocal(returnUrl);
                    }
                    else if (loginInfoAdmin != null && loginInfoAdmin.Count() > 0)
                    {

                        var logindetalsAdmin = loginInfoAdmin.First();
                        //string role = logindetalsAdmin.role;
                        string role1 = "ADMIN";
                        HttpContext.Session["AccountNameAdmin"] = logindetalsAdmin.email;
                        this.SignInUser(logindetalsAdmin.email, false, role1);
                        returnUrl = "/Admin/Index";
                        return this.RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invaild Email or Password");
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
            return RedirectToAction("Index", "Home");
        }
        private void SignInUser(string email, bool isPeristent, string roles)
        {
            var claims = new List<Claim>();
            try
            {
                claims.Add(new Claim(ClaimTypes.Name, email));
                claims.Add(new Claim(ClaimTypes.Role, roles));
                var ClaimIndenties = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

                var ctx = Request.GetOwinContext();
                var authenticationManger = ctx.Authentication;
                authenticationManger.SignIn(new AuthenticationProperties() { IsPersistent = isPeristent }, ClaimIndenties);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private void ClaimIdentities(string email, bool isPersistent)
        {
            var Claims = new List<Claim>();
            try
            {
                Claims.Add(new Claim(ClaimTypes.Name, email));
                var claimIdenties = new ClaimsIdentity(Claims, DefaultAuthenticationTypes.ApplicationCookie);
            }
            catch (Exception e)
            {

            }
        }
    }
}