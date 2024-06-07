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
    
    public class TasksController : Controller
    {
        private BazaDbContext db = new BazaDbContext();

        // GET: Tasks
        public ActionResult Index()
        {
            var tasks = db.Tasks.Include(t => t.TodoList);
            return View(tasks.ToList());
        }

        // GET: Tasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // GET: Tasks/Create5
        public ActionResult Create(int todoListId)
        {
            ViewBag.TodoListId = todoListId;
            return View();
        }

        // POST: Tasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,IsCompleted,DueDate,TodoListId")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Details", "TodoLists", new { id = task.TodoListId }); // Redirect to TodoList Details page after creating task
            }

            ViewBag.TodoListId = new SelectList(db.TodoLists, "Id", "Name", task.TodoListId);
            return View(task);
        }

        // GET: Tasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            ViewBag.TodoListId = new SelectList(db.TodoLists, "Id", "Name", task.TodoListId);
            return View(task);
        }

        // POST: Tasks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,IsCompleted,DueDate,TodoListId")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Entry(task).State = EntityState.Modified;
                task.TodoList = db.TodoLists.FirstOrDefault(t => t.Id == task.TodoListId);
                db.SaveChanges();
                return RedirectToAction("Details/" + task.TodoListId, "TodoLists");
            }
            ViewBag.TodoListId = new SelectList(db.TodoLists, "Id", "Name", task.TodoListId);
            return View(task);
        }

        // GET: Tasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            int todoListId = task.TodoListId; // Store the TodoListId before deleting the task
            db.Tasks.Remove(task);
            db.SaveChanges();

            // Redirect back to TodoList details page
            return RedirectToAction("Details", "TodoLists", new { id = todoListId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public ActionResult ToggleCompletion(int id)
        {
            var task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }

            // Toggle the completion status
            task.IsCompleted = !task.IsCompleted;
            db.Entry(task).State = EntityState.Modified;
            db.SaveChanges();

            // Redirect back to the todo list details page
            return RedirectToAction("Details", "TodoLists", new { id = task.TodoListId });
        }
        public ActionResult SortByDate(int todoListId)
        {
            var tasks = db.Tasks.Where(t => t.TodoListId == todoListId).OrderBy(t => t.DueDate).ToList();
            var todoList = db.TodoLists.Find(todoListId);
            todoList.Tasks = tasks;
            return View("~/Views/TodoLists/Details.cshtml", todoList);
        }

        public ActionResult SortByPriority(int todoListId)
        {
            var tasks = db.Tasks.Where(t => t.TodoListId == todoListId)
                                .OrderBy(t => t.DueDate.HasValue ? 0 : 1)
                                .ThenBy(t => t.DueDate)
                                .ToList();
            var todoList = db.TodoLists.Find(todoListId);
            todoList.Tasks = tasks;
            return View("~/Views/TodoLists/Details.cshtml", todoList);
        }

        public ActionResult SortByCompletion(int todoListId)
        {
            var tasks = db.Tasks.Where(t => t.TodoListId == todoListId)
                                .OrderBy(t => t.IsCompleted)
                                .ToList();
            var todoList = db.TodoLists.Find(todoListId);
            todoList.Tasks = tasks;
            return View("~/Views/TodoLists/Details.cshtml", todoList);
        }


    }
}