using FMG.NRT.Components.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMG.NRT.Objects
{
    public class ProfileDeleteView : BaseView
    {
        [Required]
        [NotTrimmed]
        [StringLength(32)]
        public String Password { get; set; }
    }
}
