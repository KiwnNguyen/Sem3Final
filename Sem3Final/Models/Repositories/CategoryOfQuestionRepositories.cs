using Sem3Final.Models.Entities;
using Sem3Final.Models.ModelsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sem3Final.Models.Repositories
{
    public class CategoryOfQuestionRepositories
    {
        private static CategoryOfQuestionRepositories instance = null;
        private CategoryOfQuestionRepositories() { }
        public static CategoryOfQuestionRepositories Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CategoryOfQuestionRepositories();
                }
                return instance;
            }
        }
        public List<CategoryOfQuestionView> GetAll()
        {
            try
            {
                dbSem3Entities entities = new dbSem3Entities();
                var q = entities.CategoryOfQuestions.Select(d => new CategoryOfQuestionView { id = d.id, title = d.title, description = d.description, status = d.status, created_at = d.created_at, updated_at = d.updated_at }).ToList();
                return q;
            }
            catch (Exception e)
            {

            }
            return null;
        }
    }
}