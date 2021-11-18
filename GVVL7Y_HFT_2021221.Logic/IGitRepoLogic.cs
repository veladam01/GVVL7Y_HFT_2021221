using GVVL7Y_HFT_2021221.Models;
using System.Collections.Generic;

namespace GVVL7Y_HFT_2021221.Logic
{
    public interface IGitRepoLogic
    {
        double AvgRepoByUsers();
        void Create(GitRepo gitRepo);
        void Delete(int id);
        IEnumerable<GitRepo> ReadAll();
        GitRepo ReadOne(int id);
        int RepoCount();
        IEnumerable<KeyValuePair<string, int>> RepoCountByOwners();
        void Update(GitRepo gitRepo);
    }
}