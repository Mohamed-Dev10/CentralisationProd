namespace CentralisationV0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingOtherTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.data",
                c => new
                    {
                        idData = c.Int(nullable: false, identity: true),
                        Titre = c.String(),
                        AcquisitionDate = c.DateTime(nullable: false),
                        PublicationDate = c.DateTime(nullable: false),
                        UrlData = c.String(),
                        TypeUrlData = c.String(),
                        Kywords = c.String(),
                        DataSize = c.Int(nullable: false),
                        Abstract = c.String(),
                        SpatialResolution = c.String(),
                        IndustryId = c.Int(nullable: false),
                        CoordinateSystemId = c.Int(nullable: false),
                        DataTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idData)
                .ForeignKey("public.coordinateSystem", t => t.CoordinateSystemId, cascadeDelete: true)
                .ForeignKey("public.datatype", t => t.DataTypeId, cascadeDelete: true)
                .ForeignKey("public.industry", t => t.IndustryId, cascadeDelete: true)
                .Index(t => t.IndustryId)
                .Index(t => t.CoordinateSystemId)
                .Index(t => t.DataTypeId);
            
            CreateTable(
                "public.Collaboration",
                c => new
                    {
                        idCollaborateur = c.Int(nullable: false, identity: true),
                        Titre = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        description = c.String(),
                        Duration = c.Int(nullable: false),
                        MarketNumber = c.Int(nullable: false),
                        Keywords = c.Int(nullable: false),
                        idClient = c.Int(nullable: false),
                        idTypeCollaboration = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idCollaborateur)
                .ForeignKey("public.Client", t => t.idClient, cascadeDelete: true)
                .ForeignKey("public.TypeCollaboration", t => t.idTypeCollaboration, cascadeDelete: true)
                .Index(t => t.idClient)
                .Index(t => t.idTypeCollaboration);
            
            CreateTable(
                "public.Role",
                c => new
                    {
                        idRole = c.Int(nullable: false, identity: true),
                        NomRole = c.String(),
                        DescriptionRole = c.String(),
                        KeywordsRole = c.String(),
                    })
                .PrimaryKey(t => t.idRole);
            
            CreateTable(
                "public.Client",
                c => new
                    {
                        idClient = c.Int(nullable: false, identity: true),
                        clientName = c.String(),
                        clientAddress = c.String(),
                        clientEmail = c.String(),
                        clientIndustry = c.String(),
                        clientSize = c.Int(nullable: false),
                        clientType = c.String(),
                        Keywords = c.String(),
                    })
                .PrimaryKey(t => t.idClient);
            
            CreateTable(
                "public.ArcgisSolution",
                c => new
                    {
                        idArcgisSolution = c.Int(nullable: false, identity: true),
                        TitredArcgisSolution = c.String(),
                        DescriptionArcgisSolution = c.String(),
                        KeywordsArcgisSolution = c.String(),
                    })
                .PrimaryKey(t => t.idArcgisSolution);
            
            CreateTable(
                "public.ContactClient",
                c => new
                    {
                        idContactClient = c.Int(nullable: false, identity: true),
                        ContactName = c.String(),
                        clientAddress = c.String(),
                        clientEmail = c.String(),
                        clientIndustry = c.String(),
                        idClient = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idContactClient)
                .ForeignKey("public.Client", t => t.idClient, cascadeDelete: true)
                .Index(t => t.idClient);
            
            CreateTable(
                "public.TypeCollaboration",
                c => new
                    {
                        idTypeCollaboration = c.Int(nullable: false, identity: true),
                        NomType = c.String(),
                        Description = c.String(),
                        Keywords = c.String(),
                    })
                .PrimaryKey(t => t.idTypeCollaboration);
            
            CreateTable(
                "public.coordinateSystem",
                c => new
                    {
                        idcoordinate = c.Int(nullable: false, identity: true),
                        nom = c.String(),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.idcoordinate);
            
            CreateTable(
                "public.datatype",
                c => new
                    {
                        iddatatype = c.Int(nullable: false, identity: true),
                        format = c.String(),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.iddatatype);
            
            CreateTable(
                "public.HistoriqueData",
                c => new
                    {
                        idHistoriqueData = c.Int(nullable: false, identity: true),
                        UrlData = c.String(),
                        DateMise_a_Jours = c.DateTime(nullable: false),
                        idData = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idHistoriqueData)
                .ForeignKey("public.data", t => t.idData, cascadeDelete: true)
                .Index(t => t.idData);
            
            CreateTable(
                "public.location",
                c => new
                    {
                        idlocation = c.Int(nullable: false, identity: true),
                        nom = c.String(),
                    })
                .PrimaryKey(t => t.idlocation);
            
            CreateTable(
                "public.useconstraint",
                c => new
                    {
                        idconstraint = c.Int(nullable: false, identity: true),
                        nom = c.String(),
                    })
                .PrimaryKey(t => t.idconstraint);
            
            CreateTable(
                "public.ApplicationUserCollaborations",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Collaboration_idCollaborateur = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Collaboration_idCollaborateur })
                .ForeignKey("public.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("public.Collaboration", t => t.Collaboration_idCollaborateur, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Collaboration_idCollaborateur);
            
            CreateTable(
                "public.ArcgisSolutionClients",
                c => new
                    {
                        ArcgisSolution_idArcgisSolution = c.Int(nullable: false),
                        Client_idClient = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ArcgisSolution_idArcgisSolution, t.Client_idClient })
                .ForeignKey("public.ArcgisSolution", t => t.ArcgisSolution_idArcgisSolution, cascadeDelete: true)
                .ForeignKey("public.Client", t => t.Client_idClient, cascadeDelete: true)
                .Index(t => t.ArcgisSolution_idArcgisSolution)
                .Index(t => t.Client_idClient);
            
            CreateTable(
                "public.CollaborationDatas",
                c => new
                    {
                        Collaboration_idCollaborateur = c.Int(nullable: false),
                        Data_idData = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Collaboration_idCollaborateur, t.Data_idData })
                .ForeignKey("public.Collaboration", t => t.Collaboration_idCollaborateur, cascadeDelete: true)
                .ForeignKey("public.data", t => t.Data_idData, cascadeDelete: true)
                .Index(t => t.Collaboration_idCollaborateur)
                .Index(t => t.Data_idData);
            
            CreateTable(
                "public.LocationDatas",
                c => new
                    {
                        Location_idlocation = c.Int(nullable: false),
                        Data_idData = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Location_idlocation, t.Data_idData })
                .ForeignKey("public.location", t => t.Location_idlocation, cascadeDelete: true)
                .ForeignKey("public.data", t => t.Data_idData, cascadeDelete: true)
                .Index(t => t.Location_idlocation)
                .Index(t => t.Data_idData);
            
            CreateTable(
                "public.UseConstraintDatas",
                c => new
                    {
                        UseConstraint_IdUseConstraint = c.Int(nullable: false),
                        Data_idData = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UseConstraint_IdUseConstraint, t.Data_idData })
                .ForeignKey("public.useconstraint", t => t.UseConstraint_IdUseConstraint, cascadeDelete: true)
                .ForeignKey("public.data", t => t.Data_idData, cascadeDelete: true)
                .Index(t => t.UseConstraint_IdUseConstraint)
                .Index(t => t.Data_idData);
            
            AddColumn("public.AspNetRoles", "Description", c => c.String());
            AddColumn("public.AspNetRoles", "IsActive", c => c.Boolean());

            AddColumn("public.AspNetRoles", "Discriminator", c => c.String(nullable: false, defaultValue: "Role"));

            // Ajouter la colonne idRole à la table AspNetUsers
            AddColumn("public.AspNetUsers", "idRole", c => c.Int(nullable: false));

            // Créer une clé étrangère entre AspNetUsers et Role
            CreateIndex("public.AspNetUsers", "idRole");
            AddForeignKey("public.AspNetUsers", "idRole", "public.Role", "idRole", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("public.UseConstraintDatas", "Data_idData", "public.data");
            DropForeignKey("public.UseConstraintDatas", "UseConstraint_IdUseConstraint", "public.useconstraint");
            DropForeignKey("public.LocationDatas", "Data_idData", "public.data");
            DropForeignKey("public.LocationDatas", "Location_idlocation", "public.location");
            DropForeignKey("public.data", "IndustryId", "public.industry");
            DropForeignKey("public.HistoriqueData", "idData", "public.data");
            DropForeignKey("public.data", "DataTypeId", "public.datatype");
            DropForeignKey("public.data", "CoordinateSystemId", "public.coordinateSystem");
            DropForeignKey("public.Collaboration", "idTypeCollaboration", "public.TypeCollaboration");
            DropForeignKey("public.CollaborationDatas", "Data_idData", "public.data");
            DropForeignKey("public.CollaborationDatas", "Collaboration_idCollaborateur", "public.Collaboration");
            DropForeignKey("public.Collaboration", "idClient", "public.Client");
            DropForeignKey("public.ContactClient", "idClient", "public.Client");
            DropForeignKey("public.ArcgisSolutionClients", "Client_idClient", "public.Client");
            DropForeignKey("public.ArcgisSolutionClients", "ArcgisSolution_idArcgisSolution", "public.ArcgisSolution");
            DropForeignKey("public.AspNetUsers", "idRole", "public.Role");
            DropIndex("public.AspNetUsers", new[] { "idRole" });
            DropForeignKey("public.ApplicationUserCollaborations", "Collaboration_idCollaborateur", "public.Collaboration");
            DropForeignKey("public.ApplicationUserCollaborations", "ApplicationUser_Id", "public.AspNetUsers");
            DropIndex("public.UseConstraintDatas", new[] { "Data_idData" });
            DropIndex("public.UseConstraintDatas", new[] { "UseConstraint_IdUseConstraint" });
            DropIndex("public.LocationDatas", new[] { "Data_idData" });
            DropIndex("public.LocationDatas", new[] { "Location_idlocation" });
            DropIndex("public.CollaborationDatas", new[] { "Data_idData" });
            DropIndex("public.CollaborationDatas", new[] { "Collaboration_idCollaborateur" });
            DropIndex("public.ArcgisSolutionClients", new[] { "Client_idClient" });
            DropIndex("public.ArcgisSolutionClients", new[] { "ArcgisSolution_idArcgisSolution" });
            DropIndex("public.ApplicationUserCollaborations", new[] { "Collaboration_idCollaborateur" });
            DropIndex("public.ApplicationUserCollaborations", new[] { "ApplicationUser_Id" });
            DropIndex("public.HistoriqueData", new[] { "idData" });
            DropIndex("public.ContactClient", new[] { "idClient" });
            DropIndex("public.AspNetUsers", new[] { "idRole" });
            DropIndex("public.Collaboration", new[] { "idTypeCollaboration" });
            DropIndex("public.Collaboration", new[] { "idClient" });
            DropIndex("public.data", new[] { "DataTypeId" });
            DropIndex("public.data", new[] { "CoordinateSystemId" });
            DropIndex("public.data", new[] { "IndustryId" });
            DropColumn("public.AspNetUsers", "idRole");
            DropColumn("public.AspNetRoles", "Discriminator");
            DropColumn("public.AspNetRoles", "IsActive");
            DropColumn("public.AspNetRoles", "Description");
            DropTable("public.UseConstraintDatas");
            DropTable("public.LocationDatas");
            DropTable("public.CollaborationDatas");
            DropTable("public.ArcgisSolutionClients");
            DropTable("public.ApplicationUserCollaborations");
            DropTable("public.useconstraint");
            DropTable("public.location");
            DropTable("public.HistoriqueData");
            DropTable("public.datatype");
            DropTable("public.coordinateSystem");
            DropTable("public.TypeCollaboration");
            DropTable("public.ContactClient");
            DropTable("public.ArcgisSolution");
            DropTable("public.Client");
            DropTable("public.Role");
            DropTable("public.Collaboration");
            DropTable("public.data");
        }
    }
}
