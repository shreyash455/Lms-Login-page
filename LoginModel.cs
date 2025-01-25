using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace Lms.Models
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string UserPassword { get; set; }
        public string AddLoginRecord()
        {
            string addLoginRecordDetailsReturn = "";
            string connetionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection oConnection = new SqlConnection(connetionString))
            {
                oConnection.Open();
                using (SqlCommand oCommand = oConnection.CreateCommand())
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.CommandText = "sp_UserLogin";

                    oCommand.Parameters.Add(new SqlParameter("@Username", SqlDbType.VarChar)).Value = Username;
                    oCommand.Parameters.Add(new SqlParameter("@UserPassword", SqlDbType.VarChar)).Value = @UserPassword;


                    try
                    {
                        oCommand.ExecuteNonQuery();
                        addLoginRecordDetailsReturn = "Login Successfully";
                    }
                    catch (Exception ex)
                    {
                        // Capture the exception message and store it in the return variable
                        addLoginRecordDetailsReturn = "Error: " + ex.Message;
                    }
                }
            }
            return addLoginRecordDetailsReturn;
        }

    }
}
