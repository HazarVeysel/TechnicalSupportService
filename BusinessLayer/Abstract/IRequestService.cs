using DataAccessLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface IRequestService
    {
        List<Request> GetRequest();
        List<Request> GetRequestsByUserId(int id);
        void RequestAdd(Request Request);
        void RequestDelete(Request Request);
        void RequestUpdate(Request Request);
        Request GetById(int id);
    }
}
