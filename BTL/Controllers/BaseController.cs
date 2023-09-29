using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BTL.Controllers
{
    public class BaseController : Controller
    {
        public string CurrenUser
        {
            get
            {
                // đọc từ session
                return HttpContext.Session.GetString("USER_NAME");
            }
            set
            {
                //gán dữ liệu cho Session
                HttpContext.Session.SetString("USER_NAME", value);
            }
        }

        public bool Islogin
        {
            get
            {
                return !string.IsNullOrEmpty(CurrenUser);
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            ViewBag.UserName = CurrenUser;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
