using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using BankApi.DbCont;
using BankApi.Models;

namespace BankApi.Controllers
{
    
    public class BranchesController : ApiController
    {
        const double CentralLat = 47.8256432;
        const double CentralLong = 35.1890217;
        
        void CreateFirstData(BankContext db)
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < 15; i++)
            {
                db.BankBranches.Add(new BankBranch
                {
                    Number = i + 1,
                    Name = "Bank branch #" + i,
                    Latitude = CentralLat + Math.Round((1.0 * (10000 - rand.Next(20000))) / 10000000, 7),
                    Longitude = CentralLong + Math.Round((1.0 * (10000 - rand.Next(20000))) / 10000000, 7)
                });
            }
            db.SaveChanges();
        }

        public IList<BankBranch> Get()
        {
            IList<BankBranch> retValues = new List<BankBranch>();
            try
            {
                using (BankContext db = new BankContext())
                {
                    retValues = db.BankBranches.ToList();
                    if (!retValues.Any())
                    {
                        CreateFirstData(db);
                        retValues = db.BankBranches.ToList();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
           

            return retValues;
        }
    }
}

