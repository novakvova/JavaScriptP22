using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.LohikaBackend.Entities
{
    [Table("tblFeedbacks")]
    public class FeedbackEntity : BaseEntity<int>
    {
        [StringLength(4000)]
        public string Message { get; set; }
        [Required, StringLength(256)]
        public string Name { get; set; }
        [StringLength(256)]
        public string Email { get; set; }
        [Required, StringLength(20)]
        public string Phone { get; set; }
    }
}
