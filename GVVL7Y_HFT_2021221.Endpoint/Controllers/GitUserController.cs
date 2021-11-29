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
    public class GitUserController : ControllerBase
    {
        private IGitUserLogic logic;

        public GitUserController(IGitUserLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<GitUser> GetAll()
        {
            return logic.ReadAll();
        }

        [HttpPost]
        public void AddOne([FromBody] GitUser user)
        {
            logic.Create(user);
        }

        [HttpDelete("{ID}")]
        public void DeleteOne([FromRoute] int userid)
        {
            logic.Delete(userid);
        }

        [HttpPut]
        public void EditOne([FromBody] GitUser user)
        {
            logic.Update(user);
        }
        [HttpGet("usercount")]
        public int GetUserCount()
        { return logic.UserCount(); }
    }
}