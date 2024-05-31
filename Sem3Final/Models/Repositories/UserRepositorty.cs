using Sem3Final.Models.Entities;
using Sem3Final.Models.ModelsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sem3Final.Models.Repositories
{
    public class UserRepositorty
    {
        private static UserRepositorty _instance = null;
        private UserRepositorty() { }
        public static UserRepositorty Instance
        {
            get
            {
                if (_instance == null) { _instance = new UserRepositorty(); }
                return _instance;
            }
        }

        public List<UserView> GetAllUser()
        {
            try
            {
                dbSem3Entities entities = new dbSem3Entities();
                var q = entities.Members.Select(d => new UserView { id = d.id, fullname = d.fullname, email = d.email, pass_word = d.password, status = d.status, created_at = d.created_at, updated_at = d.updated_at, images = d.images }).ToList();
                return q;
            }
            catch (Exception e)
            {

            }
            return null;
        }
        public int InsertUser(UserView model)
        {
            try
            {
                if (model != null)
                {
                    dbSem3Entities entities = new dbSem3Entities();
                    if (model is UserView)
                    {
                        var modelUser = model as UserView;
                        var AddUser = new Member
                        {
                            fullname = modelUser.fullname,
                            email = model.email,
                            password = model.pass_word,
                            status = model.status,
                            created_at = model.created_at,
                            updated_at = model.updated_at,
                        };
                        entities.Members.Add(AddUser);
                        entities.SaveChanges();
                        return 1;
                    }

                }

            }
            catch (Exception e)
            {

            }
            return 0;
        }
        public int UpdateImageUsr(UserView model)
        {
            try
            {
                if (model.images != null && model.id != null)
                {
                    dbSem3Entities entities = new dbSem3Entities();
                    var result = entities.Members.Find(model.id);
                    if (result != null)
                    {
                        result.images = model.images;
                        entities.SaveChanges();
                    }
                }

                return 1;
            }
            catch (Exception e)
            {

            }
            return 0;
        }
        public UserView GetByIdUser(int id)
        {
            try
            {
                dbSem3Entities entities = new dbSem3Entities();
                var user = (from c in entities.Members
                            where c.id == id
                            select c
                    ).FirstOrDefault();
                //Truyền giá trị từ class User sang class UserView
                UserView userViewList = new UserView();
                userViewList.images = user.images;
                return userViewList;
            }
            catch (Exception e)
            {
            }
            return null;
        }

    }
}