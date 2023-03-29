using Lab2ServiceDonnée.DataAccessLayer.factories; 

namespace Lab2ServiceDonnée.DataAccessLayer
{
    public class DAL
    {
        
        private StudentFactory? _studentFact = null;
        private CourFactory? _courFactory = null;


        public static string? ConnectionString { get; set; }

        public StudentFactory StudentFactory
        {
            get
            {
                if (_studentFact == null)
                {
                    _studentFact = new StudentFactory();
                }

                return _studentFact;
            }
        }
        public CourFactory CourFactory
        {
            get
            {
                if (_courFactory == null)
                {
                    _courFactory = new CourFactory();
                }

                return _courFactory;
            }
        }



    }
}
