using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace TexxtyDataAccess.Models.Utilities
{
    public class AuthenticationUtility
    {
        /// <summary>
        /// Checks if user crentials supplied is correct or not
        /// </summary>
        /// <param name="authenticationToken">( Username:Password ) in Base64 format encoded</param>
        /// <returns></returns>
        public static bool AuthenticateUser(string authenticationToken)
        {
            (string username, string password) = GetCredentials(authenticationToken);
            using (TexxtyDBEntities entities = new TexxtyDBEntities())
            {
                return entities.Users.Any(user => user.Username == username && user.Password == password);
            }
        }

        /// <summary>
        /// Checks if user crentials supplied is correct or not, and also returns username back from decoded string
        /// </summary>
        /// <param name="authenticationToken">( Username:Password ) in Base64 format encoded</param>
        /// <param name="usernameOut">Username of the user which was supplied in Encoded Token</param>
        /// <returns></returns>
        public static bool AuthenticateUser(string authenticationToken, out string usernameOut)
        {
            (string username, string password) = GetCredentials(authenticationToken);
            using (TexxtyDBEntities entities = new TexxtyDBEntities())
            {
                usernameOut = username;
                return entities.Users.Any(user => user.Username == username && user.Password == password);
            }
        }

        /// <summary>
        /// Decodes username and password from Authentication Token (which should be in Base64 format)
        /// </summary>
        /// <param name="authenticationToken">( Username:Password ) in Base64 format encoded</param>
        /// <returns>Tuple which contains the decoded username and password</returns>
        public static (string username, string password) GetCredentials(string authenticationToken)
        {
            string decodedAuthenticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));
            string[] usernamePasswordArray = decodedAuthenticationToken.Split(':');
            string username = usernamePasswordArray[0];
            string password = usernamePasswordArray[1];

            return (username, password);
        }
    }
}