using Lab2ServiceDonn�e.DataAccessLayer.factories;
using Lab2ServiceDonn�e.Model;
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