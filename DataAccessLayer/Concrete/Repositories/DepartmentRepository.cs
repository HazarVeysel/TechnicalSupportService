//Silinecek

using DataAccessLayer.Abstract;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccessLayer.Concrete.Repositories
{
    class DepartmentRepository : IDepartmentDal
    {

        helpdeskEntities c = new helpdeskEntities();

        DbSet<Department> _object;

        public void Delete(Department p)
        {
            _object.Remove(p);
            c.SaveChanges();
        }

        public Department Get(Expression<Func<Department, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void Insert(Department p)
        {
            _object.Add(p);
            c.SaveChanges();
        }

        public List<Department> List()
        {
            return _object.ToList();
        }

        public List<Department> List(Expression<Func<Department, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void Update(Department p)
        {
            c.SaveChanges();
        }
    }
}
