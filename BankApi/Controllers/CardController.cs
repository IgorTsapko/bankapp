﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Windows.Forms;
using BankApi.DbCont;
using BankApi.Models;
using Microsoft.AspNet.Identity;

namespace BankApi.Controllers
{
    [Authorize]
    public class CardController : ApiController
    {
        private string UserId => RequestContext.Principal.Identity.GetUserId();

        private List<string> ValidateCard(CardInfo cardInfo)
        {
            var results = new List<string>();

            try
            {
                using (BankContext db = new BankContext())
                {
                    
                    if (db.BankUsers.Any(u => u.UserIdentityId != UserId &&
                                              u.Cards.Any(c => c.CardNumber == cardInfo.CardNumber)))
                        results.Add("This card is not yours");
                }
            }
            catch (Exception ex)
            {
                //
            }
            return results;
        }

        public IEnumerable<CardInfo> Get()
        {
            IList<CardInfo> userCards;
            using (BankContext db = new BankContext())
            {
                userCards = db.BankUsers.FirstOrDefault(u =>
                    u.UserIdentityId == UserId)?.Cards.Where(c => c.IsActive).ToList();
            }

            return userCards;
        }

        public CardInfo Get(int id)
        {
            CardInfo retCard = null;
            using (BankContext db = new BankContext())
            {
                retCard = db.BankUsers
                    .FirstOrDefault(u => u.UserIdentityId == RequestContext.Principal.Identity.GetUserId())?.Cards
                    .FirstOrDefault(c => c.Id == id && c.IsActive);
            }
            return retCard;
        }

        public IHttpActionResult Post([FromBody]CardInfo value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var controllerValidate = ValidateCard(value);
            if (controllerValidate.Count > 0)
            {
                foreach (var errorText in controllerValidate)
                    ModelState.AddModelError("", errorText);
                return BadRequest(ModelState);
            }

            CardInfo retCard = null;
            
            using (BankContext db = new BankContext())
            {
                var currentUser = db.BankUsers.FirstOrDefault(u =>
                    u.UserIdentityId == UserId);
                if (currentUser != null)
                {
                    currentUser.Cards.Add(value);
                    db.SaveChanges();
                    retCard = currentUser.Cards.FirstOrDefault(c => c.Id == value.Id);
                }
            }

            if (retCard != null)
                return Ok(retCard);
            else
                return BadRequest();
        }


        public IHttpActionResult Put(int id, [FromBody] CardInfo value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var controllerValidate = ValidateCard(value);
            if (controllerValidate.Count > 0)
            {
                foreach (var errorText in controllerValidate)
                    ModelState.AddModelError("", errorText);
                return BadRequest(ModelState);
            }
            CardInfo retCard = null;
            try
            {
                using (BankContext db = new BankContext())
                {
                    var currentUser = db.BankUsers.Include(c => c.Cards).FirstOrDefault(u =>
                        u.UserIdentityId == UserId && u.Cards.Any(c => c.Id == id));

                    if (currentUser != null)
                    {
                        var cardForModify = currentUser.Cards.FirstOrDefault(c => c.Id == id);
                        if (cardForModify != null)
                        {
                            foreach (PropertyInfo propertyInfo in cardForModify.GetType().GetProperties()
                                .Where(p => p.Name != nameof(CardInfo.Id) && p.Name != nameof(CardInfo.Balance)))
                            {
                                if (propertyInfo.GetValue(value, null) != null)
                                    propertyInfo.SetValue(cardForModify, propertyInfo.GetValue(value, null), null);
                            }

                            db.SaveChanges();
                        }
                        retCard = currentUser.Cards.FirstOrDefault(c => c.Id == id);
                    }
                }
            }
            catch (Exception ex)
            {
                //
            }
            if (retCard != null)
                return Ok(retCard);
            else
                return BadRequest();
        }

        
        public void Delete(int id)
        {
            try
            {
                using (BankContext db = new BankContext())
                {
                    var currentUser = db.BankUsers.Include(c => c.Cards).FirstOrDefault(u =>
                        u.UserIdentityId == UserId && u.Cards.Any(c => c.Id == id));
                    if (currentUser != null)
                    {
                        currentUser.Cards.Remove(currentUser.Cards.FirstOrDefault(c => c.Id == id));
                        //currentUser.Cards.FirstOrDefault(c => c.Id == id).IsActive = false;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                //
            }
        }
    }
}