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

        public List<MemberView> GetAllUser()
        {
            try
            {
                dbSem3Entities entities = new dbSem3Entities();
                var q = entities.Members.Select(d => new MemberView { id = d.id, fullname = d.fullname, email = d.email, password = d.password, status = d.status, created_at = d.created_at, updated_at = d.updated_at, images = d.images }).ToList();
                return q;
            }
            catch (Exception e)
            {

            }
            return null;
        }
        public int InsertUser(MemberView model)
        {
            try
            {
                if (model != null)
                {
                    dbSem3Entities entities = new dbSem3Entities();
                    if (model is MemberView)
                    {

                        var modelUser = model as MemberView;

                        var AddUser = new Member
                        {
                            fullname = modelUser.fullname,
                            email = model.email,
                            password = model.password,
                            status = model.status,
                            education_details = model.education_details,

                            personal_details = model.personal_detail,
                            work_experience = model.work_experience,
                            created_at = model.created_at,
                            updated_at = model.updated_at,
                            phone = model.phone,
                            cv = model.cv,
                            images = model.images,
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
        public int UpdateImageUsr(MemberView model)
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
        public MemberView GetByIdUser(int id)
        {
            try
            {
                dbSem3Entities entities = new dbSem3Entities();
                var user = (from c in entities.Members
                            where c.id == id
                            select c
                    ).FirstOrDefault();
                //Truyền giá trị từ class User sang class UserView
                MemberView userViewList = new MemberView();
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