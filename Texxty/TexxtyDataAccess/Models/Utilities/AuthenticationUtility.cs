using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace TexxtyDataAccess.Models.Utilities
{
    public class AuthenticationUtility
    {
        public static string GenerateToken()
        {
            // http://www.programmerguide.net/2015/02/generating-unique-token-in-c-generating.html
            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = Guid.NewGuid().ToByteArray();
            string token = Convert.ToBase64String(time.Concat(key).ToArray());

            return token;
        }

        public static string AuthenticateUser(string username, string password, out int userID)
        {
            using (TexxtyDBEntities entities = new TexxtyDBEntities())
            {
                var entity = entities.Users.FirstOrDefault(user => user.Username == username && user.Password == password);
                if (entity != null)
                {
                    userID = entity.UserID;
                    return entity.Token;
                }
                else
                {
                    userID = 0;
                    return null;
                }

            }
        }

        public static string AuthenticateUser(string username, string password, out string userRole, out int userID)
        {
            using (TexxtyDBEntities entities = new TexxtyDBEntities())
            {
                var entity = entities.Users.FirstOrDefault(user => user.Username == username && user.Password == password);
                if (entity != null)
                {
                    userID = entity.UserID;
                    userRole = entity.Role;
                    return entity.Token;
                }
                else
                {
                    userID = 0;
                    userRole = null;
                    return null;
                }
            }
        }

        public static User VerifyToken(string token)
        {
            using (TexxtyDBEntities entities = new TexxtyDBEntities())
            {
                return entities.Users.FirstOrDefault(user => user.Token == token);
            }
        }
    }
}