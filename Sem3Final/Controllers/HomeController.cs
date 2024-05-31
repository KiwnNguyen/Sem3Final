using Sem3Final.Models.ModelsView;
using Sem3Final.Models.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sem3Final.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult About()
        {
            return View();
        }
        [Authorize(Roles = "USER")]
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Page404()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterAccount(UserView model)
        {
            try
            {
                if (model != null)
                {
                    string fill = model.fullname;
                    model.status = 1;
                    model.created_at = DateTime.Now;
                    model.updated_at = DateTime.Now;
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
                UserView UserModel = UserRepositorty.Instance.GetByIdUser(id);
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

                    UserView userView = new UserView();
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
        [HttpGet]
        public ActionResult ViewDtVacancy(int idvacancy)
        {
            ViewBag.vacancy = VacancyRepository.Instance.GetVacancyById(idvacancy);
            return View();
        }
    }
}