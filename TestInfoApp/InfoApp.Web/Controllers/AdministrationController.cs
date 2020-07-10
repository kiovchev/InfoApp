using Microsoft.AspNetCore.Mvc;

namespace InfoApp.Web.Controllers
{
    public class AdministrationController : Controller
    {
        public IActionResult All()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }

            return View();
        }
    }
}
