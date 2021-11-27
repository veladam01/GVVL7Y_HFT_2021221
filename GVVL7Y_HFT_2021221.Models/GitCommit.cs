using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GVVL7Y_HFT_2021221.Models
{
    [Table("Commits")]
    public class GitCommit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get;  set; }
        [NotMapped]
        public virtual GitUser User { get; set; }
        
        [ForeignKey(nameof(User))]
        public int CommiterID { get;  set; }
        
        [NotMapped]
        public virtual GitRepo Repo { get; set; }
        [ForeignKey(nameof(Repo))]
        public int TargetRepositoryID { get;  set; }
        [Required]
        public string CommitMessage { get;  set; }
        //[Required]
        //public DateTime When { get; set; }

    }
}
