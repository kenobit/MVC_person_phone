using Person_Phone.Models;
using Person_Phone.UserService;
using PersonDB_project.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonDB_project.Controllers
{
    public class UserController : Controller
    {
        IService service;

        public UserController()
        {
            service = new ServiceUser();
        }

        public UserController(IService repository)
        {
            service = repository;
        }

        [HttpPost]
        public double Calc(double a, double b, string operation)
        {
            double res = 0;
            switch (operation)
            {
                case "*":
                    res = a * b;
                    break;
                case "+":
                    res = a + b;
                    break;
                case "-":
                    res = a - b;
                    break;
                case "/":
                    res = a / b;
                    break;
            }
            return res;
        }
        public ActionResult Details(int id)
        {
            UserViewModel user = service.GetById(id);
            return PartialView(user);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MultiIndex()
        {
            return PartialView(service.GetAll());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(UserViewModel user)
        {
            service.Create(user);
            return RedirectToAction("MultiIndex");

        }

        public ActionResult Edit(int id)
        {
            UserViewModel user = service.GetById(id);
            return PartialView(user);
        }

        [HttpPost]
        public ActionResult Edit(UserViewModel user)
        {
            service.Update(user);
            service.Save();

            return RedirectToAction("MultiIndex");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            service.Delete(id);
            return RedirectToAction("MultiIndex");
        }
    }
}
