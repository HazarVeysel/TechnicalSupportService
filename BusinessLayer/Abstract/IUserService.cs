using DataAccessLayer.Models;
using System.Collections.Generic;
using System.Linq;
using static BusinessLayer.Concrete.UserManager;

namespace BusinessLayer.Abstract
{
    public interface IUserService
    {
        List<UsersData> GetUsers();
        void UserAdd(Tbl_Users user);
        void UserDelete(Tbl_Users user);
        void UserActive(Tbl_Users user);
        void UserUpdate(Tbl_Users user);
        Tbl_Users GetById(int id);

    }
}
