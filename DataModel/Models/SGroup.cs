using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel.Models
{
    [Table("sec._Group")]
    public class SGroup : Member
    {
        public string Description { get; set; }

        public virtual Member Member { get; set; }
    }
}
