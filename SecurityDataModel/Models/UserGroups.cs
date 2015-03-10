using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecurityDataModel.EntityConfigurations;

namespace SecurityDataModel.Models
{
    [Table("sec.UserGroups")]
    public class UserGroups
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdUser { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdGroup { get; set; }

    }

    public class UserGroupsConfiguration : BaseConfiguration<UserGroups>
    {
        public UserGroupsConfiguration()
        {
        }
    }
}
