using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data;

using System.Web.UI.WebControls;
using AspMvcAssign.Models;

namespace AspMvcAssign.Controllers
{
    public class ImportExcelController : Controller
    {
       
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);

        OleDbConnection Econ;

       
        public ActionResult Index()
        {
          
            return View();
        }
        [HttpPost]

        public ActionResult Index(HttpPostedFileBase file)

        {

            string filename = Guid.NewGuid() + Path.GetExtension(file.FileName);

            string filepath = "/Content/" + filename;

            file.SaveAs(Path.Combine(Server.MapPath("/Content"), filename));

            InsertExceldata(filepath, filename);
            using (var db = new Assignment2Entities3())
            {
                InfoRec data = new InfoRec()
                {
                    Activity = "ImportExcel",
                    Time = DateTime.Now
                };

                db.InfoRecs.Add(data);
                db.SaveChanges();
            }



            return RedirectToAction("Index","Details");

        }
        private void ExcelConn(string filepath)

        {

            string constr = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES;""", filepath);

            Econ = new OleDbConnection(constr);

        }
        private void InsertExceldata(string fileepath, string filename)

        {

            string fullpath = Server.MapPath("/Content/") + filename;

            ExcelConn(fullpath);

            string query = string.Format("Select * from [{0}]", "Sheet1$");

            OleDbCommand Ecom = new OleDbCommand(query, Econ);

            Econ.Open();

            DataSet ds = new DataSet();

            OleDbDataAdapter oda = new OleDbDataAdapter(query, Econ);

            Econ.Close();

            oda.Fill(ds);

            DataTable dt = ds.Tables[0];

            SqlBulkCopy objbulk = new SqlBulkCopy(con);

            objbulk.DestinationTableName = "Details";
            objbulk.ColumnMappings.Add("Id", "Id");

            objbulk.ColumnMappings.Add("Description", "Description");

           /* objbulk.ColumnMappings.Add("Timing", "Timing");*/

            con.Open();

            objbulk.WriteToServer(dt);

            con.Close();
            
        }
        /* [HttpPost]
         public async Task<ActionResult> ImportFile()
         {
             *//*string excelConnectionString = string.Empty;
             string uploadPath = "~/Uploads/";
             string filePath = Server.MapPath(uploadPath + fileUpload1.PostedFile.FileName);
             string fileExt = Path.GetExtension(fileUpload1.PostedFile.FileName);
             String strConnection = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
             if (fileExt == ".xls" || fileExt == "XLS")
             {
                 excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source='" + filePath + "'" + "; Extended Properties ='Excel 8.0;HDR=Yes'";
             }
             else if (fileExt == ".xlsx" || fileExt == "XLSX")
             {
                 excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=Excel 12.0;Persist Security Info=False";
             }
             OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
             OleDbCommand cmd = new OleDbCommand("Select * from [Sheet1$]", excelConnection);
             excelConnection.Open();
             OleDbDataReader dReader;
             dReader = cmd.ExecuteReader();
             SqlBulkCopy sqlBulk = new SqlBulkCopy(strConnection);
             sqlBulk.DestinationTableName = "Result";
             sqlBulk.WriteToServer(dReader);
             lblStatus.Text = "Congratulations! Successfully Imported.";
             excelConnection.Close();
 *//*
             return View("Index");
         }

         protected void Button2_Click(object sender, EventArgs e)
         {

         }
         string excelConnectionString = string.Empty;



             if (excelfile == null || excelfile.ContentLength == 0)
             {
                 ViewBag.Error = "There is no content in file<br>";
                 return View("Index");
             }
             else
             {
                 string strConnection = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
                 string fileName = Path.GetFileName(excelfile.FileName);
                 string path = Path.Combine(Server.MapPath("~/Content"), fileName);
                 if (excelfile.FileName.EndsWith("xls") || excelfile.FileName.EndsWith("XLS"))
                 {
                     excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source='" + path + "'" + "; Extended Properties ='Excel 8.0;HDR=Yes'";
                 }
                 else if (excelfile.FileName.EndsWith("xlsx") || excelfile.FileName.EndsWith("XLSX"))
                 {
                     excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=Excel 12.0;Persist Security Info=False";
                 }
                 else
                 {
                     ViewBag.Error = "File Type is Incorrect <br>";
                     return View("Index");
                 }
                 OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                 OleDbCommand cmd = new OleDbCommand("Select * from [Sheet1$]", excelConnection);
                 excelConnection.Open();
                 OleDbDataReader dReader;
                 dReader = cmd.ExecuteReader();
                 SqlBulkCopy sqlBulk = new SqlBulkCopy(strConnection);
                 sqlBulk.DestinationTableName = "Result";
                 sqlBulk.WriteToServer(dReader);

                 excelConnection.Close();
                 return View("Index");
             }*/
    }
}