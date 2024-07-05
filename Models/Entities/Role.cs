using CentralisationV0.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CentralisationdeDonnee.Models
{

    [Table("Role")]
    public class Role
    {
        [Key]
        [Column("idRole")]
        public int idRole { get; set; }

        [Column("NomRole")]
        public string NomRole { get; set; }

        [Column("DescriptionRole")]
        public string DescriptionRole { get; set; }

        [Column("KeywordsRole")]
        public string KeywordsRole { get; set; }

        [SQLiteNetExtensions.Attributes.OneToMany(nameof(ApplicationUser))]
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}