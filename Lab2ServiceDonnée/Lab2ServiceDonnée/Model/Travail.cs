namespace Lab2ServiceDonnée.Model
{
    public class Travail
    {

        public int Id { get; set; }
        public int Ponderation { get; set; }
        public DateTime DateRemise { get; set; }

        public Travail(int id, int ponderation, DateTime dateRemise)
        {
            Id = id;
            Ponderation = ponderation;
            DateRemise = dateRemise;
        }
        public Travail()
        { }
    }
}
