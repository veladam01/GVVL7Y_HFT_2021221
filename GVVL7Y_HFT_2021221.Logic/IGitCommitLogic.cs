using GVVL7Y_HFT_2021221.Models;
using System.Collections.Generic;

namespace GVVL7Y_HFT_2021221.Logic
{
    public interface IGitCommitLogic
    {
        double AvgCommitByRepos();
        double AvgCommitByUsers();
        int CommitCount();
        IEnumerable<KeyValuePair<string, int>> CommitCountByRepos();
        IEnumerable<KeyValuePair<string, int>> CommitCountByUsers();
        void Create(GitCommit gitCommit);
        void Delete(int id);
        IEnumerable<GitCommit> ReadAll();
        GitCommit ReadOne(int id);
        void Update(GitCommit gitCommit);
    }
}