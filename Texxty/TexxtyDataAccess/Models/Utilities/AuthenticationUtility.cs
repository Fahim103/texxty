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

        /// <summary>
        /// Checks if user crentials supplied is correct or not
        /// </summary>
        /// <param name="authenticationToken">( Username:Password ) in Base64 format encoded</param>
        /// <returns>Token for the authenticated user, otherwise null</returns>
        public static string AuthenticateUser(string username, string password)
        {
            using (TexxtyDBEntities entities = new TexxtyDBEntities())
            {
                var entity = entities.Users.FirstOrDefault(user => user.Username == username && user.Password == password);
                if (entity != null)
                    return entity.Token;
                else
                    return null;
            }
        }

        /// <summary>
        /// Checks if user crentials supplied is correct or not, and also returns username back from decoded string
        /// </summary>
        /// <param name="token">( Username:Password ) in Base64 format encoded</param>
        /// <param name="usernameOut">Username of the user which was supplied in Encoded Token</param>
        /// <returns></returns>
        public static User VerifyToken(string token)
        {
            using (TexxtyDBEntities entities = new TexxtyDBEntities())
            {
                return entities.Users.FirstOrDefault(user => user.Token == token);
            }
        }
    }
}