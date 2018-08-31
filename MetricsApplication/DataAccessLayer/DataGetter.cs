using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MetricsApplication.BussinessEntity;
using MetricsApplication.Common;

namespace MetricsApplication.DataAccessLayer
{
    public class DataGetter
    {
        CommonThings commonThings = new CommonThings();
        private string GetConnectionString()
        {
            return CommonThings.GetConnectionString();
        }
        public string LoginValidator(LoginDetails loginDetails)
        {
            string result=null;
            SqlCommand sda;
            SqlConnection con = new SqlConnection(GetConnectionString());
        
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            sda = new SqlCommand(commonThings.loginValidator, con);
            SqlParameter p1 = new SqlParameter("@email", loginDetails.email);
            SqlParameter p2 = new SqlParameter("@password", loginDetails.password);
            sda.Parameters.Add(p1);
            sda.Parameters.Add(p2);

            SqlDataReader dr = sda.ExecuteReader();
            while (dr.Read())
            {
                result = Convert.ToString(dr[0]);
            }
            return result;
        }
        public void MetricsFiller(MetricsData metricsData)
        {

        }
        public void MetricsChange(int id)
        {

        }
        public void LeaveDetailsFiller(LeaveDetails leaveDetails)
        {

        }
        public void LeaveChange(int id)
        {

        }
        public void CATWfiller(int hours)
        {

        }
    }
}