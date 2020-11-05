using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StopNShop2.Models
{
    public class ImageUpload
    {

        [Key]
        public int ImageID { get; set; }

        [Display(Name = "Image")]
        public byte[] ImagePath { get; set; }

        [Display(Name = "Filename")]
        public string FileName { get; set; }
    }
}
