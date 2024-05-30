using Sem3Final.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sem3Final.Models.ModelsView.ModelJoin
{
    public class QuesAndExam
    {
        public Examination examination { get; set; }
        public QuestionOfExamination question_exam { get; set; }
        public Question question { get; set; }
    }
}