using ECommerce_Demo_Core.Entities.EmployeeExample;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce_Demo_Core.Specifications.EmployeeExample
{
    public class EmployeeWithDepartmentSpec:BaseSpecification<Employee>
    {
        public EmployeeWithDepartmentSpec()
        {
            Includes.Add(E => E.Department);
        }
        public EmployeeWithDepartmentSpec(int id) : base(E=>E.Id == id)
        {
            Includes.Add(E => E.Department);
        }
    }
}
