using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ForeignKeyAttribute = System.ComponentModel.DataAnnotations.Schema.ForeignKeyAttribute;

namespace CentralisationdeDonnee.Models
{

    [Table("data")]
    public class Data
    {
        [Key]
        [Column("idData")]
        public int idData { get; set; }

        [Column("Titre")]
        public string Titre { get; set; }

        [Column("AcquisitionDate")]
        public DateTime AcquisitionDate { get; set; }

        [Column("PublicationDate")]
        public DateTime PublicationDate { get; set; }

        [Column("UrlData")]
        public string UrlData { get; set; }

        [Column("TypeUrlData")]
        public string TypeUrlData { get; set; }

        [Column("Kywords")]
        public string Kywords { get; set; }

        [Column("DataSize")]
        public int DataSize { get; set; }

        [Column("Abstract")]
        public string Abstract { get; set; }

        [Column("SpatialResolution")]
        public string SpatialResolution { get; set; }


        [ForeignKey(nameof(Industry))]
        public int IndustryId { get; set; }
        public virtual Industry Industry { get; set; }


        [ForeignKey(nameof(CoordinateSystem))]
        public int CoordinateSystemId { get; set; }
        public virtual CoordinateSystem CoordinateSystem { get; set; }


        [ForeignKey(nameof(DataType))]
        public int DataTypeId { get; set; }
        public virtual DataTyp DataType { get; set; }

        [OneToMany(nameof(HistoriqueData.Data))]
        public virtual ICollection<HistoriqueData> Historiques { get; set; }


        [ManyToMany(typeof(Location))]
        public virtual ICollection<Location> Locations { get; set; }


        [ManyToMany(typeof(UseConstraint))]
        public virtual ICollection<UseConstraint> UseConstraints { get; set; }


        [ManyToMany(typeof(Collaboration))]
        public virtual ICollection<Collaboration> Collaborations { get; set; }
    }
}