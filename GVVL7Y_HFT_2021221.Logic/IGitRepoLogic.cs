using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVVL7Y_HFT_2021221.Logic
{
    public interface IGitRepoLogic
    {
        public int RepoCount();

        public IEnumerable<KeyValuePair<string, int>> RepoCountByOwners();
    }
}
