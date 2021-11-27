using GVVL7Y_HFT_2021221.Models;
using System;
using System.Linq;

namespace GVVL7Y_HFT_2021221.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Threading.Thread.Sleep(5000);

            RestService rs = new RestService("http://localhost:39621/");

            var res1 = rs.Get<GitCommit>("/gitcommit");

            var res2 = rs.Get<GitRepo>("/gitrepo");

            var res3 = rs.Get<GitUser>("/gituser");
            ;
            //Console.WriteLine("Hello World!");
        }
    }
}
