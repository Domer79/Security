using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataRepository;
using Interfaces;

namespace SecurityDataModel.Models
{
    [Table("sec.Groups")]
    public class Group : ModelBase, IGroup, IMember
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdGroup { get; set; }

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
