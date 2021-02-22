using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todolistapi.Models
{
    public class taskManager
    {
        public Guid id { get; set; }
        public string TaskStatus { get; set; }
        public string Description { get; set; }
    }
}
