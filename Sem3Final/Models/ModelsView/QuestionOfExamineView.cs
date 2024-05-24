using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sem3Final.Models.ModelsView
{
    public class QuestionOfExamineView
    {
        public int id { get; set; }
        public Nullable<int> id_question { get; set; }
        public Nullable<int> id_examination { get; set; }
        public Nullable<System.DateTime> created_at { get; set; }
        public Nullable<System.DateTime> updated_at { get; set; }
    }
}