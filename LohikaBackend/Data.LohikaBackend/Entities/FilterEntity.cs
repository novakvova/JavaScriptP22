using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.LohikaBackend.Entities
{
    [Table("tblFilterEntities")]
    public class FilterEntity
    {
        public int FilterValueId { get; set; }
        public virtual FilterValueEntity FilterValueOf { get; set; }

        public int ProductId { get; set; }
        public virtual ProductEntity ProductOf { get; set; }
    }
}
