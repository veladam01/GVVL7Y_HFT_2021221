using GVVL7Y_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVVL7Y_HFT_2021221.Logic
{
    public interface IGitCommitLogic
    {
        

        public int CommitCount();

        public IEnumerable<KeyValuePair<string, int>> CommitCountByUsers();

        public IEnumerable<KeyValuePair<string, int>> CommitCountByRepos();
    }
}
