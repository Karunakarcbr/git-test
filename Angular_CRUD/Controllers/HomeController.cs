using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using Angular_CRUD.Models;
using System.Configuration;


namespace Angular_CRUD.Controllers
{
    public class HomeController : Controller
    {
        
        public JsonResult GetName(string Name)
        {

                var cn = new SqlConnection();
                var ds = new DataSet();
                string strCn = ConfigurationManager.ConnectionStrings["testconnection"].ToString();
                cn.ConnectionString = strCn;
                var cmd = new SqlCommand
                {
                    Connection = cn,
                    CommandType = CommandType.Text,
                    CommandText = "select Emp_Name from Employee Where Emp_Name like @myParameter and Emp_Name!=@myParameter2"
                };
                cmd.Parameters.AddWithValue("@myParameter", "%" + Name + "%");
                cmd.Parameters.AddWithValue("@myParameter2",  Name );
                try
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    var da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    cn.Close();
                }
                DataTable dt = ds.Tables[0];


                var txtItems = (from DataRow row in dt.Rows
                                select row["Emp_Name"].ToString()
                                    into dbValues
                                    select dbValues.ToLower()).ToList();
                return Json(txtItems, JsonRequestBehavior.AllowGet);
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        //get all data
        public JsonResult GetAll()
        {
            using (testEntities1 db = new testEntities1())
            {
                List<Employee> emp = db.Employees.ToList();
                return Json(emp, JsonRequestBehavior.AllowGet);
            }
            
        }
        // get by id
        public JsonResult GetById(string id)
        {
            using (testEntities1 db = new testEntities1())
            {
                int emp_id = int.Parse(id);
                return Json(db.Employees.Find(emp_id), JsonRequestBehavior.AllowGet);
            }
        }
        public string Insert_data(Employee empl)
        {
            if(empl != null)
            {
                using(testEntities1 db =new testEntities1())
                {
                    db.Employees.Add(empl);
                    db.SaveChanges();
                    return "Added Successfully";
                }
            }
            else
            {
                return "try again";
            }
        }
        
        public string Delete_emp(Employee Emp )
        {
            if (Emp != null)
            {
                using (testEntities1 Obj = new testEntities1())
                {
                    var Emp_ = Obj.Entry(Emp);
                    if (Emp_.State == System.Data.Entity.EntityState.Detached)
                    {
                        Obj.Employees.Attach(Emp);
                        Obj.Employees.Remove(Emp);
                    }
                    Obj.SaveChanges();
                    return "Employee Deleted Successfully";
                }
            }
            else
            {
                return "Employee Not Deleted! Try Again";
            }  
            //if (id != null)
            //{
            //    var id_ = int.Parse(id);
            //    using(testEntities1 db= new testEntities1())
            //    {
            //        var Emp_ = db.Employees.Where(a => a.Emp_Id == id_).ToList();
            //        foreach (var item in Emp_)
            //        {
            //            db.Employees.Remove(item);
            //        }
            //        db.SaveChanges();
            //    }
            //    return "Employee Deleted id:" +id_;
            //}
            //else
            //{
            //    return "Try Again id:" + id;
            //}
        }
        public string update(Employee emp)
        {
            if (emp != null)
            {
                using(testEntities1 db = new testEntities1())
                {
                    var Empl = db.Entry(emp);
                    Employee empobj = db.Employees.Where(x => x.Emp_Id == emp.Emp_Id).FirstOrDefault();
                    empobj.Emp_Name = emp.Emp_Name;
                    empobj.Emp_City = emp.Emp_City;
                    empobj.Emp_Age = emp.Emp_Age;
                    db.SaveChanges();
                }
                return "updated";
            }
            else
            {
                return "try again";
            }
        }
    }
}