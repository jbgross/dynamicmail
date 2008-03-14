using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Edu.Psu.Ist.DynamicMail
{
    /// <summary>
    /// read/write the social network filters
    /// </summary>
    public class SocialNetworkFilters
    {
        private String fileName = "c:\\snFilters.xml";
        private ArrayList networks = null;

        /// <summary>
        /// The singleton instance
        /// </summary>
        private static SocialNetworkFilters instance = null;

        /// <summary>
        /// Get the singleton instance
        /// </summary>
        public static SocialNetworkFilters Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SocialNetworkFilters();
                }
                return instance;
            }
        }
        
        /// <summary>
        /// Since this is a singleton, the constructor is private
        /// Get instances through the Instance property
        /// </summary>
        private SocialNetworkFilters()
        {
            this.ReadNetworksFromXml();
        }

        private void ReadNetworksFromXml()
        {
            //create an XML reader object
            ObjectXmlReader ReadXML = new ObjectXmlReader();
            //get a list of all the Hashtables within the XML file
            List<Object> IndexList = ReadXML.ReadObjectXml(this.fileName);

            //assign each hashtable to the proper index based on the order that they were saved
            //within the write method
            this.networks = (ArrayList) IndexList[0];
        }

        public void WriteNetworksToXml()
        {
            //create an XML writer object
            ObjectXmlWriter WriteXML = new ObjectXmlWriter();

            //creata list of all the indexes and add each index to it
            List<Object> IndexList = new List<Object>();

            //assign each hashtable to the proper index based on the order that they were saved
            //within the write method
            IndexList.Add(networks);
            //Send the list the the XML writer in order to write it to the specified file
            WriteXML.WriteObjectXml(IndexList, this.fileName);
        }


    }
}
