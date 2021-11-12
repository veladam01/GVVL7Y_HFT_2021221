using GVVL7Y_HFT_2021221.Data;
using GVVL7Y_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVVL7Y_HFT_2021221.Repository
{
    public class GitCommitRepository : IGitCommitRepository
    {
        GitDatabaseContext context;

        public GitCommitRepository(GitDatabaseContext context)
        {
            this.context = context;
        }

        #region CRUD
        public void Create(GitCommit gitCommit)
        {
            context.Commits.Add(gitCommit);
            context.SaveChanges();
            //throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            GitCommit gitCommit = ReadOne(id);
            //not sure if nullcheck needed
            if (gitCommit != null)
            {
                context.Commits.Remove(gitCommit);
                context.SaveChanges();
            }
            else throw new NullReferenceException();
            //throw new NotImplementedException();
        }

        public IQueryable<GitCommit> ReadAll()
        {
            return context.Commits;
            //throw new NotImplementedException();
        }

        public GitCommit ReadOne(int id)
        {
            return context
                .Commits
                .FirstOrDefault(x => x.ID == id);
            //throw new NotImplementedException();
        }

        public void Update(GitCommit gitCommit)
        {
            GitCommit old = ReadOne(gitCommit.ID);
            if (old != null)
            {
                old.TargetRepositoryID = gitCommit.TargetRepositoryID;
                old.CommiterID = gitCommit.CommiterID;
                old.CommitMessage = gitCommit.CommitMessage;
                old.When = gitCommit.When;
                context.SaveChanges();

            }
            else throw new NullReferenceException();
            //throw new NotImplementedException();
        }
        #endregion

        #region Non-CRUD

        #endregion
    }
}
