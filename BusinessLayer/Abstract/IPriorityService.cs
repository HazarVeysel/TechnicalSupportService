using DataAccessLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface IPriorityService
    {
        List<Priority> GetPriority();
    }
}
