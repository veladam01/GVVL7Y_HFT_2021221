using GVVL7Y_HFT_2021221.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GVVL7Y_HFT_2021221.WPFClient
{
    public class MainWindowViewModel : ObservableRecipient
    {
        public RestCollection<GitUser> gitUsers { get; set; }
        public RestCollection<GitRepo> gitRepos { get; set; }
        public RestCollection<GitCommit> gitCommits { get; set; }

        public ICommand AddGitUser { get; set; }
        public ICommand EditGitUser { get; set; }
        public ICommand DeleteGitUser { get; set; }
        public ICommand AddGitRepo { get; set; }
        public ICommand EditGitRepo { get; set; }
        public ICommand DeleteGitRepo { get; set; }
        public ICommand AddGitCommit { get; set; }
        public ICommand EditGitCommit { get; set; }
        public ICommand DeleteGitCommit { get; set; }

        private GitUser selectedUser;

        public GitUser SelectedUser
        {
            get { return selectedUser; }
            set
            {
                if (value!=null)
                {
                    selectedUser = new GitUser()
                    {
                        ID = value.ID,
                        Name = value.Name,
                        EmailContact = value.EmailContact,
                        //Repos=value.Repos,
                        //Commits=value.Commits
                    };
                    OnPropertyChanged();
                    (DeleteGitUser as RelayCommand).NotifyCanExecuteChanged();

                }
            }
            
        }

        private GitRepo selectedRepo;

        public GitRepo SelectedRepo
        {
            get { return selectedRepo; }
            set
            {
                if (value != null)
                {
                    selectedRepo = new GitRepo()
                    {
                        ID = value.ID,
                        Name = value.Name,
                        OwnerID = value.OwnerID,
                        //Owner = value.Owner,
                        //Commits = value.Commits,

                    };
                    OnPropertyChanged();
                    (DeleteGitRepo as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        private GitCommit selectedCommit;

        public GitCommit SelectedCommit
        {
            get { return selectedCommit; }
            set
            {
                if (value!=null)
                {
                    selectedCommit = new GitCommit()
                    {
                        ID = value.ID,
                        CommiterID = value.CommiterID,
                        TargetRepositoryID = value.TargetRepositoryID,
                        CommitMessage = value.CommitMessage,
                    };
                    OnPropertyChanged();
                    (DeleteGitCommit as RelayCommand).NotifyCanExecuteChanged();
                } }
        }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }


        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                gitUsers = new RestCollection<GitUser>("http://localhost:39621/", "gitUser", "hub");
                gitRepos = new RestCollection<GitRepo>("http://localhost:39621/", "gitRepo", "hub" );
                gitCommits = new RestCollection<GitCommit>("http://localhost:39621/", "gitCommit", "hub" );

                AddGitUser = new RelayCommand(() =>
                {
                    gitUsers.Add(new GitUser()
                    {
                        Name = SelectedUser.Name,
                        EmailContact = SelectedUser.EmailContact
                    });
                });
                EditGitUser = new RelayCommand(() => gitUsers.Update(selectedUser));

                DeleteGitUser = new RelayCommand(() =>
                {
                    gitUsers.Delete(SelectedUser.ID);
                    //DeleteUser(SelectedUser);
                },
                () => { return SelectedUser != null; }
                );
                selectedUser = new GitUser();

                AddGitRepo = new RelayCommand(() =>
                {
                    gitRepos.Add(new GitRepo()
                    {
                        Name = SelectedRepo.Name,
                        OwnerID = SelectedRepo.OwnerID,

                    });
                }
                );
                EditGitRepo = new RelayCommand(() => gitRepos.Update(selectedRepo));
                DeleteGitRepo = new RelayCommand(() =>
                {
                    gitRepos.Delete(SelectedRepo.ID);
                    //DeleteRepo(SelectedRepo);
                },
                () => { return SelectedRepo != null; });
                selectedRepo = new GitRepo();

                AddGitCommit = new RelayCommand(() =>
                {
                    gitCommits.Add(new GitCommit()
                    {
                        CommitMessage = SelectedCommit.CommitMessage,
                        TargetRepositoryID = SelectedCommit.TargetRepositoryID,
                        CommiterID = SelectedCommit.CommiterID
                    });
                });
                EditGitCommit = new RelayCommand(() => gitCommits.Update(selectedCommit));
                DeleteGitCommit = new RelayCommand(() =>
                {
                    gitCommits.Delete(SelectedCommit.ID);
                },
                () => { return SelectedCommit != null; });
                selectedCommit = new GitCommit();
            }
        }

    }
}
