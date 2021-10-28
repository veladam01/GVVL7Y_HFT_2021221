using GVVL7Y_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVVL7Y_HFT_2021221.Repository
{
    public interface IGitCommitRepository
    {
        #region CRUD
        //create
        void Create(GitCommit gitCommit);

        //readall
        IQueryable<GitCommit> ReadAll();

        //readone
        GitCommit ReadOne(int id);

        //update
        void Update(GitCommit gitCommit);

        //delete
        void Delete(int id);
        #endregion
    }
}
