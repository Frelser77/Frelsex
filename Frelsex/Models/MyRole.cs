using System;
using System.Linq;
using System.Web.Security;

namespace Frelsex.Models
{
    public class MyRole : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            try
            {
                using (FrelsexDbContext context = new FrelsexDbContext())
                {
                    string[] ruoliUtente = context.Utenti.Include("UtentiRuoli.Ruolo").FirstOrDefault(u => u.Username == username)?.UtentiRuoli.Select(ur => ur.Ruolo.Nome).ToArray();

                    return ruoliUtente ?? new string[] { };
                }
            }
            catch
            {
                return null;
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            using (FrelsexDbContext context = new FrelsexDbContext())
            {
                Ruolo ruolo = context.Ruoli.Include("UtentiRuoli.Utente").FirstOrDefault(r => r.Nome == roleName);

                if (ruolo != null)
                {
                    return ruolo.UtentiRuoli.Select(ur => ur.Utente.Username).ToArray();
                }
            }
            return new string[] { };
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            using (FrelsexDbContext context = new FrelsexDbContext())
            {
                Utente utente = context.Utenti.Include("UtentiRuoli.Ruolo").FirstOrDefault(u => u.Username == username);

                if (utente != null)
                {
                    return utente.UtentiRuoli.Any(ur => ur.Ruolo.Nome == roleName);
                }
            }
            return false;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}