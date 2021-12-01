using GVVL7Y_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GVVL7Y_HFT_2021221.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Threading.Thread.Sleep(5000);

            RestService rs = new RestService("http://localhost:39621/");
            GitUser linus = new GitUser { Name = "linus", EmailContact = "linus@torvalds.com" };
            GitUser brokep = new GitUser { Name = "brokep", EmailContact = "brokep@thepiratebay.org" };
            rs.Post<GitUser>(linus, "gituser");
            rs.Post<GitUser>(brokep, "gituser");

            var res1 = rs.Get<GitCommit>("/gitcommit");

            var res2 = rs.Get<GitRepo>("/gitrepo");

            var res3 = rs.Get<GitUser>("/gituser");



            //rs.Post<GitUser>(new GitUser { Name = "God", EmailContact = "god@heaven.va" }, "gituser");
            ;
            
            //Console.WriteLine("Hello World!");
        }
    }
}
