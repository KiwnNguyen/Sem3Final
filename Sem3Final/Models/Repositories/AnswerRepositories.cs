using AccpSem3.Models.Entities;
using AccpSem3.Models.ModeView;
using AccpSem3.Models.ModeView.ModelJoin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccpSem3.Models.Repository
{
    public class AnswerRepositories
    {
        private static AnswerRepositories instance = null;
        private AnswerRepositories() { }    
        public static AnswerRepositories Instance {  get {

                if (instance == null)
                {
                    instance = new AnswerRepositories();
                }
                return instance;
            } 
        }

        public List<AnswerView> GetAll()
        {
            try
            {
                dbSem3Entities entities = new dbSem3Entities();
                var q = entities.Answers.Select(d => new AnswerView {id = d.id , title = d.title ,id_question = d.id_question,is_correct = d.is_correct,created_at = d.created_at , updated_at = d.updated_at} ).ToList();
                return q;
            }
            catch (Exception e)
            {
                throw e;
            }
            return null;
        
        }
        public IEnumerable<QuestionJoin> GetListAnswerQuestionCategory()
        {
            try
            {
                dbSem3Entities entities = new dbSem3Entities();

                //var q = entities.Answers.Select(d => new AnswerView { id = d.id, title = d.title, id_question = d.id_question, is_correct = d.is_correct, created_at = d.created_at, updated_at = d.updated_at }).ToList();
                var q = from a in entities.Examinations
                        join b in entities.QuestionOfExaminations on a.id equals b.id_examination
                        join c in entities.Questions on b.id_question equals c.id
                        join d in entities.CategoryOfQuestions on c.id_categoryofquestion equals d.id
                        join e in entities.Answers on c.id equals e.id_question
                        select new QuestionJoin
                        {
                            examination = a,
                            QuestionOfExamine = b,
                            question = c,
                            category = d,
                            answer = e
                        };
                return q.ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
            return null;

        }
        public IEnumerable<ScoreResultCadi> GetResultCadi(string account)
        {
            try{
                dbSem3Entities entities = new dbSem3Entities();
                var q = from a in entities.Cadidates
                        join b in entities.Vacancies on a.id_vacancy equals b.id
                        join c in entities.Examinations on b.id_examination equals c.id
                        join d in entities.QuestionOfExaminations on c.id equals d.id_examination
                        join e in entities.Questions on d.id_question equals e.id
                        join f in entities.Answers on e.id equals f.id_question
                        where a.username == account && f.is_correct == 1
                        select new ScoreResultCadi
                        {
                            cadidate = a,
                            vacancy = b,
                            examination = c,
                            ofExamination = d,
                            question = e,
                            answer = f,
                        };
                return q.ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
            return null;
        }

        public List<AnswerView> GetById(string title)
        {
            try
            {

                dbSem3Entities entities = new dbSem3Entities();
                var q = entities.Answers.Where(y => y.title == title).ToList();

                var answerViews = q.Select(Answers => new AnswerView
                {
                    id = Answers.id,
                    id_question = Answers.id_question,
                    is_correct = Answers.is_correct,
                    created_at = Answers.created_at,
                    updated_at = Answers.updated_at
                }).ToList();
                return answerViews;
            }
            catch (Exception e)
            {
                throw e;
            }
            return null;
        }
    }
}