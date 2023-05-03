using Lab2ServiceDonnée.Model;
using MySql.Data.MySqlClient;

namespace Lab2ServiceDonnée.DataAccessLayer.factories
{
    public class TravailFactory
    {
        internal Travail CreateFromReader(MySqlDataReader mySqlDataReader)
        {
            //var
            int note = -1;
            
            
            int id = Convert.ToInt32(mySqlDataReader["Tra_ID"]);
            int ponderation = Convert.ToInt32(mySqlDataReader["Tra_ponderation"]);
            DateTime remise = Convert.ToDateTime(mySqlDataReader["Tra_date_remise"] ?? DateTime.MinValue);


            //creer et renvoie le buletin
            return new Travail(id, ponderation, remise);
        }


        public List<Travail> TravailSemaine()
        {
            //var
            List<Travail> Travaux = new List<Travail>();
            MySqlConnection? mySqlCnn = null;
            MySqlDataReader? mySqlDataReader = null;

            try
            {
                //preparation
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();
                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = " SELECT Tra_ID, Tra_ponderation, Tra_date_remise FROM individuel_travail WHERE DATEDIFF(Tra_date_remise,NOW()) < 7;";

                //execution et lecture
                mySqlDataReader = mySqlCmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    //creation de cour et ajout à la liste
                    Travail travail = CreateFromReader(mySqlDataReader);
                    Travaux.Add(travail);
                }
            }
            finally
            {
                // fermeture
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            //retour
            return Travaux;
        }
        public List<Travail> TravailSemaineECSGP(int ECSGP_ID)
        {
            //var
            List<Travail> Travaux = new List<Travail>();
            MySqlConnection? mySqlCnn = null;
            MySqlDataReader? mySqlDataReader = null;

            try
            {
                //preparation
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();
                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT Tra_ID, Tra_ponderation, Tra_date_remise,TraCSG_Tra_ID FROM individuel_travail Join individuel_ecsg_travail on Tra_ID = TraCSG_Tra_ID WHERE DATEDIFF(Tra_date_remise,NOW()) < 7 AND TraCSG_Tra_ID = @ESCGP_ID;";
                mySqlCmd.Parameters.AddWithValue("@ESCGP_ID", ECSGP_ID);
                //execution et lecture
                mySqlDataReader = mySqlCmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    //creation de cour et ajout à la liste
                    Travail travail = CreateFromReader(mySqlDataReader);
                    Travaux.Add(travail);
                }
            }
            finally
            {
                // fermeture
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            //retour
            return Travaux;
        }
        public List<Travail> GetListeTravauxEtu(string CodePerma,int ECSGP_ID)
        {
            //var
            List<Travail> Travaux = new List<Travail>();
            MySqlConnection? mySqlCnn = null;
            MySqlDataReader? mySqlDataReader = null;

            try
            {
                //preparation
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();
                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT Tra_date_remise,Tra_ID,Tra_ponderation FROM tp5_etudiant_courssessiongroupeprof Join individuel_ecsg_travail on ecsgp_id = TraCSG_ECSG_ID join individuel_travail on Tra_ID = TraCSG_Tra_ID WHERE ecsgp_etu_codepermanent = @CodePermanant AND ecsgp_id = @ECSGP_ID;";
                mySqlCmd.Parameters.AddWithValue("@CodePerma", CodePerma);
                mySqlCmd.Parameters.AddWithValue("@ECSGP_ID", ECSGP_ID);
                //execution et lecture
                mySqlDataReader = mySqlCmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    //creation de cour et ajout à la liste
                    Travail travail = CreateFromReader(mySqlDataReader);
                    Travaux.Add(travail);
                }
            }
            finally
            {
                // fermeture
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            //retour
            return Travaux;
        }
        public bool CreerTravail(int ponderation, DateTime dateRemise)
        {
            //var
            
            MySqlConnection? mySqlCnn = null;
            MySqlDataReader? mySqlDataReader = null;

            try
            {
                //var
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();
                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "insert into individuel_travail (Tra_id, Tra_ponderation,Tra_date_remise) values (@ponderation,@dateRemise);";
                

                //preparation
                mySqlCmd.Parameters.AddWithValue("@ponderation", ponderation);
                mySqlCmd.Parameters.AddWithValue("@dateRemise", dateRemise);

                //execution
                
                int check = mySqlCmd.ExecuteNonQuery();
                //si la requête modifie plus de 0 ligne on retourne true
                if (check > 0)
                {
                    mySqlCnn?.Close();
                    return true;
                }
            }
            finally
            {
                //fermeture de connection
                mySqlCnn?.Close();
            }

            //si true n'est pas retourné retourne false
            return false;
        }


    }
}
