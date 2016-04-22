//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    using Newtonsoft.Json; 
    
    public partial class Compte
    {
        public Compte()
        {
            this.Transactions = new HashSet<Transactions>();
            this.TransactionPeriodique = new HashSet<TransactionPeriodique>();
            this.Transfert = new HashSet<Transfert>();
            this.Transfert1 = new HashSet<Transfert>();
        }
    
        public int id { get; set; }
        public int idUtilisateur { get; set; }
        public double solde { get; set; }
        public string designation { get; set; }
        public string descript { get; set; }

        [JsonIgnore]
        public virtual Utilisateur Utilisateur { get; set; }
        [JsonIgnore]
        public virtual ICollection<Transactions> Transactions { get; set; }
        [JsonIgnore]
        public virtual ICollection<TransactionPeriodique> TransactionPeriodique { get; set; }
        [JsonIgnore]
        public virtual ICollection<Transfert> Transfert { get; set; }
        [JsonIgnore]
        public virtual ICollection<Transfert> Transfert1 { get; set; }
    }
}