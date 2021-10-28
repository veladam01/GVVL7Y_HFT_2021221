using GVVL7Y_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVVL7Y_HFT_2021221.Repository
{
    public interface IGitUserRepository
    {
        #region CRUD
        //create
        void Create(GitUser gitUser);

        //readall
        IQueryable<GitUser> ReadAll();

        //readone
        GitUser ReadOne(int id);

        //update
        void Update(GitUser gitUser);

        //delete
        void Delete(int id);
        #endregion
    }
}
