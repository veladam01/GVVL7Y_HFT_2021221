using GVVL7Y_HFT_2021221.Models;
using GVVL7Y_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVVL7Y_HFT_2021221.Logic
{
    public class GitRepoLogic : IGitRepoLogic
    //: IGitRepoLogic
    {
        IGitRepoRepository gitRepoRepository;

        //IGitCommitRepository gitCommitRepository;

        IGitUserRepository gitUserRepository;



        public GitRepoLogic(IGitUserRepository gitUserRepository, IGitRepoRepository gitRepoRepository)
        {
            this.gitUserRepository = gitUserRepository; this.gitRepoRepository = gitRepoRepository;
        }

        public GitRepoLogic(IGitRepoRepository gitRepoRepository)
        {
            this.gitRepoRepository = gitRepoRepository;
        }
        #region CRUD
        public void Create(GitRepo gitRepo)
        {
            if (gitRepo.Name == null || gitRepo.Owner == null)
            {
                throw new ArgumentException("Hello! All of the following fields are required:\n(1) Name\n(2) Owner\nTry this thing again!");
            }
            gitRepoRepository.Create(gitRepo);
            //throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            gitRepoRepository.Delete(id);
            //throw new NotImplementedException();
        }

        public IEnumerable<GitRepo> ReadAll()
        {
            return gitRepoRepository.ReadAll();
            //throw new NotImplementedException();
        }

        public GitRepo ReadOne(int id)
        {
            return gitRepoRepository.ReadOne(id);
            //throw new NotImplementedException();
        }

        public void Update(GitRepo gitRepo)
        {
            gitRepoRepository.Update(gitRepo);
            //throw new NotImplementedException();
        }
        #endregion

        #region Non-CRUD
        public int RepoCount()
        {
            return gitRepoRepository.ReadAll().Count();
            //throw new NotImplementedException();
        }

        public IEnumerable<KeyValuePair<string, int>> RepoCountByOwners()
        {
            return gitRepoRepository.ReadAll().GroupBy(x => x.Owner).Select(x => new KeyValuePair<string, int>(x.Key.Name, x.Count()));
            throw new NotImplementedException();
        }

        public double AvgRepoByUsers()
        {
            return gitRepoRepository.ReadAll().Count() / gitUserRepository.ReadAll().Count();
        }
        #endregion
    }
}
