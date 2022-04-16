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
            if (gitRepo.Name == null)
            {
                throw new ArgumentException("Hello! Repo name required! Try this thing again!");
            }
            else if (gitRepo.ID!=0)
            {
                throw new InvalidOperationException("Hello! Do not give me ID! The database will do this for me!");
            }
            else if (gitRepo.OwnerID==0||gitUserRepository.ReadOne(gitRepo.OwnerID)==null)
            {
                throw new ArgumentException("Hello! Repo must have a foreign key to its owner");
            }
            else gitRepoRepository.Create(gitRepo);
            //throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            if (gitRepoRepository.ReadOne(id) != null)
            {
                if (gitRepoRepository.ReadOne(id).Commits.Count!=0)
                {
                    throw new InvalidOperationException("Cannot delete -> repo has existing commits");
                }
            }
            //else if (gitRepoRepository.ReadOne(id).Commits!=null)
            //{
                //throw new Exception("This item cannot be deleted as others reference to it!");
            //}
            
            //else
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
            if (gitRepoRepository.ReadOne(gitRepo.ID)!=null)
            {
                gitRepoRepository.Update(gitRepo);
            }
            
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
            //throw new NotImplementedException();
        }

        public double AvgRepoByUsers()
        {
            return gitRepoRepository.ReadAll().Count() / gitUserRepository.ReadAll().Count();
        }
        #endregion
    }
}
