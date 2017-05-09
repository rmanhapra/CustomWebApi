using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SortingAPI.Controllers
{
    
    public class SortV2Controller : ApiController
    {
        public static List<int> collection = new List<int>();
        // GET: api/Sort
        public HttpResponseMessage Get()
        {
            SortAPIResponseV2 sResult = new SortAPIResponseV2();
            try
            {
                if (DatModelV2.dataCollection == null || DatModelV2.dataCollection.Count == 0)
                {
                    DatModelV2.dataCollection = TestDataV2.GetTestCollection();
                }
                collection = DatModelV2.dataCollection;
                if (collection.Count() != collection.Distinct().Count())
                {
                    sResult.Status = StatusV2.Warning.ToString();
                    sResult.Message = "Your sorted collection contains duplicate values";
                }
                else
                {
                    sResult.Status = StatusV2.Success.ToString();
                }
                List<int> sortedCollection = SortInputCollection();                
                sResult.result = sortedCollection;
                return Request.CreateResponse<SortAPIResponseV2>(HttpStatusCode.OK, sResult);
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An internal Error occured!!. Please contact technical support");
            }
            
        }


        // POST: api/Sort
        public HttpResponseMessage Post([FromBody]List<int> inputCollection)
        {
            if (inputCollection == null || inputCollection.Count == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Please review your request again. Request should contain onlyintegers between 1 and 1000. Duplicate values not allowed");
            }
            
            SortAPIResponseV2 resp = new SortAPIResponseV2();
            if (inputCollection.Count(x => x > 1000) > 0 || inputCollection.Count(x => x < 1) > 0)
            {
                resp.result = null;
                resp.Status = StatusV2.Failed.ToString();
                resp.Message = "Your input collection contains integers greater than thousand or less than 1";
                return Request.CreateResponse(HttpStatusCode.BadRequest, resp);
            }
            if (inputCollection.Count() != inputCollection.Distinct().Count())
            {
                resp.result = null;
                resp.Status = StatusV2.Warning.ToString();
                resp.Message = "Your sorted collection contains duplicate values";
                DatModelV2.dataCollection = inputCollection;
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            else
            {
                resp.result = null;
                resp.Status = StatusV2.Success.ToString();
                DatModelV2.dataCollection = inputCollection;
                return Request.CreateResponse(HttpStatusCode.OK,resp);
            }
        }
           

        // PUT: api/Sort/5
        public void Put(int id, [FromBody]string value)
        {
            throw new Exception("Not implemented yet!!!!!!!");
        }

        // DELETE: api/Sort/5
        public void Delete(int id)
        {
            throw new Exception("Not implemented yet!!!!!!!");
        }


        private List<int> SortInputCollection()
        {
            if(collection.Count > 0)
            {
                collection.Sort();
                return collection;
            }
            return null;
        }
    }

    public enum StatusV2 { Failed,Success,Warning}
    public class SortAPIResponseV2
    {
        public string Status;
        public List<int> result;
        public String Message;
    }

    public class DatModelV2
    {
        public static List<int> dataCollection;
    }
    public class TestDataV2
    {
        public static List<int> GetTestCollection()
        {
            return new List<int>{9,7,56,12,1,2,67,67,45,23,11};
        }
    }
}
