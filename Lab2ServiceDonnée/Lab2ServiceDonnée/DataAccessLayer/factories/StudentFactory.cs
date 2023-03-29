using Lab2ServiceDonnée.Model;
using MySql.Data.MySqlClient;

namespace Lab2ServiceDonnée.DataAccessLayer.factories
{
    public class StudentFactory
    {
        private Student CreateFromReader(MySqlDataReader mySqlDataReader)
        {
            string codePerma = mySqlDataReader["etu_code_permanent"].ToString() ?? string.Empty;
            string nom = mySqlDataReader["etu_nom"].ToString() ?? string.Empty;
            string prenom = mySqlDataReader["etu_prenom"].ToString() ?? string.Empty;
            DateTime naissance = Convert.ToDateTime(mySqlDataReader["etu_date_naissance"] ?? DateTime.MinValue);
            DateTime inscription= Convert.ToDateTime(mySqlDataReader["etu_date_inscription"] ?? DateTime.MinValue);
            DateTime? diplome;
            if (mySqlDataReader["etu_date_diplome"] == DBNull.Value)
            {
                diplome = DateTime.MinValue;
            }
            else
            {
                 diplome = Convert.ToDateTime(mySqlDataReader["etu_date_diplome"]);
            }
            int dA = Convert.ToInt32(mySqlDataReader["etu_num_da"]);



            return new Student(codePerma,  nom,  prenom,  naissance,  inscription,  diplome,  dA);
        }

        public List<Student> GetAll()
        {
            List<Student> Students = new List<Student>();
            MySqlConnection? mySqlCnn = null;
            MySqlDataReader? mySqlDataReader = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT * FROM tp5_etudiant";

                mySqlDataReader = mySqlCmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    Student Student = CreateFromReader(mySqlDataReader);
                    Students.Add(Student);
                }
            }
            finally
            {
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            return Students;
        }

        public Student? Get(string codePerma)
        {
            Student? Student = null;
            MySqlConnection? mySqlCnn = null;
            MySqlDataReader? mySqlDataReader = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT * FROM tp5_etudiant WHERE etu_code_permanent = @codePerma";
                mySqlCmd.Parameters.AddWithValue("@codePerma", codePerma);

                mySqlDataReader = mySqlCmd.ExecuteReader();
                if (mySqlDataReader.Read())
                {
                    Student = CreateFromReader(mySqlDataReader);
                }
            }
            finally
            {
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            return Student;
        }

        
    }
}
