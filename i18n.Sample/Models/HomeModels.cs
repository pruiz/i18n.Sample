using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace i18n.Sample.Models
{
    public class HomeModel
    {
        [Display(Name="Nombre")]
        [MaxLength(5, ErrorMessage="Este es un error sin traducir")]
        public string Name { get; set; }

        [Required]
        public string CampoRequerido { get; set; }
    }
}