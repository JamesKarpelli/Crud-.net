using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_with_net6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FootballTeamsController : ControllerBase
    {
        /*
            private static List<FootballTeams> teams = new List<FootballTeams>
            {
                new FootballTeams
                {
                    Id = 1,
                    ClubName = "Barcelona",
                    Captian = "Busqites",
                    League = "Spanish League"
                },
                new FootballTeams
                {
                    Id = 2,
                    ClubName = "Man City",
                    Captian = "Holland",
                    League = "PL"
                }
            };
        */
        private readonly DataContext _dataContext;

        public FootballTeamsController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<FootballTeams>>> Get()
        {
            return Ok(await _dataContext.FBTeams.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FootballTeams>> Get(int id)
        {
            var club = await _dataContext.FBTeams.FindAsync(id);
            if (club == null)
                return BadRequest("Club does not exsit");
            return Ok(club);
        }
        
        [HttpPost]
        public async Task<ActionResult<List<FootballTeams>>> AddTeam(FootballTeams club)
        {
            _dataContext.FBTeams.Add(club);
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.FBTeams.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<FootballTeams>>> UpdateClub(FootballTeams request)
        {
            var DbClub = await _dataContext.FBTeams.FindAsync(request.Id);
            if (DbClub == null)
                return BadRequest("Club does not exsit");
            DbClub.Id = request.Id;
            DbClub.Captian = request.Captian;
            DbClub.League = request.League;
            DbClub.ClubName = request.ClubName;

            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.FBTeams.ToListAsync());

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<FootballTeams>>> Delete(int id)
        {
            var DbClub = await _dataContext.FBTeams.FindAsync(id);
            if (DbClub == null)
                return BadRequest("Club does not exsit");
            _dataContext.FBTeams.Remove(DbClub);
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.FBTeams.ToListAsync());

        }
    }
}
