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
        SqlConnection con = new SqlConnection(CommonThings.GetConnectionString());
        public string LoginValidator(LoginDetails loginDetails)
        {
            string result=null;
            SqlCommand sda; 
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            sda = new SqlCommand(commonThings.loginValidator, con);
            SqlParameter p1 = new SqlParameter("@email", loginDetails.email);
            SqlParameter p2 = new SqlParameter("@secretPassword", loginDetails.password);
            sda.Parameters.Add(p1);
            sda.Parameters.Add(p2);

            SqlDataReader dr = sda.ExecuteReader();
            while (dr.Read())
            {
                result = Convert.ToString(dr[0]);
            }
            con.Close();
            return result;
        }
        public void MetricsFiller(MetricsDataDetails metricsData,string userName)
        {
            SqlCommand sda;
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            sda = new SqlCommand(commonThings.insertIntoMetricsData, con);
            sda.Parameters.AddWithValue("@userName", userName);
            sda.Parameters.AddWithValue("@applicationName",metricsData.applicationName.ToLower());
            sda.Parameters.AddWithValue("@taskDescription", metricsData.taskDescription);
            sda.Parameters.AddWithValue("@taskClassification", metricsData.taskClassification);
            sda.Parameters.AddWithValue("@startDate", metricsData.startDate);
            sda.Parameters.AddWithValue("@endDate", metricsData.endDate);
            sda.Parameters.AddWithValue("@effortHours", metricsData.effortHours);
            sda.ExecuteNonQuery();
            con.Close();
            
        }

        public int TotalDays(int startDate, int EndDate)
        {
            int[] Dates = new int[] { 1,2,8,9,13,15,16,22,23,29,30 };
           
            int tsd = startDate;
            int ed = EndDate;
            int ted = EndDate;
            int total = 0;
            List<int> absentDates = new List<int>();

            for (int i = startDate; i < EndDate + 1;)
            {
                absentDates.Add(tsd);
                tsd++;
                i++;
               
            }
            foreach (int item in absentDates)
            {
                if (!Dates.Contains(item))
                {
                    total++;
                }
            }
            return total;
        }
        public void LeaveDetailsFiller(LeaveDetails leaveDetails,string userName)
        {
            SqlCommand sda;
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            sda = new SqlCommand(commonThings.insertIntoLeaveDetails, con);
            sda.Parameters.AddWithValue("@userName", userName);
            sda.Parameters.AddWithValue("@startDate", leaveDetails.startDate);
            sda.Parameters.AddWithValue("@endDate", leaveDetails.endDate);
            int st = Convert.ToDateTime(leaveDetails.startDate).Day;
            int et = Convert.ToDateTime(leaveDetails.endDate).Day;
            sda.Parameters.AddWithValue("@noOfDays",TotalDays(st,et));
            sda.Parameters.AddWithValue("@vacationType", leaveDetails.vacationType);
            sda.Parameters.AddWithValue("@comment", leaveDetails.comment);
            sda.ExecuteNonQuery();
            con.Close();
        }
        public void LeaveChange(int id)
        {

        }
        public void CATWfiller(int hours,string userName)
        {
            SqlCommand sda;
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            sda = new SqlCommand(commonThings.updateCATWHours, con);
            sda.Parameters.AddWithValue("@userName", userName);
            sda.Parameters.AddWithValue("@totalHours", hours);
            sda.Parameters.AddWithValue("@manuallyFilled", "Yes");
            sda.ExecuteNonQuery();
            con.Close();
        }
        public int GetCatwHoursByUserName(string userName)
        {
            int result = 0;
            SqlCommand sda;
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            sda = new SqlCommand(commonThings.getCatwHoursByUserName, con);
            sda.Parameters.AddWithValue("@userName", userName);
            SqlDataReader dr = sda.ExecuteReader();
            if (dr.Read())
            {
                result = Convert.ToInt16(dr[0]);
            }
            return result;
              
        }
        //public List<MetricsDataDetails> AllMetricsGetter()
        //{
        //    SqlCommand sda;
        //    if (con.State != ConnectionState.Open)
        //    {
        //        con.Open();
        //    }
        //    sda = new SqlCommand(commonThings.allMetricsGetter, con);
        //    SqlDataReader dr = sda.ExecuteReader();
        //    List<MetricsDataDetails> list = new List<MetricsDataDetails>();
        //    while (dr.Read())
        //    {
        //        MetricsDataDetails metrics = new MetricsDataDetails()
        //        {
        //            id = Convert.ToInt32(dr[0]),
        //            applicationName = Convert.ToString(dr[1]),
        //            taskDescription = Convert.ToString(dr[2]),
        //            taskClassification = Convert.ToString(dr[3]),
        //            startDate = (Convert.ToDateTime(dr[4])).ToLongDateString(),
        //            endDate = (Convert.ToDateTime(dr[5])).ToLongDateString(),
        //            effortHours = Convert.ToDouble(dr[6])
        //        };
        //        list.Add(metrics);
        //    }
        //    con.Close();
        //    return list;
        //}
        public List<MetricsDataDetails> AllMetricsGetterbyUserName(string userName)
        {
            SqlCommand sda;
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            sda = new SqlCommand(commonThings.allMetricsGetterbyUserName, con);
            sda.Parameters.AddWithValue("@userName", userName);
            SqlDataReader dr = sda.ExecuteReader();
            List<MetricsDataDetails> list = new List<MetricsDataDetails>();
            while (dr.Read())
            {
                MetricsDataDetails metrics = new MetricsDataDetails()
                {
                    id = Convert.ToInt32(dr[0]),
                    applicationName = Convert.ToString(dr[1]),
                    taskDescription = Convert.ToString(dr[2]),
                    taskClassification = Convert.ToString(dr[3]),
                    startDate = (Convert.ToDateTime(dr[4])).ToLongDateString(),
                    endDate = (Convert.ToDateTime(dr[5])).ToLongDateString(),
                    effortHours = Convert.ToDouble(dr[6])
                };
                list.Add(metrics);
            }
            con.Close();
            return list;
        }
        public List<MetricsDataDetails> AllMetricsGetter()
        {
            SqlCommand sda;
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            sda = new SqlCommand(commonThings.allMetricsGetter, con);
            SqlDataReader dr = sda.ExecuteReader();
            List<MetricsDataDetails> list = new List<MetricsDataDetails>();
            while (dr.Read())
            {
                MetricsDataDetails metrics = new MetricsDataDetails()
                {
                    applicationName = Convert.ToString(dr[1]),
                    taskDescription = Convert.ToString(dr[2]),
                    taskClassification = Convert.ToString(dr[3]),
                    startDate = (Convert.ToDateTime(dr[4])).ToLongDateString(),
                    endDate = (Convert.ToDateTime(dr[5])).ToLongDateString(),
                    effortHours = Convert.ToDouble(dr[6]),
                    userName=Convert.ToString(dr[7])
                };
                list.Add(metrics);
            }
            con.Close();
            return list;
        }
        public List<LeaveDetails> AllLeaveDetailsbyUserName(string userName)
        {
            SqlCommand sda;
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            sda = new SqlCommand(commonThings.allLeaveDetailsbyUserName, con);
            sda.Parameters.AddWithValue("@userName", userName);
            SqlDataReader dr = sda.ExecuteReader();
            List<LeaveDetails> list = new List<LeaveDetails>();
            while (dr.Read())
            {
                LeaveDetails leaveDetails = new LeaveDetails()
                {
                    id=Convert.ToInt32(dr[0]),
                    startDate=(Convert.ToDateTime(dr[1])).ToLongDateString(),
                    endDate=(Convert.ToDateTime(dr[2])).ToLongDateString(),
                    vacationType=Convert.ToString(dr[3]),
                    comment=Convert.ToString(dr[4])
                };
                list.Add(leaveDetails);
            }
            con.Close();
            return list;
        }
        public List<LeaveDetails> AllLeaveDetailsGetter()
        {
            SqlCommand sda;
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            sda = new SqlCommand(commonThings.allLeaveDetailsGetter, con);
            SqlDataReader dr = sda.ExecuteReader();
            List<LeaveDetails> list = new List<LeaveDetails>();
            while (dr.Read())
            {
                LeaveDetails leaveDetails = new LeaveDetails()
                {
                    applicationName = Convert.ToString(dr[0]),
                    userName=Convert.ToString(dr[1]),
                    startDate = (Convert.ToDateTime(dr[2])).ToLongDateString(),
                    endDate = (Convert.ToDateTime(dr[3])).ToLongDateString(),
                    noOfDays=Convert.ToInt32(dr[4]),
                    vacationType = Convert.ToString(dr[5]),
                    comment = Convert.ToString(dr[6])
                };
                list.Add(leaveDetails);
            }
            con.Close();
            return list;
        }
        public List<StatusSheet> AllstatusSheet()
        {
            SqlCommand sda;
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            sda = new SqlCommand(commonThings.allstatussheet, con);
            SqlDataReader dr = sda.ExecuteReader();
            List<StatusSheet> list = new List<StatusSheet>();
            while (dr.Read())
            {
                StatusSheet statusSheet = new StatusSheet()
                {
                    employeeId=Convert.ToString(dr[0]),
                    userName=Convert.ToString(dr[1]),
                    applicationName=Convert.ToString(dr[2]),
                    totalhours=Convert.ToInt32(dr[3])
                };
                list.Add(statusSheet);
            }
            con.Close();
            return list;
        }
        public void MetricsDeleteByid(int id)
        {
            SqlCommand sda;
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            sda = new SqlCommand(commonThings.metricsDeletebyId, con);
            sda.Parameters.AddWithValue("@id", id);
            sda.ExecuteNonQuery();
            con.Close();
        }
        public void DeleteLeaveDetailsByid(int id)
        {
            SqlCommand sda;
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            sda = new SqlCommand(commonThings.deleteLeaveDetailsByid, con);
            sda.Parameters.AddWithValue("@id", id);
            sda.ExecuteNonQuery();
            con.Close();
        }
        public MetricsDataDetails MetricsGetterByid(int id)
        {
            SqlCommand sda;
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            sda = new SqlCommand(commonThings.metricsGetterbyId, con);
            sda.Parameters.AddWithValue("@id", id);
            SqlDataReader dr = sda.ExecuteReader();
            MetricsDataDetails no = new MetricsDataDetails();
            if (dr.Read())
            {
                MetricsDataDetails metricsDataDetails = new MetricsDataDetails()
                {
                    id = Convert.ToInt32(dr[0]),
                    applicationName = Convert.ToString(dr[2]),
                    taskDescription = Convert.ToString(dr[3]),
                    taskClassification = Convert.ToString(dr[4]),
                    startDate = (Convert.ToDateTime(dr[5])).ToLongDateString(),
                    endDate = (Convert.ToDateTime(dr[6])).ToLongDateString(),
                    effortHours = Convert.ToDouble(dr[7])
                };
                con.Close();
                return metricsDataDetails;
            }
            return no;
        }
        public List<NotFilled> AllEmployees()
        {
            List<NotFilled> emps = new List<NotFilled>();
            SqlCommand sda;
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            sda = new SqlCommand(commonThings.allEmployees, con);
            SqlDataReader dr = sda.ExecuteReader();
            MetricsDataDetails no = new MetricsDataDetails(); ;
            while (dr.Read())
            {
                NotFilled notFilled = new NotFilled()
                {
                    userName =Convert.ToString(dr[0]),
                    email = Convert.ToString(dr[1]),
                    applicationName=Convert.ToString(dr[2])
                };
                emps.Add(notFilled);
            }
            con.Close();
            return emps;
        }
        public List<NotFilled> NotFilledGuys()
        {
            List<NotFilled> notFilledList = new List<NotFilled>();
            foreach (var item in AllEmployees())
            {
                int i = 0;
                if (FilledMetrics(item.userName))
                    i++;
                if (LeavesFilled(item.userName))
                    i++;
                if (CatwFilled(item.userName))
                    i++;
                if (i != 0)
                    notFilledList.Add(item);
            }
            return notFilledList;
        }
        public bool FilledMetrics(string userName)
        {
            SqlCommand sda;
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            sda = new SqlCommand(commonThings.allMetricsGetterbyUserName, con);
            sda.Parameters.AddWithValue("@userName", userName);
            SqlDataReader dr = sda.ExecuteReader();
            if (dr.Read())
            {
                con.Close();
                return true;
            }
            else
            {
                con.Close();
                return false;
            }
        }
        public bool LeavesFilled(string userName)
        {
            SqlCommand sda;
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            sda = new SqlCommand(commonThings.allLeaveDetailsbyUserName, con);
            sda.Parameters.AddWithValue("@userName", userName);
            SqlDataReader dr = sda.ExecuteReader();
            if (dr.Read())
            {
                con.Close();
                return true;
            }
            else
            {
                con.Close();
                return false;
            }
        }
        public bool CatwFilled(string userName)
        {
            SqlCommand sda;
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            sda = new SqlCommand("CatwFilled @userName", con);
            sda.Parameters.AddWithValue("@userName", userName);
            SqlDataReader dr = sda.ExecuteReader();
            if (dr.Read())
            {
                con.Close();
                return true;
            }
            else
            {
                con.Close();
                return false;
            }
        }
    }
}