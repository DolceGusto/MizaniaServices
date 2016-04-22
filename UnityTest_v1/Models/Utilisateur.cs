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
    
    public partial class Utilisateur
    {
        public Utilisateur()
        {
            this.Compte = new HashSet<Compte>();
            this.PrivilegeUtilisateur = new HashSet<PrivilegeUtilisateur>();
        }
    
        public int id { get; set; }
        public int idPorteFeuille { get; set; }
        public string nomDeCompte { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string roleUtilisateur { get; set; }
    
        [JsonIgnore]
        public virtual ICollection<Compte> Compte { get; set; }

        [JsonIgnore]
        public virtual PorteFeuille PorteFeuille { get; set; }

        [JsonIgnore]
        public virtual ICollection<PrivilegeUtilisateur> PrivilegeUtilisateur { get; set; }
    }
}
