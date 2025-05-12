using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace To_do_list_Core.Entities
{
    public class ListItem
    {
        public int listItemId { get; set; }

        public int listId { get; set; } // FK from List entities

        public string title { get; set; }

        public string? description { get; set; }

        public bool? isActive { get; set; } = false;

        public DateTime createdDate { get; set; } = DateTime.Now;

        public DateTime? dueDate { get; set; }
    }
}
