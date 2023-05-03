using Lab2ServiceDonnée.DataAccessLayer.factories; 

namespace Lab2ServiceDonnée.DataAccessLayer
{
    public class DAL
    {
        //var
        private StudentFactory? _studentFact = null;
        private CourFactory? _courFactory = null;
        private BuletinFactory? _buletinFactory= null;
        private ApiKeyfactory? _apiKeyfactory = null;
        private TravailFactory? _travailFactory = null;
        public static string? ConnectionString { get; set; }

        //public var avec le SET
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

        public BuletinFactory BuletinFactory
        {
            get
            {
                if (_buletinFactory == null)
                {
                    _buletinFactory = new BuletinFactory();
                }

                return _buletinFactory;
            }
        }
        public ApiKeyfactory ApiKeyfactory
        {
            get
            {
                if (_apiKeyfactory == null)
                {
                    _apiKeyfactory = new ApiKeyfactory();
                }

                return _apiKeyfactory;
            }
        }
        public TravailFactory TravailFactory
        {
            get
            {
                if (_travailFactory == null)
                {
                    _travailFactory = new TravailFactory();
                }

                return _travailFactory;
            }
        }

    }
}
