using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityTest_v1.Models; 

namespace UnityTest_v1.IServices
{
    public interface ICategorieService
    {
        Categorie GetCategorieById(int CategoryId);
        IEnumerable<Categorie> GetAllCategories();
        bool AddCategorie(Categorie categorie);
        bool DeleteCategorie(int id);
        bool UpdateCategorie(Categorie categorie);
        
    }
}
