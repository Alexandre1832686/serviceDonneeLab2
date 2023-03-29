using Lab2ServiceDonnée.DataAccessLayer;
using Lab2ServiceDonnée.DataAccessLayer.factories;
using Lab2ServiceDonnée.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab2ServiceDonnée.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class monController : Controller
    {
        private readonly ILogger<monController> _logger;

        public monController(ILogger<monController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("GetNomStudents")]

        public List<string> GetNomStudents()
        {
            DAL dal = new DAL();
            StudentFactory facto = dal.StudentFactory;
            List<Student> list = facto.GetAll();
            List<string> retour = new List<string>();
            foreach(Student s in list)
            {
                retour.Add(s.Nom);
            }
            return retour;
        }


        [HttpGet]
        [Route("GetCourByCodePerma/{codePerma}")]

        public string GetNomStudentByCodePerma(string codePerma)
        {
            DAL dal = new DAL();
            StudentFactory facto = dal.StudentFactory;
            return facto.Get(codePerma).Nom;
        }

        [HttpGet]
        [Route("GetCoursByCodePerma/{codePerma}")]

        public List<Cour> GetCoursByCodePerma(string codePerma)
        {
            DAL dal = new DAL();
            CourFactory facto = dal.CourFactory;
            return facto.listeCour(codePerma);
        }
    }
}
