﻿using Person_Phone.UserService;
using PersonDB_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonDB_project.Controllers
{
    public class UserController : Controller
    {
        ServiceUser service = new ServiceUser();

        public UserController(IRepository<User> repository)
        {
                
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            Person_Phone.UserService.UserViewModel user = service.GetById(id);
            return View(user);
        }
        public ActionResult MultiIndex()
        {
            return View(service.GetAll());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // GET: User/Create
        [HttpPost]
        public ActionResult Create(Person_Phone.UserService.UserViewModel user, FormCollection collection)
        {
            for (int i = 4; i < collection.Keys.Count; i += 2)
            {
                if (collection.GetValue(collection.Keys[i]).AttemptedValue != "" || collection.GetValue(collection.Keys[i + 1]).AttemptedValue != "")
                {
                    user.Phones.Add(new Person_Phone.UserService.PhoneViewModel(collection.GetValue(collection.Keys[i]).AttemptedValue, collection.GetValue(collection.Keys[i + 1]).AttemptedValue, user.Id));
                }
            }
            service.Create(user);
            service.Save();
            return RedirectToAction("MultiIndex");
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            Person_Phone.UserService.UserViewModel user = service.GetById(id);
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(Person_Phone.UserService.UserViewModel user)
        {
            service.Update(user);
            service.Save();

            return RedirectToAction("MultiIndex");
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            Person_Phone.UserService.UserViewModel user = service.GetById(id);

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                service.Delete(id);
                service.Save();
                return RedirectToAction("MultiIndex");
            }
            catch
            {
                return View();
            }
        }
    }
}
