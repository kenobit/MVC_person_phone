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
            //  return Content(res.ToString());
            return res;
        }
        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            UserViewModel user = service.GetById(id);
            // return View(user);
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
            //return View();
            return PartialView();
        }

        // GET: User/Create
        [HttpPost]
        public ActionResult Create(UserViewModel user)
        {
            //for (int i = 4; i < collection.Keys.Count-1; i += 2)
            //{
            //    if (collection.GetValue(collection.Keys[i]).AttemptedValue != "" || collection.GetValue(collection.Keys[i + 1]).AttemptedValue != "")
            //    {
            //        user.Phones.Add(new PhoneViewModel(collection.GetValue(collection.Keys[i]).AttemptedValue, collection.GetValue(collection.Keys[i + 1]).AttemptedValue, user.Id));
            //    }
            //}
            service.Create(user);
            //service.Save();
            return RedirectToAction("MultiIndex");

        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            UserViewModel user = service.GetById(id);
            // return View(user);
            return PartialView(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(UserViewModel user)
        {
            service.Update(user);
            service.Save();

            return RedirectToAction("MultiIndex");
        }

        // GET: User/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
           // UserViewModel user = service.GetById(id);
            service.Delete(id);
            //        service.Save();
            //return View(user);
            return RedirectToAction("MultiIndex");
        }

        //// POST: User/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id,FormCollection form)
        //{
        //    try
        //    {
        //        service.Delete(id);
        //        service.Save();
        //        return RedirectToAction("MultiIndex");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
