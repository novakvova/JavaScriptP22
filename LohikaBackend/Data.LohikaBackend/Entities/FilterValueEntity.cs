using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.LohikaBackend.Entities
{
    [Table("tblFilterValues")]
    public class FilterValueEntity : BaseEntity<int>
    {
        [Required, StringLength(255)]
        public string Name { get; set; }
        [ForeignKey("FilterName")]
        public int FilterNameId { get; set; }
        public virtual FilterNameEntity FilterName { get; set; }
        public virtual ICollection<FilterEntity> Filtres { get; set; }
    }
}
