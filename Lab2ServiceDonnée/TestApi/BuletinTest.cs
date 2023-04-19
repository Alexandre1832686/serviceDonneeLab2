using Lab2ServiceDonnée.DataAccessLayer.factories;
using Lab2ServiceDonnée.Model;
using MySql.Data.MySqlClient;

namespace TestApi
{
    public class BuletinTest
    {
        BuletinFactory buletinfacto;
        [SetUp]
        public void Setup()
        {
             buletinfacto = new BuletinFactory();
        }

        

        [Test]
        public void TestGetBuletin()
        { 

        }
    }
}