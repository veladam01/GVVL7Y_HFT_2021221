using GVVL7Y_HFT_2021221.Models;
using System.Collections.Generic;

namespace GVVL7Y_HFT_2021221.Logic
{
    public interface IGitUserLogic
    {
        void Create(GitUser gitUser);
        void Delete(int id);
        IEnumerable<GitUser> ReadAll();
        GitUser ReadOne(int id);
        void Update(GitUser gitUser);
        int UserCount();
    }
}