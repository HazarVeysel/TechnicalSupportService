using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
    public class RequestCategoryManager : IRequestCategoryService
    {
        IRequestCategoryDal _requestCategorydal;

        public RequestCategoryManager(IRequestCategoryDal requestCategorydal)
        {
            _requestCategorydal = requestCategorydal;
        }
        public void RequestCategoryAdd(RequestCategory requestCategory)
        {
            _requestCategorydal.Insert(requestCategory);
        }

        public void RequestCategoryDelete(RequestCategory requestCategory)
        {
            requestCategory.IsActive = false;
            _requestCategorydal.Update(requestCategory);
        }
        public void RequestCategoryActivate(RequestCategory requestCategory)
        {
            requestCategory.IsActive = true;
            _requestCategorydal.Update(requestCategory);
        }
        public void RequestCategoryUpdate(RequestCategory requestCategory)
        {
            _requestCategorydal.Update(requestCategory);
        }

        public List<RequestCategory> GetRequestCategory()
        {
           return _requestCategorydal.List();
        }

        public RequestCategory GetById(int id)
        {
            return _requestCategorydal.Get(x => x.RequestCategory_Id == id);
        }
    }
}
