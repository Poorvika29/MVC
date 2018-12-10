using mvc.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace mvc.Controllers
{

    public class DoctorController : Controller
    {

        string Constr = ConfigurationManager.ConnectionStrings["DocDB"].ConnectionString;
        List<Doctor> DocList = new List<Doctor>();
        //DAL ObjDal = new DAL();

        public ActionResult Login()
        {
            return View();
        }

        //public ActionResult Contact( )
        //{
        //    // return PartialView("_Contact", doctor);
        //    //Doctor dd = new Doctor();
        //    //dd = ViewBag.doc;
        //    //return View(dd);
        //    return View();


        //}
        //[HttpPost]
        public ActionResult Contact( Doctor doctor)
        {
            return View(doctor);
        }

        // GET: Doctor
        //public ActionResult Index(string option, string search)
        //{
        //    var doc = GetAllDoc();
        //    if (option == "Speciality")
        //    {
        //        List<Doctor> doctor = new List<Doctor>();
        //        for (int i = 0; i < doc.Count; i++)
        //        {
        //            if (doc[i].Speciality == search)
        //                doctor.Add(doc[i]);
        //        }

        //        return View(doctor);

        //    }
        //    else if (option == "City")
        //    {
        //        List<Doctor> doctor = new List<Doctor>();
        //        for (int i = 0; i < doc.Count; i++)
        //        {
        //            if (doc[i].City == search)
        //                doctor.Add(doc[i]);

        //        }
        //        return View(doctor);

        //    }
        //    return View(doc);
        //    //return Json(doc, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult Index(string search)
        {
            var doc = GetAllDoc();
            ViewBag.data = doc;
            List<Doctor> doctor = new List<Doctor>();
            if (search != null)
            {
                for (int i = 0; i < doc.Count; i++)
                {
                    if (doc[i].name == search)
                        doctor.Add(doc[i]);
                    else if (doc[i].Speciality == search)
                        doctor.Add(doc[i]);
                    else if (doc[i].Gender == search)
                        doctor.Add(doc[i]);
                    else if (doc[i].Degree == search)
                        doctor.Add(doc[i]);
                    else if (doc[i].state == search)
                        doctor.Add(doc[i]);
                    else if (doc[i].contact == search)
                        doctor.Add(doc[i]);
                    else if (doc[i].City == search)
                        doctor.Add(doc[i]);
                    else if (doc[i].Country == search)
                        doctor.Add(doc[i]);
                    else if (doc[i].Address == search)
                        doctor.Add(doc[i]);
                }
                return View("~/Views/Doctor/Serachdoc.cshtml", doctor);

            }
            else
                return View(doc);

            // doc.Find(search);



            //return Json(doc, JsonRequestBehavior.AllowGet);
        }

        private List<Doctor> GetAllDoc()
        {
            var doc = new List<Doctor>();
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:53080/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage responseMessage = client.GetAsync("api/Doctorapi").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                doc = responseMessage.Content.ReadAsAsync<List<Doctor>>().Result;
            }
            //return RedirectToAction("Error");
            using (StreamWriter file = System.IO.File.CreateText(@"C:\Users\ahalgark\source\repos\mvc\mvc\JsonData\Doctor.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                //serialize object directly into file stream
                serializer.Serialize(file, doc);
            }
            return doc;




            //var DocList = new List<Doctor>();
            //var client = new HttpClient();
            //var GetDAtaTask = client.GetAsync("http://localhost:53080/api/Doctorapi/").ContinueWith(response =>
            //{
            //    var result = response.Result;
            //    if (result.StatusCode == System.Net.HttpStatusCode.OK)
            //    {
            //        var readres = result.Content.ReadAsAsync<List<Doctor>>();
            //        readres.Wait();
            //        DocList = readres.Result;
            //    }
            //});
            //GetDAtaTask.Wait();
            //return DocList;
        }

        //public ActionResult SearchDoc(string option, string search)
        //{
        //    var doc = new Doctor();
        //    if (option == "Speciality")
        //    {
        //        using (SqlConnection sqlConnection = new SqlConnection(Constr))
        //        {
        //            SqlCommand cmd = new SqlCommand("select * from Doctor where Speciality = " + search, sqlConnection);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            sqlConnection.Open();
        //            SqlDataReader reader = cmd.ExecuteReader();
        //            while (reader.Read())
        //            {
        //                doc.id = Convert.ToInt32(reader["Id"]);
        //                doc.name = reader["Name"].ToString();
        //                doc.Address = reader["Address"].ToString();
        //                doc.Speciality = reader["Speciality"].ToString();
        //                doc.Gender = reader["Gender"].ToString();
        //                doc.contact = reader["contact"].ToString();
        //                doc.City = reader["City"].ToString();
        //                doc.state = reader["state"].ToString();
        //                doc.Country = reader["Country"].ToString();
        //                doc.Degree = reader["Degree"].ToString();
        //                return View(doc);
        //            }
        //        }
        //        return doctor(db.Students.Where(x => x.Subjects == search || search == null).ToList().ToPagedList(pageNumber ?? 1, 3));
        //    }
        //    else if (option == "City")
        //    {
        //        using (SqlConnection sqlConnection = new SqlConnection(Constr))
        //        {
        //            SqlCommand cmd = new SqlCommand("select * from Doctor where City = " + search, sqlConnection);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            sqlConnection.Open();
        //            SqlDataReader reader = cmd.ExecuteReader();
        //            while (reader.Read())
        //            {
        //                doc.id = Convert.ToInt32(reader["Id"]);
        //                doc.name = reader["Name"].ToString();
        //                doc.Address = reader["Address"].ToString();
        //                doc.Speciality = reader["Speciality"].ToString();
        //                doc.Gender = reader["Gender"].ToString();
        //                doc.contact = reader["contact"].ToString();
        //                doc.City = reader["City"].ToString();
        //                doc.state = reader["state"].ToString();
        //                doc.Country = reader["Country"].ToString();
        //                doc.Degree = reader["Degree"].ToString();
        //                return View(doc);
        //            }
        //        }
        //    }

        //    return null;
        //}
        public Doctor GetAllDoc(int id)
        {
            var DocList = new Doctor();
            var client = new HttpClient();
            var GetDAtaTask = client.GetAsync("http://localhost:53080/api/Doctorapi/id").ContinueWith(response =>
            {
                var result = response.Result;
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var readres = result.Content.ReadAsAsync<Doctor>();
                    readres.Wait();
                    DocList = readres.Result;
                }
            });
            GetDAtaTask.Wait();
            return DocList;
        }
        public ActionResult Create()
        {
            return View();
        }

        //The Post method
        [HttpPost]
        public ActionResult Create(Doctor doctor)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:53080/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage responseMessage = client.PostAsJsonAsync("api/Doctorapi/", doctor).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                // return responseMessage.Content.ReadAsAsync<Doctor>().Result;
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }

        public ActionResult Edit(int id)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:53080/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage responseMessage = client.GetAsync("api/Doctorapi/" + id).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var doc = responseMessage.Content.ReadAsAsync<Doctor>().Result;
                return View(doc);
                //return RedirectToAction("Index");
            }
            return RedirectToAction("Error");

            //var DocList = new Doctor();
            //var client = new HttpClient();
            //var GetDAtaTask = client.GetAsync("http://localhost:53080/api/Doctorapi/" + id).ContinueWith(response =>
            //{
            //    var result = response.Result;
            //    if (result.StatusCode == System.Net.HttpStatusCode.OK)
            //    {
            //        var readres = result.Content.ReadAsAsync<Doctor>();
            //        readres.Wait();
            //        DocList = readres.Result;
            //    }
            //});
            //GetDAtaTask.Wait();
            //return View(DocList);
        }

        //  The PUT Method
        [HttpPost]
        public ActionResult Edit(int id, Doctor doctor)
        {
            // DocList[id] = doctor;
            var client = new HttpClient();
            //client.BaseAddress = new Uri("http://localhost:53080/api/Doctorapi/" + id);
            client.BaseAddress = new Uri("http://localhost:53080/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage responseMessage = client.PostAsJsonAsync("api/Doctorapi/" + id, doctor).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }


        public ActionResult Delete(int id)
        {

            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:53080/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage responseMessage = client.GetAsync("api/Doctorapi/" + id).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var doc = responseMessage.Content.ReadAsAsync<Doctor>().Result;
                return View(doc);
                //return RedirectToAction("Index");
            }
            return RedirectToAction("Error");

            //var DocList = new Doctor();
            //var client = new HttpClient();
            //var GetDAtaTask = client.GetAsync("http://localhost:53080/api/Doctorapi/" + id).ContinueWith(response =>
            //{
            //    var result = response.Result;
            //    if (result.StatusCode == System.Net.HttpStatusCode.OK)
            //    {
            //        var readres = result.Content.ReadAsAsync<Doctor>();
            //        readres.Wait();
            //        DocList = readres.Result;
            //    }
            //});
            //GetDAtaTask.Wait();
            //return View(DocList);

        }

        //The DELETE method
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult Delete_Post(int id)
        {
            // DocList[id] = doctor;
            var client = new HttpClient();
            //client.BaseAddress = new Uri("http://localhost:53080/api/Doctorapi/" + id);
            client.BaseAddress = new Uri("http://localhost:53080/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage responseMessage = client.DeleteAsync("api/Doctorapi/" + id).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
            //HttpResponseMessage responseMessage = await client.DeleteAsync(url + "/" + id);
            //if (responseMessage.IsSuccessStatusCode)
            //{
            //    return RedirectToAction("Index");
            //}
            //return RedirectToAction("Error");
        }
    }
}





