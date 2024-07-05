using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CentralisationdeDonnee.Models
{

    [Table("coordinateSystem")]
    public class CoordinateSystem
    {

        [Key]
        [Column("idcoordinate")]
        public int IdUseConstraint { get; set; }


        [Column("nom")]
        public string nom { get; set; }

        [Column("description")]
        public string description { get; set; }


        [SQLiteNetExtensions.Attributes.OneToMany(nameof(Data.Industry))]
        public virtual ICollection<Data> Datas { get; set; }
    }
}