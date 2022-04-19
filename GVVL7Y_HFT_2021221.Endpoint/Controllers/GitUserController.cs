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
        private readonly IHubContext<SignalRHub> hub;

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
            logic.Create(user);
            hub.Clients.All.SendAsync("UserCreated", user);
        }

        [HttpDelete("{ID}")]
        public void DeleteOne([FromRoute] int id)
        {

            //HttpResponseException
            var toDel = logic.ReadOne(id);
            logic.Delete(id);
            hub.Clients.All.SendAsync("UserDeleted", toDel);
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
            hub.Clients.All.SendAsync("UserUpdated", user);
        }
        [HttpGet("usercount")]
        public int GetUserCount()
        { return logic.UserCount(); }
    }
}