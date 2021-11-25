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
    public class GitCommitController : ControllerBase
    {
        private IGitCommitLogic logic;

        public GitCommitController(IGitCommitLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<GitCommit> GetAll()
        {
            return logic.ReadAll();
        }

        [HttpPost]
        public void AddOne([FromBody] GitCommit commit)
        {
            logic.Create(commit);
        }

        [HttpDelete("{ID}")]
        public void DeleteOne([FromRoute] int commitid)
        {
            logic.Delete(commitid);
        }

        [HttpPut]
        public void EditOne([FromBody] GitCommit commit)
        {
            logic.Update(commit);
        }
    }
}