using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sem3Final.Models.ModelsView
{
    public class UserView
    {
        public int id { get; set; }
        public string email { get; set; }
        public string pass_word { get; set; }
        public string fullname { get; set; }
        public Nullable<int> status { get; set; }
        public string images { get; set; }
        public Nullable<System.DateTime> created_at { get; set; }
        public Nullable<System.DateTime> updated_at { get; set; }
    }
}