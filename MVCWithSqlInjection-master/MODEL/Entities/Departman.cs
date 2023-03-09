using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Entities
{
    public class Departman
    {
        public int DepartmanID { get; set; }
        public string DepartmanName { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
