using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ObjectModel;
using System.Data.SqlClient;
using POS;

namespace Model.DataAccessLayer
{
    public class StaffDAO : IStaffDAO
    {
        /// <summary>
        /// Retreive staff record from the database.
        /// </summary>
        /// <param name="id">Staff id</param>
        /// <returns>Task Staff object</returns>
        public Staff getStaff(int id)
        {
            Staff staff = new Staff();
            staff.setID(id);

            string queryGetStaff = "SELECT * FROM Staff WHERE StaffID = @id";

            using (SqlConnection conn = new SqlConnection(Configuration.CONNECTION_STRING))
            {
                // define the command object
                SqlCommand cmd = new SqlCommand(queryGetStaff, conn);

                // parameterise
                SqlParameter idParam = new SqlParameter();
                idParam.ParameterName = "@id";
                idParam.Value = id;
                cmd.Parameters.Add(idParam);

                SqlDataReader reader;

                    // try a connection
                    conn.Open();

                    // execute the query
                    reader = cmd.ExecuteReader();
   

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        staff.setName(reader.GetString(1));
                        staff.setPasswordHash(reader.GetString(2));
                        string sPrivelege = reader.GetString(3);
                        switch (sPrivelege)
                        {
                            case "Admin":
                                staff.setPrivelege(Staff.Privelege.Admin);

                                break;

                            case "Normal":
                                staff.setPrivelege(Staff.Privelege.Normal);

                                break;

                            default:
                                // this shouldn't happen
                                // TODO: deal with it properly
                                throw new Exception("Invalid data in database");
                        }
                    }
                }
                else
                {
                    staff = null;
                }
            }

            return staff;
        }

        /// <summary>
        /// Retrieves a list of all staff records in the database.
        /// </summary>
        /// <returns>Task List of Staff objects.</returns>
        public List<Staff> getAllStaff()
        {
            List<Staff> staffList = new List<Staff>();

            string queryGetAllStaff = "SELECT * FROM Staff;";

            using (SqlConnection conn = new SqlConnection(Configuration.CONNECTION_STRING))
            {
                // define the command object
                SqlCommand cmd = new SqlCommand(queryGetAllStaff, conn);

                SqlDataReader reader;
                
                    // try a connection
                    conn.Open();

                    // execute the query
                    reader = cmd.ExecuteReader();
                

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Staff staff = new Staff();
                        staff.setID(reader.GetInt32(0));
                        staff.setName(reader.GetString(1));
                        staff.setPasswordHash(reader.GetString(2));
                        switch (reader.GetString(3))
                        {
                            case "Admin":
                                staff.setPrivelege(Staff.Privelege.Admin);

                                break;

                            case "Normal":
                                staff.setPrivelege(Staff.Privelege.Normal);

                                break;

                            default:
                                // this shouldn't happen
                                // TODO: deal with it properly
                                throw new Exception("Invalid data in database");
                        }

                        staffList.Add(staff);
                    }
                }
                else
                {
                    staffList = null;
                }
            }

            return staffList;
        }

        // this works
        /// <summary>
        /// Delete a staff record in the database.
        /// </summary>
        /// <param name="staff"></param>
        public void deleteStaff(int id)
        {
            // StaffID in the datbase is the PK
            // TODO: parameterise this
            string queryDeleteStaff = "DELETE FROM Staff WHERE StaffID = " + id + ";";

            using (SqlConnection conn = new SqlConnection(Configuration.CONNECTION_STRING))
            {
                SqlCommand cmd = new SqlCommand(queryDeleteStaff, conn);

                    // try a connection
                    conn.Open();

                    // execute the query
                    cmd.ExecuteNonQuery();
                
            }
            
            return;
        }

        /// <summary>
        /// Add a staff record to the database.
        /// </summary>
        /// <param name="staff">Staff object.</param>
        public void addStaff(Staff staff)
        {
            // StaffID in the database is PK and AI
            string queryAddStaff = "INSERT INTO Staff (FullName, PasswordHash, Privelege) " +
                                   "VALUES (@name, @password, @privelege);";

            using (SqlConnection conn = new SqlConnection(Configuration.CONNECTION_STRING))
            {
                // prepare the command
                SqlCommand cmd = new SqlCommand(queryAddStaff, conn);

                // parameterise
                SqlParameter nameParam = new SqlParameter();
                nameParam.ParameterName = "@name";
                nameParam.Value = staff.getName();
                cmd.Parameters.Add(nameParam);

                SqlParameter passParam = new SqlParameter();
                passParam.ParameterName = "@password";
                passParam.Value = staff.getPasswordHash();
                cmd.Parameters.Add(passParam);

                SqlParameter privParam = new SqlParameter();
                privParam.ParameterName = "@privelege";
                Staff.Privelege privelege = staff.getPrivelege();
                switch (privelege)
                {
                    case Staff.Privelege.Admin:
                        privParam.Value = "Admin";

                        break;

                    case Staff.Privelege.Normal:
                        privParam.Value = "Normal";

                        break;

                    default:
                        // this shouldn't happen
                        throw new Exception("Invalid staff data");
                }

                cmd.Parameters.Add(privParam);

               
                    // try a connection
                    conn.Open();

                    // execute the query
                    cmd.ExecuteNonQuery();
               
            }

            return;
        }

        /// <summary>
        /// Update a staff record in the database.
        /// </summary>
        /// <param name="staff">Staff object.</param>
        public void updateStaff(Staff staff)
        {
            // StaffID in the database is PK and AI
            string queryUpdateCustomer = "UPDATE Staff " +
                                         "SET FullName = @name, Passwordhash = @passHash, Privelege = @privelege " +
                                         "WHERE StaffID = @id;";

            using (SqlConnection conn = new SqlConnection(Configuration.CONNECTION_STRING))
            {
                SqlCommand cmd = new SqlCommand(queryUpdateCustomer, conn);

                // parameterise
                SqlParameter idParam = new SqlParameter();
                idParam.ParameterName = "@id";
                idParam.Value = staff.getID();
                cmd.Parameters.Add(idParam);

                SqlParameter nameParam = new SqlParameter();
                nameParam.ParameterName = "@name";
                nameParam.Value = staff.getName();
                cmd.Parameters.Add(nameParam);

                SqlParameter passParam = new SqlParameter();
                passParam.ParameterName = "@passHash";
                passParam.Value = staff.getPasswordHash();
                cmd.Parameters.Add(passParam);

                SqlParameter privParam = new SqlParameter();
                privParam.ParameterName = "@privelege";
                switch (staff.getPrivelege())
                {
                    case Staff.Privelege.Admin:
                        privParam.Value = "Admin";

                        break;

                    case Staff.Privelege.Normal:
                        privParam.Value = "Normal";

                        break;

                    default:
                        // should never happen
                        throw new Exception("Invalid staff data");
                }

                cmd.Parameters.Add(privParam);

               
                    // try a connection
                    conn.Open();

                    // execute the query
                    cmd.ExecuteNonQuery();
               
            }
        }
    }
}
