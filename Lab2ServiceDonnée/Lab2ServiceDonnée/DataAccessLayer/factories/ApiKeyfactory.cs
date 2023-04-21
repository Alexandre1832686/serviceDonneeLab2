using Lab2ServiceDonnée.Model;
using MySql.Data.MySqlClient;

namespace Lab2ServiceDonnée.DataAccessLayer.factories
{
    public class ApiKeyfactory
    {
        public bool checkApiKey(string apikey)
        {
            if(apikey.Length != 12)
            {
                return false;
            }

            //var
            bool verif = false;
            MySqlConnection? mySqlCnn = null;
            MySqlDataReader? mySqlDataReader = null;

            try
            {
                //preparation connection
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                //preparation requête
                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT * FROM apikeys WHERE value = @apikey";
                mySqlCmd.Parameters.AddWithValue("@apikey", apikey);
               

                //execution et lecture de la requête
                mySqlDataReader = mySqlCmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    verif = true;

                }
            }
            finally
            {
                //fermeture du lecteur et connection
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            //retourne la liste
            return (verif);
        }

        public bool GenerateApiKey(string apikey)
        {
            //var
            MySqlConnection? mySqlCnn = null;


            try
            {
                //preparation
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();
                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "INSERT INTO apikeys (value) VALUES (@apikey); ";

                mySqlCmd.Parameters.AddWithValue("@apikey", apikey);
                

                //execution
                int check = mySqlCmd.ExecuteNonQuery();

                if (check > 0)
                {
                    //si l'execution à modifier des lignes, retourne true
                    // fermeture
                    mySqlCnn?.Close();
                    return true;
                }

            }
            finally
            {
                // fermeture
                mySqlCnn?.Close();
            }

            //si n'a pas de retour true retourne false
            return false;
        }
    
}
