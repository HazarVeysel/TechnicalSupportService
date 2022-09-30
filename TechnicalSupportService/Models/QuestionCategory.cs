﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechnicalSupportService.Models
{
    public class QuestionCategory
    {
        public int Question_ID { get; set; }
        public string Question_Title { get; set; }
        public string Question_Category_Name { get; set; }
        public string Question_Category_Img { get; set; }
        public string Question_Details { get; set; }
        public string Answer_Content { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<DateTime> Create_Date { get; set; }
        public Nullable<int> Category_ID { get; set; }
        public Nullable<int> Reading_Count { get; set; }
    }
}