using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Edu.Psu.Ist.DynamicMail.Interface;
using System.Windows.Forms;


namespace Edu.Psu.Ist.DynamicMail
{
    /// <summary>
    /// Class to ready data for clustering
    /// </summary>
    public class PrepareClusterData
    {

        /// <summary>
        /// Constructor, opens and loads index data
        /// </summary>
        public PrepareClusterData()
        {
            Indexes indices = Indexes.Instance;
            indices.ReadIndexFromXML();
            Hashtable addresses = indices.contactsIndex;
            InfoBox box = new InfoBox();
            box.AddText("Size: " + addresses.Count);
            foreach (Object k in addresses.Keys)
            {
                box.AddText(k.ToString());
                box.AddText("\t" + addresses[k].ToString());
            }
        }
    }
}
