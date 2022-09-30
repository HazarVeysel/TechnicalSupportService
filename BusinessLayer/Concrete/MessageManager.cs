using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
    public class MessageManager : IMessageService
    {
        IMessageDal _messageDal;

        public MessageManager(IMessageDal messageDal)
        {
            _messageDal = messageDal;
        }

        public Message GetById(int id)
        {
            return _messageDal.Get(x => x.Message_Id == id);
        }

        public List<Message> GetList()
        {
            return _messageDal.List();
        }

        public List<Message> GetListByRequestID(int id)
        {
            return _messageDal.List(x => x.Request_Id == id);
        }

        public List<Message> GetListByUser(int id)
        {
            return _messageDal.List(x=>x.Request.User_Id==id && x.Request.IsActive == true);
        }

        public void MessageAdd(Message message)
        {
            _messageDal.Insert(message);
        }

        public void MessageDelete(Message message)
        {
            _messageDal.Delete(message);
        }

        public void MessageUpdate(Message message)
        {
            _messageDal.Update(message);
        }
    }
}
