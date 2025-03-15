using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.LohikaBackend.Entities
{
    [Table("tblBlogImages")]
    public class BlogImageEntity : BaseEntity<int>
    {
        [Required, StringLength(255)]
        public string Name { get; set; }
        [ForeignKey("Blog")]
        public int? BlogId { get; set; }
        public virtual BlogEntity Blog { get; set; }
    }
}
