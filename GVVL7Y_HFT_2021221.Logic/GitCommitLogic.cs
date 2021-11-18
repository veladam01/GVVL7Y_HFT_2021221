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
            if ( gitCommit.Repo == null || gitCommit.User == null || gitCommit.CommitMessage == null)
            {
                throw new ArgumentException("Hello! All of the following fields are required:\n(1) Commit message\n(2) Commiter\n(3) Targeted Repository\nTry this thing again");
            }
            else gitCommitRepository.Create(gitCommit);
        }

        public void Delete(int id)
        {
            gitCommitRepository.Delete(id);
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
            gitCommitRepository.Update(gitCommit);
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
            return (gitCommitRepository.ReadAll().Count() / gitUserRepository.ReadAll().Count());
        }
        #endregion
    }
}
