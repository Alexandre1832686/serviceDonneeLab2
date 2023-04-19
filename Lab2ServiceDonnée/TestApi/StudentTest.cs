using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab2ServiceDonnée.DataAccessLayer;
using Lab2ServiceDonnée.DataAccessLayer.factories;
using Lab2ServiceDonnée.Model;
using MySql.Data.MySqlClient;

namespace TestApi
{
    internal class StudentTest
    {
        StudentFactory studentFacto;
        [SetUp]
        public void Setup()
        {
            DAL dal = new DAL();
            DAL.ConnectionString = "Server=sql.decinfo-cchic.ca;Port=33306;Database=h23_intro_services_tp5_1832686;Uid=dev-1832686;Pwd=Info2020";
            studentFacto = dal.StudentFactory;
        }

        [Test]
        public void TestGetAll()
        {
            List<Student> liste = new List<Student>();
            try
            {
                liste = studentFacto.GetAll();
            }
            catch(Exception e)
            {
                Assert.Fail(e.Message);
            }

            if(liste == null)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void TestGet()
        {
            Student suposéInvalide = null;
            Student suposéValide = null;
            try
            {
                suposéInvalide = studentFacto.Get("FRANK_EST_UN_NIAISEU");
                suposéValide = studentFacto.Get("ABCD11111111");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            if (suposéInvalide != null)
            {
                Assert.Fail();
            }

            if(suposéValide == null)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void TestGetStudentParCoursNegatif()
        {
            List<Student> liste = new List<Student>();

            try
            {
                liste = studentFacto.GetStudentParCours("Interface utilisateur");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            if (liste == null)
            {
                Assert.Fail();
            }

            if (liste[0].Nom == "")
            {
                Assert.Fail();
            }
        }

        [Test]
        public void TestGetStudentParCoursPositif()
        {
            List<Student> liste = new List<Student>();
            List<Student> liste2 = new List<Student>();

            try
            {
                liste = studentFacto.GetStudentParCours("Introduction à la Mopologie");
                liste2 = studentFacto.GetStudentParCours("");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            if (liste.Count > 0)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void TestGetDiplome()
        {
            List<Student> liste = new List<Student>();
            List<Student> listeinvalide = new List<Student>();

            try
            {
                liste = studentFacto.GetDiplome(2022);
                listeinvalide = studentFacto.GetDiplome(651561);
                listeinvalide = studentFacto.GetDiplome(-651651);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            if (liste.Count != 0)
            {
                Assert.Fail();
            }
        }
    }
}
