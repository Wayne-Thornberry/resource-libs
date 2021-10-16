using Proline.CentralEngine.DBApi.Contexts;
using Proline.CentralEngine.DBApi.Models.Central;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.CentralEngine.DBApi
{
    public class DBConnection : IDisposable
    {
        private ProlineCentralContext db = new ProlineCentralContext();

        public IQueryable<UserAccount> GetUsers()
        {
            return db.Users;
        }

        public IEnumerable<PlayerIndentity> GetIdentities(long playerId)
        {
            var a = db.PlayerIndentity.ToArray();
            return a.Where(e => e.PlayerId == playerId);
        }

        public PlayerIndentity GetIdentity(string identity)
        {
            return db.PlayerIndentity.FirstOrDefault(e => e.Identifier.Equals(identity));
        }
        public UserDeny GetDenial(UserDeny user)
        {
            return db.UserDeny.FirstOrDefault(e => e.UserId == user.UserId);
        }

        public UserDeny GetDenial(long userid)
        {
            return db.UserDeny.FirstOrDefault(e => e.UserId == userid);
        }

        public Instance InsertInstance(Instance instance1)
        { 
            db.Instance.Add(instance1);
            db.SaveChanges();
            return instance1;
        }

        public void UpdateInstance(Instance instance1)
        {
            db.Entry(instance1).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!InstanceExists(instance1.InstanceId))
                {
                    return;
                }
                else
                {
                    throw;
                }
            }

        }

        public IEnumerable<InstancePlayer> GetInstancePlayers(long instanceId)
        { 
            return db.InstancePlayer.Where(e => e.InstanceId == instanceId);
        }

        public void DeleteAllow(UserAllow allow)
        {
            db.UserAllow.Remove(allow);
            db.SaveChanges();
            return;
        }

        public UserDeny InsertDenial(UserDeny user)
        { 
            db.UserDeny.Add(user);
            db.SaveChanges();
            return user;
        }

        public InstanceUserDeny GetInstanceDeny(long userId)
        {
            return db.InstanceUserDeny.FirstOrDefault(i => i.UserId == userId);
        }

        public UserInstanceLicence InsertInstanceIdentity(UserInstanceLicence instanceIdentity)
        {
            db.UserInstanceLicence.Add(instanceIdentity);
            db.SaveChanges();
            return instanceIdentity; 
        }

        public InstancePlayer InsertInstancePlayer(InstancePlayer instancePlayer)
        {
            db.InstancePlayer.Add(instancePlayer);
            db.SaveChanges();
            return instancePlayer;
        }

        public InstancePlayer GetInstancePlayer(long playerId)
        {
            return db.InstancePlayer.FirstOrDefault(e => e.PlayerId == playerId); 
        }

        public void DeleteInstancePlayer(long id)
        {
            InstancePlayer user = db.InstancePlayer.Find(id);
            db.InstancePlayer.Remove(user);
            db.SaveChanges(); 
        }

        public UserAllow GetAllow(long userId)
        {
            return db.UserAllow.FirstOrDefault(e => e.UserId  == userId);
        }

        public UserAllow InsertAllow(UserAllow user)
        {
            db.UserAllow.Add(user);
            db.SaveChanges();
            return user;
        }

        public UserInstanceLicence GetInstanceIdentity(long instanceId, bool x = false)
        {
            return db.UserInstanceLicence.Find(instanceId);
        }

        public UserInstanceLicence GetInstanceIdentity(long userId)
        {
            return db.UserInstanceLicence.FirstOrDefault(e => e.UserId == userId);
        }

        private bool InstanceExists(long id)
        {
            return db.Instance.Count(e => e.InstanceId == id) > 0;
        }

        public Instance GetInstance(string name)
        { 
            return db.Instance.FirstOrDefault(e => e.Name.Equals(name));
        }

        public Instance GetInstance(long id, bool key = true)
        {
            return db.Instance.FirstOrDefault(i=>i.InstanceIdentityId == id);
        }

        public Instance GetInstance(long id)
        {
            return db.Instance.Find(id);
        }

        public UserAccount GetUser(long id)
        {
            UserAccount user = db.Users.Find(id);
            return user;
        }

        public UserAccount GetUser(string username)
        {
            UserAccount user = db.Users.FirstOrDefault(i=>i.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
            return user;
        }

        public void UpdateUser(long id, UserAccount user)
        {

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!UserExists(id))
                {
                    return;
                }
                else
                {
                    throw;
                }
            }

            return;
        }

        public UserAccount InsertUser(UserAccount user)
        {
            db.Users.Add(user);
            db.SaveChanges(); 
            return user;
        }

        public PlayerAccount InsertPlayerAccount(PlayerAccount playerRegistration)
        { 
            db.PlayerRegistration.Add(playerRegistration);
            db.SaveChanges();
            return playerRegistration;
        }

        public PlayerAccount GetPlayerRegistration(long playerId)
        {
            return db.PlayerRegistration.Find(playerId);
        }

        public PlayerIndentity InsertPlayerIdentity(PlayerIndentity idenity)
        {
            db.PlayerIndentity.Add(idenity);
            db.SaveChanges();
            return idenity;
        }

        public void DeleteUser(long id)
        {
            UserAccount user = db.Users.Find(id);
            if (user == null)
            {
                return;
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return;
        }

        public void Dispose()
        {
            db.Dispose();
        }

        private bool UserExists(long id)
        {
            return db.Users.Count(e => e.UserId == id) > 0;
        }

    }
}
