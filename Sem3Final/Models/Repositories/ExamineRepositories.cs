using Sem3Final.Models.Entities;
using Sem3Final.Models.ModelsView;
using Sem3Final.Models.ModelsView.ModelJoin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sem3Final.Models.Repositories
{
    public class ExamineRepositories
    {
        private static ExamineRepositories _instance;

        private ExamineRepositories() { }

        public static ExamineRepositories Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ExamineRepositories();
                }
                return _instance;
            }
        }
        public List<ExamineView> GetAll()
        {
            try
            {
                dbSem3Entities entities = new dbSem3Entities();
                var q = entities.Examinations.Select(e => new ExamineView { id = e.id, title = e.title, created_at = e.created_at, updated_at = e.updated_at }).ToList();
                return q;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<QuesAndExam> GetAll_Ques_Ans(int? id)
        {
            try
            {
                int? id_test = id;
                if (id != null)
                {
                    dbSem3Entities entities = new dbSem3Entities();
                    var q = from a in entities.Examinations
                            join b in entities.QuestionOfExaminations on a.id equals b.id_examination
                            join c in entities.Questions on b.id_question equals c.id
                            //join d in entities.Answers on c.id equals d.id_question
                            where a.id == id
                            select new QuesAndExam
                            {
                                examination = a,
                                question_exam = b,
                                question = c,
                                //answer = d

                            };
                    return q.ToList();

                }


            }
            catch (Exception e)
            {
                throw e;
            }
            return null;
        }
        public List<ExamineView> GetByIdExa(int? id)
        {
            try
            {
                int? id_test = id;

                dbSem3Entities entities = new dbSem3Entities();
                var q = entities.Examinations.Where(y => y.id == id).ToList();
                var exaviews = q.Select(exa => new ExamineView
                {
                    id = exa.id,
                    title = exa.title,
                    created_at = exa.created_at,
                    updated_at = exa.updated_at
                }).ToList();
                return exaviews;
            }
            catch (Exception e)
            {

            }
            return null;
        }
    }
}