using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.Repositories;
using DataAccessLayer.Models;

namespace DataAccessLayer.EntityFramework
{
    public class EfMessageDal : GenericRepository<Message>, IMessageDal
    {
    }
}
