using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemTools.Interfaces;
using DataRepository;

namespace SecurityDataModel.Models
{
    [Table("sec.Groups")]
    public class Group : ModelBase, IGroup
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdGroup { get; set; }

        [Required]
        [StringLength(200)]
        [Column("name")]
        public string GroupName { get; set; }

        public string Description { get; set; }

            #region IMember members

        [NotMapped]
        int IMember.IdMember {
            get { return IdGroup; }
            set { IdGroup = value; }
        }

        [NotMapped]
        string IMember.Name
        {
            get { return GroupName; }
            set { GroupName = value; }
        }

        #endregion
    }
}
