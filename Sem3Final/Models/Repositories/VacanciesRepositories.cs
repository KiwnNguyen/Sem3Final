using Sem3Final.Models.Entities;
using Sem3Final.Models.ModelsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sem3Final.Models.Repositories
{
    public class VacanciesRepositories
    {
        private static VacanciesRepositories instance = null;
        private VacanciesRepositories() { }
        public static VacanciesRepositories Instance
        {
            get
            {

                if (instance == null)
                {
                    instance = new VacanciesRepositories();
                }
                return instance;
            }
        }

        public List<VacanciesView> GetById(int? id)
        {
            try
            {
                int? id_test = id;

                dbSem3Entities entities = new dbSem3Entities();
                var q = entities.Vacancies.Where(y => y.id == id).ToList();

                var vanViews = q.Select(van => new VacanciesView
                {
                    id = van.id,
                    name = van.name,
                    description = van.description,
                    quantity_emp = van.quantity_emp,
                    id_examination = van.id_examination,
                    id_dep = van.id_dep,
                    status = van.status,
                    created_at = van.created_at,
                    updated_at = van.updated_at,
                    salary = van.salary,
                    dateline = van.dateline,
                    featured = van.featured,
                    jobnature = van.jobnature

                }).ToList();
                return vanViews;
            }
            catch (Exception e)
            {
                throw e;
            }
            return null;
        }
        public List<VacanciesView> GetAll()
        {
            try
            {
                dbSem3Entities entities = new dbSem3Entities();
                var q = entities.Vacancies.Select(d => new VacanciesView { id = d.id, name = d.name, quantity_emp = d.quantity_emp, id_examination = d.id_examination, id_dep = d.id_dep, status = d.status, created_at = d.created_at, updated_at = d.updated_at, salary = d.salary, dateline = d.dateline, jobnature = d.jobnature, featured = d.featured });
                return q.ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
            return null;
        }

        public void InsertVan(VacanciesView model)
        {
            try
            {
                if (model != null)
                {
                    Vacancy vacancy = new Vacancy();
                    dbSem3Entities entities = new dbSem3Entities();

                    vacancy.name = model.name;
                    vacancy.description = model.description;
                    vacancy.quantity_emp = model.quantity_emp;
                    vacancy.id_examination = model.id_examination;
                    vacancy.id_dep = model.id_dep;
                    vacancy.status = "open";
                    vacancy.created_at = DateTime.Now;
                    vacancy.updated_at = DateTime.Now;
                    vacancy.salary = model.salary;
                    vacancy.dateline = model.dateline;
                    vacancy.jobnature = model.jobnature;
                    vacancy.featured = model.featured;
                    //Insert data in Vacancies
                    entities.Vacancies.Add(vacancy);
                    entities.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void UpdateVan(int id, VacanciesView model)
        {
            try
            {
                if (id != null && model != null)
                {
                    using (dbSem3Entities entities = new dbSem3Entities())
                    {
                        var q = entities.Vacancies.Find(id);
                        if (q != null)
                        {
                            Vacancy vacancy = new Vacancy();
                            q.description = model.description;
                            q.quantity_emp = model.quantity_emp;
                            q.updated_at = DateTime.Now;
                            q.salary = model.salary;
                            q.dateline = model.dateline;
                            q.jobnature = model.jobnature;
                            q.featured = model.featured;

                            entities.SaveChanges();
                        }
                    }



                }
            }
            catch (Exception e)
            {

            }
        }
    }
}