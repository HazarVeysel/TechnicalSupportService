using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalSupportService.Controllers
{
    //This class doesn't in db
    public class ReqMesClass
    {
        public int Message_Id { get; set; }
        public string Message_Content { get; set; }
        public string Message_Image1 { get; set; }
        public string Message_Image2 { get; set; }
        public string Message_Image3 { get; set; }
        public string Message_Image4 { get; set; }
        public string Message_Image5 { get; set; }
        public string User_Name { get; set; }
        public string Department_Name { get; set; }
        public string Responsible_Name { get; set; }
        public string Responsible_Image { get; set; }
        public int? Responsible_Id { get; set; }
        public string Request_Priority_Name { get; set; }
        public bool Message_Status { get; set; }
        public DateTime? Sent_Date { get; set; }
        public DateTime? Receiving_Date { get; set; }

        public int? User_Id { get; set; }
        public int? Message_User_Id { get; set; }

        public int Request_Id { get; set; }
        public int? Department_Id { get; set; }

        public string Request_Subject { get; set; }

        public int Request_Priority_Id { get; set; }
        public int? RequestCategory_Id { get; set; }
        public string RequestCategory_Name { get; set; }

        public bool? Request_Status { get; set; }
        public bool? Request_Undertaken { get; set; }
        public DateTime? Request_Undertaken_Date { get; set; }
        public bool IsActive { get; set; }
        public DateTime Create_Date { get; set; }
        public DateTime? End_Date { get; set; }
        public int? Rate { get; set; }
        public string Comment { get; set; }
        public DateTime? RateDate { get; set; }

    }
}
