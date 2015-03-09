using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityDataModel.Models
{
    [Table("sec.AccessType")]
    public partial class AccessType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idAccessType { get; set; }

        [Required]
        [StringLength(300)]
        public string name { get; set; }

        public HashSet<Grant> Grants { get; set; }
    }
}
