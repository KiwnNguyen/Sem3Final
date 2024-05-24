using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sem3Final.Models.ModelsView.ModelJoin
{
    public class QuestionJoinView
    {
        public QuestionOfExamineView QuestionOfExamineView { get; set; }

        public QuestionView questionView { get; set; }
    }
}