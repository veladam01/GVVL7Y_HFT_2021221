using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GVVL7Y_HFT_2021221.Logic;
using GVVL7Y_HFT_2021221.Models;
using Microsoft.AspNetCore.SignalR;

namespace GVVL7Y_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GitRepoController : ControllerBase
    {
        private IGitRepoLogic logic;
        private readonly IHubContext<SignalRHub> hub;

        public GitRepoController(IGitRepoLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<GitRepo> GetAll()
        {
            return logic.ReadAll();
        }

        [HttpGet("{ID}")]
        public GitRepo GetOne([FromRoute]int id)
        {
            return logic.ReadOne(id);
        }

        [HttpPost]
        public void AddOne([FromBody] GitRepo repo)
        {
            logic.Create(repo);
            hub.Clients.All.SendAsync("RepoCreated", repo);
        }

        [HttpDelete("{ID}")]
        public void DeleteOne([FromRoute] int id)
        {
            var toDel = logic.ReadOne(id);
            logic.Delete(id);
            hub.Clients.All.SendAsync("RepoDeleted", toDel);

        }

        [HttpPut]
        public void EditOne([FromBody] GitRepo repo)
        {
            logic.Update(repo);
            hub.Clients.All.SendAsync("RepoUpdated", repo);
        }
        [HttpGet("repocount")]
        public int GetRepoCount()
        {
            return logic.RepoCount();
        }
        [HttpGet("avgrepobyusers")]
        public double GetAvgRepoByUsers()
        {
            return logic.AvgRepoByUsers();
        }

        //[HttpGet("repocountbyowners")]
        //public IEnumerable< KeyValuePair<string, int>> GetRepoCountByOwners()
        //{
        //    return logic.RepoCountByOwners();
        //}
            
    }
}