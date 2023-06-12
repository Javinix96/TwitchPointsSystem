using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using TwitchPointsSystem.Models;
using TwitchPointsSystem.src.Clases;

namespace TwitchPointsSystem.src.Interfaces
{
    public interface IPoints
    {
        List<UserPoint> GetUsers();

        InfoHelper GetUser(string id);

        InfoHelper AddUser(UserPoint user);

        InfoHelper ModifyUser(string userID, string user, long points);

        InfoHelper SetPoints([FromUri] string userID, string Nameuser, long points);

        InfoHelper DeleteUser(string userID);
    }
}
