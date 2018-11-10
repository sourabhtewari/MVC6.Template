using FMG.NRT.Objects;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMG.NRT.Tests.Objects
{
    public class TestModel : BaseModel
    {
        [StringLength(128)]
        public String Title { get; set; }
    }
}
