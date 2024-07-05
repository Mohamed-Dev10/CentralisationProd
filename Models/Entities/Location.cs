using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CentralisationdeDonnee.Models
{

    [Table("location")]
    public class Location
    {

        [Key]
        [Column("idlocation")]
        public int idlocation { get; set; }


        [Column("nom")]
        public string nom { get; set; }


        [ManyToMany(typeof(Data))]
        public virtual ICollection<Data> Datas { get; set; }
    }
}