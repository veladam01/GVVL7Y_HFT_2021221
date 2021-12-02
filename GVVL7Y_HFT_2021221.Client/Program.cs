using GVVL7Y_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;

using ConsoleTools;

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
            //rs.Post<GitUser>(linus, "gituser");
            //rs.Post<GitUser>(brokep, "gituser");
            GitRepo tpblibrary = new GitRepo { Name = "piratebay torrent collection", OwnerID = 4 };
            //rs.Post<GitRepo>(tpblibrary, "gitrepo");
            GitCommit update1 = new GitCommit { CommitMessage = "added category: media", TargetRepositoryID = 4, CommiterID = 4 };
            GitCommit update2 = new GitCommit { CommitMessage = "added category: software", TargetRepositoryID = 4, CommiterID = 4 };
            GitCommit update3 = new GitCommit { CommitMessage = "backup, restricted access", TargetRepositoryID = 4, CommiterID = 4 };
            GitCommit linux = new GitCommit { CommitMessage = "added linux distributions to software category", TargetRepositoryID = 4, CommiterID = 3 };

            rs.Post<GitUser>(linus, "gituser");
            rs.Post<GitUser>(brokep, "gituser");
            rs.Post<GitRepo>(tpblibrary, "gitrepo");
            rs.Post<GitCommit>(update1, "gitcommit");
            rs.Post<GitCommit>(update2, "gitcommit");
            rs.Post<GitCommit>(update3, "gitcommit");
            rs.Post<GitCommit>(linux, "gitcommit");


            var res1 = rs.Get<GitCommit>("/gitcommit");

            var res2 = rs.Get<GitRepo>("/gitrepo");

            var res3 = rs.Get<GitUser>("/gituser");

            //rs.Delete(1, "/gitusers");
            #region CommitMenu
            var CommitMenu = new ConsoleMenu(args, level: 1)
                    .Add("Get all commits", () =>
                    {
                        var commits = rs.Get<GitCommit>("/gitcommit");
                        if (commits!=null)
                        {
                            foreach (var item in commits)
                            {
                                Console.WriteLine(item.ID + " " + item.User.Name + " " + item.Repo.Name + " " + item.CommitMessage);
                            }
                            Console.WriteLine("Press any key to continue");
                        }
                        else Console.WriteLine("No commits in database. Press any key to continue");
                        Console.ReadLine();
                    })
                    .Add("Get one commit", () =>
                    {
                        bool end = false;
                        do
                        {
                            try
                            {
                                Console.Write("Please give an ID: ");
                                int readcommitid = Convert.ToInt32(Console.ReadLine());
                                string endpoint = "/gitcommit/" + readcommitid;
                                var commit = rs.GetSingle<GitCommit>(endpoint);
                                if (commit != null) Console.WriteLine(commit.ID + " " + commit.User.Name + " " + commit.Repo.Name + " " + commit.CommitMessage+ "\nPress any key to continue");
                                else Console.WriteLine("No commit found with the given ID. Press any key to continue");
                                Console.ReadLine();
                                end = true;

                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Error occured. Would you like to exit the process? (Y||N)");
                                string input = Console.ReadLine();
                                if (input == "Y" || input == "y")
                                {
                                    end = true;
                                }
                                else end = false;
                            }
                        } while (!end);

                        ;
                        Console.ReadLine();
                    })
                    .Add("Add a new commit", () => {
                        bool end = false;
                        do
                        {
                            try
                            {
                                Console.Write("Provide the commit message: ");
                                string cmsg = Console.ReadLine();
                                Console.Write("Provide the key of the commiter: ");
                                int userid = int.Parse(Console.ReadLine());
                                Console.Write("Provide the key of the targeted repository: ");
                                int repoid = int.Parse(Console.ReadLine());

                                GitCommit gitcommit = new GitCommit { CommitMessage = cmsg, CommiterID = userid, TargetRepositoryID = repoid };
                                try
                                {
                                    rs.Post<GitCommit>(gitcommit, "/gitcommit");
                                    Console.WriteLine("Commit was added to database. Press any key to continue");
                                    end = true;
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Error occured. Would you like to exit the process? (Y||N)");
                                string input = Console.ReadLine();
                                if (input == "Y" || input == "y")
                                {
                                    end = true;
                                }
                                else end = false;

                            }

                        }
                        while (!end);
                    })
                    .Add("Update a commit", () => {
                        bool end = false;
                        do
                        {
                            try
                            {
                                Console.WriteLine("ATTENTION! In order this to work, you not only required to fill just the data you want to modify, but you must fill the ones you don't want to modify too!");
                                Console.WriteLine("Please provide the ID of the commit you want to modify");
                                int commitid = int.Parse(Console.ReadLine());
                                Console.Write("Provide the new commit message or the original, if you don't want to modify it: ");
                                string cmsg = Console.ReadLine();
                                Console.Write("Provide the key of the new commiter or the original, if you don't want to modify it: ");
                                int userid = int.Parse(Console.ReadLine());
                                Console.Write("Provide the key of the new targeted repository or the original, if you don't want to modify it: ");
                                int repoid = int.Parse(Console.ReadLine());

                                GitCommit gitcommit = new GitCommit {ID=commitid, CommitMessage = cmsg, CommiterID = userid, TargetRepositoryID = repoid };
                                try
                                {
                                    rs.Put<GitCommit>(gitcommit, "/gitcommit");
                                    Console.WriteLine("Commit {0} was updated! Press any key to continue", commitid);
                                    end = true;
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Error occured. Would you like to exit the process? (Y||N)");
                                string input = Console.ReadLine();
                                if (input == "Y" || input == "y")
                                {
                                    end = true;
                                }
                                else end = false;

                            }

                        }
                        while (!end);
                    })
                    .Add("Delete a commit", () =>
                    {
                        bool end = false;
                        do
                        {
                            try
                            {
                                Console.Write("Please give an ID: ");
                                int deletecommitid = int.Parse(Console.ReadLine());
                                string endpoint = "/gitcommit";
                                try
                                {
                                    rs.Delete(deletecommitid, endpoint);
                                    Console.WriteLine("Git commit with ID {0} deleted. \nPress any key to continue", deletecommitid);
                                    Console.ReadLine();
                                    end = true;
                                }
                                catch (Exception ex)
                                {

                                    Console.WriteLine(ex.Message);
                                }

                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Error occured! Would you like to exit the process? (Y||N)");
                                string input = Console.ReadLine();
                                if (input == "Y" || input == "Y")
                                {
                                    end = true;
                                }
                                else end = false;

                            }
                        } while (!end);
                    })
                    .Add("back to main menu", ConsoleMenu.Close)
                    .Configure(config =>
                    {
                        config.Selector = "--> ";
                        config.EnableFilter = true;
                        config.Title = "Git Commits menu";
                        config.EnableBreadcrumb = true;
                        config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
                    });
            #endregion
            #region Usermenu
            var UserMenu = new ConsoleMenu(args, level: 1)
                    .Add("Get all users", () =>
                    {
                        var users = rs.Get<GitUser>("/gituser");
                        if (users != null)
                        {
                            foreach (var item in users)
                            {
                                Console.WriteLine(item.ID + " " + item.Name + " " + item.EmailContact);
                            }
                            Console.WriteLine("Press any key to continue"); 
                        }
                        else Console.WriteLine("No users in database Press any key to continue");
                        Console.ReadLine();
                    })
                    .Add("Get one user", () =>
                    {
                        bool end = false;
                        do
                        {
                            try
                            {
                                Console.Write("Please give an ID: ");
                                int readuserid = Convert.ToInt32(Console.ReadLine());
                                string endpoint = "/gituser/" + readuserid;
                                var user = rs.GetSingle<GitUser>(endpoint);


                                if (user != null) Console.WriteLine(user.ID + " " + user.Name + " " + user.EmailContact+ "\nPress any key to continue");
                                else Console.WriteLine("No user found with the given ID. Press any key to continue");
                                Console.ReadLine();
                                end = true;
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Error occured. Would you like to exit the process? (Y||N)");
                                string input = Console.ReadLine();
                                if (input == "Y" || input == "y")
                                {
                                    end = true;
                                }
                                else end = false;
                            }

                        } while (!end);

                        ;

                    })
                    .Add("Add a user", () =>
                    {

                        bool end = false;
                        do
                        {
                            try
                            {
                                Console.Write("Provide a username: ");
                                string username = Console.ReadLine();
                                Console.Write("Provide a contact email address (not required): ");
                                string mail = Console.ReadLine();
                                GitUser gitUser = new GitUser { Name = username, EmailContact = mail };
                                try
                                {
                                    rs.Post<GitUser>(gitUser, "/gituser");
                                    Console.WriteLine("User {0} has been added to database. Press any key to continue", username);
                                    Console.ReadLine();
                                    end = true;
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Error occured. Would you like to exit the process? (Y||N)");
                                string input = Console.ReadLine();
                                if (input == "Y" || input == "y")
                                {
                                    end = true;
                                }
                                else end = false;
                            }

                        }
                        while (!end);

                    })
                    .Add("Update a user", () =>
                    {
                        bool end = false;
                        do
                        {
                            try
                            {
                                Console.WriteLine("ATTENTION! In order this to work, you not only required to fill just the data you want to modify, but you must fill the ones you don't want to modify too!");
                                Console.Write("Provide the ID of the user you want to modify ");
                                int userid = int.Parse(Console.ReadLine());
                                Console.Write("Provide the new name of the user or the original, if you don't want to modify it: ");
                                string username = Console.ReadLine();
                                Console.Write("Provide the key of the new id of the user or the original, if you don't want to modify it: ");
                                string emailcontact = Console.ReadLine();
                                GitUser gitUser = new GitUser { Name = username, EmailContact = emailcontact };
                                try
                                {
                                    rs.Put<GitUser>(gitUser, "/gituser");
                                    Console.WriteLine("User {0} has been updated\nPress any key to continue", userid);
                                    end = true;
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Error occured. Would you like to exit the process? (Y||N)");
                                string input = Console.ReadLine();
                                if (input == "Y" || input == "y")
                                {
                                    end = true;
                                }
                                else end = false;

                            }

                        }
                        while (!end);
                    })
                    .Add("Delete a user", () =>
                    {
                        bool end = false;
                        do
                        {
                            try
                            {
                                Console.Write("Please give an ID: ");
                                int deleteuserid = int.Parse(Console.ReadLine());

                                string endpoint = "/gituser";
                                try
                                {
                                    rs.Delete(deleteuserid, endpoint);
                                    Console.WriteLine("Git user with ID {0} deleted. Press any key to continue",deleteuserid);
                                    Console.ReadLine();
                                    end = true;
                                }
                                catch (Exception ex)
                                {

                                    Console.WriteLine(ex.Message);
                                }

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error occured! Would you like to exit the process? (Y||N)");
                                string input = Console.ReadLine();
                                if (input == "Y" || input == "Y")
                                {
                                    end = true;
                                }
                                else end = false;
                                
                            }
                        } while (!end);
                        
                        

                        

                        

                    })
                    .Add("Back to main menu", ConsoleMenu.Close)
                    .Configure(config =>
                    {
                        config.Selector = "--> ";
                        config.EnableFilter = true;
                        config.Title = "Git Users menu";
                        config.EnableBreadcrumb = true;
                        config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
                    });
            #endregion
            #region RepoMenu
            var RepoMenu = new ConsoleMenu(args, level: 1)
                    .Add("Read all repos", () =>
                    {
                        var repos = rs.Get<GitRepo>("/gitrepo");
                        if (repos!=null)
                        {
                            foreach (var item in repos)
                            {
                                Console.WriteLine(item.ID + " " + item.Owner.Name + " " + item.Name);
                            }
                            Console.WriteLine("Press any key to continue");
                        }
                        else Console.WriteLine("no repos in database. Press any key to continue");
                        Console.ReadLine();
                    })
                    .Add("Get one repo", () =>
                    {
                        bool end = false;
                        do
                        {
                            try
                            {
                                Console.Write("Please give an ID: ");
                                int readrepoid = Convert.ToInt32(Console.ReadLine());
                                string endpoint = "/gitrepo/" + readrepoid;
                                var repo = rs.GetSingle<GitRepo>(endpoint);


                                if (repo != null)
                                {
                                    Console.WriteLine(repo.ID + " " + repo.Owner.Name + " " + repo.Name+ "\nPress any key to continue");
                                }
                                else Console.WriteLine("No user found with the given ID. Press any key to continue");
                                Console.ReadLine();
                                end = true;
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Error occured. Would you like to exit the process? (Y||N)");
                                string input = Console.ReadLine();
                                if (input == "Y" || input == "y")
                                {
                                    end = true;
                                }
                                else end = false;

                            }

                        } while (!end);

                        ;

                    })
                    .Add("Add a repo", () => {
                        bool succes=false;
                        do
                        {
                            try
                            {
                                Console.Write("Provide a repo name: ");
                                string reponame = Console.ReadLine();
                                Console.Write("Provide the key of the owner: ");
                                int userkey = int.Parse(Console.ReadLine());
                                GitRepo gitRepo = new GitRepo { Name = reponame, OwnerID = userkey };
                                try
                                {
                                    rs.Post<GitRepo>(gitRepo, "/gitrepo");
                                    Console.WriteLine("Repo {0} added to database. Press any key to continue", reponame);
                                    succes = true;
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                                
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Error occured. Would you like to exit the process? (Y||N)");
                                string input = Console.ReadLine();
                                if (input == "Y"|| input == "Y")
                                {
                                    succes = true;
                                }
                                else succes = false;
                            }

                        }
                        while (!succes);
                                
                    })
                    .Add("Update a repo", () => {
                        bool end = false;
                        do
                        {
                            try
                            {
                                Console.WriteLine("ATTENTION! In order this to work, you not only required to fill just the data you want to modify, but you must fill the ones you don't want to modify too!");
                                Console.Write("Provide the ID of the repo you want to modify ");
                                int repoid = int.Parse(Console.ReadLine());
                                Console.Write("Provide the new name of the repo or the original, if you don't want to modify it: ");
                                string username = Console.ReadLine();
                                Console.Write("Provide the key of the new id of the owner or the original, if you don't want to modify it: ");
                                int newowner= int.Parse( Console.ReadLine());
                                GitRepo gitRepo = new GitRepo{ Name = username, OwnerID=newowner };
                                try
                                {
                                    rs.Put<GitRepo>(gitRepo, "/gitrepo");
                                    Console.WriteLine("Repo {0} has been updated. Press any key to continue", repoid);
                                    end = true;
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Error occured. Would you like to exit the process? (Y||N)");
                                string input = Console.ReadLine();
                                if (input == "Y" || input == "y")
                                {
                                    end = true;
                                }
                                else end = false;

                            }

                        }
                        while (!end);
                    })
                    .Add("Delete a repo", () =>
                    {
                        bool end = false;
                        do
                        {
                            try
                            {
                                Console.Write("Please give an ID: ");
                                int deleterepoid = int.Parse(Console.ReadLine());
                                string endpoint = "/gituser";
                                try
                                {
                                    rs.Delete(deleterepoid, endpoint);
                                    Console.WriteLine("Git repo with ID {0} deleted. Press any key to continue", deleterepoid);
                                    Console.ReadLine();
                                    end = true;
                                }
                                catch (Exception ex)
                                {

                                    Console.WriteLine(ex.Message);
                                }

                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Error occured! Would you like to exit the process? (Y||N)");
                                string input = Console.ReadLine();
                                if (input == "Y" || input == "Y")
                                {
                                    end = true;
                                }
                                else end = false;

                            }
                        } while (!end);
                    })
                    .Add("Back to main menu", ConsoleMenu.Close)
                    .Configure(config =>
                    {
                        config.Selector = "--> ";
                        config.EnableFilter = true;
                        config.Title = "Git Repos menu";
                        config.EnableBreadcrumb = true;
                        config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
                    });
            #endregion
            #region MainMenu


            var menu = new ConsoleMenu(args, level: 0)
              .Add("Commit menu", CommitMenu.Show)
              .Add("Repo menu", RepoMenu.Show)
              .Add("User menu", UserMenu.Show)
              .Add("Exit", () => Environment.Exit(0))
              .Configure(config =>
              {
                  config.Selector = "--> ";
                  config.EnableFilter = true;
                  config.Title = "Git Database Client";
                  config.EnableWriteTitle = true;
                  config.EnableBreadcrumb = true;
              });

            menu.Show();

            #endregion
            //rs.Post<GitUser>(new GitUser { Name = "God", EmailContact = "god@heaven.va" }, "gituser");
            ;

            //Console.WriteLine("Hello World!");
        }
    }
}
