
using Sem3Final.Models.Entities;
using Sem3Final.Models.ModelsView;
using Sem3Final.Models.ModelsView.ModelJoin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sem3Final.Models.Repository
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
                    cadi.status = 4;
                    cadi.concern_person = model.concern_person;
                    entities.Cadidates.Add(cadi);
                    entities.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<CadidateView> GetByIdCadi(int id)
        {
            try
            {
                dbSem3Entities entities = new dbSem3Entities();
                var q = entities.Cadidates.Where(y => y.id == id).ToList();
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
        public IEnumerable<ScoreResultCadi> GetQuantiyCandil(int? id_van)
        {
            try
            {
                if (id_van != null)
                {
                    dbSem3Entities entities = new dbSem3Entities();
                    var q1 = from a in entities.Vacancies
                             join b in entities.Cadidates on a.id equals b.id_vacancy
                             join c in entities.Members on b.id_member equals c.id
                             where b.id_vacancy == id_van
                             select new ScoreResultCadi
                             {
                                 vacancy = a,
                                 cadidate = b,
                                 member = c,

                             };
                    return q1.ToList();
                }
                return null;
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
        public List<CadidateView> GetByIdMem(int? id_mem)
        {
            try
            {

                dbSem3Entities entities = new dbSem3Entities();
                var q = entities.Cadidates.Where(y => y.id_member == id_mem).ToList();

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
        public int CreateAccountCadi(CadidateView model)
        {
            try
            {
                if (model != null)
                {
                    dbSem3Entities entities = new dbSem3Entities();
                    var q = entities.Cadidates.Find(model.id);
                    if (q != null)
                    {
                        q.username = model.username;
                        q.password = model.password;
                        q.status = 3;
                        entities.SaveChanges();
                        return 1;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return 0;
        }
        public int RefusedCadi(int id)
        {
            try
            {
                if (id != null)
                {
                    dbSem3Entities entities = new dbSem3Entities();
                    var q = entities.Cadidates.Find(id);
                    if (q != null)
                    {
                        q.status = 2;
                        entities.SaveChanges();
                    }
                    return 1;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return 0;
        }
        public int StatusCadiOne(int id)
        {
            try
            {
                if (id != 0)
                {
                    dbSem3Entities entities = new dbSem3Entities();
                    var q = entities.Cadidates.Find(id);
                    if (q != null)
                    {
                        q.status = 1;
                        entities.SaveChanges();
                        return 1;
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