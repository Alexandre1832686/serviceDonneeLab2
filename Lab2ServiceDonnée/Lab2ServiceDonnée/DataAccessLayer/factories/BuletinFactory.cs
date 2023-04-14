using Lab2ServiceDonnée.Model;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Lab2ServiceDonnée.DataAccessLayer.factories
{
    public class BuletinFactory
    {

        /*
         
         EN CAS DE COURS EN COUR.....

        si on aperçoit des notes de -1, il s'agit d'un cour dans lequel la note n'est toujours pas entré.
         
         */





        private Buletin CreateFromReader(MySqlDataReader mySqlDataReader)
        {
            //var
            int note = -1;
            string sigle = mySqlDataReader["cou_sigle"].ToString() ?? string.Empty;
            string titre = mySqlDataReader["cou_titre"].ToString() ?? string.Empty;
            int duree = Convert.ToInt32(mySqlDataReader["cou_duree"]);

            //evite de cast un DBNull en int 
            if (mySqlDataReader["ecsgp_resultat"] != DBNull.Value)
            {
                 note = Convert.ToInt32(mySqlDataReader["ecsgp_resultat"]);
            }

            //creer et renvoie le buletin
            return new Buletin(new Cour(sigle,titre,duree),note);
        }
        public List<Buletin> GetBuletin(string prenom, string nom)
        {
            //var
            List<Buletin> ListeBult = new List<Buletin>();
            MySqlConnection? mySqlCnn = null;
            MySqlDataReader? mySqlDataReader = null;

            try
            {
                //preparation connection
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                //preparation requête
                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT ecsgp_resultat,cou_sigle, cou_titre, cou_duree FROM tp5_etudiant_courssessiongroupeprof inner join tp5_etudiant On ecsgp_etu_codepermanent = etu_code_permanent LEft JOIN tp5_cours_session_groupe_prof ON tp5_cours_session_groupe_prof.csgp_id = tp5_etudiant_courssessiongroupeprof.ecsgp_csgp_id JOIN tp5_cours on tp5_cours.cou_sigle = tp5_cours_session_groupe_prof.csgp_sigle_cours WHERE etu_prenom = @prenom AND etu_nom = @nom;";
                mySqlCmd.Parameters.AddWithValue("@Prenom", prenom);
                mySqlCmd.Parameters.AddWithValue("@Nom", nom);

                //execution et lecture de la requête
                mySqlDataReader = mySqlCmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    //creer un buletin et l'ajoute à la liste
                    Buletin bult = CreateFromReader(mySqlDataReader);
                    ListeBult.Add(bult);

                }
            }
            finally
            {
                //fermeture du lecteur et connection
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            //retourne la liste
            return (ListeBult);
        }
    }
}