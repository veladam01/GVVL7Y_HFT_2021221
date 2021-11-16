using GVVL7Y_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVVL7Y_HFT_2021221.Logic
{
    interface IGitCommitLogic
    {
        

        int CommitCount();

        IEnumerable<KeyValuePair<string, int>> CommitCountByUsers();

        IEnumerable<KeyValuePair<string, int>> CommitCountByRepos();
    }
}
