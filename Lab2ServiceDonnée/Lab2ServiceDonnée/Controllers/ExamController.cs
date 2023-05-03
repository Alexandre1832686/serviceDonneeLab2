using Lab2ServiceDonnée.DataAccessLayer.factories;
using Lab2ServiceDonnée.DataAccessLayer;
using Lab2ServiceDonnée.Model;
using Microsoft.AspNetCore.Mvc;

namespace Lab2ServiceDonnée.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExamController : Controller
    {
        private readonly ILogger<monController> _logger;
        DAL dal = new DAL();
        TravailFactory travailFactory;

        public ExamController(ILogger<monController> logger)
        {
            _logger = logger;
            InitialiseFacto();
        }
        void InitialiseFacto()
        {
            travailFactory = dal.TravailFactory;
        }

        [HttpGet]
        [Route("TravauxSemaine")]
        public List<Travail> TravauxSemaine()
        {
            return travailFactory.TravailSemaine();
        }
        [HttpGet]
        [Route("TravauxSemaineCSGP/{ECSGP_ID}")]
        public List<Travail> TravauxSemaineCSGP(int CSGP_ID)
        {

            return travailFactory.TravailSemaineECSGP(CSGP_ID); ;
        }
        [HttpGet]
        [Route("GetListeTravauxEtu/{codePerma}")]
        public List<Travail> GetListeTravauxEtu(string codePerma, int ESCGP_ID)
        {
            return travailFactory.GetListeTravauxEtu(codePerma, ESCGP_ID);
        }
        [HttpPost]
        [Route("CreerTravail/{ponderation}/{dateRemise}")]
        public string CreerTravail(int ponderation, DateTime dateRemise)
        {
            if (travailFactory.CreerTravail(ponderation, dateRemise))
            {
                return "Ajouté avec succès";
            }
            else
            {
                return "Problème de modification";
            }
        }
        [HttpPut]
        [Route("InscrireNote/{codePermanant}/{ECSGP_ID}/{note}")]
        public string InscrireNote(int codePermanant, int CSGP_ID, int Signotele)
        {
            if (true)
            {
                return "Modifié avec succès";
            }
            else
            {
                return "Problème de modification";
            }
        }
        [HttpPut]
        [Route("ModifierNote/{travail_ID}/{ECSGP_ID}/{note}")]
        public string ModifierNote(int travail_ID, int ECSGP_ID, int note)
        {
            if (true)
            {
                return "Modifié avec succès";
            }
            else
            {
                return "Problème de modification";
            }
        }
    }
}
