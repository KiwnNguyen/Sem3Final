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
        public void UpdateCv(MemberView model)
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
    }
}