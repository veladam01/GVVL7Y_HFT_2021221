using GVVL7Y_HFT_2021221.Models;
using GVVL7Y_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVVL7Y_HFT_2021221.Logic
{
    public class GitCommitLogic : IGitCommitLogic
    //: IGitCommitLogic
    {
        IGitCommitRepository gitCommitRepository;

        IGitUserRepository gitUserRepository;

        IGitRepoRepository gitRepoRepository;
        #region CRUD
        public void Create(GitCommit gitCommit)
        {
            if (gitCommit.CommitMessage == null || gitCommit.CommitMessage == "")
            {
                throw new ArgumentException("Hello! Commit message required! Try this thing again");
            }
            else if (gitCommit.ID != 0)
            {
                throw new InvalidOperationException("Hello! Do not give me ID! The database will do this for me!");
            }
            else if (gitCommit.CommiterID==0)
            {
                //throw new ArgumentException("Hello! Commit must have a foreign key to its commiter!");
            }
            else if (gitUserRepository.ReadOne(gitCommit.CommiterID)==null)
            {
                //throw new ArgumentException("Hello! Commiter must be a valid (already existing) user!");
            }
            else if (gitCommit.TargetRepositoryID == 0)
            {
                //throw new ArgumentException("Hello! Commit must have a foreign key to its targeted repo!");
            }
            else if (gitRepoRepository.ReadOne(gitCommit.TargetRepositoryID) == null)
            {
                //throw new ArgumentException("Hello! Targeted repository must be a valid (already existing) repo!");
            }
            
            else gitCommitRepository.Create(gitCommit);
        }

        public void Delete(int id)
        {
            if (gitCommitRepository.ReadOne(id)!=null)
            {
                gitCommitRepository.Delete(id);
            }
            
        }

        public IEnumerable<GitCommit> ReadAll()
        {
            return gitCommitRepository.ReadAll();
            //throw new NotImplementedException();
        }

        public GitCommit ReadOne(int id)
        {
            return gitCommitRepository.ReadOne(id);
            //throw new NotImplementedException();
        }

        public void Update(GitCommit gitCommit)
        {
            if (gitCommitRepository.ReadOne(gitCommit.ID)!=null)
            {
                gitCommitRepository.Update(gitCommit);
            }
            
            //throw new NotImplementedException();
        }

        #endregion

        public GitCommitLogic(IGitUserRepository gitUserRepository, IGitRepoRepository gitRepoRepository, IGitCommitRepository gitCommitRepository)
        {
            this.gitUserRepository = gitUserRepository; this.gitRepoRepository = gitRepoRepository; this.gitCommitRepository = gitCommitRepository;
        }
        public GitCommitLogic(IGitRepoRepository gitRepoRepository, IGitCommitRepository gitCommitRepository)
        {
           this.gitRepoRepository = gitRepoRepository; this.gitCommitRepository = gitCommitRepository;
        }
        public GitCommitLogic(IGitCommitRepository gitCommitRepository)
        {
            this.gitCommitRepository = gitCommitRepository;
        }
        #region Non-CRUD
        public int CommitCount()
        {
            return gitCommitRepository.ReadAll().Count();
            //throw new NotImplementedException();
        }

        public IEnumerable<KeyValuePair<string, int>> CommitCountByRepos()
        {
            return gitCommitRepository.ReadAll().GroupBy(x => x.Repo).Select(x => new KeyValuePair<string, int>(x.Key.Name, x.Count()));
            //throw new NotImplementedException();
        }

        public IEnumerable<KeyValuePair<string, int>> CommitCountByUsers()
        {
            return gitCommitRepository.ReadAll().GroupBy(x => x.User).Select(x => new KeyValuePair<string, int>(x.Key.Name, x.Count()));
            //throw new NotImplementedException();
        }
        public double AvgCommitByUsers()
        {
            return (gitCommitRepository.ReadAll().Count() / gitUserRepository.ReadAll().Count());
        }

        public double AvgCommitByRepos()
        {
            return (gitCommitRepository.ReadAll().Count() / gitRepoRepository.ReadAll().Count());
        }
        #endregion
    }
}
