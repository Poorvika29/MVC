using mvc.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace PractoWebApi.Controllers
{
    public class DoctorapiController : ApiController
    {

        DAL ObjDal = new DAL();

       // List<Doctor> DocList = new List<Doctor>();
        //public DoctorapiController()
        //{
        //    ObjDal.GetAllDoc();
        //}
        public IEnumerable<Doctor> Get()
        {
            return ObjDal.GetAllDoc();

           //  DocList;
        }
        public Doctor Get(string option, string search)
        {
            return ObjDal.Search(option,search);

            //  DocList;
        }

        public Doctor Get( int id)
        {
            return ObjDal.GetAllDoc(id);

            //  DocList;
        }
        //  [HttpPost]
        public void Post([FromBody] Doctor doctor)
        {
            ObjDal.AddDoc(doctor);
        }

        public Doctor Put(int id)
        {
          return ObjDal.GetAllDoc(id);
           
        }
        //[HttpPost]

        public void Post( int id , [FromBody]Doctor doctor)
        {
          
          ObjDal.EditDoc(id , doctor);
            //ObjDal.AddDoc(doctor);
        }

        public void Delete(int id)
        {
            ObjDal.DeleteDoc(id);
        }
    }
}
