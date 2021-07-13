using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab5.Models
{
    public class AnswerImage
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Image ID")]
        public int AnswerImageId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "File Name")]
        [StringLength(50, MinimumLength = 5)]
        public string FileName { get; set; }

        [Required]
        [DataType(DataType.Url)]
        [Display(Name = "Image")]
        public string Url { get; set; }

        [Required]
        [EnumDataType(typeof(Question))]
        [Display(Name = "Question")]
        public int QuestionId { get; set; }

        [NotMapped]
        public Question Question
        {
            get
            {
                return (Question)this.QuestionId;
            }

            set
            {
                this.QuestionId = (int)value;
            }
        }
    }
}
