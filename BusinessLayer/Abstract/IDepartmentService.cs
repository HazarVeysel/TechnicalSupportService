using DataAccessLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface IDepartmentService
    {
        List<Department> GetList();
        void DepartmentAdd(Department department);
        Department GetById(int id);
        void DepartmentDelete(Department department);
        void DepartmentActivate(Department department);
        void DepartmentUpdate(Department department);
         


    }
}
