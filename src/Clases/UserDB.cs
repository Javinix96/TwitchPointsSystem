using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using TwitchPointsSystem.Models;
using TwitchPointsSystem.src.Interfaces;

namespace TwitchPointsSystem.src.Clases
{
    public class UserDB : IPoints
    {
        public InfoHelper AddUser(UserPoint user)
        {
            InfoHelper hp = new InfoHelper(false);

            try
            {
                using (PointsSystemDBEntities1 db = new PointsSystemDBEntities1())
                {
                    int count = (from x in db.UserPoints where x.UserID == user.UserID select x).CountAsync().Result;
                    if (count > 0)
                    {
                        var us = (from x in db.UserPoints where x.UserID == user.UserID select x).FirstAsync().Result;
                        return hp.SetInfo(true, $"Ya existe el usuario {us.UserName}");
                    }

                    user.UserName = user.UserName.ToLower();
                    db.UserPoints.Add(user);
                    var r = db.SaveChangesAsync().Result;
                    db.Dispose();
                    return hp.SetInfo(true, "Exito");
                }
            }
            catch (Exception e)
            {
                return hp.SetInfo(false, e.Message);
            }
        }

        public List<UserPoint> GetUsers()
        {
            List<UserPoint> users = new List<UserPoint>();
            using (PointsSystemDBEntities1 db = new PointsSystemDBEntities1())
            {
                users = db.UserPoints.ToList();
                db.Dispose();
            }
            return users;
        }

        public InfoHelper ModifyUser(string userID, string user, long points)
        {
            InfoHelper hp = new InfoHelper(false);
            try
            {
                using (PointsSystemDBEntities1 db = new PointsSystemDBEntities1())
                {
                    int count = (from x in db.UserPoints where x.UserID == userID select x).CountAsync().Result;
                    if (count <= 0)
                    {
                        var add = AddUser(new UserPoint() { UserID = userID, UserName = user, Points = points });
                        return hp.SetInfo(true, $"Usuario inexistente, se ha agregado el usario {user} con exito");
                    }
                    UserPoint userTmp = (from x in db.UserPoints where x.UserID == userID select x).FirstAsync().Result;
                    userTmp.Points += points;
                    db.Entry(userTmp);
                    _ = db.SaveChangesAsync().Result;
                    db.Dispose();
                }
            }
            catch (Exception e)
            {
                return hp.SetInfo(e.Message);
            }

            return hp.SetInfo(true, "Exito");

        }

        public InfoHelper SetPoints([FromUri] string userID, string Nameuser, long points)
        {
            InfoHelper hp = new InfoHelper(false);
            try
            {
                using (PointsSystemDBEntities1 db = new PointsSystemDBEntities1())
                {
                    int count = (from x in db.UserPoints where x.UserID == userID select x).CountAsync().Result;
                    if (count <= 0)
                    {
                        AddUser(new UserPoint() { UserID = userID, UserName = Nameuser, Points = points });
                        return hp.SetInfo(true, $"Usuario inexistente, se ha agregado el usario {Nameuser} con exito");
                    }

                    UserPoint userTmp = (from x in db.UserPoints where x.UserID == userID select x).FirstAsync().Result;
                    userTmp.Points = points;
                    db.Entry(userTmp);
                    _ = db.SaveChangesAsync();
                    db.Dispose();
                }
            }
            catch (Exception e)
            {
                return hp.SetInfo(e.Message);
            }
            return hp.SetInfo(true, "Exito");
        }

        public InfoHelper DeleteUser(string userID)
        {
            InfoHelper hp = new InfoHelper(false);

            try
            {
                using (PointsSystemDBEntities1 db = new PointsSystemDBEntities1())
                {
                    int count = (from x in db.UserPoints where x.UserID == userID select x).CountAsync().Result;
                    if (count <= 0)
                        return hp.SetInfo(false, $"El usuario no existe");
                    
                    UserPoint userTmp = (from x in db.UserPoints where x.UserID == userID select x).FirstAsync().Result;
                    db.Entry(userTmp).State = EntityState.Deleted;
                    _ = db.SaveChangesAsync().Result;
                    db.Dispose();
                }

            }
            catch (Exception e)
            {
                return hp.SetInfo(e.Message);
            }
            return hp.SetInfo(true, "Exito");
        }

        public InfoHelper GetUser(string id)
        {
            InfoHelper hp = new InfoHelper(false);

            try
            {
                using (PointsSystemDBEntities1 db = new PointsSystemDBEntities1())
                {
                    int count = (from x in db.UserPoints where x.UserID == id select x).CountAsync().Result;
                    if (count <= 0)
                        return hp.SetInfo(false, $"El usuario no existe");

                    UserPoint userTmp = (from x in db.UserPoints where x.UserID == id select x).FirstAsync().Result;
                    hp.UserPoint = userTmp;
                    db.Dispose();
                }

            }
            catch (Exception e)
            {
                return hp.SetInfo(e.Message);
            }
            return hp.SetInfo(true, "Exito");
        }
    }
}