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
                using (var context = new FrelsexDbContext())
                {
                    var ruoloUtente = context.Utenti
                                             .Where(u => u.Username == username)
                                             .Select(u => u.Ruolo.Nome)
                                             .SingleOrDefault();

                    return ruoloUtente != null ? new string[] { ruoloUtente } : new string[] { };
                }
            }
            catch
            {
                // Considera di loggare l'eccezione
                return new string[] { };
            }
        }


        public override string[] GetUsersInRole(string roleName)
        {
            using (FrelsexDbContext context = new FrelsexDbContext())
            {
                // Prendi tutti gli utenti che hanno un ruolo con il nome specificato.
                var utenti = context.Utenti.Include("Ruolo").Where(u => u.Ruolo.Nome == roleName).Select(u => u.Username).ToArray();

                return utenti;
            }
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            using (FrelsexDbContext context = new FrelsexDbContext())
            {
                return context.Utenti.Include("Ruolo").Any(u => u.Username == username && u.Ruolo.Nome == roleName);
            }
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