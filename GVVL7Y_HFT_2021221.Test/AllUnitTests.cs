using GVVL7Y_HFT_2021221.Models;
using GVVL7Y_HFT_2021221.Repository;
using GVVL7Y_HFT_2021221.Logic;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System;

namespace GVVL7Y_HFT_2021221.Test
{
    [TestFixture]
    class AllUnitTests
    {
        IGitUserLogic gitUserLogic;
        IGitRepoLogic gitRepoLogic;
        IGitCommitLogic gitCommitLogic;

        [SetUp]
        public void Setup()
        {
            Mock<IGitRepoRepository> mockRepoRepo = new Mock<IGitRepoRepository>();

            Mock<IGitUserRepository> mockUserRepo = new Mock<IGitUserRepository>();

            Mock<IGitCommitRepository> mockCommitRepo = new Mock<IGitCommitRepository>();

            GitUser OE_teacher = new GitUser() { Name = "Teacher of the University of Obuda", EmailContact = "dunnowich@nik.uni-obuda.hu" };
            GitUser oenikprog = new GitUser() { Name = "oenikprog", };
            GitUser hank = new GitUser() { Name = "Hank Pym", EmailContact = "hankpym@villa.in" };
            GitUser gaben = new GitUser() { Name = "Gabe Newell", EmailContact = "gaben@valvesoftware.com" };
            GitUser aperture = new GitUser() { Name = "Aperture Science", EmailContact = "aperture@aperture.com" };
            GitUser caveJ = new GitUser() { Name = "CaveJ", EmailContact = "cavejohnson@aperture.com" };

            GitRepo adt = new GitRepo() { Name = "Advanced Development Techniques", Owner = OE_teacher };
            GitRepo swdd2 = new GitRepo() { Name = "Software Desing and Development 2", Owner = OE_teacher };
            GitRepo ultron = new GitRepo() { Name = "Ultron Source Code", Owner = hank };
            GitRepo hl3 = new GitRepo() { Owner = gaben, Name = "Half-Life 3" };
            GitRepo l4d3 = new GitRepo() { Owner = gaben, Name = "Left 4 Dead 3" };
            GitRepo tf3 = new GitRepo() { Owner = gaben, Name = "Team Fortress 3" };
            GitRepo portal3 = new GitRepo() { Owner = gaben, Name = "Portal 3" };
            GitRepo glados = new GitRepo() { Name = "Genetic Lifeform and Disk Operating System", Owner = aperture };

            mockRepoRepo
                .Setup(x => x.ReadAll())
                .Returns(new List<GitRepo>
                {adt, swdd2, ultron, hl3, l4d3, tf3, portal3, glados }.AsQueryable());

            mockUserRepo
                .Setup(x => x.ReadAll())
                .Returns(new List<GitUser>
                {OE_teacher, oenikprog, hank, gaben, aperture, caveJ }.AsQueryable());

            mockCommitRepo
                .Setup(x => x.ReadAll())
                .Returns(new List<GitCommit>
                {
                    new GitCommit()
                    {
                        User = hank,
                        Repo = ultron,
                        CommitMessage="My little robot's source code!"
                    },
                    new GitCommit()
                    {
                        User = hank,
                        Repo = ultron,
                        CommitMessage="Added TerminateHumanity() method"
                    },
                    new GitCommit()
                    {
                        User = OE_teacher,
                        Repo = swdd2,
                        CommitMessage="Last commit for this repo"
                    },
                    new GitCommit()
                    {
                        User = OE_teacher,
                        Repo = adt,
                        CommitMessage="Welcome to ADT, Students!"
                    },
                    new GitCommit()
                    {
                        User = OE_teacher,
                        Repo=ultron,
                        CommitMessage="Added EmergencyShutdown() and SelfDestroy() methods just in case"
                    },
                    new GitCommit()
                    {
                        User=gaben,
                        Repo=hl3,
                        CommitMessage="NOPE"
                    },
                    new GitCommit()
                    {
                        User=gaben,
                        Repo=l4d3,
                        CommitMessage="NOPE again"
                    },
                    new GitCommit()
                    {
                        User=gaben,
                        Repo=tf3,
                        CommitMessage="Still nope"
                    },
                    new GitCommit()
                    {
                        User=gaben,
                        Repo=hl3,
                        CommitMessage="like it's ever going to happen"
                    },
                    new GitCommit()
                    {
                        User=gaben,
                        Repo=l4d3,
                        CommitMessage="can we even count to 3?"
                    },
                    new GitCommit()
                    {
                        User=gaben,
                        Repo=portal3,
                        CommitMessage="srsly this serie was already closed"
                    },
                    new GitCommit()
                    {
                        User=caveJ,
                        Repo=glados,
                        CommitMessage="Development started"
                    },
                    new GitCommit()
                    {
                        User = hank,
                        Repo=glados,
                        CommitMessage="Added AI"
                    },
                    new GitCommit()
                    {
                        User = hank,
                        Repo=glados,
                        CommitMessage="Evil mode added"
                    },
                    new GitCommit()
                    {
                        User = caveJ,
                        Repo=glados,
                        CommitMessage="Caroline lives in her! :O"
                    },
                    new GitCommit()
                    {
                        User = caveJ,
                        Repo=glados,
                        CommitMessage="Added: Evil mode restrictions by Caroline"
                    },
                }.AsQueryable());
            gitRepoLogic = new GitRepoLogic(mockUserRepo.Object, mockRepoRepo.Object);
            gitUserLogic = new GitUserLogic(mockUserRepo.Object);
            gitCommitLogic = new GitCommitLogic(mockUserRepo.Object, mockRepoRepo.Object,mockCommitRepo.Object);

        }

