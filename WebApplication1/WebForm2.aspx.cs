using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
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
    }
}