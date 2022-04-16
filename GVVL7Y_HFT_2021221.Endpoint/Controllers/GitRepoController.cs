using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GVVL7Y_HFT_2021221.Logic;
using GVVL7Y_HFT_2021221.Models;

namespace GVVL7Y_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GitRepoController : ControllerBase
    {
        private IGitRepoLogic logic;

        public GitRepoController(IGitRepoLogic logic)
        {
            this.logic = logic;
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
        }

        [HttpDelete("{ID}")]
        public void DeleteOne([FromRoute] int id)
        {
            try
            {
                logic.Delete(id);
            }
            catch (Exception)
            {

                //throw;
            }
            
        }

        [HttpPut]
        public void EditOne([FromBody] GitRepo repo)
        {
            logic.Update(repo);
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