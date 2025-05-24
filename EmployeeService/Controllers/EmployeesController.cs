using EmployeeService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmployeeService.Controllers
{
    public class EmployeesController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage LoadAllEmp(string gender = "All")
        {
            List<Employee>employees = new List<Employee>(); 
            using (employeeDbEntities2
                 dbContext = new employeeDbEntities2())
            {
                employees = dbContext.Employees.ToList();
                //if(employees.Count==0)
                //{
                //    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Please Try again later");
                //}

                switch (gender.ToLower())
                {
                    case "all":
                        return Request.CreateResponse(HttpStatusCode.OK,dbContext.Employees.ToList());

                    case "male":
                        return Request.CreateResponse(HttpStatusCode.OK, dbContext.Employees.Where(e => e.Gender == "male").ToList());

                    case "female":
                        return Request.CreateResponse(HttpStatusCode.OK, dbContext.Employees.Where(e => e.Gender == "female").ToList());

                    default:
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "value for gender must be male or female " + gender + " is in valid");   
                }


            }
        }
        [HttpGet]
        public HttpResponseMessage LoadEmpById(int id)
        {
            using (employeeDbEntities2
                 dbContext = new employeeDbEntities2())
            {
                var emp = dbContext.Employees.FirstOrDefault(e => e.ID == id);
                if (emp != null) {
                    return Request.CreateResponse(HttpStatusCode.OK, emp);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Employee With Id "+id+" Not found");
                }
            }
        }

        public HttpResponseMessage Post(Employee emp)
        {
            using (employeeDbEntities2 dbContext = new employeeDbEntities2())
            {
                if(emp != null)
                {
                    dbContext.Employees.Add(emp);
                    dbContext.SaveChanges();  //eta na korle databae e jabe na 
                    return Request.CreateResponse(HttpStatusCode.Created, emp);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "please proovide  the info");
                }
            }
                
            
        }

        public HttpResponseMessage Put(int id ,Employee employee)
        {
            try
            {
                using (employeeDbEntities2 dbContext = new employeeDbEntities2())
                {
                    var emp = dbContext.Employees.FirstOrDefault(e => e.ID == id);
                    if (emp != null)
                    {
                        emp.FirstName = employee.FirstName;
                        emp.LastName = employee.LastName;
                        emp.Gender = employee.Gender;
                        emp.salary = employee.salary;


                        dbContext.SaveChanges();  //eta na korle databae e jabe na 
                        return Request.CreateResponse(HttpStatusCode.OK, emp);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "please proovide  the info");
                    }
                }


            }
            catch (Exception ex)
            { 
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
           
        }

        public HttpResponseMessage Delete(int id)
        {
            using (employeeDbEntities2 dbContext = new employeeDbEntities2())
            {
                var emp = dbContext.Employees.FirstOrDefault(e => e.ID == id);
                if (emp != null)
                {
                    dbContext.Employees.Remove(emp);


                    dbContext.SaveChanges();  //eta na korle databae e jabe na 
                    return Request.CreateResponse(HttpStatusCode.OK, emp);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "please proovide  the info");
                }
            }
        }


    }
}
