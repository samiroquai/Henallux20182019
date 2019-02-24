using System;
using System.ComponentModel.DataAnnotations;

namespace DDDDemo.DTO
{
    public class Shop
    {
        public virtual int Id { get; set; }
        [Required]
        public virtual string Name { get; set; }
        public int OwnerId { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
