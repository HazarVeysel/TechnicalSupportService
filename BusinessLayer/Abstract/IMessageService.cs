using DataAccessLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface IMessageService
    {
        List<Message> GetList();
        List<Message> GetListByUser(int id);
        List<Message> GetListByRequestID(int id);
        void MessageAdd(Message message);
        Message GetById(int id);
        void MessageDelete(Message message);
        void MessageUpdate(Message message);


    }
}
