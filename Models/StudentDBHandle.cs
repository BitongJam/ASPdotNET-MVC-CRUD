using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace CRUD.Models
{
    public class StudentDBHandle
    {
        private SqlConnection con;
        private void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["studentconn"].ToString();
            con = new SqlConnection(constring);
        }

        // **************** ADD NEW STUDENT *********************
        public bool AddStudent(StudentModel smodel)
        {
            connection();
            SqlCommand cmd = new SqlCommand("pro_addnewstudent", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@name", smodel.Name);
            cmd.Parameters.AddWithValue("@city", smodel.City);
            cmd.Parameters.AddWithValue("@address", smodel.Address);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        // ********** VIEW STUDENT DETAILS ********************
        public List<StudentModel> GetStudent()
        {
            connection();
            List<StudentModel> studentlist = new List<StudentModel>();

            SqlCommand cmd = new SqlCommand("pro_getstudentdetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sd.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                studentlist.Add(
                    new StudentModel
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Name = Convert.ToString(dr["Name"]),
                        City = Convert.ToString(dr["City"]),
                        Address = Convert.ToString(dr["Address"])
                    });
            }
            return studentlist;
        }

        // ***************** UPDATE STUDENT DETAILS *********************
        public bool UpdateDetails(StudentModel smodel)
        {
            connection();
            SqlCommand cmd = new SqlCommand("pro_updatestudentDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@studentId", smodel.Id);
            cmd.Parameters.AddWithValue("@studentName", smodel.Name);
            cmd.Parameters.AddWithValue("@studentCity", smodel.City);
            cmd.Parameters.AddWithValue("@studentAddress", smodel.Address);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        // ********************** DELETE STUDENT DETAILS *******************
        public bool DeleteStudent(int id)
        {
            connection();
            SqlCommand cmd = new SqlCommand("pro_deleteStudent", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@studentId", id);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }
    
}
}