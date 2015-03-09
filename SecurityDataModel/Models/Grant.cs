using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataRepository;
using Interfaces;

namespace SecurityDataModel.Models
{
    [Table("sec.Grants")]
    public class Grant : ModelBase, IGrant, IRole, IAccessType, ISecObject
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdGrants { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdSecObject { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(200)]
        public string ObjectName { get; set; }

        [StringLength(100)]
        public string Type1 { get; set; }

        [StringLength(100)]
        public string Type2 { get; set; }

        [StringLength(100)]
        public string Type3 { get; set; }

        [StringLength(100)]
        public string Type4 { get; set; }

        [StringLength(100)]
        public string Type5 { get; set; }

        [StringLength(100)]
        public string Type6 { get; set; }

        [StringLength(100)]
        public string Type7 { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdRole { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(200)]
        public string RoleName { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdAccessType { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(300)]
        public string AccessName { get; set; }

        public virtual Role Role { get; set; }
        public virtual AccessType AccessType { get; set; }
        public virtual SecObject SecObject { get; set; }
    }
}
