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
    public class GitCommitController : ControllerBase
    {
        private IGitCommitLogic logic;
        private IHubContext<SignalRHub> hub;
        public GitCommitController(IGitCommitLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<GitCommit> GetAll()
        {
            return logic.ReadAll();
        }
        [HttpGet("{ID}")]
        public GitCommit GetOne([FromRoute] int id)
        {
            return logic.ReadOne(id);
        }

        [HttpPost]
        public void AddOne([FromBody] GitCommit commit)
        {
            logic.Create(commit);
            hub.Clients.All.SendAsync("GitCommitCreated", commit);
        }

        [HttpDelete("{ID}")]
        public void DeleteOne([FromRoute] int id)
        {
            var toDel = logic.ReadOne(id);
            logic.Delete(id);
            hub.Clients.All.SendAsync("GitCommitDeleted", toDel);
        }

        [HttpPut]
        public void EditOne([FromBody] GitCommit commit)
        {
            logic.Update(commit);
            hub.Clients.All.SendAsync("GitCommitUpdated", commit);
        }

        [HttpGet("commitcount")]
        public int GetCommitCount()
        {
            return logic.CommitCount();
        }
        [HttpGet("avgcommitbyrepos")]
        public double GetAvgCommitByRepos()
        {
            return logic.AvgCommitByRepos();
        }
        [HttpGet("avgcommitbyusers")]
        public double GetAvgCommitByUsers()
        {
            return logic.AvgCommitByUsers();
        }


        //[HttpGet("commitcountbyusers")]
        //public IEnumerable<KeyValuePair<string, int>> GetCommitCountByUsers()
        //{
        //    return logic.CommitCountByUsers();
        //}
        //[HttpGet("commitcountbyrepos")]
        //public IEnumerable<KeyValuePair<string, int>> GetCommitCountByRepos()
        //{
        //    return logic.CommitCountByRepos();
        //}
    }
}