using Sem3Final.Models.Entities;
using Sem3Final.Models.ModelsView.ModelJoin;
using Sem3Final.Models.ModelsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sem3Final.Models.Repositories
{
    public class QuestionRepositories
    {
        private static QuestionRepositories instance = null;

        private QuestionRepositories() { }

        public static QuestionRepositories Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new QuestionRepositories();
                }
                return instance;
            }
        }
        public List<QuestionView> GetAll()
        {
            try
            {
                dbSem3Entities entities = new dbSem3Entities();
                var q = entities.Questions.Select(d => new QuestionView { id = d.id, title = d.title, id_categoryofquestion = d.id_categoryofquestion, status = d.status, created_at = d.created_at, updated_at = d.updated_at }).ToList();
                return q;
            }
            catch (Exception e)
            {
                throw e;
            }
            return null;
        }

        public IEnumerable<QuestionJoin> GetById(int? id_q)
        {
            try
            {
                //Phần I Kien thuc chung
                dbSem3Entities entities = new dbSem3Entities();
                var q = from a in entities.Examinations
                        join b in entities.QuestionOfExaminations on a.id equals b.id_examination
                        join c in entities.Questions on b.id_question equals c.id
                        join d in entities.CategoryOfQuestions on c.id_categoryofquestion equals d.id
                        join e in entities.Answers on c.id equals e.id_question
                        where a.id == id_q && d.id == 2
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
        public IEnumerable<QuestionJoin> GetById1(int? id_q)
        {
            try
            {
                //Phần II Toán học
                dbSem3Entities entities = new dbSem3Entities();
                var q = from a in entities.Examinations
                        join b in entities.QuestionOfExaminations on a.id equals b.id_examination
                        join c in entities.Questions on b.id_question equals c.id
                        join d in entities.CategoryOfQuestions on c.id_categoryofquestion equals d.id
                        join e in entities.Answers on c.id equals e.id_question
                        where a.id == id_q && d.id == 3
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
        public IEnumerable<QuestionJoin> GetById2(int? id_q)
        {
            try
            {
                //Phần III Khoa học máy tính
                dbSem3Entities entities = new dbSem3Entities();
                var q = from a in entities.Examinations
                        join b in entities.QuestionOfExaminations on a.id equals b.id_examination
                        join c in entities.Questions on b.id_question equals c.id
                        join d in entities.CategoryOfQuestions on c.id_categoryofquestion equals d.id
                        join e in entities.Answers on c.id equals e.id_question
                        where a.id == id_q && d.id == 4
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
    }
}