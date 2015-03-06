using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel.Models
{
    [Table("sec._User")]
    public class SUser : Member
    {
        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string Sid { get; set; }

        [StringLength(200)]
        public string DisplayName { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        public virtual Member Member { get; set; }
    }
}
