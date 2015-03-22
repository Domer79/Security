using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemTools.Interfaces;
using DataRepository;

namespace SecurityDataModel.Models
{
    [Table("sec.Users")]
    public class User : ModelBase, IUser, IMember
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUser { get; set; }

        [Required]
        [StringLength(200)]
        public string Login { get; set; }

        [StringLength(200)]
        public string DisplayName { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(100)]
        public string Usersid { get; set; }

        [StringLength(32)]
        public string Password { get; set; }

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
