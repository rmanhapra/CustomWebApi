using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SortingAPI.Controllers
{
    public class SortController : ApiController
    {
        public static List<int> collection = new List<int>();
        // GET: api/Sort
        public HttpResponseMessage Get()
        {
            SortedResult sResult = new SortedResult();
            try
            {
                if (DatModel.dataCollection == null || DatModel.dataCollection.Count == 0)
                {
                    DatModel.dataCollection = TestData.GetTestCollection();
                }
                collection = DatModel.dataCollection;
                if (collection.Count(x => x > 1000) > 0 || collection.Count(x => x < 1) > 0)
                {
                    sResult.Message = "Warning!!! Input collection should contain numbers between 1 & 1000 only";
                    collection.RemoveAll(x => x > 1000);
                    collection.RemoveAll(x => x < 1);
                    sResult.Status = Status.Warning;
                }
                else
                {
                    sResult.Status = Status.Success;
                }
                List<int> sortedCollection = SortInputCollection();                
                sResult.result = sortedCollection;
                return Request.CreateResponse<SortedResult>(HttpStatusCode.OK, sResult);
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An internal Errro occured!!. Please contact technical support");
            }
            
        }


        // POST: api/Sort
        public HttpResponseMessage Post([FromBody]List<int> inputCollection)
        {
            if(inputCollection == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Make sure you pass an array of integers between 1 and 1000");
            }
            DatModel.dataCollection = inputCollection;
            return Request.CreateResponse();
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
                return collection.Distinct().ToList<int>();
                
            }
            return null;
        }
    }

    public enum Status { Failed,Success,Warning}
    public class SortedResult
    {
        public Status Status;
        public List<int> result;
        public String Message;
    }

    public class DatModel
    {
        public static List<int> dataCollection;
    }
    public class TestData
    {
        public static List<int> GetTestCollection()
        {
            return new List<int>{9,7,56,12,1,2,67,45,23,11,9};
        }
    }
}
