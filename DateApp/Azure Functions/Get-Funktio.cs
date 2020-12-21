using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Documents.Client;
using System.Linq;
using Microsoft.Azure.Documents.Linq;
using System.Collections.Generic;

namespace FunctionAppi
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            [CosmosDB(ConnectionStringSetting = "anttik_DOCUMENTDB")] DocumentClient client,
            ILogger log)
        {
            DateTime pvm1;
            DateTime pvm2;
            var i = DateTime.Parse(req.Query["pvm1"]);
            var i2 = DateTime.Parse(req.Query["pvm2"]);
            var option = new FeedOptions { EnableCrossPartitionQuery = true };
            var collectionUri = UriFactory.CreateDocumentCollectionUri("outDatabase", "MyCollection");
            var document = client.CreateDocumentQuery<CalendarMark>(
                collectionUri,
                option).Where(b => b.duedate >= i && b.duedate <= i2)
                .AsDocumentQuery();
              {
                List<CalendarMark> calendarList = new List<CalendarMark>();

                while (document.HasMoreResults)
                {
                    foreach(CalendarMark b in await document.ExecuteNextAsync<CalendarMark>())
                    {
                        calendarList.Add(b);
                    }
                    return new JsonResult(calendarList);
                }
            }
            if(document == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult("ee");
        }
    }
    public class CalendarMark
    {
        public string ID { get; set; }
        public string name { get; set; }
        public DateTime duedate { get; set; }
    }
 }
