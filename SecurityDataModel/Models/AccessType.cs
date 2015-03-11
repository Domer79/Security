using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure;
using DataRepository;
using Interfaces;

namespace SecurityDataModel.Models
{
    [Table("sec.AccessType")]
    public class AccessType : ModelBase, IAccessType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAccessType { get; set; }

        [Required]
        [StringLength(300)]
        [Column("name")]
        public string AccessName { get; set; }

        public HashSet<Grant> Grants { get; set; }

        public override string ToString()
        {
            return AccessName;
        }
    }
}
