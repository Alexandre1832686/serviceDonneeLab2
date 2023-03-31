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
        DAL dal = new DAL();
        StudentFactory StudentFacto;
        CourFactory CourFacto;

        public monController(ILogger<monController> logger)
        {
            _logger = logger;
            InitialiseFacto();
        }

        [HttpGet]
        [Route("GetNomStudentByCodePerma/{codePerma}")]
        public string GetNomStudentByCodePerma(string codePerma)
        {
            return StudentFacto.Get(codePerma).Nom;
        }

        [HttpGet]
        [Route("GetCoursByCodePerma/{codePerma}")]

        public List<Cour> GetCoursByCodePerma(string codePerma)
        {
            
            
            return CourFacto.listeCour(codePerma);
        }
        [HttpGet]
        [Route("GetNomStudentsParCours/{nomCours}")]

        public List<Student> GetNomStudentsParCours(string nomCours)
        {
            return( StudentFacto.GetStudentParCours(nomCours));
        }

        void InitialiseFacto()
        {
            CourFacto = dal.CourFactory;
            StudentFacto = dal.StudentFactory;
        }
    }
}
