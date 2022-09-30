using BusinessLayer.Abstract;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Models;
using System.Data.Entity;

namespace BusinessLayer.Concrete
{
    public class FAQManager:IFAQService
    {
        //public void SaveCategory(Categories category)
        //{
        //    using (FAQ_DBEntities1 db = new FAQ_DBEntities1())
        //    {
        //        if (category.Category_ID==0)
        //        {
        //            db.Categories.Add(category);
        //            db.SaveChanges();
        //        }
        //        else
        //        {
        //            var updatedCategory = db.Categories.SingleOrDefault(x => x.Category_ID == category.Category_ID);
        //            updatedCategory.Category_Description = category.Category_Description;
        //            db.SaveChanges();
        //        }
        //    }
        //}
        public void DeleteCategory(int id)
        {
            using (helpdeskEntities db = new helpdeskEntities())
            {

                var deletedCategory = db.Categories.SingleOrDefault(x => x.Category_ID == id);
                deletedCategory.IsActive = false;
                db.Entry(deletedCategory).State = EntityState.Modified;
                db.SaveChanges();
            }

        }
        public void UpdateCategory(Categories category)
        {
            using (helpdeskEntities db = new helpdeskEntities())
            {
                //context üretmedik çünkü contextimiz burda db
                var updatedCategory = db.Entry(category);
                category.IsActive = true;
                updatedCategory.State = EntityState.Modified;
                db.SaveChanges();

            }
        }


        public bool UpdateCategorywithID(int ID)
        {
            using (helpdeskEntities db = new helpdeskEntities())
            {

                var guncellenecek = db.Categories.Where(x => x.Category_ID == ID).SingleOrDefault();

                if (guncellenecek.IsActive == true)
                {
                    guncellenecek.IsActive = false;
                    db.SaveChanges();
                    return false;
                }
                else
                {
                    guncellenecek.IsActive = true;
                    db.SaveChanges();
                    return true;
                }

            }
        }

        public void AddCategory(Categories category)
        {
            using (helpdeskEntities db = new helpdeskEntities())
            {
                var AddedCategory = db.Entry(category);
                AddedCategory.State = EntityState.Added;
                db.SaveChanges();
            }
        }

        public Categories GetCategory(int id)
        {
            Categories selectedCategory = null;

            using (helpdeskEntities db = new helpdeskEntities())
            {
                 selectedCategory = db.Categories.SingleOrDefault(x => x.Category_ID == id);
               
            }
            return selectedCategory;
        }

       
        public void AddQuestion(Questions question)
        {
            using (helpdeskEntities db = new helpdeskEntities())
            {

                var AddedQuestion = db.Entry(question);
                AddedQuestion.State = EntityState.Added;
                db.SaveChanges();
            }
        }

        public void UpdateQuestion(Questions question)
        {
            using (helpdeskEntities db = new helpdeskEntities())
            {
                //context üretmedik çünkü contextimiz burda db
                var updatedQuestion = db.Entry(question);
                question.IsActive = true;
                updatedQuestion.State = EntityState.Modified;
                db.SaveChanges();

            }
        }

        public bool UpdateQuestionWithID(int ID)
        {
            using (helpdeskEntities db = new helpdeskEntities())
            {

                var guncellenecek = db.Questions.Where(x => x.Question_ID == ID).SingleOrDefault();

                if (guncellenecek.IsActive == true)
                {
                    guncellenecek.IsActive = false;
                    db.SaveChanges();
                    return false;
                }
                else
                {
                    guncellenecek.IsActive = true;
                    db.SaveChanges();
                    return true;
                }

            }
        }
        public void DeleteQuestion(int id)
        {
            using (helpdeskEntities db = new helpdeskEntities())
            {

                var passiveQuestion = db.Questions.SingleOrDefault(x => x.Question_ID == id);
                passiveQuestion.IsActive = false;
                db.Entry(passiveQuestion).State = EntityState.Modified;

                db.SaveChanges();
            }
        }

        public Questions GetQuestion(int id)
        {
            Questions selectedQuestion = null;
            using (helpdeskEntities db = new helpdeskEntities())
            {
                selectedQuestion = db.Questions.Include(c => c.Categories).SingleOrDefault(x => x.Question_ID == id);


            }

            return selectedQuestion;
        }

        public List<Questions> GetCategoryQuestions(int id)
        {
            using (helpdeskEntities db = new helpdeskEntities())
            {

                return db.Questions.Where(x => x.Category_ID == id).ToList();


            }
        }
        public List<Categories> ListAllCategory()
        {
            using (helpdeskEntities db = new helpdeskEntities())
            {

                return db.Categories.ToList();

            }
        }
        public List<Categories> ListSubCategoriesByID(int id)
        {
            using (helpdeskEntities db = new helpdeskEntities())
            {

                return db.Categories.Where(x => x.Category_Parent_ID == id).ToList();

            }
        }
        public List<Questions> ListAllQuestion()
        {
            using (helpdeskEntities db = new helpdeskEntities())
            {

                return db.Questions.ToList();

            }
        }
        public List<Questions> ListQuestionsByCategoryId(int id)
        {
            using (helpdeskEntities db = new helpdeskEntities())
            {

                return db.Questions.Where(x => x.Category_ID == id).ToList();

            }
        }


        //public List<Questions, Categories> Search()
        //{
        //    using (FAQ_DBEntities db = new FAQ_DBEntities())
        //    {
        //        var values = from x in db.Questions
        //    }
        //}

        //public IEnumerable<CategoryType> CategoryList()
        //{
        //    List<CategoryType> secimler = new List<CategoryType>();
        //    using (FAQ_DBEntities db = new FAQ_DBEntities())
        //    {
        //        secimler.AddRange(db.Categories.Select(x => new CategoryType() { CategoryID = x.Category_ID, CategoryName = x.Category_Name }).ToList());
        //    }
        //    return secimler;



        //}




        //public class CategoryType
        //{
        //    public int CategoryID { get; set; }
        //    public string CategoryName { get; set; }
        //}
    }
}

