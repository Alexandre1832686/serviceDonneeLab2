using Lab2ServiceDonnée.Model;
using MySql.Data.MySqlClient;

namespace Lab2ServiceDonnée.DataAccessLayer.factories
{
    public class CourFactory
    {

        private Cour CreateFromReader(MySqlDataReader mySqlDataReader)
        {
            //associe var au données recu en paramètre par le MySqlDataReader
            string sigle = mySqlDataReader["cou_sigle"].ToString() ?? string.Empty;
            string titre = mySqlDataReader["cou_titre"].ToString() ?? string.Empty;
            int duree = Convert.ToInt32(mySqlDataReader["cou_duree"]);

            //construit et renvoie un cour
            return new Cour( sigle,  titre,  duree);
        }

        public List<Cour> listeCour(string codePerma)
        {
            //var
            List<Cour> Cours = new List<Cour>();
            MySqlConnection? mySqlCnn = null;
            MySqlDataReader? mySqlDataReader = null;

            try
            {
                //preparation
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();
                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT * FROM view_etu_cours WHERE etu_code_permanent = @codePerma";
                mySqlCmd.Parameters.AddWithValue("@codePerma", codePerma);

                //execution et lecture
                mySqlDataReader = mySqlCmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    //creation de cour et ajout à la liste
                    Cour cour = CreateFromReader(mySqlDataReader);
                    Cours.Add(cour);
                }
            }
            finally
            {
                //fermeture
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            //retour
            return Cours;
        }
        public List<Cour> listeCourSessionActive(string codePerma)
        {
            //var
            List<Cour> Cours = new List<Cour>();
            MySqlConnection? mySqlCnn = null;
            MySqlDataReader? mySqlDataReader = null;

            try
            {
                //preparation
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();
                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT * FROM view_etu_cours WHERE etu_code_permanent = @codePerma";
                mySqlCmd.Parameters.AddWithValue("@codePerma", codePerma);

                //execution et lecture
                mySqlDataReader = mySqlCmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    //creation de cour et ajout à la liste
                    Cour cour = CreateFromReader(mySqlDataReader);
                    Cours.Add(cour);
                }
            }
            finally
            {
                // fermeture
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            //retour
            return Cours;
        }

        public List<Cour> listeCourEnseignant(string prenom, string nom)
        {
            //var
            List<Cour> Cours = new List<Cour>();
            MySqlConnection? mySqlCnn = null;
            MySqlDataReader? mySqlDataReader = null;

            try
            {
                //preparation
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();
                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = " SELECT cou_sigle, cou_titre, cou_duree FROM tp5_cours inner Join tp5_enseignant ON cou_sigle WHERE en_prenom = @Prenom AND en_nom = @Nom;";
                mySqlCmd.Parameters.AddWithValue("@Prenom", prenom);
                mySqlCmd.Parameters.AddWithValue("@Nom", nom);

                //execution et lecture
                mySqlDataReader = mySqlCmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    //creation de cour et ajout à la liste
                    Cour cour = CreateFromReader(mySqlDataReader);
                    Cours.Add(cour);
                }
            }
            finally
            {
                // fermeture
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            //retour
            return Cours;
        }

