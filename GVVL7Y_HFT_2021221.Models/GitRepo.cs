using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GVVL7Y_HFT_2021221.Models
{
    [Table("Repositories")]
    public class GitRepo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get;  set; }
        [Required]
        public string Name { get; set; }
        [NotMapped]
        public virtual GitUser Owner { get; set; }
        [ForeignKey(nameof(Owner))]
        public int OwnerID { get;  set; }
        //[Required]
        //public DateTime Created { get; set; }

        [NotMapped] 
        public virtual ICollection<GitCommit> Commits { get; set; }

        public GitRepo()
        {
            Commits = new HashSet<GitCommit>();
        }
    }
}
