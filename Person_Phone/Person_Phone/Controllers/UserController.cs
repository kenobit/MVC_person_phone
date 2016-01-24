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
        IRepository<User> repository = new DBFactory().GetInstance("Shit");

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            User user = repository.GetById(id);

            return View(user);
        }
        public ActionResult MultiIndex()
        {
            return View(repository.GetAll());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // GET: User/Create
        [HttpPost]
        public ActionResult Create(User user, FormCollection collection)
        {
            for (int i = 4; i < collection.Keys.Count; i += 2)
            {
                if (collection.GetValue(collection.Keys[i]).AttemptedValue != "" || collection.GetValue(collection.Keys[i + 1]).AttemptedValue != "")
                {
                    user.Phones.Add(new Phone(collection.GetValue(collection.Keys[i]).AttemptedValue, collection.GetValue(collection.Keys[i + 1]).AttemptedValue, user.Id));
                }
            }
            repository.Create(user);
            repository.Save();
            return RedirectToAction("MultiIndex");
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            User user = repository.GetById(id);
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(User item,FormCollection collection)
        {
            try
            {
                repository.Update(item);
                repository.Save();

                return RedirectToAction("MultiIndex");
            }
            catch
            {
                return View(item);
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            User user = repository.GetById(id);

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                repository.Delete(id);
                repository.Save();
                return RedirectToAction("MultiIndex");
            }
            catch
            {
                return View();
            }
        }
    }
}
