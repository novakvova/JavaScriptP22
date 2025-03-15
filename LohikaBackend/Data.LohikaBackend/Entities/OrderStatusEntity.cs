using Data.LohikaBackend.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.LohikaBackend.Entities
{
    [Table("tblOrderStatusEntities")]
    public class OrderStatusEntity : BaseEntity<int>
    {
        [StringLength(200)]
        public string Name { get; set; }

        public virtual ICollection<OrderEntity> Orders { get; set; }
    }
}
