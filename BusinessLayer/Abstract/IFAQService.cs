using System.Collections.Generic;
using DataAccessLayer.Models;
namespace BusinessLayer.Abstract
{
    public interface IFAQService
    {

        void AddCategory(Categories category);
        void UpdateCategory(Categories category);
        void DeleteCategory(int id);
        Categories GetCategory(int id);


        void AddQuestion(Questions question);
        void UpdateQuestion(Questions question);
        void DeleteQuestion(int id);
        Questions GetQuestion(int id);

        List<Questions> GetCategoryQuestions(int id);
        List<Categories> ListAllCategory();
        List<Questions> ListAllQuestion();
    }
}
