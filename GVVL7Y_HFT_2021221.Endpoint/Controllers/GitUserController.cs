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
    public class GitUserController : ControllerBase
    {
        private IGitUserLogic logic;
        private IHubContext<SignalRHub> hub;

        public GitUserController(IGitUserLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<GitUser> GetAll()
        {
            return logic.ReadAll();
        }
        [HttpGet("{ID}")]
        public GitUser GetOne([FromRoute] int id)
        {
            return logic.ReadOne(id);
        }
        [HttpPost]
        public void AddOne([FromBody] GitUser user)
        {
            this.logic.Create(user);
            this.hub.Clients.All.SendAsync("GitUserCreated", user);

        }

        [HttpDelete("{ID}")]
        public void DeleteOne([FromRoute] int id)
        {

            //HttpResponseException
            var toDel = logic.ReadOne(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("GitUserDeleted", toDel);
            //try
            //{

            //}
            //catch (Exception)
            //{

            //throw new Exception(e.Message);
            //}

        }

        [HttpPut]
        public void EditOne([FromBody] GitUser user)
        {
            logic.Update(user);
            hub.Clients.All.SendAsync("GitUserUpdated", user);
        }
        [HttpGet("usercount")]
        public int GetUserCount()
        { return logic.UserCount(); }
    }
}