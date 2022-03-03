using K_O_Project.Models;
using K_O_Project.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace K_O_Project.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public LoginController(AppDbContext context)
        {
            _appDbContext = context;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Login model)
        {
            if (!ModelState.IsValid)
            {
               
                return View();
            }
            var user = _appDbContext.Logins.FirstOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            if (user != null)
            {
               
                HttpContext.Session.SetInt32("Id", user.Id);
                return Redirect("/Home/Index");
            }
            else
            {
              
                var userLogin = _appDbContext.Logins.FirstOrDefault(x => x.Username == model.Username);
                if (userLogin==null)
                {
                    ModelState.AddModelError("Username", "Kullanıcı bulunamadı");
                    
                }
                else
                {
                    ModelState.AddModelError("Password", "Hatalı Şifre");
                }
               
                return View("Login",model);
            }

        }
    }
}
