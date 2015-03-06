using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataRepository;

namespace DataModel.Models
{
    [Table("sec._Grant")]
    public class SGrant : ModelBase
    {
        [Key]
        public int IdGrants { get; set; }

        public int IdSecObject { get; set; }

        public int IdRole { get; set; }

        public int IdAccessType { get; set; }

        public virtual AccessType AccessType { get; set; }

        public virtual SRole SRole { get; set; }

        public virtual SecObject SecObject { get; set; }
    }
}
