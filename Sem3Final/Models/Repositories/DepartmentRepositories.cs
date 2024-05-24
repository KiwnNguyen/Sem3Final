using Sem3Final.Models.Entities;
using Sem3Final.Models.ModelsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sem3Final.Models.Repositories
{
    public class DepartmentRepositories
    {
        private static DepartmentRepositories _instance;

        private DepartmentRepositories() { }

        public static DepartmentRepositories Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DepartmentRepositories();
                }
                return _instance;
            }
        }
        public List<DepartmentView> GetAll()
        {
            try
            {
                dbSem3Entities entities = new dbSem3Entities();
                var q = entities.Departments.Select(d => new DepartmentView { id = d.id, Dname = d.Dname }).ToList();
                return q;

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<DepartmentView> GetByIdDep(int? id)
        {
            try
            {
                int? id_test = id;

                dbSem3Entities entities = new dbSem3Entities();
                var q = entities.Departments.Where(y => y.id == id).ToList();

                var depviews = q.Select(dep => new DepartmentView
                {
                    id = dep.id,

                    Dname = dep.Dname,
                }).ToList();
                return depviews;
            }
            catch (Exception e)
            {

            }
            return null;
        }
    }
}