using Data.LohikaBackend.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.LohikaBackend.Entities
{
    [Table("tblOrderItemEntities")]
    public class OrderItemEntity : BaseEntity<int>
    {
        /// <summary>
        /// Кількість товару, який ми замовили
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Замовлення
        /// </summary>
        [ForeignKey("Order")]
        public int OrderId { get; set; }

        /// <summary>
        /// Продукт, який замовив
        /// </summary>
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public decimal BuyPrice { get; set; }
        public virtual OrderEntity Order { get; set; }
        public virtual ProductEntity Product { get; set; }



    }
}
