using Sem3Final.Models.ModelsView;
using Sem3Final.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sem3Final.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PageViewVancacies()
        {
            try
            {
                List<VacanciesView> ls = VacanciesRepositories.Instance.GetAll();
                ViewBag.Vanciest = ls;
            }
            catch (Exception e)
            {
                throw e;
            }
            return View();
        }
        public ActionResult PageViewDetails(int? id)
        {
            try
            {
                if (id != null)
                {
                    List<VacanciesView> ls = VacanciesRepositories.Instance.GetById(id);
                    if (ls != null)
                    {
                        ViewBag.VanciesDetail = ls;
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("PageViewVancacies", "Home");
                    }
                }
                return null;
            }
            catch (Exception e)
            {

            }
            return View();
        }

        public ActionResult PageViewDetailDepart(int? id)
        {
            try
            {
                if (id != null)
                {
                    List<DepartmentView> ls = DepartmentRepositories.Instance.GetByIdDep(id);
                    if (ls != null)
                    {
                        ViewBag.DepDetail = ls;
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("PageViewDetails", "Home");
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return View();
        }
        public ActionResult PageViewDetailExa(int? id)
        {
            try
            {
                if (id != null)
                {
                    List<ExamineView> ls = ExamineRepositories.Instance.GetByIdExa(id);
                    if (ls != null)
                    {
                        ViewBag.ExaDetail = ls;
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("PageViewDetails", "Home");
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return View();
        }

        public ActionResult PageAddVancacies()
        {
            try
            {
                VacanciesView vacanciesView = new VacanciesView();
                List<ExamineView> ls = ExamineRepositories.Instance.GetAll();
                List<DepartmentView> ls1 = DepartmentRepositories.Instance.GetAll();
                if (ls != null)
                {
                    ViewBag.Examine = ls;
                    ViewBag.Depart = ls1;
                }
                return View(vacanciesView);
            }
            catch (Exception e)
            {
                throw e;
            }
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SubmitVancies(VacanciesView model)
        {
            try
            {
                var derciption = Request.Form["myData"].ToString();

                string id_exam = Request.Params["exam"];
                string id_dep = Request.Params["depart"];
                string jobnature = Request.Params["jobnature"];
                string featured = Request.Params["featured"];

                model.description = derciption;
                model.id_dep = int.Parse(id_dep);
                model.id_examination = int.Parse(id_exam);
                model.jobnature = jobnature;
                if (model != null)
                {
                    VacanciesRepositories.Instance.InsertVan(model);
                    return RedirectToAction("PageAddVancacies", "Admin");

                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return null;
        }
        public ActionResult PageUpdateVancies(int id)
        {
            try
            {
                if (id != null)
                {
                    VacanciesView view = new VacanciesView();
                    List<VacanciesView> ls = VacanciesRepositories.Instance.GetById(id);
                    if (ls != null)
                    {

                        foreach (VacanciesView item in ls)
                        {
                            ViewBag.decription = item.description;
                            ViewBag.quantity = item.quantity_emp;
                            ViewBag.salary1 = item.salary;
                            ViewBag.dateline1 = item.dateline;
                            ViewBag.id = item.id;

                        }
                    }
                    return View(view);
                }
                else
                {

                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SubmitEditVancies(VacanciesView model)
        {
            try
            {
                if (model != null)
                {
                    var derciption_t = Request.Form["myData"].ToString();
                    string jobnature = Request.Params["jobnature"];
                    string featured = Request.Params["featured"];
                    string id_test = Request.Params["id"];
                    model.description = derciption_t;
                    model.jobnature = jobnature;
                    model.featured = int.Parse(featured);
                    int id = int.Parse(id_test);
                    VacanciesRepositories.Instance.UpdateVan(id, model);
                }
                return RedirectToAction("PageViewVancacies", "Admin");
            }
            catch (Exception e)
            {
                throw e;
            }
            return null;
        }
    }
}