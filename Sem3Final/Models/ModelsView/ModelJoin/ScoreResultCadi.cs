using Sem3Final.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sem3Final.Models.ModelsView.ModelJoin
{
    public class ScoreResultCadi
    {
        public Member member { get; set; }
        public Cadidate cadidate { get; set; }
        public Vacancy vacancy { get; set; }
        public Examination examination { get; set; }
        public QuestionOfExamination ofExamination { get; set; }
        public Question question { get; set; }
        public Answer answer { get; set; }
    }
}