using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Upp1_admin.Models
{
    public class SchoolCourse
    {

        [Key]
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        public string Id { get; set; }


        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }
        public virtual ICollection<SchoolClass> SchoolClasses { get; set; } //--> schoolclass 
        // kan ha flera olicka classer
        //(tabel -schoolClassId) 

    }
}
