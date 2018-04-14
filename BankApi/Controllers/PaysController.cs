using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BankApi.DbCont;
using BankApi.Models;
using Microsoft.AspNet.Identity;

namespace BankApi.Controllers
{
    [Authorize]
    public class PaysController : ApiController
    {
        private string UserId => RequestContext.Principal.Identity.GetUserId();
        void CreateFirstData(BankContext db, int cardId)
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < 15; i++)
            {
                db.BankUsers.FirstOrDefault(u =>
                    u.UserIdentityId == UserId)?.Pays.Add(new PayInfo
                {
                    CardId = cardId,
                    Description = "Pay #"+i,
                    Sum = rand.Next(1000)
                    });
            }
            db.SaveChanges();
        }

        public IList<PayInfo> Get()
        {
            IList<PayInfo> userPays = new List<PayInfo>();
            try
            {
                using (BankContext db = new BankContext())
                {
                    userPays = db.BankUsers.FirstOrDefault(u =>
                        u.UserIdentityId == UserId)?.Pays;
                }
            }
            catch (Exception e)
            {
                //
            }
            return userPays;
        }

        [HttpGet]
        public IList<PayInfo> GetByCard(int cardId)
        {
            IList<PayInfo> userPays = new List<PayInfo>();
            try
            {
                using (BankContext db = new BankContext())
                {
                    var userPaysEnum = db.BankUsers.FirstOrDefault(u =>
                        u.UserIdentityId == UserId)?.Pays.Where(p => p.CardId == cardId);
                    if (!userPaysEnum.Any())
                    {
                        CreateFirstData(db, cardId);
                        userPaysEnum = db.BankUsers.FirstOrDefault(u =>
                            u.UserIdentityId == UserId)?.Pays.Where(p => p.CardId == cardId);
                    }

                    return userPaysEnum.ToList();
                }
            }
            catch (Exception e)
            {
                //
            }

            return userPays;
        }

        public PayInfo Get(int id)
        {
            PayInfo userPay = null;

            try
            {
                using (BankContext db = new BankContext())
                {
                    userPay = db.BankUsers.FirstOrDefault(u =>
                        u.UserIdentityId == UserId)?.Pays.FirstOrDefault(p => p.Id == id);
                }
            }
            catch (Exception e)
            {
                //
            }
            

            return userPay;
        }
    }
}
