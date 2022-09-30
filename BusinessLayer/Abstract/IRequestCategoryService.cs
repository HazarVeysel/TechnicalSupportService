using DataAccessLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface IRequestCategoryService
    {
        List<RequestCategory> GetRequestCategory();
        void RequestCategoryAdd(RequestCategory requestCategory);
        void RequestCategoryDelete(RequestCategory requestCategory);
        void RequestCategoryActivate(RequestCategory requestCategory);
        void RequestCategoryUpdate(RequestCategory requestCategory);
        RequestCategory GetById(int id);
    }
}