        public bool CreateCour(string sigle, string titre, int duree)
        {
            //var
            MySqlConnection? mySqlCnn = null;
            

            try
            {
                //preparation
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();
                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "INSERT INTO tp5_cours (cou_sigle,  cou_titre,  cou_duree) VALUES (@sigle, @titre, @duree); ";
                mySqlCmd.Parameters.AddWithValue("@sigle", sigle);
                mySqlCmd.Parameters.AddWithValue("@titre", titre);
                mySqlCmd.Parameters.AddWithValue("@duree", duree);

                //execution
                int check = mySqlCmd.ExecuteNonQuery();
                if (check > 0)
                {
                    //si l'execution à modifier des lignes, retourne true
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

        public bool AddEtuToCour(string CodePerma, string Sigle)
        {
            //var
            MySqlConnection? mySqlCnn = null;
            MySqlDataReader? mySqlDataReader = null;
            string id ="";

            try
            {
                //preparation
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();
                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = " SELECT csgp_id FROM h23_intro_services_tp5_1832686.tp5_cours_session_groupe_prof WHERE csgp_sigle_cours = @sigle;";
                mySqlCmd.Parameters.AddWithValue("@sigle", Sigle);

                //execution
                mySqlDataReader = mySqlCmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    //set la variable id 
                    id = mySqlDataReader["csgp_id"].ToString() ?? string.Empty;

                }

            }
            finally
            {
                //ferme le lecteur
                mySqlDataReader ?.Close();
                mySqlCnn?.Close();
            }

            try
            {
                //var
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();
                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "INSERT INTO tp5_etudiant_courssessiongroupeprof (ecsgp_csgp_id, ecsgp_etu_codepermanent, ecsgp_resultat) VALUES (@ecsgp_csgp_id,@ecsgp_etu_codepermanent,null); ";
                bool test = Int32.TryParse(id, out int result);
                //si le test ne passe pas on retourne false
                if(!test)
                {
                    mySqlCnn?.Close();
                    return false;
                }

                //preparation
                mySqlCmd.Parameters.AddWithValue("@ecsgp_csgp_id", result);
                mySqlCmd.Parameters.AddWithValue("@ecsgp_etu_codepermanent", CodePerma);

                //execution
                int check = mySqlCmd.ExecuteNonQuery();
                //si la requête modifie plus de 0 ligne on retourne true
                if (check > 0)
                {
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


        public bool ModifieProfCour(string PrenomProf, string NomProf, string Sigle)
        {
            //var
            MySqlConnection? mySqlCnn = null;
            MySqlDataReader? mySqlDataReader = null;
            string id = "";

            try
            {
                //preparation
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();
                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = " SELECT en_id FROM tp5_enseignant WHERE en_prenom = @Prenom AND en_nom = @Nom;";
                mySqlCmd.Parameters.AddWithValue("@Prenom", PrenomProf);
                mySqlCmd.Parameters.AddWithValue("@Nom", NomProf);

                //execution et lecture
                mySqlDataReader = mySqlCmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    //set la variable id
                    id = mySqlDataReader["en_id"].ToString() ?? string.Empty;
                }
            }
            finally
            {
                //fermeture du lecteur et connection
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            //SEULEMENT si le id n'est pas null
            if(id != "")
            {
                try
                {
                    //preparation
                    mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                    mySqlCnn.Open();
                    MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                    mySqlCmd.CommandText = "UPDATE tp5_cours_session_groupe_prof SET csgp_id_prof = @id WHERE csgp_sigle_cours = @sigle; ";
                    mySqlCmd.Parameters.AddWithValue("@sigle", Sigle);
                    mySqlCmd.Parameters.AddWithValue("@id", id);
                    
                    //execution
                    int check = mySqlCmd.ExecuteNonQuery();
                    //si la requête modifie plus de 0 ligne on retourne true
                    if (check > 0)
                    {
                        return true;
                    }

                }
                finally
                {
                    //fermeture de la connection
                    mySqlCnn?.Close();
                }
            }

            // si ne retourne pas true plus haut, on retourne false
            return false;
        }

        public bool ModifieNote(string Prenom, string Nom, int Note, string Sigle, int Session)
        {
            //var
            MySqlConnection? mySqlCnn = null;
            MySqlDataReader? mySqlDataReader = null;
            string code = "";
            int id =0;

            try
            {
                //preparation
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();
                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT etu_code_permanent FROM tp5_etudiant WHERE etu_nom = @nom AND etu_prenom = @prenom;";
                mySqlCmd.Parameters.AddWithValue("@prenom", Prenom);
                mySqlCmd.Parameters.AddWithValue("@nom", Nom);

                //execution et lecture
                mySqlDataReader = mySqlCmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    //set la variable code
                    code = mySqlDataReader["etu_code_permanent"].ToString() ?? string.Empty;
                }

            }
            finally
            {
                //fermeture reader et connection
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            try
            {
                //preparation
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();
                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT csgp_id FROM tp5_cours_session_groupe_prof WHERE csgp_sigle_cours = @Sigle AND csgp_id_session = @Session;";
                mySqlCmd.Parameters.AddWithValue("@Sigle", Sigle);
                mySqlCmd.Parameters.AddWithValue("@Session", Session);

                //execute et lecture
                mySqlDataReader = mySqlCmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    //set la variable id
                    id = Convert.ToInt32(mySqlDataReader["csgp_id"]);
                }

            }
            finally
            {
                //ferme le lecteur et connection
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            //seulement si les variable code et id ont été modifiés
            if (code != "" && id != 0)
            {
                try
                {
                    //preparation
                    mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                    mySqlCnn.Open();
                    MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                    mySqlCmd.CommandText = "UPDATE tp5_etudiant_courssessiongroupeprof SET ecsgp_resultat = @note WHERE ecsgp_etu_codepermanent = @code AND ecsgp_id=@id; ";
                    mySqlCmd.Parameters.AddWithValue("@note", Note);
                    mySqlCmd.Parameters.AddWithValue("@code", code);
                    mySqlCmd.Parameters.AddWithValue("@id", id);


                    //execution
                    int check = mySqlCmd.ExecuteNonQuery();
                    //si on modifie plus d'une ligne on retourne true
                    if (check > 0)
                    {
                        return true;
                    }

                }
                finally
                {
                    //ferme la connection
                    mySqlCnn?.Close();
                }
            }

            //sinon retourne false
            return false;
        }
        public bool DeleteEtuCour(string Prenom, string Nom, string Sigle, int Session)
        {
            //var
            MySqlConnection? mySqlCnn = null;
            MySqlDataReader? mySqlDataReader = null;
            string code = "";
            int id = 0;

            try
            {
                //preparation
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();
                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT etu_code_permanent FROM tp5_etudiant WHERE etu_nom = @nom AND etu_prenom = @prenom;";
                mySqlCmd.Parameters.AddWithValue("@prenom", Prenom);
                mySqlCmd.Parameters.AddWithValue("@nom", Nom);

                //execute and read
                mySqlDataReader = mySqlCmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    //set variable code
                    code = mySqlDataReader["etu_code_permanent"].ToString() ?? string.Empty;
                }
            }
            finally
            {
                //fermeture reader et connection
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            try
            {
                //preparation
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();
                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT csgp_id FROM tp5_cours_session_groupe_prof WHERE csgp_sigle_cours = @Sigle AND csgp_id_session = @Session;";
                mySqlCmd.Parameters.AddWithValue("@Sigle", Sigle);
                mySqlCmd.Parameters.AddWithValue("@Session", Session);

                //execute et lie
                mySqlDataReader = mySqlCmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    // set la variable id
                    id = Convert.ToInt32(mySqlDataReader["csgp_id"]);
                }

            }
            finally
            {
                //ferme la connection et le lecteur
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            //seulement si les variables code et id ont été modifiés
            if (code != "" && id != 0)
            {
                try
                {
                    //preparation
                    mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                    mySqlCnn.Open();
                    MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                    mySqlCmd.CommandText = "DElete from tp5_etudiant_courssessiongroupeprof WHERE ecsgp_csgp_id = @id AND ecsgp_etu_codepermanent = @code;";
                    mySqlCmd.Parameters.AddWithValue("@code", code);
                    mySqlCmd.Parameters.AddWithValue("@id", id);


                    //execute
                    int check = mySqlCmd.ExecuteNonQuery();
                    //si la requête a modifier plus de 0 lignes retourne true
                    if (check > 0)
                    {
                        return true;
                    }

                }
                finally
                {
                    //ferme la connection
                    mySqlCnn?.Close();
                }
            }

            //si true n'a pas été renvoyé renvoie false
            return false;
        }

    }
}
