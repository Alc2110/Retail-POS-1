using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ObjectModel;
using System.Data.SqlClient;
using POS;

namespace Model.DataAccessLayer.SqlServerInterface
{
    // this class might be faked for testing, but won't be tested itself
    public class StaffDAO : IStaffDAO
    {
        // connection string
        public string connString { get; set; }

        // default constructor
        // loads the connection string
        public StaffDAO()
        {
            this.connString = Configuration.CONNECTION_STRING;
        }

        // constructor with parameter
        public StaffDAO(string connString)
        {
            this.connString = connString;
        }

        /// <summary>
        /// Retreive staff record from the database.
        /// </summary>
        /// <param name="id">Staff id</param>
        /// <returns>Task Staff object</returns>
        public IStaff getStaff(int id)
        {
            IStaff staff = new Staff();
            staff.StaffID = id;
            // prepare the query
            string queryGetStaff = "SELECT * FROM Staff WHERE StaffID = @id";

            using (SqlConnection conn = new SqlConnection(this.connString))
            {
                // prepare the command
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
                        staff.FullName = reader.GetString(1);
                        staff.PasswordHash = reader.GetString(2);
                        string sPrivelege = reader.GetString(3);
                        switch (sPrivelege)
                        {
                            case "Admin":
                                staff.privelege = Staff.Privelege.Admin;

                                break;

                            case "Normal":
                                staff.privelege = Staff.Privelege.Normal;

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
        public IEnumerable<IStaff> getAllStaff()
        {
            string queryGetAllStaff = "SELECT * FROM Staff;";

            using (SqlConnection conn = new SqlConnection(this.connString))
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
                        IStaff staff = new Staff();
                        staff.StaffID = reader.GetInt32(0);
                        staff.FullName = reader.GetString(1);
                        staff.PasswordHash = reader.GetString(2);
                        switch (reader.GetString(3))
                        {
                            case "Admin":
                                staff.privelege = Staff.Privelege.Admin;

                                break;

                            case "Normal":
                                staff.privelege = Staff.Privelege.Normal;

                                break;

                            default:
                                // this shouldn't happen
                                // TODO: deal with it properly
                                throw new Exception("Invalid data in database");
                        }

                        yield return staff;
                    }
                }
                else
                {

                }
            }
        }

        // this works
        /// <summary>
        /// Delete a staff record in the database.
        /// </summary>
        /// <param name="staff"></param>
        public void deleteStaff(int id)
        {
            // StaffID in the datbase is the PK
            // prepare the query
            string queryDeleteStaff = "DELETE FROM Staff WHERE StaffID = @id";

            using (SqlConnection conn = new SqlConnection(this.connString))
            {
                // prepare the command
                SqlCommand cmd = new SqlCommand(queryDeleteStaff, conn);

                // parameterise the query
                SqlParameter idParam = new SqlParameter();
                idParam.ParameterName = "@id";
                idParam.Value = id;
                cmd.Parameters.Add(idParam);

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
        public void addStaff(IStaff staff)
        {
            // StaffID in the database is PK and AI
            string queryAddStaff = "INSERT INTO Staff (FullName, PasswordHash, Privelege) " +
                                   "VALUES (@name, @password, @privelege);";

            using (SqlConnection conn = new SqlConnection(this.connString))
            {
                // prepare the command
                SqlCommand cmd = new SqlCommand(queryAddStaff, conn);

                // parameterise
                SqlParameter nameParam = new SqlParameter();
                nameParam.ParameterName = "@name";
                nameParam.Value = staff.FullName;
                cmd.Parameters.Add(nameParam);

                SqlParameter passParam = new SqlParameter();
                passParam.ParameterName = "@password";
                passParam.Value = staff.PasswordHash;
                cmd.Parameters.Add(passParam);

                SqlParameter privParam = new SqlParameter();
                privParam.ParameterName = "@privelege";
                Staff.Privelege privelege = staff.privelege;
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
        public void updateStaff(IStaff staff)
        {
            // StaffID in the database is PK and AI
            string queryUpdateCustomer = "UPDATE Staff " +
                                         "SET FullName = @name, Passwordhash = @passHash, Privelege = @privelege " +
                                         "WHERE StaffID = @id;";

            using (SqlConnection conn = new SqlConnection(this.connString))
            {
                SqlCommand cmd = new SqlCommand(queryUpdateCustomer, conn);

                // parameterise
                SqlParameter idParam = new SqlParameter();
                idParam.ParameterName = "@id";
                idParam.Value = staff.StaffID;
                cmd.Parameters.Add(idParam);

                SqlParameter nameParam = new SqlParameter();
                nameParam.ParameterName = "@name";
                nameParam.Value = staff.FullName;
                cmd.Parameters.Add(nameParam);

                SqlParameter passParam = new SqlParameter();
                passParam.ParameterName = "@passHash";
                passParam.Value = staff.PasswordHash;
                cmd.Parameters.Add(passParam);

                SqlParameter privParam = new SqlParameter();
                privParam.ParameterName = "@privelege";
                switch (staff.privelege)
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

        /// <summary>
        /// Perform import/update operation. 
        /// If there is no staff record with this ID, create it.
        /// Else, update it with the remaining fields.
        /// </summary>
        /// <param name="staff">Staff interface</param>
        public void importUpdateStaff(IStaff staff)
        {
            if (getStaff(staff.StaffID) == null)
            {
                addStaff(staff);
            }
            else
            {
                updateStaff(staff);
            }
        }
    }
}
