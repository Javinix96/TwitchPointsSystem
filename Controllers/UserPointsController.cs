using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using TwitchPointsSystem.Models;
using TwitchPointsSystem.src.Clases;
using TwitchPointsSystem.src.Interfaces;

namespace TwitchPointsSystem.Controllers
{
    public class UserPointsController : ApiController
    {
        private IPoints _userDB;

        public UserPointsController()
        {
            _userDB = new UserDB();
        }

        [ActionName("GetAllUsers")]
        [HttpGet]
        public IEnumerable<UserPoint> GetUser()
        {
            return _userDB.GetUsers();
        }

        [ActionName("GetUser")]
        [HttpGet]
        public InfoHelper GetUser([FromUri] string id) => _userDB.GetUser(id);


        [ActionName("AddNewUser")]
        [HttpPost]
        public InfoHelper AddUser([FromBody] UserPoint user) => _userDB.AddUser(user);
        

        [ActionName("SumUserPoints")]
        [HttpPut]
        public InfoHelper ModifyUserPoints([FromUri] string userID, string user, long points) => _userDB.ModifyUser(userID, user, points);


        [ActionName("SetUserPoints")]
        [HttpPut]
        public InfoHelper SetPoints([FromUri] string userID, string nameuser, long points)=> _userDB.SetPoints(userID, nameuser, points);


        [ActionName("DeleteUser")]
        [HttpDelete]
        public InfoHelper DeleteUser([FromUri] string userID) => _userDB.DeleteUser(userID);

    }
}
