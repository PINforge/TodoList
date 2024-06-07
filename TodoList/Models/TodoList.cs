
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TodoList.Models
{
    [Table("TodoLists")]
    public class TodoList
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string korisnickoIme { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}

// Models/Task.cs
namespace TodoList.Models
{
    [Table("Tasks")]
    public class Task
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } // Add Title property
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? DueDate { get; set; }
        public int TodoListId { get; set; }
        public virtual TodoList TodoList { get; set; }
    }

}