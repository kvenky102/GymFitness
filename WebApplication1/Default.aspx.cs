using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using Newtonsoft.Json;

namespace WebApplication1
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public class Wrapper
        {
            public DataTable data { get; set; }
        }


        public void Membership_Data()
        {
            try
            {

                string Server_finalPath = Server.MapPath("~/App_Data/");
                if (!Directory.Exists(Server_finalPath))
                {
                    //If Directory (Folder) does not exists. Create it.
                    Directory.CreateDirectory(Server_finalPath);
                }

                // 1. Read JSON data from a file
                //string json = File.ReadAllText("C://Venkat//WorkingSource//SaiFitness//WebApplication1//App_Data//gym_members_with_subscription.json");
                String FileName = "gym_members_with_subscription.json";

                string json = File.ReadAllText(Server_finalPath + FileName);

                //Wrapper wrapper = JsonConvert.DeserializeObject<Wrapper>(json);
                //DataTable dt = wrapper.data;

                //json = "[" + json + "]";
                //DataTable dt1 = JsonConvert.DeserializeObject<DataTable>(json);

                // 2. Deserialize the JSON data into a DataTable
                DataTable dt = (DataTable)JsonConvert.DeserializeObject(json, (typeof(DataTable)));

                // Calculate end date, remaining days, and status using LINQ
                var updatedRows = dt.AsEnumerable()
                    .Select(row =>
                    {
                        //DateTime startDate = row.Field<DateTime>("Start Date");
                        DateTime startDate;
                        if (!DateTime.TryParse(row["Start Date"].ToString(), out startDate))
                        {
                            startDate = DateTime.Now; // Default value if parsing fails
                        }
                        DateTime endDate = startDate;
                        string subscriptionType = row.Field<string>("Subscription Type");

                        switch (subscriptionType)
                        {
                            case "Monthly":
                                endDate = startDate.AddMonths(1);
                                break;
                            case "Quarterly":
                                endDate = startDate.AddMonths(3);
                                break;
                            case "Half Yearly":
                                endDate = startDate.AddMonths(6);
                                break;
                            case "Yearly":
                                endDate = startDate.AddYears(1);
                                break;
                        }

                        int remainingDays = (endDate - DateTime.Now).Days;
                        string status = remainingDays > 0 ? "Active" : "Inactive";

                        row.SetField("End Date", endDate);
                        row.SetField("Remaining Days", remainingDays);
                        row.SetField("Status", status);

                        return row;
                    }).CopyToDataTable();

                // Replace the original DataTable with the updated one
                dt = updatedRows;


                // 4. Serialize the DataTable back to JSON and write it back to the file
                json = JsonConvert.SerializeObject(dt);
                // File.WriteAllText("C://Venkat//WorkingSource//SaiFitness//WebApplication1//App_Data//gym_members_with_subscription.json", json);
                File.WriteAllText(Server_finalPath + FileName, json);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static string Membership_Data_Fn_New()
        {
            try
            {
                string Server_finalPath = HttpContext.Current.Server.MapPath("~/App_Data/");
                if (!Directory.Exists(Server_finalPath))
                {
                    Directory.CreateDirectory(Server_finalPath);
                }

                string FileName = "gym_members_with_subscription.json";
                string json = File.ReadAllText(Path.Combine(Server_finalPath, FileName));

                DataTable dt = JsonConvert.DeserializeObject<DataTable>(json);

                var updatedRows = dt.AsEnumerable()
                    .Select(row =>
                    {
                        DateTime startDate;
                        if (!DateTime.TryParse(row["Start Date"].ToString(), out startDate))
                        {
                            startDate = DateTime.Now;
                        }
                        DateTime endDate = startDate;
                        string subscriptionType = row.Field<string>("Subscription Type");

                        switch (subscriptionType)
                        {
                            case "Monthly":
                                endDate = startDate.AddMonths(1);
                                break;
                            case "Quarterly":
                                endDate = startDate.AddMonths(3);
                                break;
                            case "Half Yearly":
                                endDate = startDate.AddMonths(6);
                                break;
                            case "Yearly":
                                endDate = startDate.AddYears(1);
                                break;
                        }

                        int remainingDays = (endDate - DateTime.Now).Days;
                        string status = remainingDays > 0 ? "Active" : "Inactive";

                        // Assign values
                        row.SetField("End Date", endDate.ToString("yyyy-MM-dd"));
                        row.SetField("Remaining Days", remainingDays);
                        row.SetField("Status", status);

                        return row;
                    }).CopyToDataTable();

                dt = updatedRows;

                // Convert DataTable to List of Dictionary for cleaner JSON output
                var list = dt.AsEnumerable().Select(row => dt.Columns.Cast<DataColumn>()
                    .ToDictionary(col => col.ColumnName, col => row[col])).ToList();

                return JsonConvert.SerializeObject(list);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { error = ex.Message });
            }
        }


        public static string Membership_Data_Fn()
        {
            try
            {
                string Server_finalPath = HttpContext.Current.Server.MapPath("~/App_Data/");
                if (!Directory.Exists(Server_finalPath))
                {
                    Directory.CreateDirectory(Server_finalPath);
                }

                string FileName = "gym_members_with_subscription.json";
                string json = File.ReadAllText(Path.Combine(Server_finalPath, FileName));

                DataTable dt = JsonConvert.DeserializeObject<DataTable>(json);

                var updatedRows = dt.AsEnumerable()
                    .Select(row =>
                    {
                        DateTime startDate;
                        if (!DateTime.TryParse(row["Start Date"].ToString(), out startDate))
                        {
                            startDate = DateTime.Now;
                        }
                        DateTime endDate = startDate;
                        string subscriptionType = row.Field<string>("Subscription Type");

                        switch (subscriptionType)
                        {
                            case "Monthly":
                                endDate = startDate.AddMonths(1);
                                break;
                            case "Quarterly":
                                endDate = startDate.AddMonths(3);
                                break;
                            case "Half Yearly":
                                endDate = startDate.AddMonths(6);
                                break;
                            case "Yearly":
                                endDate = startDate.AddYears(1);
                                break;
                        }

                        int remainingDays = (endDate - DateTime.Now).Days;
                        string status = remainingDays > 0 ? "Active" : "Inactive";

                        row.SetField("End Date", endDate.ToString("yyyy-MM-dd"));
                        row.SetField("Remaining Days", remainingDays);
                        row.SetField("Status", status);

                        return row;
                    }).CopyToDataTable();

                dt = updatedRows;

                var list = dt.AsEnumerable().Select(row => dt.Columns.Cast<DataColumn>()
                    .ToDictionary(col => col.ColumnName, col => row[col]?.ToString())).ToList();

                // Return a clean JSON response
                return JsonConvert.SerializeObject(new { success = true, data = list });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { success = false, error = ex.Message });
            }
        }

        public static string Membership_Data_Fn_old()
        {
            try
            {
                string Server_finalPath = HttpContext.Current.Server.MapPath("~/App_Data/");
                if (!Directory.Exists(Server_finalPath))
                {
                    // If Directory (Folder) does not exist, create it.
                    Directory.CreateDirectory(Server_finalPath);
                }

                // 1. Read JSON data from a file
                string FileName = "gym_members_with_subscription.json";
                string json = File.ReadAllText(Path.Combine(Server_finalPath, FileName));

                // 2. Deserialize the JSON data into a DataTable
                DataTable dt = (DataTable)JsonConvert.DeserializeObject(json, typeof(DataTable));

                // Calculate end date, remaining days, and status using LINQ
                var updatedRows = dt.AsEnumerable()
                    .Select(row =>
                    {
                        DateTime startDate;
                        if (!DateTime.TryParse(row["Start Date"].ToString(), out startDate))
                        {
                            startDate = DateTime.Now; // Default value if parsing fails
                        }
                        DateTime endDate = startDate;
                        string subscriptionType = row.Field<string>("Subscription Type");

                        switch (subscriptionType)
                        {
                            case "Monthly":
                                endDate = startDate.AddMonths(1);
                                break;
                            case "Quarterly":
                                endDate = startDate.AddMonths(3);
                                break;
                            case "Half Yearly":
                                endDate = startDate.AddMonths(6);
                                break;
                            case "Yearly":
                                endDate = startDate.AddYears(1);
                                break;
                        }

                        int remainingDays = (endDate - DateTime.Now).Days;
                        string status = remainingDays > 0 ? "Active" : "Inactive";

                        row.SetField("End Date", endDate);
                        row.SetField("Remaining Days", remainingDays);
                        row.SetField("Status", status);

                        return row;
                    }).CopyToDataTable();

                // Replace the original DataTable with the updated one
                dt = updatedRows;

                // 4. Serialize the DataTable back to JSON and write it back to the file
                json = JsonConvert.SerializeObject(dt);
                File.WriteAllText(Path.Combine(Server_finalPath, FileName), json);

                //// 4. Serialize the DataTable back to JSON
                //json = JsonConvert.SerializeObject(dt);

                // Return the JSON string
                return json;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void FileUpload()
        {
            try
            {


                // 1. Read JSON data from a file
                string json = File.ReadAllText("C://Venkat//WorkingSource//SaiFitness//WebApplication1//App_Data//gym_members_with_subscription.json");

                //Wrapper wrapper = JsonConvert.DeserializeObject<Wrapper>(json);
                //DataTable dt = wrapper.data;

                //json = "[" + json + "]";
                //DataTable dt1 = JsonConvert.DeserializeObject<DataTable>(json);

                // 2. Deserialize the JSON data into a DataTable
                DataTable dt = (DataTable)JsonConvert.DeserializeObject(json, (typeof(DataTable)));

                // Calculate end date, remaining days, and status using LINQ
                var updatedRows = dt.AsEnumerable()
                    .Select(row =>
                    {
                        //DateTime startDate = row.Field<DateTime>("Start Date");
                        DateTime startDate;
                        if (!DateTime.TryParse(row["Start Date"].ToString(), out startDate))
                        {
                            startDate = DateTime.Now; // Default value if parsing fails
                        }
                        DateTime endDate = startDate;
                        string subscriptionType = row.Field<string>("Subscription Type");

                        switch (subscriptionType)
                        {
                            case "Monthly":
                                endDate = startDate.AddMonths(1);
                                break;
                            case "Quarterly":
                                endDate = startDate.AddMonths(3);
                                break;
                            case "Half Yearly":
                                endDate = startDate.AddMonths(6);
                                break;
                            case "Yearly":
                                endDate = startDate.AddYears(1);
                                break;
                        }

                        int remainingDays = (endDate - DateTime.Now).Days;
                        string status = remainingDays > 0 ? "Active" : "Inactive";

                        row.SetField("End Date", endDate);
                        row.SetField("Remaining Days", remainingDays);
                        row.SetField("Status", status);

                        return row;
                    }).CopyToDataTable();

                // Replace the original DataTable with the updated one
                dt = updatedRows;


                // 4. Serialize the DataTable back to JSON and write it back to the file
                json = JsonConvert.SerializeObject(dt);
                File.WriteAllText("C://Venkat//WorkingSource//SaiFitness//WebApplication1//App_Data//gym_members_with_subscription.json", json);


                string filePath = "-";

                //Check whether Directory (Folder) exists.
                string Server_finalPath = Server.MapPath("~/Files/");
                if (!Directory.Exists(Server_finalPath))
                {
                    //If Directory (Folder) does not exists. Create it.
                    Directory.CreateDirectory(Server_finalPath);
                }

                DateTime todayDate = DateTime.Now; //MM/DD/YYYY
                String ConvertTodayDate = todayDate.ToString("dd.MM.yyyy HH.mm.ss");
                string strFileName = "SaiFitness_export_" + "" + ConvertTodayDate + "" + ".xlsx";
                FileUpload1.SaveAs(Server_finalPath + strFileName);
                Session["FileName"] = strFileName;

                // filePath = folderPath + Path.GetFileName(FU_SelectFile.FileName);
                filePath = Server_finalPath + strFileName;

                if (filePath != "-")
                {

                    DataTable dtUploadFileinDB = new DataTable();
                    dtUploadFileinDB = ConvertExcelToDataTable(filePath);

                    string date;
                    DateTime date_Curr = DateTime.Now;
                    TimeSpan time = new TimeSpan(0, 5, 30, 0);
                    DateTime Date_Time_Curr_combined = date_Curr.Add(time);
                    date = Date_Time_Curr_combined.ToString();


                    // Calculate end date, remaining days, and status using LINQ
                    var updatedRows1 = dtUploadFileinDB.AsEnumerable()
                        .Select(row =>
                        {
                            DateTime startDate = row.Field<DateTime>("Start Date");
                            DateTime endDate = startDate;
                            string subscriptionType = row.Field<string>("Subscription Type");

                            switch (subscriptionType)
                            {
                                case "Monthly":
                                    endDate = startDate.AddMonths(1);
                                    break;
                                case "Quarterly":
                                    endDate = startDate.AddMonths(3);
                                    break;
                                case "Half Yearly":
                                    endDate = startDate.AddMonths(6);
                                    break;
                                case "Yearly":
                                    endDate = startDate.AddYears(1);
                                    break;
                            }

                            int remainingDays = (endDate - DateTime.Now).Days;
                            string status = remainingDays > 0 ? "Active" : "Inactive";

                            row.SetField("End Date", endDate);
                            row.SetField("Remaining Days", remainingDays);
                            row.SetField("Status", status);

                            return row;
                        }).CopyToDataTable();

                    // Replace the original DataTable with the updated one
                    dtUploadFileinDB = updatedRows1;


                    dtUploadFileinDB.Columns.AddRange(new DataColumn[8] {

                         new DataColumn("Region",typeof(string)),
                         new DataColumn("Createdby_EmployeeID",typeof(string)),   new DataColumn("Createdby_Manager",typeof(string)),
                         new DataColumn("CreatedBy_Instance",typeof(string)), new DataColumn("CreatedBy_UserName",typeof(string)),
                          new DataColumn("Createdby_EmailID",typeof(string)),    new DataColumn("Createdby_DateTime",typeof(string)),
                        new DataColumn("Id_Code",typeof(string)),
                      });

                     

                    string IdCode = RandomString(20);
                    HttpContext.Current.Session["Id_Code"] = IdCode;
                    IEnumerable<DataRow> DataRows_IdCode = dtUploadFileinDB.Rows.Cast<DataRow>().Where(x => x["Id_Code"].ToString().Trim() == "");
                    if (DataRows_IdCode.Any())
                    {
                        DataRows_IdCode.ToList().ForEach(r => r.SetField("Id_Code", IdCode));
                    }
                   

                }

                if (File.Exists(Server_finalPath + strFileName))
                {
                    File.Delete(Server_finalPath + strFileName);
                }

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "any text", "<script type='text/javascript'>showCompletedDiv();</script>", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
            
        public static DataTable ConvertExcelToDataTable(string FileName)
        {
            DataTable dtResult = null;
            int totalSheet = 0; //No of sheets on excel file  

            using (OleDbConnection objConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';"))

            //using (OleDbConnection objConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.15.0;Data Source=" + FileName + ";Extended Properties='Excel 15.0;HDR=YES;IMEX=1;';"))
            {
                objConn.Open();
                OleDbCommand cmd = new OleDbCommand();
                OleDbDataAdapter oleda = new OleDbDataAdapter();
                DataSet ds = new DataSet();
                DataTable dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string sheetName = string.Empty;
                if (dt != null)
                {
                    //var resulList_Remove = new string[] { "FilterDatabase", "Sheet2$", "_xlnm#_FilterDatabase" };
                    //var resulList_Select = new string[] { "FilterDatabase", "Sheet2$", "_xlnm#_FilterDatabase" };
                    //var tempDataTable = (from dataRow in dt.AsEnumerable()
                    //                     where !dataRow["TABLE_NAME"].ToString().Contains("FilterDatabase") //BillingBlock$
                    //                     select dataRow).CopyToDataTable();

                    //var tempDataTable = (from dataRow in dt.AsEnumerable()
                    //                     where !resulList_Remove.Contains(dataRow["TABLE_NAME"].ToString()) //BillingBlock$
                    //                     select dataRow).CopyToDataTable();

                    var resulList_Remove = new string[] { "FilterDatabase", "_xlnm#_FilterDatabase" };
                    //var tempDataTable = (from dataRow in dt.AsEnumerable()
                    //                     where dataRow["TABLE_NAME"].ToString() == "Sheet1$" //BillingBlock$
                    //                     select dataRow).CopyToDataTable();
                    var tempDataTable = (from dataRow in dt.AsEnumerable()
                                         where !resulList_Remove.Contains(dataRow["TABLE_NAME"].ToString()) //BillingBlock$
                                         select dataRow).CopyToDataTable();

                    dt = tempDataTable;
                    totalSheet = dt.Rows.Count;
                    sheetName = dt.Rows[0]["TABLE_NAME"].ToString();
                }
                cmd.Connection = objConn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [" + sheetName + "]";
                oleda = new OleDbDataAdapter(cmd);
                oleda.Fill(ds, "excelData");
                dtResult = ds.Tables["excelData"];

                int dtCount = dtResult.Rows.Count;

                //List<DataRow> rows = dtResult.Rows.Cast<DataRow>().ToList();
                //List<DataRow> list = new List<DataRow>(dtResult.Select());

                objConn.Close();
                return dtResult; //Returning Dattable  
            }
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());

        }

        protected void FileUpload1_Load(object sender, EventArgs e)
        {
           
        }

        protected void btnSumbit_Click(object sender, EventArgs e)
        {
            //FileUpload();
            Membership_Data();
        }
    }
}