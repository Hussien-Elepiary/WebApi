using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce_Demo_Core.Entities.EmployeeExample
{
    /// <summary>
    /// this is just an example this will not be migrated in the data base
    /// </summary>
    public class Employee:BaseEntity
    {
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public int? Age { get; set; }

        public int DepartmentId { get; set; }// Foreign Key
        public Department Department { get; set; } // nav prop [1]
    }
}
