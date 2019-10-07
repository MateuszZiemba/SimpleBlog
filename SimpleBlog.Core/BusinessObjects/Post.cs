using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlog.Core.BusinessObjects
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        [StringLength(500)]
        public string Title { get; set; }
        [Required]
        [StringLength(500)]
        public string ShortDescription { get; set; }
        [Required]
        [StringLength(5000)]
        public string Description { get; set; }
        [Required]
        [StringLength(1000)]
        public string Meta { get; set; }
        [Required]
        [StringLength(200)]
        public string UrlSlug { get; set; }
        [Required]
        public bool IsPublished { get; set; }
        [Required]
        public DateTime PublishedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        [Required]
        public Category Category { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}