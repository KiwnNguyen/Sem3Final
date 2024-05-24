using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sem3Final.Models.ModelsView
{
    public class MemberView
    {
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string fullname { get; set; }
        public Nullable<int> status { get; set; }
        public string images { get; set; }
        public string education_details { get; set; }
        public string personal_detail { get; set; }
        public string phone { get; set; }
        public string cv { get; set; }
        public string work_experience { get; set; }
        public Nullable<System.DateTime> created_at { get; set; }
        public Nullable<System.DateTime> updated_at { get; set; }
    }
}