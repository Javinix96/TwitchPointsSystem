using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TwitchPointsSystem.Models;

namespace TwitchPointsSystem.src.Clases
{
    public class InfoHelper
    {
        private bool _success;
        private string _message;
        private UserPoint userPoint;

        public bool Success { get => _success; }
        public string Message { get => _message; }
        public UserPoint UserPoint { get => userPoint; set { userPoint = value; } }

        public InfoHelper(bool success, string message = " ") 
        {
            _success = success;
            _message = message;
        }

        public InfoHelper SetInfo(bool succes = false, string mess = "")
        {
            _success = succes;
            _message = mess;
            return this;
        }
        public InfoHelper SetInfo( string mess = "", bool succes = false)
        {
            _success = succes;
            _message = mess;
            return this;
        }

    }
}