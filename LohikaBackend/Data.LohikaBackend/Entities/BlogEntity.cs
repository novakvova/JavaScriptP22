using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.LohikaBackend.Entities
{
    [Table("tblBlogs")]
    public class BlogEntity : BaseEntity<int>
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        [StringLength(100)]
        public string Image { get; set; }

        [StringLength(250)]
        public string Slug { get; set; }

        public bool IsShow { get; set; }

        public DateTime DateTimePublish { get; set; }

        public virtual ICollection<BlogImageEntity> BlogImages { get; set; }
    }
}
