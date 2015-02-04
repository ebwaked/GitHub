using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Resume_MIO.Models
{
    public class Post
    {
        public Post()
        {
            this.Comments = new HashSet<Comment>();
        }

        [Key]
        public int ID { get; set; }
        public DateTimeOffset Created { get; set; }
        public Nullable<DateTimeOffset> Updated { get; set; }
        public string Title { get; set; }
        public string Body { get; set;}
        public string MediaURL { get; set;}
        //public string Slug { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }


    }

    public class Comment
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("ParentPost")]
        public int PostID { get; set; }
        // ADD comments to comments
        //[ForeignKey("ParentComment")]
        //public Nullable<int> CommentID { get; set; }
        [ForeignKey("Author")]
        public string AuthorID { get; set; }
        public string Body { get; set; }
        public DateTimeOffset Created { get; set; }
        public Nullable<DateTimeOffset> Updated { get; set;}
        public string UpdateReason { get; set; }

        public virtual Post ParentPost { get; set; }
        public virtual ApplicationUser Author { get; set; }
        // Add comments to commentss
        // public virtual Comment ParentComment { get; set; }
    }

}