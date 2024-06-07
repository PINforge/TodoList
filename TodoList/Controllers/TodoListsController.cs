using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TodoList.Misc;
using TodoList.Models;

namespace TodoList.Controllers
{
    [Authorize(Roles = OvlastiKorisnik.Administrator + ", " + OvlastiKorisnik.Moderator)]
    public class TodoListsController : Controller
    {
        private BazaDbContext db = new BazaDbContext();

        // GET: TodoLists/Index
        [AllowAnonymous]
        public ActionResult Index()
        {
            if ((User as LogiraniKorisnik) == null)
                return RedirectToAction("Prijava", "Korisnici");

            if (!(User as LogiraniKorisnik).IsInRole(OvlastiKorisnik.Administrator))
                return View(db.TodoLists.ToList().Where(t => t.korisnickoIme == (User as LogiraniKorisnik).KorisnickoIme));
            else
                return View(db.TodoLists.ToList());
        }

        // GET: TodoLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TodoList.Models.TodoList todoList = db.TodoLists.Find(id);
            if (todoList == null)
            {
                return HttpNotFound();
            }
            return View(todoList);
        }

        // GET: TodoLists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TodoLists/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] TodoList.Models.TodoList todoList)
        {
            if(todoList.Name == null)
            {
                return View(todoList);

            }

            if (ModelState.IsValid)
            {
                todoList.korisnickoIme = (User as LogiraniKorisnik).KorisnickoIme;
                db.TodoLists.Add(todoList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(todoList);
        }

        // GET: TodoLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TodoList.Models.TodoList todoList = db.TodoLists.Find(id);
            if (todoList == null)
            {
                return HttpNotFound();
            }
            return View(todoList);
        }

        // POST: TodoLists/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] TodoList.Models.TodoList todoList)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the existing TodoList from the database
                var existingTodoList = db.TodoLists.Find(todoList.Id);
                if (existingTodoList == null)
                {
                    return HttpNotFound();
                }

                // Update the properties of the existing TodoList
                existingTodoList.Name = todoList.Name;

                // Mark the TodoList as modified and save changes
                db.Entry(existingTodoList).State = EntityState.Modified;
                db.SaveChanges();

                // Redirect to the details view of the edited TodoList
                return RedirectToAction("Details", new { id = existingTodoList.Id });
            }
            return View(todoList);
        }
        // GET: TodoLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TodoList.Models.TodoList todoList = db.TodoLists.Find(id);
            if (todoList == null)
            {
                return HttpNotFound();
            }
            return View(todoList);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TodoList.Models.TodoList todoList = db.TodoLists.Find(id);

            // Find all tasks associated with the TodoListId
            var tasksToDelete = db.Tasks.Where(t => t.TodoListId == id);

            // Remove all associated tasks
            foreach (var task in tasksToDelete)
            {
                db.Tasks.Remove(task);
            }

            // Remove the TodoList
            db.TodoLists.Remove(todoList);

            // Save changes to the database
            db.SaveChanges();

            return RedirectToAction("Index");
        }

    }

}
