using Sem3Final.Models.Entities;
using Sem3Final.Models.ModelsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sem3Final.Models.Repositories
{
    public class MemberRepositories
    {
        public static MemberRepositories _instance { get; set; }
        public MemberRepositories() { }
        public static MemberRepositories Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MemberRepositories();
                }
                return _instance;
            }
        }
        public List<MemberView> GetById(int? id)
        {
            try
            {
                if (id != null)
                {
                    dbSem3Entities entities = new dbSem3Entities();
                    var q = entities.Members.Where(y => y.id == id).ToList();
                    if (q != null)
                    {
                        var member1 = q.Select(mem => new MemberView
                        {
                            id = mem.id,
                            email = mem.email,
                            education_details = mem.education_details,
                            cv = mem.cv,
                            fullname = mem.fullname,
                            work_experience = mem.work_experience,
                            personal_detail = mem.personal_details,
                            phone = mem.phone,
                            status = mem.status,
                        });

                        return member1.ToList();
                    }

                }

            }
            catch (Exception e)
            {
                throw e;
            }
            return null;
        }
        public void UpdateCv(MemberView model)
        {
            try
            {
                if (model != null)
                {
                    dbSem3Entities entities = new dbSem3Entities();
                    var q = entities.Members.Find(model.id);
                    if (q != null)
                    {
                        q.cv = model.cv;

                        entities.SaveChanges();
                    }

                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public MemberView GetEmailMembers(string email)
        {
            try
            {
                if (email != null)
                {
                    dbSem3Entities entities = new dbSem3Entities();
                    var q = (from a in entities.Members
                             where a.email == email
                             select a
                             ).FirstOrDefault();

                    if (q != null)
                    {
                        MemberView memberView = new MemberView();
                        memberView.id = q.id;
                        memberView.email = q.email;

                        return memberView;
                    }


                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return null;
        }
        public int UpdateStatusMem(int id)
        {
            try
            {
                if (id != null)
                {
                    dbSem3Entities entities = new dbSem3Entities();
                    var q = entities.Members.Find(id);
                    if (q != null)
                    {
                        if (q.status == 1)
                        {
                            q.status = 0;
                            entities.SaveChanges();
                            return 1;
                        }
                        if (q.status == 0)
                        {
                            q.status = 1;
                            entities.SaveChanges();
                            return 1;
                        }

                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return 0;
        }
    }
}