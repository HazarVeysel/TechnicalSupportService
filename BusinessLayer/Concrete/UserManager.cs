using BusinessLayer.Abstract;

using System.Collections.Generic;
using DataAccessLayer.Models;
using System.Linq;
using System.Data.Entity;

namespace BusinessLayer.Concrete
{
    public class UserManager : IUserService
    {
        //IUserDal _userDal;

        //public UserManager(IUserDal userDal)
        //{
        //    _userDal = userDal;
        //}

        public Tbl_Users GetById(int id)
        {
            Tbl_Users userdata = null;
            using (bati_serverEntities db = new bati_serverEntities())
            {
                userdata= db.Tbl_Users.Where(x => x.id == id).SingleOrDefault();
                
            }
            return userdata;
            

        }

        public List<UsersData> GetUsers()
        {
            List<UsersData> userData1 = new List<UsersData>();
   
            using (bati_serverEntities db = new bati_serverEntities())
            {
                IQueryable<UsersData> userData = db.Tbl_Users.Where(x => (x.Status ?? 0) == 0).Select(x => new UsersData
                {
                   Id= x.id,
                    NameSurname=  x.Name_Surname,
                    PersonelTur= x.PersonelTur,
                    Bolum=x.Bolum,
                    UserName=x.username,
                    Password= x.password,
                    Email= x.Email,
                    TelefonNo= x.TelefonNo,
                    Status= x.Status
                });

                userData1= userData.ToList();
               
            }

            return userData1;

        }

        public void UserAdd(Tbl_Users user)
        {
            using (bati_serverEntities db = new bati_serverEntities())
            {
                //_userDal.Insert(user);
                var addeduser = db.Entry(user);
                addeduser.State = EntityState.Added;
                db.SaveChanges();
            }
        }

        public void UserDelete(Tbl_Users user)
        {
            using (bati_serverEntities db = new bati_serverEntities())
            {
                var deleteduser = db.Entry(user);

                user.Status = 1;
                db.Entry(deleteduser).State = EntityState.Modified;
                db.SaveChanges();
                //_userDal.Update(user);
            }
        }
        public void UserActive(Tbl_Users user)
        {
            using (bati_serverEntities db = new bati_serverEntities())
            {
                var activeuser = db.Entry(user);
                user.Status = 0;
                db.Entry(activeuser).State = EntityState.Modified;
                db.SaveChanges();
                //_userDal.Update(user);
            }
        }
        public void UserUpdate(Tbl_Users user)
        {
            using (bati_serverEntities db = new bati_serverEntities())
            {
                var updateduser = db.Entry(user);
                user.Status = 0;
                updateduser.State = EntityState.Modified;
                db.SaveChanges();
                //_userDal.Update(user);
            }
        }

        public class UsersData
        {

            public int Id { get; set; }
            public string NameSurname { get; set; }
            public int? PersonelTur { get; set; }
            public string Bolum { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
            public string TelefonNo { get; set; }
            public int? Status { get; set; }

        }
    }
}
