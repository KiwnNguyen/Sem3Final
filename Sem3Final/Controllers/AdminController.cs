using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sem3Final.Controllers
{
    public class AdminController : Controller
    {
        [Authorize(Roles = "ADMIN")]
        public class AdminController : Controller
        {
            // GET: Admin
            public ActionResult Index()
            {
                return View();
            }

            //Question MANAGER
            public ActionResult QuestionMgr()
            {
                ViewBag.qt = QuestionRepository.Instance.GetQuestions();
                return View();
            }

            public ActionResult ChangeStatusQuestion(int id)
            {
                QuestionRepository.Instance.ChangeStatusQuestion(id);
                return RedirectToAction("QuestionMgr");
            }
            public ActionResult AddQuestion()
            {
                ViewBag.catques = QuestionRepository.Instance.GetCategoryOfQuestions();
                return View();
            }
            [HttpPost]
            public ActionResult SaveQuestion(FormCollection form)
            {
                Question question = new Question();
                question.title = form["title"];
                question.id_categoryofquestion = int.Parse(form["enhanced-select-idcat"]);
                question.status = 1;

                List<Answer> answerList = new List<Answer>();
                string[] lsas = form["titleas"].Split(',');

                string isCorrectValue = form["iscorrect"];
                int correctIndex;  // Biến lưu trữ index của đáp án đúng (nếu có)

                // Kiểm tra xem có giá trị iscorrect không (xử lý trường hợp iscorrect trống)
                if (!string.IsNullOrEmpty(isCorrectValue))
                {
                    correctIndex = int.Parse(isCorrectValue);
                }
                else
                {
                    correctIndex = -1;
                }

                for (int i = 0; i < lsas.Length; i++)
                {
                    Answer answer = new Answer();
                    answer.title = lsas[i];
                    answer.is_correct = (i == correctIndex) ? 1 : 0;
                    answerList.Add(answer);
                }

                QuestionRepository.Instance.AddQuestion(question, answerList.ToArray());

                return RedirectToAction("QuestionMgr");
            }

            [HttpPost]
            public ActionResult SaveEditQuestion(int id, FormCollection form)
            {
                try
                {
                    // Fetch the question to edit from the repository
                    var question = QuestionRepository.Instance.GetQuestionById(id);
                    Question qt = new Question();
                    qt.id = id;
                    qt.title = question.title;
                    qt.status = question.status;
                    qt.id_categoryofquestion = question.id_categoryofquestion;
                    qt.created_at = question.created_at;
                    qt.updated_at = question.updated_at;

                    if (question == null)
                    {
                        // Handle case where the question to edit doesn't exist
                        return HttpNotFound(); // Or throw an exception
                    }

                    // Update question properties
                    qt.title = form["title"];
                    qt.id_categoryofquestion = int.Parse(form["enhanced-select-idcat"]);

                    // Update existing answers or add new ones
                    List<Answer> answerList = new List<Answer>();
                    string[] lsas = form["titleas"].Split(',');
                    string[] lsasid = form["idas"].Split(',');
                    string isCorrectValue = form["iscorrect"];
                    int correctIndex;

                    // Check if iscorrect value exists
                    if (!string.IsNullOrEmpty(isCorrectValue))
                    {
                        correctIndex = int.Parse(isCorrectValue);
                    }
                    else
                    {
                        correctIndex = -1;
                    }
                    // Clear existing answers (assuming you want to replace all answers)
                    //question.Clear(); // Adjust based on your logic
                    for (int i = 0; i < lsas.Length; i++)
                    {
                        Answer answer = new Answer();
                        answer.id = int.Parse(lsasid[i]);
                        answer.title = lsas[i];
                        answer.is_correct = (i == correctIndex) ? 1 : 0;
                        answerList.Add(answer);
                    }

                    // Save changes using the repository
                    QuestionRepository.Instance.UpdateQuestion(qt, answerList.ToArray());

                    return RedirectToAction("QuestionMgr"); // Or your desired action after successful edit
                }
                catch (Exception ex)
                {
                    // Handle exceptions appropriately (e.g., logging)
                    return null; // Or handle error differently
                }
            }

            public ActionResult ViewDtQuestion(int id)
            {
                ViewBag.catques = QuestionRepository.Instance.GetCategoryOfQuestions();
                ViewBag.qt = QuestionRepository.Instance.GetQuestionById(id);
                ViewBag.answer = QuestionRepository.Instance.GetAnswerByQuestion(id);
                return View();
            }
            //Examiniation Manager
            public ActionResult ExaminiationMgr()
            {
                ViewBag.listExam = QuestionRepository.Instance.GetExaminations();
                return View();
            }
            public ActionResult AddExaminiation()
            {
                ViewBag.qtgk = QuestionRepository.Instance.GetQuestionByCategory(1);
                ViewBag.qtm = QuestionRepository.Instance.GetQuestionByCategory(2);
                ViewBag.qtct = QuestionRepository.Instance.GetQuestionByCategory(3);
                return View();
            }

            [HttpPost]
            public ActionResult SaveExaminiation(FormCollection form)
            {
                Examination examination = new Examination();
                examination.title = form["title"];

                List<QuestionOfExamination> questionList = new List<QuestionOfExamination>();
                string[] lsqt = form["questionidlist"].Split(',');

                for (int i = 0; i < lsqt.Length; i++)
                {
                    QuestionOfExamination questionOfExamination = new QuestionOfExamination();
                    if (int.Parse(lsqt[i]) != 0)
                    {
                        questionOfExamination.id_question = int.Parse(lsqt[i]);
                        questionList.Add(questionOfExamination);
                    }
                }
                QuestionRepository.Instance.AddQuestionOfExaminiation(examination, questionList.ToArray());

                return RedirectToAction("ExaminiationMgr");
            }

            [HttpPost]
            public ActionResult SaveEditExaminiation(int idex, FormCollection form)
            {
                var examiniation = QuestionRepository.Instance.GetExaminationById(idex);
                Examination examination = new Examination();
                examination.title = form["title"];

                return RedirectToAction("ExaminiationMgr");
            }

            public ActionResult ViewDtExaminiation(int id)
            {
                var ex = QuestionRepository.Instance.GetExaminationById(id);
                ViewBag.examiniation = ex;
                var listqt = QuestionRepository.Instance.GetQuestionOfExaminiation(ex.id);
                ICollection<QuestionView> listqtgk = new List<QuestionView>();
                ICollection<QuestionView> listqtm = new List<QuestionView>();
                ICollection<QuestionView> listqtct = new List<QuestionView>();
                foreach (var item in listqt)
                {
                    if (item.id_categoryofquestion == 1)
                    {
                        listqtgk.Add(item);
                    }
                    else if (item.id_categoryofquestion == 2)
                    {
                        listqtm.Add(item);
                    }
                    else if (item.id_categoryofquestion == 3)
                    {
                        listqtct.Add(item);
                    }
                }
                ViewBag.listqtgk = listqtgk;
                ViewBag.listqtm = listqtm;
                ViewBag.listqtct = listqtct;

                ViewBag.qtgk = QuestionRepository.Instance.GetQuestionByCategory(1);
                ViewBag.qtm = QuestionRepository.Instance.GetQuestionByCategory(2);
                ViewBag.qtct = QuestionRepository.Instance.GetQuestionByCategory(3);
                return View();
            }
        }
    }
}