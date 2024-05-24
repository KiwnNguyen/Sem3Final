using Sem3Final.Models.Entities;
using Sem3Final.Models.ModelsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sem3Final.Models.Repositories
{
    public class QuestionOfExaminationRepositories
    {
        private static QuestionOfExaminationRepositories instance = null;
        private QuestionOfExaminationRepositories() { }
        public static QuestionOfExaminationRepositories Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new QuestionOfExaminationRepositories();
                }
                return instance;
            }
        }
        public List<QuestionOfExamineView> GetById(int? id_examine)
        {
            try
            {

                dbSem3Entities entities = new dbSem3Entities();
                var q = entities.QuestionOfExaminations.Where(y => y.id_examination == id_examine).ToList();

                var QuestionofExamineViews = q.Select(questofexam => new QuestionOfExamineView
                {
                    id = questofexam.id,
                    id_question = questofexam.id_question,
                    id_examination = questofexam.id_examination,
                    created_at = questofexam.created_at,
                    updated_at = questofexam.updated_at
                }).ToList();
                return QuestionofExamineViews;
            }
            catch (Exception e)
            {
                throw e;
            }
            return null;
        }
    }
}