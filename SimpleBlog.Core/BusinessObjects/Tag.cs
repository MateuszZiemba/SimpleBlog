using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlog.Core.BusinessObjects
{
    public class Tag
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string UrlSlug { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}