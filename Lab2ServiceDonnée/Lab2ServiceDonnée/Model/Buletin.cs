using Microsoft.AspNetCore.Http;

namespace Lab2ServiceDonnée.Model
{
    public class Buletin
    {
        public Cour Cour { get; set; }
        public int Note { get; set; }
        

        public Buletin(Cour cour,int note)
        {
            Cour= cour;
            Note= note;
        }
        public Buletin()
        { }
    }
}
