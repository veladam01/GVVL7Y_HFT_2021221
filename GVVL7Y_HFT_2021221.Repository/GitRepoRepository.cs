using GVVL7Y_HFT_2021221.Data;
using GVVL7Y_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVVL7Y_HFT_2021221.Repository
{
    public class GitRepoRepository : IGitRepoRepository
    {
        GitDatabaseContext context;

        public GitRepoRepository(GitDatabaseContext context)
        {
            this.context = context;
        }

        #region CRUD
        public void Create(GitRepo gitRepo)
        {
            context.Repositories.Add(gitRepo);
            context.SaveChanges();
            //throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            context.Repositories.Remove(ReadOne(id));
            context.SaveChanges();
            //throw new NotImplementedException();
        }

        public IQueryable<GitRepo> ReadAll()
        {
            return context.Repositories;
            //throw new NotImplementedException();
        }

        public GitRepo ReadOne(int id)
        {
            return context
                .Repositories
                .FirstOrDefault(x => x.ID == id);
            //throw new NotImplementedException();
        }

        public void Update(GitRepo gitRepo)
        {
            GitRepo old = ReadOne(gitRepo.ID);
            old.Name = gitRepo.Name;
            old.OwnerID = gitRepo.OwnerID;
            context.SaveChanges();
            //throw new NotImplementedException();
        }
        #endregion
    }
}
