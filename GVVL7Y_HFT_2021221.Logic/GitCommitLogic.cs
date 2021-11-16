using GVVL7Y_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVVL7Y_HFT_2021221.Logic
{
    class GitCommitLogic : IGitCommitLogic
    {
        IGitCommitRepository gitCommitRepository;

        public GitCommitLogic(IGitCommitRepository gitCommitRepository)
        {
            this.gitCommitRepository = gitCommitRepository;
        }

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
    }
}
