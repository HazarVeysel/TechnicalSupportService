using System.Collections.Generic;
using DataAccessLayer.Models;

namespace TechnicalSupportService.Models
{
    public class categoryListViewModel
    {
        public List<Categories> CategoryList { get; set; }
        public Categories Category { get; set; }
    }
}