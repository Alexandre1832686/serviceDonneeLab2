using Lab2ServiceDonnée.DataAccessLayer;

var builder = WebApplication.CreateBuilder(args);
DAL.ConnectionString = builder.Configuration.GetConnectionString("Default");
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();



/*
    CREATE VIEW view_etu_cours AS
    SELECT etu_code_permanent, etu_nom, etu_prenom, etu_date_naissance, etu_date_inscription, etu_date_diplome, etu_num_da, cou_sigle, cou_titre, cou_duree FROM tp5_etudiant 
    Join tp5_etudiant_courssessiongroupeprof 
    On tp5_etudiant.etu_code_permanent = tp5_etudiant_courssessiongroupeprof.ecsgp_etu_codepermanent
    Join tp5_cours_session_groupe_prof
    On tp5_cours_session_groupe_prof.csgp_id = tp5_etudiant_courssessiongroupeprof.ecsgp_csgp_id
    Join tp5_cours
    On tp5_cours.cou_sigle = tp5_cours_session_groupe_prof.csgp_sigle_cours;
 */

/*
  CREATE VIEW view_etu_cours_actif AS
    SELECT etu_code_permanent,etu_nom,etu_prenom, cou_sigle, cou_titre, cou_duree FROM 
    (SELECT etu_code_permanent,etu_nom,etu_prenom, cou_sigle, cou_titre, cou_duree,csgp_id_session  FROM tp5_etudiant 
    Join tp5_etudiant_courssessiongroupeprof 
    On tp5_etudiant.etu_code_permanent = tp5_etudiant_courssessiongroupeprof.ecsgp_etu_codepermanent
    Join tp5_cours_session_groupe_prof
    On tp5_cours_session_groupe_prof.csgp_id = tp5_etudiant_courssessiongroupeprof.ecsgp_csgp_id
    Join tp5_cours
    On tp5_cours.cou_sigle = tp5_cours_session_groupe_prof.csgp_sigle_cours) as tbl
    WHERE tbl.csgp_id_session = (Select MAX(se_id) from tp5_session);
 */