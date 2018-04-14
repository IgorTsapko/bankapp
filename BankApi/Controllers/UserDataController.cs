using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Principal;
using System.Web.Http;
using BankApi.DbCont;
using BankApi.Models;
using Microsoft.AspNet.Identity;

namespace BankApi.Controllers
{
    [Authorize]
    public class UserDataController : ApiController
    {
        
        public BankUser Get()
        {
            try
            {
                using (BankContext db = new BankContext())
                {
                    string userId = RequestContext.Principal.Identity.GetUserId();
                    var findedUser = db.BankUsers.Include(c => c.Cards).Include(c => c.Pays).FirstOrDefault(u => u.UserIdentityId == userId);
                    return findedUser;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;

        }

        public IHttpActionResult Post([FromBody]BankUser value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                using (BankContext db = new BankContext())
                {
                    string userId = RequestContext.Principal.Identity.GetUserId();
                    value.UserIdentityId = userId;
                    if (!db.BankUsers.Any(u => u.UserIdentityId == userId))
                    {
                        db.BankUsers.Add(value);
                        db.SaveChanges();
                        var createdUser = db.BankUsers.FirstOrDefault(u => u.UserIdentityId == userId);
                        return Ok(createdUser);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            

            return BadRequest();
        }


        public IHttpActionResult Put(int id, [FromBody]BankUser value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using (BankContext db = new BankContext())
            {
                string userId = RequestContext.Principal.Identity.GetUserId();
                var userForModify = db.BankUsers.FirstOrDefault(u => u.Id == id && u.UserIdentityId == userId);
                if (userForModify != null)
                {
                    foreach (PropertyInfo propertyInfo in userForModify.GetType().GetProperties()
                        .Where(p => p.Name != nameof(BankUser.Id)))
                    {
                        if (propertyInfo.GetValue(value, null) == null)
                            propertyInfo.SetValue(value, propertyInfo.GetValue(userForModify, null), null);
                    }
                    db.Entry(userForModify).CurrentValues.SetValues(value);
                    db.Entry(userForModify).State = EntityState.Modified;
                    db.SaveChanges();
                    var createdUser = db.BankUsers.FirstOrDefault(u => u.UserIdentityId == userId);
                    return Ok(createdUser);
                }
            }

            return BadRequest();
        }

        [Authorize(Roles = "Administrators")]
        public IHttpActionResult Delete(int id)
        {
            using (BankContext db = new BankContext())
            {
                var userForDelete = db.BankUsers.FirstOrDefault(u => u.Id == id);
                db.BankUsers.Remove(userForDelete);
            }
            return Ok();
        }
    }
}
