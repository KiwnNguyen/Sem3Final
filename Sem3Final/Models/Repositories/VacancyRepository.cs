using Sem3Final.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sem3Final.Models.Repositories
{
    public class VacancyRepository
    {
        private static VacancyRepository instance = null;
        private VacancyRepository() { }
        public static VacancyRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new VacancyRepository();
                }
                return instance;
            }
        }

        public ICollection<Vacancy> GetAllVacancies()
        {
            try
            {
                dbSem3Entities en = new dbSem3Entities();
                var rs = en.Vacancies.ToList();
                return rs;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Vacancy GetVacancyById(int id)
        {
            try
            {
                dbSem3Entities en = new dbSem3Entities();
                var rs = en.Vacancies.Where(va => va.id == id).FirstOrDefault();
                return rs;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}