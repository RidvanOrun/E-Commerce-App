using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ECommerceApp.DomainLayer.Entities.Concrete
{
    public class AppUserToProduct:BaseEntity<int>
    {
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public int AppUserId { get; set; }
        [ForeignKey("AppUserId")]
        public virtual AppUser AppUser { get; set; }


    }
}
