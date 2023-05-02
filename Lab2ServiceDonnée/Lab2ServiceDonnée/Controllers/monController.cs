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
        BuletinFactory BuletinFacto;
        ApiKeyfactory ApiKeyFacto;

        public monController(ILogger<monController> logger)
        {
            _logger = logger;
            InitialiseFacto();
        }

        [HttpGet]
        [Route("CheckApiKey/{Key}")]
        public bool CheckApiKey(string Key)
        {
            return ApiKeyFacto.checkApiKey(Key);
        }

        [HttpGet]
        [Route("GenerateApiKey")]
        public string GenerateApiKey()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[12];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            bool check = ApiKeyFacto.GenerateApiKey(new String(stringChars));

            if (check)
                return new String(stringChars);
            else
                return "Un problème est survenu";
            
        }

        [HttpGet]
        [Route("GetNomStudentByCodePerma/{codePerma}")]
        public string GetNomStudentByCodePerma(string codePerma)
        {
            Student s = StudentFacto.Get(codePerma);
            if (s != null)
                return s.Nom;
            else
                return "Code permanant invalide";
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
        [HttpGet]
        [Route("GetCoursEnseignant/{PrenomEnseignant}/{NomEnseignant}")]
        public List<Cour> GetCoursEnseignant(string PrenomEnseignant, string NomEnseignant)
        {
            return (CourFacto.listeCourEnseignant(PrenomEnseignant, NomEnseignant));
        }

        [HttpGet]
        [Route("BuletinÉtudiant/{PrenomÉtu}/{NomÉtu}")]
        public List<Buletin> BuletinÉtudiant(string PrenomÉtu, string NomÉtu)
        {
            return (BuletinFacto.GetBuletin(PrenomÉtu, NomÉtu));
        }

        [HttpGet]
        [Route("DiplomeParAnnee/{Annee}")]
        public List<Student> DiplomeParAnnee(int Annee)
        {
            return (StudentFacto.GetDiplome(Annee));
        }

        [HttpPost]
        [Route("CreerCour/{sigle}/{titre}/{duree}")]
        public string CreerCour(string sigle, string titre, int duree) 
        {
            if(CourFacto.CreateCour( sigle,  titre,  duree))
            {
                return "Créé avec succès";
            }
            else
            {
                return "Problème de création";
            }
        }


        [HttpPut]
        [Route("AjoutEtuACour/{PrenomEnseignant}/{NomEnseignant}/{Sigle}")]
        public string AjoutEtuACour( string PrenomEnseignant, string NomEnseignant, string Sigle)
        {
            if (CourFacto.AddEtuToCour(PrenomEnseignant, NomEnseignant, Sigle))
            {
                return "Modifié avec succès";
            }
            else
            {
                return "Problème de modification";
            }
        }

        [HttpPut]
        [Route("ModifierNote/{Prenom}/{Nom}/{Note}/{Sigle}/{Session}")]
        public string ModifierNote(string Prenom, string Nom, int Note,string Sigle,int Session)
        {
            if (CourFacto.ModifieNote(Prenom, Nom, Note, Sigle, Session))
            {
                return "Modifié avec succès";
            }
            else
            {
                return "Problème de modification";
            }
        }

        [HttpDelete]
        [Route("DeleteEtuFromCours/{Prenom}/{Nom}/{Sigle}/{Session}")]
        public string DeleteEtuFromCours(string Prenom, string Nom, string Sigle, int Session)
        {
            if (CourFacto.DeleteEtuCour(Prenom, Nom, Sigle, Session))
            {
                return "Modifié avec succès";
            }
            else
            {
                return "Problème de modification";
            }
        }

        void InitialiseFacto()
        {
            CourFacto = dal.CourFactory;
            StudentFacto = dal.StudentFactory;
            BuletinFacto = dal.BuletinFactory;
            ApiKeyFacto = dal.ApiKeyfactory;
        }
    }
}
