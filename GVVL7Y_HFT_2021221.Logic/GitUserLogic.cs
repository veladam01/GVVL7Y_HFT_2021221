using GVVL7Y_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVVL7Y_HFT_2021221.Logic
{
    class GitUserLogic : IGitUserLogic
    {
        IGitUserRepository gitUserRepository;

        public GitUserLogic(IGitUserRepository gitUserRepository)
        {
            this.gitUserRepository = gitUserRepository;
        }
        public int UserCount()
        {
            return gitUserRepository.ReadAll().Count();
            //throw new NotImplementedException();
        }
    }
}
