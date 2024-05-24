
using Sem3Final.Models.Entities;
using Sem3Final.Models.ModelsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccpSem3.Models.Repository
{
    public class CandidateRepositories
    {
        private static CandidateRepositories instance;
        private CandidateRepositories() { }
        public static CandidateRepositories Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CandidateRepositories();
                }
                return instance;
            }
        }
        public void UpdateCadi(int id, int score1, string listAnswer, string listAnswer_of_cadidate)
        {
            try
            {
                if (id != 0)
                {
                    if (score1 != 0 && listAnswer != null)
                    {

                        using (dbSem3Entities entities = new dbSem3Entities())
                        {
                            var cadidate = entities.Cadidates.Find(id);
                            if (cadidate != null)
                            {
                                cadidate.score = score1;
                                cadidate.answer_of_cadidate = listAnswer;
                                cadidate.status = 0;
                                cadidate.submit_cadidate_cadidate = listAnswer_of_cadidate;
                                entities.SaveChanges();
                            }
                        }

                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public void SubmitSendInfo(CadidateView model)
        {
            try
            {
                if (model != null)
                {
                    Cadidate cadi = new Cadidate();
                    dbSem3Entities entities = new dbSem3Entities();
                    cadi.id_member = model.id_member;
                    cadi.id_vacancy = model.id_vacancy;
                    cadi.created_at = DateTime.Now;
                    cadi.updated_at = DateTime.Now;
                    entities.Cadidates.Add(cadi);
                    entities.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<CadidateView> GetById(string username1)
        {
            try
            {

                dbSem3Entities entities = new dbSem3Entities();
                var q = entities.Cadidates.Where(y => y.username == username1).ToList();

                var caidateViews = q.Select(Cadidate => new CadidateView
                {
                    id = Cadidate.id,
                    id_member = Cadidate.id_member,
                    id_vacancy = Cadidate.id_vacancy,
                    concern_person = Cadidate.concern_person,
                    status = Cadidate.status,
                    username = Cadidate.username,
                    password = Cadidate.password,
                    score = Cadidate.score,
                    answer_of_cadidate = Cadidate.answer_of_cadidate,
                    created_at = Cadidate.created_at,
                    updated_at = Cadidate.updated_at,
                    submit_cadidate_cadidate = Cadidate.submit_cadidate_cadidate

                }).ToList();
                return caidateViews;
            }
            catch (Exception e)
            {
                throw e;
            }
            return null;
        }
        public IEnumerable<CadidateView> GetByNameStatus(string username1)
        {
            try
            {

                dbSem3Entities entities = new dbSem3Entities();
                var q = entities.Cadidates.Where(y => y.username == username1).ToList();

                var caidateViews = q.Select(Cadidate => new CadidateView
                {
                    id = Cadidate.id,
                    id_member = Cadidate.id_member,
                    id_vacancy = Cadidate.id_vacancy,
                    concern_person = Cadidate.concern_person,
                    status = Cadidate.status,
                    username = Cadidate.username,
                    password = Cadidate.password,
                    score = Cadidate.score,
                    answer_of_cadidate = Cadidate.answer_of_cadidate,
                    created_at = Cadidate.created_at,
                    updated_at = Cadidate.updated_at,

                }).ToList();
                return caidateViews;
            }
            catch (Exception e)
            {
                throw e;
            }
            return null;
        }
    }
}