using Lab2ServiceDonnée.Model;
using MySql.Data.MySqlClient;

namespace Lab2ServiceDonnée.DataAccessLayer.factories
{
    public class CourFactory
    {

        private Cour CreateFromReader(MySqlDataReader mySqlDataReader)
        {
            string sigle = mySqlDataReader["cou_sigle"].ToString() ?? string.Empty;
            string titre = mySqlDataReader["cou_titre"].ToString() ?? string.Empty;
            int duree = Convert.ToInt32(mySqlDataReader["cou_duree"]);

            return new Cour( sigle,  titre,  duree);
        }

        public List<Cour> listeCour(string codePerma)
        {
            List<Cour> Cours = new List<Cour>();
            MySqlConnection? mySqlCnn = null;
            MySqlDataReader? mySqlDataReader = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT * FROM view_etu_cours WHERE etu_code_permanent = @codePerma";
                mySqlCmd.Parameters.AddWithValue("@codePerma", codePerma);

                mySqlDataReader = mySqlCmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    Cour cour = CreateFromReader(mySqlDataReader);
                    Cours.Add(cour);
                }
            }
            finally
            {
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            return Cours;

        }
        public List<Cour> listeCourSessionActive(string codePerma)
        {
            List<Cour> Cours = new List<Cour>();
            MySqlConnection? mySqlCnn = null;
            MySqlDataReader? mySqlDataReader = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT * FROM view_etu_cours WHERE etu_code_permanent = @codePerma";
                mySqlCmd.Parameters.AddWithValue("@codePerma", codePerma);

                mySqlDataReader = mySqlCmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    Cour cour = CreateFromReader(mySqlDataReader);
                    Cours.Add(cour);
                }
            }
            finally
            {
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            return Cours;

        }
    }
}
