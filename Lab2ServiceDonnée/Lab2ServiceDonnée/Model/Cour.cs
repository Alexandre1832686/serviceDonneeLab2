namespace Lab2ServiceDonnée.Model
{
    public class Cour
    {
        public string Sigle { get; set; }
        public string Titre { get; set; }
        public int Duree { get; set; }

        public Cour(string sigle, string titre, int duree)
        {
            Sigle = sigle;
            Titre = titre;
            Duree = duree;
        }
        public Cour()
        { }
    }
}
