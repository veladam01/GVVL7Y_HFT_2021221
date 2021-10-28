using GVVL7Y_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVVL7Y_HFT_2021221.Repository
{
    public interface IGitRepoRepository
    {
        #region CRUD
        //create
        void Create(GitRepo gitRepo);

        //readall
        IQueryable<GitRepo> ReadAll();

        //readone
        GitRepo ReadOne(int id);

        //update
        void Update(GitRepo gitRepo);

        //delete
        void Delete(int id);
        #endregion
    }
}