        [Test]
        public void TestUsernameNull()
        {
            var user = new GitUser();
            Assert.Throws<ArgumentException>(()=>gitUserLogic.Create(user));
        }

        [Test]
        public void TestUserIDNot0()
        {
            var user = new GitUser {Name="Satan himself", ID = 666 };
            Assert.Throws<InvalidOperationException>(() => gitUserLogic.Create(user));
        }
        [Test]
        public void TestRepoNamenull()
        {
            var repo = new GitRepo () ;
            Assert.Throws<ArgumentException>(() => gitRepoLogic.Create(repo));
        }

        [Test]
        public void TestRepoIDNot0()
        {
            var repo = new GitRepo { Name="something", ID = 420};
            Assert.Throws<InvalidOperationException>(() => gitRepoLogic.Create(repo));
        }

        [Test]
        public void TestCommitMessageNull()
        {
            var commit = new GitCommit();
            Assert.Throws<ArgumentException>(() => gitCommitLogic.Create(commit));
        }
        [Test]
        public void TestCommitIDNot0()
        {
            var commit = new GitCommit { CommitMessage="asd", ID = 69 };
            Assert.Throws<InvalidOperationException>(() => gitCommitLogic.Create(commit));
        }

        [Test]
        public void RepoCountTest()
        {
            int ct = gitRepoLogic.RepoCount();
            Assert.That(ct, Is.EqualTo(8));
        }
        [Test]
        public void CommitCountTest()
        {
            int ct = gitCommitLogic.CommitCount();
            Assert.That(ct, Is.EqualTo(16));
        }
        [Test]
        public void UserCountTest()
        {
            int ct = gitUserLogic.UserCount();
            Assert.That(ct, Is.EqualTo(6));
        }
        [Test]
        public void AvgCommitsByUsersTest()
        {
            double avg = gitCommitLogic.AvgCommitByUsers();
            Assert.That(avg, Is.EqualTo(16 / 6));
        }
        [Test]
        public void AvgCommitsByReposTest()
        {
            double avg = gitCommitLogic.AvgCommitByRepos();
            Assert.That(avg, Is.EqualTo(16 / 7));
        }
        [Test]
        public void AvgReposByUsersTest()
        {
            double avg = gitRepoLogic.AvgRepoByUsers();
            Assert.That(avg, Is.EqualTo(7 / 6));
        }
    }
}
