using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.LohikaBackend.Entities
{
    public class CategoryEntity : BaseEntity<int>
    {
        public string Title { get; set; }
        public string UrlSlug { get; set; }
        public int Priority { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
        public virtual CategoryEntity Parent { get; set; }
        public virtual ICollection<ProductEntity> Products { get; set; }
        public virtual ICollection<CategoryEntity> Children { get; set; }
        public virtual ICollection<FilterNameEntity> FilterNames { get; set; }
    }
}
