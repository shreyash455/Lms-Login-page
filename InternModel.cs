using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace LMS2.Models
{
    public class InternModel
    {
        public int? DesignationId { get; set; }
        public string DesignationName { get; set; }
        public bool IsActive { get; set; } = true;

        public List<InternDetails> GetInternsByDesignation()
        {
            List<InternDetails> interns = new List<InternDetails>();
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("usp_GetUsersByDesignation", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.Add(new SqlParameter("@DesignationId", SqlDbType.Int)).Value = (object)DesignationId ?? DBNull.Value;
                    command.Parameters.Add(new SqlParameter("@DesignationName", SqlDbType.VarChar, 100)).Value = (object)DesignationName ?? DBNull.Value;
                    command.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit)).Value = IsActive;

                    try
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                InternDetails intern = new InternDetails
                                {
                                    UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                                    FullName = reader.GetString(reader.GetOrdinal("FullName")),
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    UserAddress = reader.GetString(reader.GetOrdinal("UserAddress")),
                                    Pincode = reader.GetString(reader.GetOrdinal("Pincode")),
                                    MobileNumber = reader.GetString(reader.GetOrdinal("MobileNumber")),
                                    DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                                    YearOfPassout = reader.GetInt32(reader.GetOrdinal("YearOfPassout")),
                                    DateOfJoining = reader.GetDateTime(reader.GetOrdinal("DateOfJoining")),
                                    QualificationName = reader.GetString(reader.GetOrdinal("QualificationName")),
                                    DesignationName = reader.GetString(reader.GetOrdinal("DesignationName")),
                                    ModeName = reader.GetString(reader.GetOrdinal("ModeName"))
                                };
                                interns.Add(intern);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions (e.g., log the error)
                        throw new Exception("Error fetching interns by designation: " + ex.Message);
                    }
                }
            }

            return interns;
        }
    }

    public class InternDetails
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserAddress { get; set; }
        public string Pincode { get; set; }
        public string MobileNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int YearOfPassout { get; set; }
        public DateTime DateOfJoining { get; set; }
        public string QualificationName { get; set; }
        public string DesignationName { get; set; }
        public string ModeName { get; set; }
    }
}
