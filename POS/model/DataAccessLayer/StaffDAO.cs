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
        public Staff getStaff(int id)
        {
            Staff staff = new Staff();
            staff.setID(id);

            string queryGetStaff = "SELECT * FROM Staff WHERE StaffID = @id";

            try
            {
                using (SqlConnection conn = new SqlConnection(Configuration.CONNECTION_STRING))
                {
                    // define the command object
                    SqlCommand cmd = new SqlCommand(queryGetStaff, conn);

                    // parameterise
                    SqlParameter idParam = new SqlParameter();
                    idParam.ParameterName = "@id";
                    idParam.Value = id;
                    cmd.Parameters.Add(idParam);

                    // try a connection
                    conn.OpenAsync();

                    // execute the query
                    Task<SqlDataReader> readerTask = cmd.ExecuteReaderAsync();
                    SqlDataReader reader = readerTask.Result;
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
                                throw new Exception("Invalid data in database");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return staff;
        }

        public IList<Staff> getAllStaff()
        {
            IList<Staff> staffList = new List<Staff>();

            string queryGetAllStaff = "SELECT * FROM Staff;";

            try
            {
                using (SqlConnection conn = new SqlConnection(Configuration.CONNECTION_STRING))
                {
                    // define the command object
                    SqlCommand cmd = new SqlCommand(queryGetAllStaff, conn);

                    // try a connection
                    conn.OpenAsync();

                    // execute the query
                    Task<SqlDataReader> readerTask = cmd.ExecuteReaderAsync();
                    SqlDataReader reader = readerTask.Result;
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
                                throw new Exception("Invalid data in database");
                        }

                        staffList.Add(staff);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return staffList;
        }

        // this works
        public async void deleteStaff(Staff staff)
        {
            // StaffID in the datbase is the PK
            int id = staff.getID();

            string queryDeleteStaff = "DELETE FROM Staff WHERE StaffID = " + id + ";";

            try
            {
                using (SqlConnection conn = new SqlConnection(Configuration.CONNECTION_STRING))
                {
                    SqlCommand cmd = new SqlCommand(queryDeleteStaff, conn);

                    // try a connection
                    await conn.OpenAsync();

                    // execute the query
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return;
        }

        public async void addStaff(Staff staff)
        {
            // StaffID in the database is PK and AI
            string queryAddStaff = "INSERT INTO Staff (FullName, PasswordHash, Privelege) " +
                                   "VALUES (@name, @password, @privelege);";
 
            try
            {
                using (SqlConnection conn = new SqlConnection(Configuration.CONNECTION_STRING))
                {
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
                    await conn.OpenAsync();

                    // execute the query
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return;
        }

        public async void updateStaff(Staff staff)
        {
            // StaffID in the database is PK and AI


            string queryUpdateCustomer = "UPDATE Staff " +
                                         "SET FullName = @name, Passwordhash = @passHash, Privelege = @privelege " +
                                         "WHERE StaffID = @id;";

            try
            {
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
                    await conn.OpenAsync();

                    // execute the query
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }
    }
}
