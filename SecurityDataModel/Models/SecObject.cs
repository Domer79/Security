using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemTools.Interfaces;
using DataRepository;

namespace SecurityDataModel.Models
{
    [Table("sec.SecObject")]
    public class SecObject : ModelBase, ISecObject
    {
        [Key]
        public int IdSecObject { get; set; }

        [Required]
        [StringLength(200)]
        public string ObjectName { get; set; }

//        [StringLength(100)]
//        public string type1 { get; set; }
//
//        [StringLength(100)]
//        public string type2 { get; set; }
//
//        [StringLength(100)]
//        public string type3 { get; set; }
//
//        [StringLength(100)]
//        public string type4 { get; set; }
//
//        [StringLength(100)]
//        public string type5 { get; set; }
//
//        [StringLength(100)]
//        public string type6 { get; set; }
//
//        [StringLength(100)]
//        public string type7 { get; set; }

        public HashSet<Grant> Grants { get; set; }
    }
}
