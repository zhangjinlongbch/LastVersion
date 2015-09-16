using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EssayBashBoard.Models
{
    public class CommentsViewModels
    {
        [Required]
        [Display(Name ="评论人")]
        public string UserId;

        [Required]
        [Display(Name ="文章标题")]
        public string title;

        [Required]
        [Display(Name ="评论内容")]
        [StringLength(200,ErrorMessage ="{0}不能为空",MinimumLength =1)]
        public string Content;
    }
}