using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GVVL7Y_HFT_2021221.Models;
using Microsoft.EntityFrameworkCore;

namespace GVVL7Y_HFT_2021221.Data
{
    public class GitDatabaseContext:DbContext
    {
        #region Fields
        public virtual DbSet<GitRepo> Repositories { get; set; }
        public virtual DbSet<GitCommit> Commits { get; set; }
        public virtual DbSet<GitUser> Users { get; set; }
        #endregion

        public GitDatabaseContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.
                    UseLazyLoadingProxies().
                    UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region FluentAPI
            //FluentAPI, not sure if needed
            modelBuilder.Entity<GitCommit>(entity =>
            {
                entity.HasOne(comitter => comitter.User)
                .WithMany(users => users.Commits)
                .HasForeignKey(committer => committer.CommiterID)
                .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(target => target.Repo)
                .WithMany(repos => repos.Commits)
                .HasForeignKey(target => target.TargetRepositoryID)
                .OnDelete(DeleteBehavior.ClientSetNull);

            });

            /*modelBuilder.Entity<GitUser>(entity =>
            {
                entity.HasMany(repos => repos.Repos)
                  .WithOne(owner => owner.Owner)
                  .HasForeignKey(owner => owner.ID)
                  .OnDelete(DeleteBehavior.ClientSetNull);
            }
            );*/

            modelBuilder.Entity<GitRepo>(entity =>
            {
                entity.HasOne(owner => owner.Owner)
                   .WithMany(repos => repos.Repos)
                   .HasForeignKey(owner => owner.OwnerID)
                   .OnDelete(DeleteBehavior.ClientSetNull);
            }
            );
            //FluentAPI end
            #endregion
            #region DbSeed
            GitUser veladam01 = new GitUser() { ID = 1, Name = "veladam01", EmailContact = "veladam01@gmail.com"  };
            GitUser krakenattack = new GitUser() { ID = 2, Name = "KrakenAttack" };
            //GitUser kovi = new GitUser() { ID = 3, Name = "Kovács András", EmailContact = "kovacs.andras@nik.uni-obuda.hu", Registered = Convert.ToDateTime("2018-09-01") };

            GitRepo GVVL7Y_HFT_2021221 = new GitRepo() { ID = 1, Name = "GVVL7Y_HFT_2021221", OwnerID = veladam01.ID  };
            GitRepo dayofdeath = new GitRepo() { ID = 3, Name = "Day Of Death Source Code", OwnerID = krakenattack.ID  };
            GitRepo riseofundead = new GitRepo() { ID = 2, Name = "Rise of Undead Source Code", OwnerID = krakenattack.ID  };

            modelBuilder.Entity<GitUser>().HasData(veladam01, krakenattack);
            modelBuilder.Entity<GitRepo>().HasData(GVVL7Y_HFT_2021221, riseofundead, dayofdeath);
            modelBuilder.Entity<GitCommit>().HasData(
                new GitCommit()
                {
                    //ID = Convert.ToInt32("9e8f9dab1", 16),
                    ID = 1,
                    CommiterID = veladam01.ID,
                    CommitMessage = "Add .gitignore and .gitattributes.",
                    TargetRepositoryID = GVVL7Y_HFT_2021221.ID
                },
                new GitCommit()
                {
                    //ID = Convert.ToInt32("07415080", 16),
                    ID = 2,
                    CommiterID = veladam01.ID,
                    CommitMessage = "Add project files",
                    TargetRepositoryID = GVVL7Y_HFT_2021221.ID
                },
                new GitCommit()
                {
                    //ID = Convert.ToInt32("07415080", 16),
                    ID = 3,
                    CommiterID = krakenattack.ID,
                    CommitMessage = "Project startup commit",
                    TargetRepositoryID = riseofundead.ID
                    //When = Convert.ToDateTime("2019. 06. 09.")
                },
                new GitCommit()
                {
                    //ID = Convert.ToInt32("07415080", 16),
                    ID = 4,
                    CommiterID = veladam01.ID,
                    CommitMessage = "Weapon models added",
                    TargetRepositoryID = riseofundead.ID
                    //When = Convert.ToDateTime("2019. 06. 13.")
                },
                new GitCommit()
                {
                    //ID = Convert.ToInt32("07415080", 16),
                    ID = 5,
                    CommiterID = krakenattack.ID,
                    CommitMessage = "Zombie AI added",
                    TargetRepositoryID = riseofundead.ID
                    //When = Convert.ToDateTime("2019. 06. 15.")
                },
                new GitCommit()
                {
                    //ID = Convert.ToInt32("07415080", 16),
                    ID = 6,
                    CommiterID = krakenattack.ID,
                    CommitMessage = "Initial commit",
                    TargetRepositoryID = dayofdeath.ID
                    //When = Convert.ToDateTime("2020. 04. 20.")
                }
                );
            #endregion
        }
    }
}
