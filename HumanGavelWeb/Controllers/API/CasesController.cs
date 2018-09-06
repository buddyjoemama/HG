using HumanGavel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HumanGavelWeb.Controllers
{
    [RoutePrefix("api/content")]
    public class CasesController : ApiController
    {
        [Route("cases/{category}"), HttpGet]
        public IHttpActionResult ListByCategory(Category category, int skip = 0, int take = 100)
        {
            var result = (from c in Case.GetByCategory(category, 0, 100)
                            .OrderBy(s => s.Votes)
                            .GroupBy(s => s.CaseId)
                            .Where(s => s.Count() > 1)
                          let meta = c.First()
                          select new
                          {
                              caseId = c.Key,
                              image = meta.ImageUrl,
                              name = meta.CaseName,
                              caseEndDate = meta.ExpirationDate,
                              voteData = c.Select(t => new
                              {
                                  name = t.ParticipantName,
                                  value = t.Votes,
                                  participantId = t.ParticipantId
                              }).ToArray()
                          }).ToList();

            return Json(result);
        }
    }
}
