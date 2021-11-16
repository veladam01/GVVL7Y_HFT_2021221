using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVVL7Y_HFT_2021221.Logic
{
    interface IGitRepoLogic
    {
        int RepoCount();

        IEnumerable<KeyValuePair<string, int>> RepoCountByOwners();
    }
}
