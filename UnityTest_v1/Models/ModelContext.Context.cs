﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UnityTest_v1.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using Newtonsoft.Json; 
    
    public partial class DbContextEntities : DbContext
    {
        public DbContextEntities()
            : base("name=DbContextEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Categorie> Categorie { get; set; }
        public virtual DbSet<Compte> Compte { get; set; }
        public virtual DbSet<PorteFeuille> PorteFeuille { get; set; }
        public virtual DbSet<Privilege> Privilege { get; set; }
        public virtual DbSet<PrivilegeUtilisateur> PrivilegeUtilisateur { get; set; }
        public virtual DbSet<TransactionPeriodique> TransactionPeriodique { get; set; }
        public virtual DbSet<Transactions> Transactions { get; set; }
        public virtual DbSet<Transfert> Transfert { get; set; }
        [JsonIgnore]
        public virtual DbSet<Utilisateur> Utilisateur { get; set; }
    }
}
