using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataRepository;
using Interfaces;

namespace SecurityDataModel.Models
{
    [Table("sec.Users")]
    public class User : ModelBase, IUser
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUser { get; set; }

        [StringLength(200)]
        public string Login { get; set; }

        [StringLength(200)]
        public string DisplayName { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(100)]
        public string Usersid { get; set; }

        #region IMember members

        [NotMapped]
        int IMember.IdMember
        {
            get { return IdUser; }
            set { IdUser = value; }
        }

        [NotMapped]
        string IMember.Name {
            get { return Login; }
            set { Login = value; }
        }

        #endregion
    }
}
