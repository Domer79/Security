using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SystemTools.Interfaces;
using DataRepository;

namespace SecurityDataModel.Models
{
    [Table("sec.UserGroupsDetail")]
    public class UserGroupsDetail : ModelBase, IUserGroupsDetail
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdUser { get; set; }

        public string Login { get; set; }

        public string DisplayName { get; set; }

        public string Email { get; set; }

        public string Usersid { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdGroup { get; set; }

        public string GroupName { get; set; }

        public string GroupDescription { get; set; }

        [NotMapped]
        string IGroup.Description
        {
            get { return GroupDescription; }
            set { GroupDescription = value; }
        }

        [NotMapped]
        public int IdMember
        {
            get { throw new NotImplementedException();}
            set { throw new NotImplementedException(); }
        }

        [NotMapped]
        public string Name
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}
