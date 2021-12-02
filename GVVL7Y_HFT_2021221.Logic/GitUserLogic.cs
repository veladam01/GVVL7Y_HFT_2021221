using GVVL7Y_HFT_2021221.Models;
using GVVL7Y_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVVL7Y_HFT_2021221.Logic
{
    public class GitUserLogic : IGitUserLogic
    //: IGitUserLogic
    {
        //IGitRepoRepository gitRepoRepository;

        //IGitCommitRepository gitCommitRepository;

        IGitUserRepository gitUserRepository;

        #region CRUD
        public void Create(GitUser gitUser)
        {
            if (gitUser.Name == null)
            {
                throw new ArgumentException("Hello! Username required! Try this thing again!");
            }
            else if (gitUser.ID!=0)
            {
                throw new InvalidOperationException("Hello! Do not give me ID! The database will do this for me!");
            }
            else gitUserRepository.Create(gitUser);
            //throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            if (gitUserRepository.ReadOne(id)==null) 
            {
                
            }
            else if (gitUserRepository.ReadOne(id).Repos != null)
            {

            }
            else if ( gitUserRepository.ReadOne(id).Commits != null)
            {

            }
            else
            gitUserRepository.Delete(id);
            //throw new NotImplementedException();
        }

        public IEnumerable<GitUser> ReadAll()
        {
            return gitUserRepository.ReadAll();
            //throw new NotImplementedException();
        }

        public GitUser ReadOne(int id)
        {
            return gitUserRepository.ReadOne(id);
            //throw new NotImplementedException();
        }

        public void Update(GitUser gitUser)
        {
            if (gitUserRepository.ReadOne(gitUser.ID)!=null)
            {
                gitUserRepository.Update(gitUser);
            }
            
            //throw new NotImplementedException();
        }
        #endregion

        public GitUserLogic(IGitUserRepository gitUserRepository)
        {
            this.gitUserRepository = gitUserRepository;
        }
        #region Non-CRUD
        public int UserCount()
        {
            return gitUserRepository.ReadAll().Count();
            //throw new NotImplementedException();
        }

        #endregion
    }
}
