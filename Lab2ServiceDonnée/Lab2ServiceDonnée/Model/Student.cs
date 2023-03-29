namespace Lab2ServiceDonnée.Model
{
    public class Student
    {
        public string CodePerma { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;
        public DateTime Naissance { get; set; }
        public DateTime Inscription { get; set; }
        public DateTime? Diplome { get; set; }
        public int DA { get; set; }


        public Student()
        {

        }
        public Student(string codePerma,string nom,string prenom, DateTime naissance,DateTime inscription,DateTime? diplome,  int dA)
        {
            CodePerma = codePerma;
            Nom = nom;
            Prenom = prenom;
            Naissance = naissance;
            Inscription = inscription;
            Diplome = diplome;
            DA = dA;
        }
    }
}
