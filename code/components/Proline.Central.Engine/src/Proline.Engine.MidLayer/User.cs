using Proline.Online.Data;
using Proline.CentralEngine.DBApi; 
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proline.CentralEngine.DBApi.Models.Central;

namespace Proline.CentralEngine.MidLayer
{
    public static partial class Engine
    {
        public static ReturnCode RegisterUser(string username)
        {
            return RegisterUser(username, out var x);
        }


        public static ReturnCode RegisterUser(string username, out UserAccount user)
        {
            try
            {
                var connection = new DBConnection();
                user = connection.GetUser(username);
                if(user != null)
                {
                    user = null;
                    return ReturnCode.UserAlreadyExists;
                }
                user = new UserAccount()
                {
                    Username = username,
                    CreatedOn = DateTime.UtcNow,
                    GroupId = 0,
                    Priority = 0
                };
                user = connection.InsertUser(user);
            }
            catch (Exception e)
            {
                user = null;
                return ReturnCode.SystemError;
                throw;
            }

            return ReturnCode.Success;
        }
        public static ReturnCode GetUserDetails(string username, out UserDetailsOutParameter userDetails)
        { 
            var connection = new DBConnection();
            var user = connection.GetUser(username);
            userDetails = new UserDetailsOutParameter() { UserId = user.UserId };
            return ReturnCode.Success;
        }


        public static ReturnCode DenyUser(long userId, out long denialId)
        {
            denialId = 0;
            try
            {


                UserDeny denia = new UserDeny()
                {
                    UserId = userId,
                    BannedAt = DateTime.UtcNow,
                    ExpiresAt = DateTime.MaxValue,
                    Reason = "Test Ban"
                };

                var connection = new DBConnection(); 
                var d = connection.GetDenial(denia);
                if (d != null)
                    return ReturnCode.AllowUserDenied; 
                var x = connection.GetAllow(userId);
                if (x != null)
                    connection.DeleteAllow(x);

                var denial = connection.InsertDenial(denia);
                denialId = denial.DenyId;
            }
            catch (Exception e)
            { 
                return ReturnCode.SystemError;
                throw;
            }
            return ReturnCode.Success;
        }

        public static ReturnCode IsUserAllowed(long userId, out bool x)
        {
            var connection = new DBConnection();
            var y = connection.GetAllow(userId);
            x = y != null;
            return ReturnCode.Success;
        }

        public static ReturnCode AllowUser(long userId, out long allowId)
        {
            allowId = 0;
            try
            {
                var connection = new DBConnection();
                var d = connection.GetDenial(userId);
                if (d != null)
                    return ReturnCode.AllowUserDenied;
                 
                var x = connection.GetAllow(userId);
                if (x != null)
                    return ReturnCode.AllowUserAlreadyAllowed;
                var allowal = new UserAllow()
                {
                    UserId = userId,
                    AllowedAt = DateTime.UtcNow
                };
                allowId = connection.InsertAllow(allowal).AllowId;

            }
            catch (Exception e)
            { 
                return ReturnCode.SystemError;
                throw;
            }
            return ReturnCode.Success;
        }

        public static ReturnCode GetUser(string username, out UserAccount user2)
        {
            try
            {
                var connection = new DBConnection();
                user2 = connection.GetUser(username);
                if (user2 == null)
                {
                    return ReturnCode.UserNotFound;
                }
            }
            catch (Exception e)
            {
                user2 = null;
                return ReturnCode.SystemError;
                throw;
            }

            return ReturnCode.Success;
        }
    }
}
