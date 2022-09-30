using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
    public class PriorityManager : IPriorityService
    {
        IPriorityDal _prioritydal;

        public PriorityManager(IPriorityDal prioritydal)
        {
            _prioritydal = prioritydal;
        }

        public List<Priority> GetPriority()
        {
           return _prioritydal.List();
        }
    }
}
