using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CentralisationdeDonnee.Models
{

    [Table("useconstraint")]
    public class UseConstraint
    {
        [Key]
        [Column("idconstraint")]
        public int IdUseConstraint { get; set; }


        [Column("nom")]
        public string nom { get; set; }



        [ManyToMany(typeof(Data))]
        public virtual ICollection<Data> Datas { get; set; }
    }
}