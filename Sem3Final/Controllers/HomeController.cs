using Sem3Final.Models.Repository;
using Newtonsoft.Json.Linq;
using Sem3Final.Models;
using Sem3Final.Models.ModelsView.ModelJoin;
using Sem3Final.Models.ModelsView;
using Sem3Final.Models.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sem3Final.Models.Linear;
using Sem3Final.Models.Encryption;

namespace Sem3Final.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Page404()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterAccount(MemberView model)
        {
            try
            {
                if (model != null)
                {
                    string fill = model.fullname;
                    model.status = 1;
                    model.created_at = DateTime.Now;
                    model.updated_at = DateTime.Now;
                    model.images = null;
                    model.cv = null;
                    //Encryption Passwork
                    string pass = model.password;
                    //string passEncrypt = PasswordHasher.HashPassword(pass);
                    string passEncrypt = PasswordHasher1.EncodePasswordToBase64(pass);
                    model.password = passEncrypt;

                    var userRepository = UserRepositorty.Instance; // Tạo biến userRepository
                    var t = userRepository.InsertUser(model);
                    if (t > 0)
                    {
                        HttpContext.Session["infoAccount"] = "Đăng ký thành công";
                    }
                    else
                    {
                        HttpContext.Session["infoAccount"] = "Đăng ký thất bại";
                    }
                }
            }
            catch (Exception e)
            {

            }
            return RedirectToAction("PageLogin", "Login");
        }
        [Authorize(Roles = "CADIDATE")]
        public ActionResult FormTestCadi()
        {
            try
            {
                string id_Ca = HttpContext.Session["idCadi"] as string;
                int id = int.Parse(id_Ca);
                //HttpContext.Session["idStatusCadi"];
                List<CadidateView> views = CandidateRepositories.Instance.GetByIdCadi(id);
                if (views != null)
                {
                    int? status1 = 0;
                    foreach (CadidateView item in views)
                    {
                        status1 = item.status;
                    }
                    string t = status1.ToString();
                    HttpContext.Session["idStatusCadi"] = t.ToString();

                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return View();
        }
        [Authorize(Roles = "CADIDATE")]
        public ActionResult PageHistoryCadi()
        {
            try
            {
                string account = HttpContext.Session["AccountNameCadidate"] as string;
                List<CadidateView> caivi = CandidateRepositories.Instance.GetById(account);
                if (caivi != null)
                {

                    string listAQ = "";
                    foreach (CadidateView view in caivi)
                    {
                        listAQ = view.answer_of_cadidate;
                    }

                    JArray jsonArray = JArray.Parse(listAQ);
                    List<string> questions = new List<string>();
                    List<string> answers = new List<string>();

                    foreach (JObject item in jsonArray)
                    {
                        string question = item["question"].ToString();
                        string answer = item["answer"].ToString();

                        questions.Add(question);
                        answers.Add(answer);
                    }

                    ViewBag.Questions = questions;
                    ViewBag.Answers = answers;

                }


            }
            catch (Exception e)
            {

            }
            return View();
        }


        [HttpGet]
        public ActionResult GetData()
        {
            try
            {

                //Lấy id examine từ cadidate
                string name = "";
                if (HttpContext.Session["AccountNameCadidate"] != null)
                {
                    name = HttpContext.Session["AccountNameCadidate"] as string;
                }
                else
                {
                    name = "eqweqwe";
                }

                List<CadidateView> listCaidate = CandidateRepositories.Instance.GetById(name);

                string listAQ = "";
                foreach (CadidateView view in listCaidate)
                {
                    listAQ = view.submit_cadidate_cadidate;
                }
                List<string> t2 = new List<string>();
                if (!string.IsNullOrEmpty(listAQ))
                {
                    JArray jsonArray = JArray.Parse(listAQ);
                    //List<string> questions = new List<string>();
                    List<string> answers = new List<string>();

                    foreach (JObject item in jsonArray)
                    {
                        //string question = item["question"].ToString();
                        string answer = item["answer"].ToString();

                        //questions.Add(question);
                        answers.Add(answer);
                    }
                    ///
                    //List<string> t = questions;
                    if (answers != null)
                    {
                        t2 = answers;
                    }
                    else
                    {
                        t2.Add("chua có dữ liệu");
                    }

                }



                IEnumerable<CadidateView> listCadidate_status = CandidateRepositories.Instance.GetByNameStatus(name);
                var dataCandidate = listCadidate_status.Select(ev => new
                {
                    id = ev.id,
                    status = ev.status
                }).ToList();


                int? id_vacacy = 0;
                foreach (var cadidateView in listCaidate)
                {
                    id_vacacy = cadidateView.id_vacancy;
                }
                List<VacanciesView> listVancies = VacanciesRepositories.Instance.GetById(id_vacacy);
                int? id_examine = 0;
                foreach (var vacanView in listVancies)
                {
                    id_examine = vacanView.id_examination;
                }
                IEnumerable<QuestionJoin> listQuesstion = QuestionRepositories.Instance.GetById(id_examine);
                var dataQuestion = listQuesstion.Select(ev => new
                {

                    id = ev.question.id,
                    title = ev.question.title,
                    id_categoryofquestion = ev.question.id_categoryofquestion,
                    status = ev.question.status,
                    created_at = ev.question.created_at,
                    updated_at = ev.question.updated_at,
                    is_correct = ev.answer.is_correct,
                    title_answer = ev.answer.title


                }).ToList();

                //List<QuestionView> listQuesstion = QuestionRepositories.Instance.GetAll();
                List<AnswerView> listAnswer = AnswerRepositories.Instance.GetAll();

                //1.Cần biết được vị trí tuyển dụng từ người user
                //2.Cần biết được vị trí tuyển dụng mà user lựa chọn ứng tuyển đó gồm có những bộ đề nào
                //3.Cần biết đucợ bộ đề đó có những câu hỏi và đáp án nào
                IEnumerable<QuestionJoin> listQuesstion1 = QuestionRepositories.Instance.GetById1(id_examine);
                var dataQuestion1 = listQuesstion1.Select(ev => new
                {
                    id = ev.question.id,
                    title = ev.question.title,
                    id_categoryofquestion = ev.question.id_categoryofquestion,
                    status = ev.question.status,
                    created_at = ev.question.created_at,
                    updated_at = ev.question.updated_at,
                    is_correct = ev.answer.is_correct,
                    title_answer = ev.answer.title,
                }).ToList();

                //III
                IEnumerable<QuestionJoin> listQuesstion2 = QuestionRepositories.Instance.GetById2(id_examine);
                var dataQuestion2 = listQuesstion2.Select(ev => new
                {
                    id = ev.question.id,
                    title = ev.question.title,
                    id_categoryofquestion = ev.question.id_categoryofquestion,
                    status = ev.question.status,
                    created_at = ev.question.created_at,
                    updated_at = ev.question.updated_at,
                    title_answer = ev.answer.title,
                    is_correct = ev.answer.is_correct,
                }).ToList();



                //variable Datas for droped variable combinedData

                var combinedData = new
                {
                    Data = dataQuestion,
                    Data1 = dataQuestion1,
                    Data2 = dataQuestion2,
                    Data3 = dataCandidate,
                    Data4 = t2
                };
                return Json(combinedData, "application/json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);


            }
            catch (Exception e)
            {
                throw e;
            }

            return null;
        }
        [HttpPost]
        public ActionResult ProcessAnswers(List<string> answers, List<string> answers1)
        {
            try
            {
                if (answers != null)
                {
                    LinearRegressionModel model = new LinearRegressionModel();
                    IEnumerable<QuestionJoin> trainingData1 = AnswerRepositories.Instance.GetListAnswerQuestionCategory();
                    model.Train(trainingData1, answers);
                    //List<AnswerView> testAnswer = AnswerRepositories.Instance.GetListAnswerQuestionCategory()();
                    //Answer answer = new Answer
                    //{
                    //    title = testAnswer[0].title.ToString()//lấy title của phần tư
                    //};
                }
                return RedirectToAction("ResultCadi", "Home");
            }
            catch (Exception e)
            {
                throw e;
            }
            return RedirectToAction("", "");
        }


        public ActionResult ResultCadi()
        {
            return View();
        }
        public ActionResult FormEditImage(int id)
        {
            try
            {
                int id_T = id;

            }
            catch (Exception e)
            {

            }
            return View();
        }
        [HttpPost]
        public ActionResult UpdateImageUser(HttpPostedFileBase image)
        {
            try
            {
                int t = 0;
                string avatarPath = Server.MapPath("~/Content/Assets/Image");
                string idString = Request.Params["id"];
                int id = int.Parse(idString);
                //Check image đã tồn tại 
                MemberView UserModel = UserRepositorty.Instance.GetByIdUser(id);
                if (UserModel.images != null)
                {
                    // Tìm tất cả các hình ảnh trong thư mục
                    string[] imageFiles = Directory.GetFiles(avatarPath, "*.jpg");

                    foreach (string imagePath in imageFiles)
                    {
                        string imageName = Path.GetFileName(imagePath);
                        if (imageName == UserModel.images)
                        {
                            // Xóa hình ảnh cũ
                            System.IO.File.Delete(imagePath);
                            break;
                        }
                    }
                }

                //Move Image in Path Folder Image
                string imageString = DateTime.Now.Ticks + image.FileName;

                string newPath = Path.Combine(avatarPath, imageString);
                image.SaveAs(newPath);

                if (imageString != null && id != null)
                {

                    MemberView userView = new MemberView();
                    userView.id = id;
                    userView.images = imageString;
                    UserRepositorty.Instance.UpdateImageUsr(userView);
                }


            }
            catch (Exception e)
            {

            }

            return RedirectToAction("Index", "Home");
        }

        //HttpContext.Session["IdAccountUser"]
        [Authorize(Roles = "USER")]
        public ActionResult PageSendCv(string name)
        {
            try
            {
                CadidateView model = new CadidateView();
                List<VacanciesView> responsitories = VacanciesRepositories.Instance.GetAll();
                if (responsitories != null)
                {
                    string id = HttpContext.Session["IdAccountUser"] as string;
                    int? id_mem = int.Parse(id);

                    List<MemberView> members = MemberRepositories.Instance.GetById(id_mem);
                    if (members != null)
                    {
                        foreach (MemberView item in members)
                        {
                            ViewBag.cv_test = item.cv;
                        }
                    }
                    ViewBag.Vancies = responsitories;

                }
                ViewBag.Email = HttpContext.Session["EmailAccountUser"];
                return View(model);
            }
            catch (Exception e)
            {
                throw e;
            }
            return View();
        }
        private static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [Authorize(Roles = "USER")]
        [HttpPost]
        public ActionResult SubmitSendCv(CadidateView model, HttpPostedFileBase link)
        {
            try
            {
                if (model != null)
                {
                    //string randomString = GenerateRandomString(8);
                    string n1 = Path.GetFileNameWithoutExtension(link.FileName) + Path.GetExtension(link.FileName);
                    string projectPath = Server.MapPath("~");
                    string filePath = Path.Combine(projectPath, "Content", "Assets", "Cv");
                    string resultPath = filePath + "\\" + n1;
                    link.SaveAs(resultPath);
                    if (link.ContentLength > 0)
                    {
                        //Send Info Candidate with Vacacies
                        string idvan = Request.Params["idvan"];

                        string emailMem = Request.Params["email"];
                        string concert_person = Request.Params["concert_person"];
                        string emailMem1 = HttpContext.Session["EmailAccountUser"] as string;

                        MemberView modelMem = MemberRepositories.Instance.GetEmailMembers(emailMem);
                        model.id_member = modelMem.id;
                        model.id_vacancy = int.Parse(idvan);
                        //model.concern_person = concert_person;
                        CandidateRepositories.Instance.SubmitSendInfo(model);
                        //Update CV
                        MemberView ModelMember = new MemberView();
                        ModelMember.cv = n1;
                        ModelMember.id = modelMem.id;
                        MemberRepositories.Instance.UpdateCv(ModelMember);
                        ViewBag.Info = "Nộp thành công";
                        return RedirectToAction("PageSendCv", "Home");
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return null;
        }

    }
}