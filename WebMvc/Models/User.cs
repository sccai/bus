using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebMvc.Models
{
    public class UserLogin
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Display(Name = "用户名", Description = "4-20个字符")]
        [Required(ErrorMessage = "用户名不能为空")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "4-20个字符")]
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Display(Name = "密码", Description = "6-20个字符")]
        [Required(ErrorMessage = "密码不能为空")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "6-20个字符")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        [Display(Name = "验证码", Description = "请输入图片中的验证码")]
        [Required(ErrorMessage = "验证码不能为空")]
        //[StringLength(4, MinimumLength = 4, ErrorMessage = "×")]
        public string VerificationCode { get; set; }
    }
}