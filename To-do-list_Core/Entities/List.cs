using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace To_do_list_Core.Entities
{
    public class List
    {  
        public int listId { get; set; }

        public string title { get; set; }

        public string description { get; set; }

        public DateTime createdDate { get; set; } = DateTime.Now;

        public DateTime updatedDate { get; set; }

        public ICollection<ListItem>? items { get; set; }

    }
}
