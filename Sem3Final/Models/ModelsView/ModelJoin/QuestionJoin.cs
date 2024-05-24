using Sem3Final.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sem3Final.Models.ModelsView.ModelJoin
{
    public class QuestionJoin
    {
        public QuestionOfExamination QuestionOfExamine { get; set; }

        public Question question { get; set; }

        public Answer answer { get; set; }
        public Examination examination { get; set; }
        public CategoryOfQuestion category { get; set; }
    }
}