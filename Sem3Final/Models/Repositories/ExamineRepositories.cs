using Sem3Final.Models.Entities;
using Sem3Final.Models.ModelsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sem3Final.Models.Repositories
{
    public class ExamineRepositories
    {
        private static ExamineRepositories _instance;

        private ExamineRepositories() { }

        public static ExamineRepositories Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ExamineRepositories();
                }
                return _instance;
            }
        }
        public List<ExamineView> GetAll()
        {
            try
            {
                dbSem3Entities entities = new dbSem3Entities();
                var q = entities.Examinations.Select(e => new ExamineView { id = e.id, title = e.title, created_at = e.created_at, updated_at = e.updated_at }).ToList();
                return q;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<ExamineView> GetByIdExa(int? id)
        {
            try
            {
                int? id_test = id;

                dbSem3Entities entities = new dbSem3Entities();
                var q = entities.Examinations.Where(y => y.id == id).ToList();
                var exaviews = q.Select(exa => new ExamineView
                {
                    id = exa.id,
                    title = exa.title,
                    created_at = exa.created_at,
                    updated_at = exa.updated_at
                }).ToList();
                return exaviews;
            }
            catch (Exception e)
            {

            }
            return null;
        }
    }
}