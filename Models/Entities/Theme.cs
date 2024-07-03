
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CentralisationdeDonnee.Models
{

    [Table("theme")]
    public class Theme
    {

        [Key]
        [Column("IdTheme")]
        public int IdUseConstraint { get; set; }


        [Column("nom")]
        public string nom { get; set; }

        [Column("description")]
        public string description { get; set; }

        [Column("Keywords")]
        public string Keywords { get; set; }


        [OneToMany(nameof(Industry.Theme))]
        public virtual ICollection<Industry> Industries { get; set; }

    }
}