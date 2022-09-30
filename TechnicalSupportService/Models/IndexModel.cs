using System.Collections.Generic;
using DataAccessLayer.Models;
namespace TechnicalSupportService.Models
{
    public class IndexModel
    {
        public List<Categories> CategoryList { get; set; }
        public Categories Category { get; set; }

        public List<Questions> QuestionList { get; set; }
        public Questions Question { get; set; }
    }
}