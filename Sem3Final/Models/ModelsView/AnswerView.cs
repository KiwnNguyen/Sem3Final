using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccpSem3.Models.ModeView
{
    public class AnswerView
    {
        public int id { get; set; }
        public string title { get; set; }
        public Nullable<int> id_question { get; set; }
        public bool Is_correct1 { get; set; }
        public Nullable<int> is_correct { get; set; }
        public Nullable<System.DateTime> created_at { get; set; }
        public Nullable<System.DateTime> updated_at { get; set; }
    }
}