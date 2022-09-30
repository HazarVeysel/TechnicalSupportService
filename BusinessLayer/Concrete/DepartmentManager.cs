using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
    public class DepartmentManager : IDepartmentService
    {
        // GenericRepository<Department> repo = new GenericRepository<Department>();

        //public List<Department> GetAll()
        //{
        //    return repo.List();
        //}
        //public void DepartmentAddBL(Department p)
        //{
        //    if (p.Department_Name == "" || p.Department_Name.Length <= 4 || p.Department_Description == "" || p.Department_Name.Length >= 50)
        //    {
        //        //error message
        //    }
        //    else
        //    {
        //        repo.Insert(p);
        //    }
        //}

        IDepartmentDal _departmentDal;

        public DepartmentManager(IDepartmentDal departmentDal)
        {
            _departmentDal = departmentDal;
        }

        public void DepartmentAdd(Department department)
        {
            _departmentDal.Insert(department);
                }

        public void DepartmentDelete(Department department)
        {
            department.Is_Active = false;
            _departmentDal.Update(department);
        }
        public void DepartmentActivate(Department department)
        {
            department.Is_Active = true;
            _departmentDal.Update(department);
        }

        public void DepartmentUpdate(Department department)
        {
            _departmentDal.Update(department);
        }

        public Department GetById(int id)
        {
            return _departmentDal.Get(x => x.Department_Id == id);
        }

        public List<Department> GetList()
        {
            return _departmentDal.List();
        }

        
    }
}
