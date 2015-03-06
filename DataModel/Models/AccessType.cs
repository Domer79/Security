using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel.Models
{
    [Table("sec.AccessType")]
    public sealed class AccessType
    {
        public AccessType()
        {
            SGrant = new HashSet<SGrant>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdAccessType { get; set; }

        [Required]
        [StringLength(300)]
        public string Description { get; set; }

        public ICollection<SGrant> SGrant { get; set; }
    }
}
