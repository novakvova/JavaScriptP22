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
    [Table("tblOrderEntities")]
    public class OrderEntity : BaseEntity<int>
    {
         /// <summary>
        /// Контакти отримувача
        /// </summary>
        [StringLength(100)]
        public string ConsumerFirstName { get; set; }
        [StringLength(100)]
        public string ConsumerSecondName { get; set; }
        [StringLength(20)]
        public string ConsumerPhone { get; set; }

        /// <summary>
        /// Дані доставки товару
        /// </summary>
        public string Region { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HomeNumber { get; set; }


        [ForeignKey("OrderStatus")]
        public int StatusId { get; set; }

        /// <summary>
        /// Користувач який змовив
        /// </summary>
        [ForeignKey("User")]
        public long UserId { get; set; }

        public virtual OrderStatusEntity OrderStatus { get; set; }
        public virtual AppUser User { get; set; }
        public virtual ICollection<OrderItemEntity> OrderItems { get; set; }
    }
}
