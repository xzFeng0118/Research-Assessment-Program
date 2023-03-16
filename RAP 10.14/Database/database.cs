using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using RAP.Research;
using RAP.Controller;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace RAP.Database
{
    class database
    {
        //Database details
        private const string db = "kit206";
        private const string user = "kit206";
        private const string pass = "kit206";
        private const string server = "alacritas.cis.utas.edu.au";

        private static MySqlConnection conn = null;

        public ObservableCollection<Researcher> allReseachers = null;
        public ObservableCollection<Position> allPositions = null;
        public ObservableCollection<Publication> allPublications = null;

        //A bool to control whether the error is actually displayed or silently ignored
        private static bool reportingErrors = false;

        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        //Creates connection to the database
        private static MySqlConnection GetConnection()
        {
            if (conn == null)
            {
                //Note: This approach is not thread-safe
                string connectionString = String.Format("Database={0};Data Source={1};User Id={2};Password={3}", db, server, user, pass);
                conn = new MySqlConnection(connectionString);
            }
            return conn;
        }

        //Load all researcher detalis from database, ordered by asc
        public static List<Researcher> LoadAll()
        {
            List<Researcher> staff = new List<Researcher>();

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(" SELECT id, given_name, family_name, title, photo, campus, IFNULL(level,'Student'), unit, email, current_start, utas_start from researcher order by given_name ASC", conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    //Get Researcher data
                    staff.Add(new Researcher
                    {
                        Id = rdr.GetInt32(0),
                        GivenName = rdr.GetString(1),
                        FamilyName = rdr.GetString(2),
                        Title = rdr.GetString(3),
                        Unit = rdr.GetString(7),
                        Campus = rdr.GetString(5),
                        Email = rdr.GetString(8),
                        Photo = rdr.GetString(4),
                        //Photo = new Uri("rdr.GetString(4)", UriKind.RelativeOrAbsolute),   tried to get the uri type to make the photo display, failed.
                        CurrentStart = rdr.GetDateTime(9),
                        UtasStart = rdr.GetDateTime(10),
                        CurrentLevel = ParseEnum<level>(rdr.GetString(6)),
                    });
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Loading Researcher: " + e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return staff;
        }

        //Display previous job/position name 
        public static string GetPositionName(string level)
        {
            //initialise the variable job
            string job = "";

            switch (level)
            {
                case (string)("Student"):
                    job = "Student";
                    break;
                case (string)("A"):
                    job = "PostDoc";
                    break;
                case (string)("B"):
                    job = "Lecturer";
                    break;
                case (string)("C"):
                    job = "Senior Lecturer";
                    break;
                case (string)("D"):
                    job = "Associate Professor";
                    break;
                case (string)("E"):
                    job = "Professor";
                    break;
            }
            return job;

        }

        



        //Get data from position table in database.
        public static List<Position> LoadPosition(int id)
        {
            List<Position> pos = new List<Position>();
            MySqlConnection conn = GetConnection(); //Connect to the database
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("Select level, start, end from position where end is not null and id=?id", conn);

                cmd.Parameters.AddWithValue("id", id);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    pos.Add(new Position
                    {
                        Level = ParseEnum<level>(rdr.GetString(0)),
                        Start = rdr.GetDateTime(1),
                        End = rdr.GetDateTime(2),
                        PositionName = GetPositionName(rdr.GetString(0))
                        
                    });
                }
            }
            catch (MySqlException e)
            {
                ReportError("Loading Position", e);  //Report error
            }
            finally
            {
                if (rdr != null)  //Disconnet
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return pos;
        }

        //Get data from Publication table in database.
        public static List<Publication> LoadPublication(int id)
        {
            List<Publication> publcation = new List<Publication>();
            MySqlConnection conn = GetConnection();  //Connect to the database
            MySqlDataReader rdr = null;
            try
            {
                conn.Open();
                //Choose all publication with same researcher id, ordered by year desc and by title first character asc
                MySqlCommand cmd = new MySqlCommand("Select p.doi, title, authors, year, type, cite_as, available from publication as p, researcher_publication as rp where p.doi=rp.doi and researcher_id=?id order by year DESC, title ASC", conn);
                cmd.Parameters.AddWithValue("id", id);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    publcation.Add(new Publication
                    {
                        Doi = rdr.GetString(0),
                        PublicationTitle = rdr.GetString(1),
                        Authors = rdr.GetString(2),
                        Year = rdr.GetInt32(3),  
                        PublicationType = ParseEnum<OutputType>(rdr.GetString(4)),
                        CiteAs = rdr.GetString(5),
                        Available = rdr.GetDateTime(6),
                    });
                }
            }
            catch (MySqlException e)
            {
                ReportError("Loading Publication", e); //Report error
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)  //Disconnect
                {
                    conn.Close();
                }
            }
            return publcation;
        }

        //Get the name for supervisor through their ID
        public static string GetName(int id)
        {

            string SupervisorName = "null";
            MySqlConnection conn = GetConnection();  //Connect to the database
            MySqlDataReader rdr = null;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("Select given_name, family_name from researcher where id=?id", conn);
                cmd.Parameters.AddWithValue("id", id);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    SupervisorName = rdr.GetString(0) + " " + rdr.GetString(1);
                }
            }
            catch (MySqlException e)
            {
                ReportError("Loading Name", e);  //Reprot Error
            }
            finally
            {
                if (rdr != null)  //Disconnect
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return SupervisorName;
        }

        //Get data for staffs who are the supervisors of other people
        public static Staff LoadStaff(int id)
        {
            Staff staff = new Staff();
            List<Researcher> students = new List<Researcher>();
            MySqlConnection conn = GetConnection();  //Connect to the database
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select t.id, s.id, s.given_name, s.family_name from researcher t, researcher s where t.id = s.supervisor_id and t.id=?id", conn);
                cmd.Parameters.AddWithValue("id", id);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    students.Add(new Researcher
                    {
                        GivenName = rdr.GetString(2),
                        FamilyName = rdr.GetString(3)
                    });

                }
            }
            catch (MySqlException e)
            {
                ReportError("Loading staff", e);  //Reprot Error
            }
            finally
            {
                if (rdr != null)  //Disconnect
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
            staff.Student = students;
            return staff;

        }

        //Get student data from researcher table, and define their degree, supervisor
        public static Student LoadStudent(int id)
        {
            Student student = new Student();
            MySqlConnection conn = GetConnection();  //Connect to the database
            MySqlDataReader rdr = null;
            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT type, degree, supervisor_id from researcher where researcher.id=?id", conn);
                cmd.Parameters.AddWithValue("id", id);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if (rdr.GetString(0) == "Student"){
                        student.Degree = rdr.GetString(1);
                        student.SupervisorId = rdr.GetInt32(2);
                    }
                    //student.Degree = rdr.GetString(0);
                    //student.SupervisorId = rdr.GetInt32(1);
                }
            }
            catch (MySqlException e)
            {

                ReportError("Loading Student", e);  //Report Error
            }
            finally
            {
                if (rdr != null)  //Disconnect
                {

                    rdr.Close();
                }
                if (conn != null)
                {

                    conn.Close();
                }
            }

            return student;
        }

        //Count the number of publications from same author
        public static List<PublicationCount> CntPublications(int id) 
        {
            List<PublicationCount> cum = new List<PublicationCount>();
            MySqlConnection conn = GetConnection();  //Connect to the database
            MySqlDataReader rdr = null;
            try
            {

                conn.Open();
                //Count researchers' publications group by id and year
                MySqlCommand cmd = new MySqlCommand("select r.id,p.year, count(p.doi) from researcher r, researcher_publication rp, publication p where r.id = rp.researcher_id and p.doi = rp.doi and r.id=?id group by r.id, p.year", conn);


                cmd.Parameters.AddWithValue("id", id);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    cum.Add(new PublicationCount
                    {

                        Year = rdr.GetInt32(1),
                        Count = rdr.GetInt32(2)
                    });

                }
            }
            catch (MySqlException e)
            {
                ReportError("Loading Number of Publications", e);  //Report Error
            }
            finally
            {
                if (rdr != null)  //Disconnect
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return cum;
        }

        //Get all publications from one author
        public ObservableCollection<Publication> GetPubByID(int id)
        {
            ObservableCollection<Publication> filteredPublications = new ObservableCollection<Publication>();
            try
            {
                conn.Open();
                string query = "SELECT * FROM publication WHERE doi in (SELECT doi FROM researcher_publication WHERE researcher_id=?id) order by year ASC;";
                MySqlCommand sqlQuery = new MySqlCommand(query, conn);
                sqlQuery.Parameters.AddWithValue("id", id);

                MySqlDataReader mySqlReader = sqlQuery.ExecuteReader();

                while (mySqlReader.Read())
                {
                    Publication p = new Publication();
                    p.Doi = mySqlReader.GetString(0);
                    p.PublicationTitle = mySqlReader.GetString(1);
                    p.Authors = mySqlReader.GetString(2);
                    p.Year = mySqlReader.GetInt32(3);
                    p.PublicationType = ParseEnum<OutputType>(mySqlReader.GetString(4));
                    p.CiteAs = mySqlReader.GetString(5);
                    p.Available = mySqlReader.GetDateTime(6);
                    filteredPublications.Add(p);
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Loading Filter Publication By RID : " + e);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return filteredPublications;
        }


        //Report error
        private static void ReportError(string msg, Exception a)
        {
            if (reportingErrors)
            {

                MessageBox.Show("An error occurred while " + msg + ". Try again later.\n\nError Details:\n" + a,
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
