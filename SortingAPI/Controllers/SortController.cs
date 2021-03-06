﻿using System;
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
            SortAPIResponse sResult = new SortAPIResponse();
            try
            {
                if (DatModel.dataCollection == null || DatModel.dataCollection.Count == 0)
                {
                    DatModel.dataCollection = TestData.GetTestCollection();
                }
                collection = DatModel.dataCollection;
                sResult.Status = Status.Success.ToString();
                List<int> sortedCollection = SortInputCollection();                
                sResult.result = sortedCollection;
                return Request.CreateResponse<SortAPIResponse>(HttpStatusCode.OK, sResult);
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An internal Error occured!!. Please contact technical support");
            }
            
        }


        // POST: api/Sort
        public HttpResponseMessage Post([FromBody]List<int> inputCollection)
        {
            if (inputCollection == null || inputCollection.Count==0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Please review your request again. Request should contain onlyintegers between 1 and 1000. Duplicate values not allowed");
            }
            
            SortAPIResponse resp = new SortAPIResponse();
            if (inputCollection.Count(x => x > 1000) > 0 || inputCollection.Count(x => x < 1) > 0)
            {
                resp.result = null;
                resp.Status = Status.Failed.ToString();
                resp.Message = "Your input collection contains integers greater than thousand or less than 1";
                return Request.CreateResponse(HttpStatusCode.BadRequest, resp);
            }
            if (inputCollection.Count() != inputCollection.Distinct().Count())
            {
                resp.result = null;
                resp.Status = Status.Failed.ToString();
                resp.Message = "Your input collection duplicate values";
                return Request.CreateResponse(HttpStatusCode.BadRequest, resp);
            }
            DatModel.dataCollection = inputCollection;
            resp.result = null;
            resp.Status = Status.Success.ToString();
            return Request.CreateResponse(HttpStatusCode.OK,resp);
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

    public enum Status { Failed,Success}
    public class SortAPIResponse
    {
        public String Status;
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
            return new List<int>{9,7,56,12,1,2,67,45,23,11};
        }
    }
}
