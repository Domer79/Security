using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataRepository;
using Interfaces;

namespace SecurityDataModel.Models
{
    [Table("sec.Groups")]
    public class Group : ModelBase, IGroup
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdGroup { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(200)]
        public string Name { get; set; }

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
            get { return Name; }
            set { Name = value; }
        }

        #endregion
    }
}
