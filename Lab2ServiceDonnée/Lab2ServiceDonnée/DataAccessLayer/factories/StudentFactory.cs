using Lab2ServiceDonnée.Model;
using MySql.Data.MySqlClient;

namespace Lab2ServiceDonnée.DataAccessLayer.factories
{
    public class StudentFactory
    {
        private Student CreateFromReader(MySqlDataReader mySqlDataReader)
        {
            //remplie les variables avec les information du MysqlDataReader recu en paramètre
            string codePerma = mySqlDataReader["etu_code_permanent"].ToString() ?? string.Empty;
            string nom = mySqlDataReader["etu_nom"].ToString() ?? string.Empty;
            string prenom = mySqlDataReader["etu_prenom"].ToString() ?? string.Empty;
            DateTime naissance = Convert.ToDateTime(mySqlDataReader["etu_date_naissance"] ?? DateTime.MinValue);
            DateTime inscription= Convert.ToDateTime(mySqlDataReader["etu_date_inscription"] ?? DateTime.MinValue);
            DateTime? diplome;

            //seulement si la reponse n'est pas un DBNull
            if (mySqlDataReader["etu_date_diplome"] == DBNull.Value)
            {
                diplome = DateTime.MinValue;
            }
            else
            {
                 diplome = Convert.ToDateTime(mySqlDataReader["etu_date_diplome"]);
            }
            int dA = Convert.ToInt32(mySqlDataReader["etu_num_da"]);


            //construit un student et le renvoie
            return new Student(codePerma,  nom,  prenom,  naissance,  inscription,  diplome,  dA);
        }

        public List<Student> GetAll()
        {
            //var
            List<Student> Students = new List<Student>();
            MySqlConnection? mySqlCnn = null;
            MySqlDataReader? mySqlDataReader = null;

            try
            {
                //preparation
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();
                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT * FROM tp5_etudiant";

                //execute et lie
                mySqlDataReader = mySqlCmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    //créer un student et l'ajoute a la liste
                    Student Student = CreateFromReader(mySqlDataReader);
                    Students.Add(Student);
                }
            }
            finally
            {
                //fermeture
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            //revoie la liste
            return Students;
        }

        public Student? Get(string codePerma)
        {
            //var
            Student? Student = null;
            MySqlConnection? mySqlCnn = null;
            MySqlDataReader? mySqlDataReader = null;

            try
            {
                //preparation
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();
                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT * FROM tp5_etudiant WHERE etu_code_permanent = @codePerma";
                mySqlCmd.Parameters.AddWithValue("@codePerma", codePerma);

                //execution et lecture
                mySqlDataReader = mySqlCmd.ExecuteReader();
                if (mySqlDataReader.Read())
                {
                    //crer un Student et le met dans la variable
                    Student = CreateFromReader(mySqlDataReader);
                }
            }
            finally
            {
                //fermeture
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            //renvoi le student
            return Student;
        }
        public List<Student> GetStudentParCours(string cours)
        {
            //var
            List<Student> Students = new List<Student>();
            MySqlConnection? mySqlCnn = null;
            MySqlDataReader? mySqlDataReader = null;

            try
            {
                //preparation
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();
                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT * FROM view_etu_cours_actif WHERE cou_titre = @cours";
                mySqlCmd.Parameters.AddWithValue("@cours",cours);

                //execute et lie
                mySqlDataReader = mySqlCmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    //crée un student et ajoute à la liste
                    Student Student = CreateFromReader(mySqlDataReader);
                    Students.Add(Student);
                }
            }
            finally
            {
                //fermeture
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            //renvoie le student
            return Students;
        }
        public List<Student> GetDiplome(int annee)
        {
            //var
            List<Student> Students = new List<Student>();
            MySqlConnection? mySqlCnn = null;
            MySqlDataReader? mySqlDataReader = null;

            try
            {
                //preparation
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();
                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT * FROM tp5_etudiant WHERE YEAR(etu_date_diplome) = @annee;";
                mySqlCmd.Parameters.AddWithValue("@annee", annee);

                //execute et lie
                mySqlDataReader = mySqlCmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    //crer un student et l'ajoute à la liste
                    Student Student = CreateFromReader(mySqlDataReader);
                    Students.Add(Student);
                }
            }
            finally
            {
                //fermeture
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            //renvoie le student
            return Students;
        }


    }
}
