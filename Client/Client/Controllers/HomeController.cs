using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Message"] = "Добро пожаловать в ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Calculate(string method, string btn, Calculator.CalcData cd)
        {
            Calculator.CalculatorClient cc = new Calculator.CalculatorClient();

            switch (method)
            {
                case "clck":
                    cd = cc.clck(cd, btn);
                    break;
                case "c":
                    cd = cc.c(cd);
                    break;
                case "ce":
                    cd = cc.ce(cd);
                    break;
                case "bckspc":
                    cd = cc.bckspc(cd);
                    break;
            }

            cc.Close();

            return View(cd);
        }
    }
}
