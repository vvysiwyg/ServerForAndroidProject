using System;
using System.Collections.Generic;

namespace ServerForAndroidProject.data
{
    public class PharmacyNetwork
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public List<Pharmacy> pharmacyList { get; set; }

        public PharmacyNetwork(Guid id, string name, List<Pharmacy> pharmacyList)
        {
            this.id = id;
            this.name = name;
            this.pharmacyList = pharmacyList;
        }
    }
}
