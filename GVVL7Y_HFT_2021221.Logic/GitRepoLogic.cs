using GVVL7Y_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVVL7Y_HFT_2021221.Logic
{
    public class GitRepoLogic : IGitRepoLogic
    {
        IGitRepoRepository gitRepoRepository;

        public GitRepoLogic(IGitRepoRepository gitRepoRepository)
        {
            this.gitRepoRepository = gitRepoRepository;
        }
        public int RepoCount()
        {
            return gitRepoRepository.ReadAll().Count();
            //throw new NotImplementedException();
        }

        public IEnumerable<KeyValuePair<string, int>> RepoCountByOwners()
        {
            return gitRepoRepository.ReadAll().GroupBy(x=>x.Owner).Select(x => new KeyValuePair<string, int>(x.Key.Name, x.Count()));
            throw new NotImplementedException();
        }
    }
}
