using System;
using System.Collections.Generic;

namespace ServerForAndroidProject.data
{
    public class PharmacyNetworkList
    {
        public List<PharmacyNetwork> pharmacyNetworkList { get; set; }

        public void deletePharmacyNetwork(Guid id)
        {
            foreach (PharmacyNetwork pn in pharmacyNetworkList)
                if (pn.id.Equals(id))
                {
                    pharmacyNetworkList.Remove(pn);
                    break;
                }
        }

        public void deletePharmacy(Guid pnId, Guid pId)
        {
            foreach(PharmacyNetwork pn in pharmacyNetworkList)
            {
                if (pn.id.Equals(pnId))
                {
                    List<Pharmacy> pl = pharmacyNetworkList[pharmacyNetworkList.IndexOf(pn)].pharmacyList;
                    foreach (Pharmacy p in pl)
                        if (p.id.Equals(pId))
                        {
                            pharmacyNetworkList[pharmacyNetworkList.IndexOf(pn)].pharmacyList.Remove(p);
                            break;
                        }
                    break;
                }
            }
        }

        public void updatePharmacyNetwork(PharmacyNetwork pn)
        {
            foreach(PharmacyNetwork pn2 in pharmacyNetworkList)
            {
                if (pn2.id.Equals(pn.id))
                {
                    int index = pharmacyNetworkList.IndexOf(pn2);
                    pharmacyNetworkList[index] = pn;
                    break;
                }
            }
        }

        public void updatePharmacy(Guid pnId, Pharmacy p)
        {
            foreach(PharmacyNetwork pn in pharmacyNetworkList)
            {
                if (pn.id.Equals(pnId))
                {
                    foreach(Pharmacy p2 in pn.pharmacyList)
                    {
                        int index = pn.pharmacyList.IndexOf(p2);
                        pharmacyNetworkList[pharmacyNetworkList.IndexOf(pn)].pharmacyList[index] = p;
                        break;
                    }
                    break;
                }
            }
        }

        public void addPharmacyNetwork(PharmacyNetwork pn)
        {
            pharmacyNetworkList.Add(pn);
        }

        public void addPharmacy(Guid pnId, Pharmacy p)
        {
            foreach(PharmacyNetwork pn in pharmacyNetworkList)
            {
                if (pn.id.Equals(pnId))
                {
                    int index = pharmacyNetworkList.IndexOf(pn);
                    pharmacyNetworkList[index].pharmacyList.Add(p);
                    break;
                }
            }
        }
    }
}
