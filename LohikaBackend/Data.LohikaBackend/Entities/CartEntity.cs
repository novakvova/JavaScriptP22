using Data.LohikaBackend.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.LohikaBackend.Entities
{
    [Table("tblCartEntities")]
    public class CartEntity : BaseEntity<int>
    {
        /// <summary>
        /// Кількість товару, який ми замовили
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Користувач який змовив
        /// </summary>
        [ForeignKey("User")]
        public long UserId { get; set; }

        /// <summary>
        /// Продукт, який замовив
        /// </summary>
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public virtual AppUser User { get; set; }
        public virtual ProductEntity Product { get; set; }



    }
}
