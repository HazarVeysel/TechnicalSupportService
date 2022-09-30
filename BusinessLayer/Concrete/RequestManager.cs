using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
    public class RequestManager : IRequestService
    {
        IRequestDal _requestDal;
        UserManager um = new UserManager();

        public RequestManager(IRequestDal requestDal)
        {
            _requestDal = requestDal;
        }

        public Request GetById(int id)
        {
            return _requestDal.Get(x => x.Request_Id == id);
        }

        public List<Request> GetRequestsByUserId(int id)
        {
            return _requestDal.List(x => um.GetById(id).id == id);
        }
        public List<Request> GetRequest()
        {
            return _requestDal.List();
        }


        public void RequestAdd(Request Request)
        {
            _requestDal.Insert(Request);
        }

        public void RequestDelete(Request Request)
        {
            _requestDal.Delete(Request);
        }

        public void RequestUpdate(Request Request)
        {
            _requestDal.Update(Request);
        }

        //List<Request> IRequestService.GetRequestsByUserId(int id)
        //{
        //    throw new NotImplementedException();
        //}
        //public void RequestSolved(Request Request)
        //{

        //    _requestDal.Update(Request);
        //}
        //public void RequestNotResolved(Request Request)
        //{

        //    _requestDal.Update(Request);
        //}
        //public void RequestTake(Request Request)
        //{

        //    //Request.IsActive = false;
        //    _requestDal.Update(Request);
        //}

    }
}
