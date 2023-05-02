using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab2ServiceDonnée.DataAccessLayer;
using Lab2ServiceDonnée.DataAccessLayer.factories;
using Lab2ServiceDonnée.Model;

namespace TestApi
{
    internal class CourTest
    {
        
        CourFactory courFacto;
        [SetUp]
        public void Setup()
        {
           
            DAL dal = new DAL();
            DAL.ConnectionString = "Server=sql.decinfo-cchic.ca;Port=33306;Database=h23_intro_services_tp5_1832686;Uid=dev-1832686;Pwd=Info2020";
            courFacto = dal.CourFactory;
        }

        [Test]
        public void TestListeCour()
        {
            List<Cour> liste = new List<Cour>();
            List<Cour> listeInvalide = new List<Cour>();
            try
            {
                liste = courFacto.listeCour("ABCD11111111");
                listeInvalide = courFacto.listeCour("");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            if (liste.Count == 0)
            {
                Assert.Fail();
            }
            if (listeInvalide.Count != 0)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void TestlisteCourSessionActive()
        {
            List<Cour> liste = new List<Cour>();
            List<Cour> listeInvalide = new List<Cour>();
            try
            {
                liste = courFacto.listeCourSessionActive("ABCD11111111");
                listeInvalide = courFacto.listeCourSessionActive("");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            if (liste.Count == 0)
            {
                Assert.Fail();
            }
            if (listeInvalide.Count !=0)
            {
                Assert.Fail();
            }
        }
        [Test]
        public void TestlisteCourEnseignant()
        {
            List<Cour> liste = new List<Cour>();
            List<Cour> listeInvalide = new List<Cour>();
            try
            {
                liste = courFacto.listeCourEnseignant("Louis-Andre", "Guerin");
                listeInvalide = courFacto.listeCourEnseignant("","");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            if (liste.Count == 0)
            {
                Assert.Fail();
            }
            if (listeInvalide.Count != 0)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void TestCreateCour()
        {
            bool test = false;
            
            try
            {
                test = courFacto.CreateCour("test46", "test46", 100);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            
            if(!test)
            {
                Assert.Fail();
            }

            //courFacto.DeleteCour("test", "test", 100);
        }
        [Test]
        public void TestCreateCourExistant()
        {
            
            bool test2 = false;
            try
            {
                Random rand = new Random();
                
                test2 = courFacto.CreateCour("test", "test", -129813);

            }
            catch (Exception e)
            {
                if(e.Message == "Duplicate entry 'test' for key 'tp5_cours.PRIMARY'")
                {
                    Assert.Pass();
                }
                Assert.Fail(e.Message);
            }

        }
        [Test]
        public void TestAddEtuToCour()
        {

        }
    }
}
