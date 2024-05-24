using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sem3Final.Models.ModelsView
{
    public class QuestionView
    {
        public int id { get; set; }
        public string title { get; set; }
        public Nullable<int> id_categoryofquestion { get; set; }
        public Nullable<int> status { get; set; }
        public Nullable<System.DateTime> created_at { get; set; }
        public Nullable<System.DateTime> updated_at { get; set; }

    }
}