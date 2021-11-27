using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GVVL7Y_HFT_2021221.Models
{
    [Table("Users")]
    public class GitUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get;  set; }
        [Required]
        public string Name { get;  set; }
        
        public string EmailContact { get;  set; }
        //[Required]
        //public DateTime Registered { get; set; }

        [NotMapped][JsonIgnore]
        public virtual ICollection<GitCommit> Commits { get; set; }
        [NotMapped][JsonIgnore]
        public virtual ICollection<GitRepo> Repos{ get; set; }

        public GitUser()
        {
            Commits = new HashSet<GitCommit>();
            Repos = new HashSet<GitRepo>();
        }
    }
}
