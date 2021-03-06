﻿using System.Collections.Generic;
namespace UnityTest_v1.Models
{

    public class FacebookUserModel
    {
       
        public string id { get; set; }
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string gender { get; set; }
        public string locale { get; set; }
        public string link { get; set; }
        public string username { get; set; }
        public int timezone { get; set; }
        public FacebookLocation location { get; set; }
        public Picture picture { get; set; }
        public List<Utilisateur> listUsers { get; set; }

        
    }

}