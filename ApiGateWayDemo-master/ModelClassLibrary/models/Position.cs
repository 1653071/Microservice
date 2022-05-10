using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelClassLibrary.models
{
    public class Position
    {
        [Key]
        public int positionid { get; set; }
        public string positionname { get; set; }
        public Boolean active { get; set; }
        public DateTime createday { get; set; }
        public DateTime updateday { get; set; }
        public int unittypeid { get; set; }
    }
}
