using GVVL7Y_HFT_2021221.Data;
using GVVL7Y_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVVL7Y_HFT_2021221.Repository
{
    public class GitUserRepository : IGitUserRepository
    {
        GitDatabaseContext context;

        public GitUserRepository(GitDatabaseContext context)
        {
            this.context = context;
        }

        #region CRUD
        public void Create(GitUser gitUser)
        {
            context.Users.Add(gitUser);
            context.SaveChanges();
            //throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            context.Users.Remove(ReadOne(id));
            context.SaveChanges();
            //throw new NotImplementedException();
        }

        public IQueryable<GitUser> ReadAll()
        {
            return context.Users;
            //throw new NotImplementedException();
        }

        public GitUser ReadOne(int id)
        {
            return context
                .Users
                .FirstOrDefault(x => x.ID == id);
            //throw new NotImplementedException();
        }

        public void Update(GitUser gitUser)
        {
            GitUser old = ReadOne(gitUser.ID);

            old.Name = gitUser.Name;
            old.EmailContact = gitUser.EmailContact;
            //old.Registered = gitUser.Registered; //cuz y not?
            context.SaveChanges();
            //throw new NotImplementedException();
        }
        #endregion
    }
}
