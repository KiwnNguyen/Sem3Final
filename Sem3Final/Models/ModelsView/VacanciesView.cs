using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sem3Final.Models.ModelsView
{
    public class VacanciesView
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public Nullable<int> quantity_emp { get; set; }
        public Nullable<int> id_examination { get; set; }
        public Nullable<int> id_dep { get; set; }
        public string status { get; set; }
        public Nullable<System.DateTime> created_at { get; set; }
        public Nullable<System.DateTime> updated_at { get; set; }
        public string salary { get; set; }
        public Nullable<System.DateTime> dateline { get; set; }
        public string jobnature { get; set; }
        public Nullable<int> featured { get; set; }
    }
}