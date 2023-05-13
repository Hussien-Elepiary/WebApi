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
    public class Department:BaseEntity
    {
        public  string Name { get; set; }
        public DateOnly DateOfCreation { get; set; }
    }
}
