using Sem3Final.Models.Entities;
using Sem3Final.Models.ModelsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sem3Final.Models.Repositories
{
    public class QuestionRepository
    {
        private static QuestionRepository instance = null;
        private QuestionRepository() { }
        public static QuestionRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new QuestionRepository();
                }
                return instance;
            }
        }
        public ICollection<CategoryOfQuestion> GetCategoryOfQuestions()
        {
            try
            {
                dbSem3Entities en = new dbSem3Entities();
                var rs = en.CategoryOfQuestions.ToList();
                return rs;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public ICollection<QuestionView> GetQuestions()
        {
            try
            {
                dbSem3Entities en = new dbSem3Entities();
                List<QuestionView> rs = new List<QuestionView>();
                var lsq = en.Questions.OrderByDescending(q => q.updated_at).ToList();
                foreach (var q in lsq)
                {
                    QuestionView view = new QuestionView();
                    var tlcat = en.CategoryOfQuestions.Where(cat => cat.id == q.id_categoryofquestion).FirstOrDefault();
                    view.title = q.title;
                    view.id = q.id;
                    view.id_categoryofquestion = q.id_categoryofquestion;
                    view.updated_at = q.updated_at;
                    view.created_at = q.created_at;
                    view.status = q.status;
                    if (tlcat != null)
                    {
                        view.titleofcategory = tlcat.title;
                    }
                    rs.Add(view);
                }
                return rs;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int AddQuestion(Question question, Answer[] answers)
        {
            try
            {
                using (dbSem3Entities en = new dbSem3Entities())
                {
                    using (var transaction = en.Database.BeginTransaction())
                    {
                        question.created_at = DateTime.Now;
                        question.updated_at = DateTime.Now;
                        en.Questions.Add(question);

                        int newQuestionId = question.id;
                        foreach (var answer in answers)
                        {
                            answer.id_question = newQuestionId;
                            answer.created_at = DateTime.Now;
                            answer.updated_at = DateTime.Now;
                            en.Answers.Add(answer);
                        }
                        en.SaveChanges();
                        transaction.Commit();
                        return 1;
                    }

                }
            }
            catch (Exception ex)
            {
                // Handle exception appropriately
                return -1;
            }
        }
        public int UpdateQuestion(Question question, Answer[] answers)
        {
            try
            {
                using (dbSem3Entities en = new dbSem3Entities())
                {
                    using (var transaction = en.Database.BeginTransaction())
                    {
                        // Fetch the existing question from the database
                        var existingQuestion = en.Questions.Find(question.id);

                        if (existingQuestion == null)
                        {
                            // Handle case where the question to edit doesn't exist
                            throw new Exception("Question not found");
                        }

                        // Update question properties
                        existingQuestion.title = question.title; // Update other relevant properties as needed
                        existingQuestion.updated_at = DateTime.Now;

                        // Update existing answers or add new ones
                        foreach (var answer in answers)
                        {
                            if (answer.id > 0) // Existing answer based on presence of id
                            {
                                // Find the existing answer
                                var existingAnswer = en.Answers.Find(answer.id);
                                if (existingAnswer != null)
                                {
                                    existingAnswer.title = answer.title; // Update other relevant properties as needed
                                    existingAnswer.is_correct = answer.is_correct;
                                    existingAnswer.updated_at = DateTime.Now;
                                }
                                else
                                {
                                    // Handle case where an answer to edit doesn't exist (potential error)
                                    throw new Exception("Answer not found");
                                }
                            }
                            else // New answer
                            {
                                answer.id_question = question.id;
                                answer.created_at = DateTime.Now;
                                answer.updated_at = DateTime.Now;
                                en.Answers.Add(answer);
                            }
                        }
                        en.SaveChanges();
                        transaction.Commit();
                        return 1; // Indicate successful edit
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception appropriately (e.g., logging)
                return -1; // Indicate failure
            }
        }

        public bool ChangeStatusQuestion(int idquestion)
        {
            try
            {
                dbSem3Entities en = new dbSem3Entities();
                var qt = en.Questions.Where(d => d.id == idquestion).FirstOrDefault();
                qt.status = (qt.status == 1) ? 0 : 1;
                en.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public QuestionView GetQuestionById(int idqt)
        {
            try
            {
                dbSem3Entities en = new dbSem3Entities();
                var qt = en.Questions.Where(qts => qts.id == idqt).FirstOrDefault();
                var cat = en.CategoryOfQuestions.Where(cts => cts.id == qt.id_categoryofquestion).FirstOrDefault();
                QuestionView question = new QuestionView();
                question.id = qt.id;
                question.title = qt.title;
                question.status = qt.status;
                question.id_categoryofquestion = qt.id_categoryofquestion;
                question.titleofcategory = cat.title;
                question.created_at = qt.created_at;
                question.updated_at = qt.updated_at;
                return question;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ICollection<Answer> GetAnswerByQuestion(int idqt)
        {
            try
            {
                dbSem3Entities en = new dbSem3Entities();
                var rs = en.Answers.Where(iq => iq.id_question == idqt).ToList();
                return rs;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public ICollection<Question> GetQuestionByCategory(int idcate)
        {
            try
            {
                dbSem3Entities en = new dbSem3Entities();
                var rs = en.Questions.Where(iq => iq.id_categoryofquestion == idcate).ToList();
                return rs;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int AddQuestionOfExaminiation(Examination examination, QuestionOfExamination[] qoe)
        {
            try
            {
                using (dbSem3Entities en = new dbSem3Entities())
                {
                    using (var transaction = en.Database.BeginTransaction())
                    {
                        examination.created_at = DateTime.Now;
                        examination.updated_at = DateTime.Now;
                        en.Examinations.Add(examination);

                        int newExamId = examination.id;
                        foreach (var item in qoe)
                        {
                            item.id_examination = newExamId;
                            item.created_at = DateTime.Now;
                            item.updated_at = DateTime.Now;
                            en.QuestionOfExaminations.Add(item);
                        }
                        en.SaveChanges();
                        transaction.Commit();
                        return 1;
                    }

                }
            }
            catch (Exception ex)
            {
                // Handle exception appropriately
                return -1;
            }
        }

        public int UpdateQuestionOfExaminiation(Examination examination, QuestionOfExamination[] qoe)
        {
            try
            {
                using (dbSem3Entities en = new dbSem3Entities())
                {
                    using (var transaction = en.Database.BeginTransaction())
                    {
                        var existingExam = en.Questions.Find(examination.id);

                        if (existingExam != null)
                        {
                            throw new Exception("Examiniation not found");
                        }
                        existingExam.title = examination.title;
                        existingExam.updated_at = DateTime.Now;
                        foreach (var question in qoe)
                        {
                            if (question.id > 0)
                            {
                                var existingQuestion = en.QuestionOfExaminations.Find(question.id);
                                if (existingQuestion != null)
                                {
                                    existingQuestion.id_question = question.id_question;
                                    existingQuestion.updated_at = DateTime.Now;
                                }
                                else
                                {
                                    throw new Exception("Question not found");
                                }
                            }

                        }
                        en.SaveChanges();
                        transaction.Commit();
                        return 1;

                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception appropriately
                return -1;
            }
        }

        public ICollection<Examination> GetExaminations()
        {
            try
            {
                dbSem3Entities en = new dbSem3Entities();
                var rs = en.Examinations.ToList();
                return rs;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Examination GetExaminationById(int id)
        {
            try
            {
                dbSem3Entities en = new dbSem3Entities();
                var rs = en.Examinations.Where(idex => idex.id == id).FirstOrDefault();
                return rs;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ICollection<QuestionView> GetQuestionOfExaminiation(int idexamination)
        {
            try
            {
                dbSem3Entities en = new dbSem3Entities();
                var rs = en.QuestionOfExaminations.Where(id => id.id_examination == idexamination).ToList();
                ICollection<QuestionView> result = new List<QuestionView>();
                foreach (var item in rs)
                {
                    var qt = en.Questions.Where(quest => quest.id == item.id_question).FirstOrDefault();
                    var cate = en.CategoryOfQuestions.Where(categ => categ.id == qt.id_categoryofquestion).FirstOrDefault();
                    QuestionView questionView = new QuestionView();
                    questionView.id = qt.id;
                    questionView.title = qt.title;
                    questionView.status = qt.status;
                    questionView.id_categoryofquestion = qt.id_categoryofquestion;
                    questionView.titleofcategory = cate.title;
                    questionView.updated_at = qt.updated_at;
                    questionView.created_at = qt.created_at;
                    result.Add(questionView);
                }
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int EditExaminiation(Examination examination, QuestionOfExamination[] qoe)
        {
            try
            {
                using (dbSem3Entities en = new dbSem3Entities())
                {
                    using (var transaction = en.Database.BeginTransaction())
                    {
                        // Fetch the existing question from the database
                        var existingExaminiation = en.Questions.Find(examination.id);

                        if (existingExaminiation == null)
                        {
                            // Handle case where the question to edit doesn't exist
                            throw new Exception("Examiniation not found");
                        }

                        // Update question properties
                        existingExaminiation.title = examination.title; // Update other relevant properties as needed
                        existingExaminiation.updated_at = DateTime.Now;

                        // Update existing answers or add new ones
                        foreach (var item in qoe)
                        {
                            if (item.id > 0) // Existing answer based on presence of id
                            {
                                // Find the existing answer
                                var existingQOE = en.QuestionOfExaminations.Find(item.id);
                                if (existingQOE != null)
                                {
                                    existingQOE.id_question = item.id_question; // Update other relevant properties as needed
                                    existingQOE.updated_at = DateTime.Now;
                                }
                                else
                                {
                                    // Handle case where an answer to edit doesn't exist (potential error)
                                    throw new Exception("Answer not found");
                                }
                            }
                        }
                        en.SaveChanges();
                        transaction.Commit();
                        return 1; // Indicate successful edit
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception appropriately (e.g., logging)
                return -1; // Indicate failure
            }
        }
    }
}