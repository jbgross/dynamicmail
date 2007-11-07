using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Edu.Psu.Ist.DynamicMail
{
    /// <summary>
    /// This class generates objects from ObjectXml Xml files.  The input uses 
    /// the ObjectXML.xsd schema for validation.
    /// </summary>
    /// <remarks>
    /// The ObjectXmlReader is one of the two key classes of the ObjectXml project.
    /// This class is responsible for all actions related to generating objects from
    /// ObjectXml.  It is responsible for setting up the read stream.  It is responsible
    /// for reading an object's data and determining the type of the object stored.
    /// It must generate a list of primitives and object references to give the
    /// IObjectXmlConverter isntance in order to reconstruct the object stored.
    /// Two lists are created for every object read in although only the lists for
    /// the current object are in memory.  The class does not buffer the XML file.
    /// Thus stored objects must appear in reverse dependency order.  That is to
    /// say, an object must appear after all objects on which it is dependent.
    /// Otherwise a missing dependency error will be generated.  Additionally
    /// all primitive values for an object must be stored before any object values
    /// for the stored object.  This class is Thread safe.
    /// </remarks>
    public class ObjectXmlReader
    {
        /// <summary>
        /// String constants corresponding to the names of tags in the ObjectXml schema
        /// </summary>
        private static String
            ROOT_ELEMENT = "SavedObjects",
            OBJECT_ELEMENT_NAME = "Object",
            PRIMITIVE_ELEMENT_NAME = "Primitive",
            REFERENCE_ELEMENT_NAME = "Reference",
            CLASS_TYPE = "Class",
            ROOT_OBJECT = "RootObject",
            OBJECT_ID = "ObjectID",
            VARIABLE_NAME = "VariableName",
            VALUE = "Value";

        /// <summary>
        /// Generates a list of objects stored in an ObjectXml XML file.
        /// </summary>
        /// <remarks>
        /// This method setups up the stream from which the ObjectXml will
        /// be read, reads the ObjectXml, and then closes the stream.
        /// </remarks>
        /// <param name="sourceLocation">The location of the ObjectXml XML file.</param>
        /// <returns>A List of objects explicitly stored in the ObjectXml XML file.</returns>
        public List<Object> ReadObjectXml(String sourceLocation)
        {
            XmlReader reader = SetupXmlReadStream(sourceLocation);
            List<Object> readObjects = ReadObjectXml(reader);
            CloseXmlReadStream(reader);
            return readObjects;
        }

        /// <summary>
        /// Generates objects from ObjectXml in the given stream.
        /// </summary>
        /// <remarks>
        /// This method reads in each object stored in ObjectXml and adds it
        /// to a dictionary indexed by the ObjectID.  The stream is validated using
        /// the ObjectXml schema.  If the stream does not start with the root element
        /// then a MalformedXmlException is thrown.  The method relies on correct
        /// validation provided by the underlying XmlReader.  For each object stored,
        /// the method reads the meta information about the object and then
        /// reads the primitives and object references associated with the object.
        /// It then looks up the converter from the registry based on the type
        /// specified in the meta information.  If the object references an object
        /// that has not yet been reconstructed, an exception is thrown.  The method
        /// only returns objects that were explictly saved as root objects.  All
        /// other objects that were saved implicitly due to dependencies are
        /// reconstructed but not returned and should be accessed through the 
        /// root objects.
        /// </remarks>
        /// <param name="reader">The stream from which to read ObjectXml</param>
        /// <returns>A list of explicitly saved objects in the specified stream</returns>
        /// <exception cref="MalformedXmlException">
        /// Thrown if the XML stream does not correspond to a correctly formed ObjectXml document.
        /// </exception>
        /// <exception cref="InvalidObjectIDException">
        /// Thrown if the object being read references an object not yet read.
        /// </exception>
        private List<Object> ReadObjectXml(XmlReader reader)
        {
            List<Object> rootObjects = new List<Object>();
            Dictionary<Int32, Object> readObjects = new Dictionary<Int32, Object>();
            reader.Read();
            if (!(reader.Read() && reader.Name.Equals(ROOT_ELEMENT)))
                throw new MalformedXmlException();
            ObjectXmlConverterRegistry registry = ObjectXmlConverterRegistry.Instance;
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name.Equals(OBJECT_ELEMENT_NAME))
                {
                    int id = Int32.Parse(reader.GetAttribute(OBJECT_ID));
                    String classType = reader.GetAttribute(CLASS_TYPE);
                    bool readObject = Boolean.Parse(reader.GetAttribute(ROOT_OBJECT));
                    Dictionary<String, String> primitives = new Dictionary<String, String>();
                    Dictionary<String, Object> references = new Dictionary<String, Object>();
                    while (!(reader.NodeType == XmlNodeType.EndElement && reader.Name.Equals(OBJECT_ELEMENT_NAME)))
                    {
                        if (reader.Name.Equals(PRIMITIVE_ELEMENT_NAME))
                            primitives.Add(
                                reader.GetAttribute(VARIABLE_NAME),
                                reader.GetAttribute(VALUE));
                        else if (reader.Name.Equals(REFERENCE_ELEMENT_NAME))
                        {
                            String name = reader.GetAttribute(VARIABLE_NAME);
                            int rid = Int32.Parse(reader.GetAttribute(OBJECT_ID));
                            if (!readObjects.ContainsKey(rid)) throw new InvalidObjectIDException(rid.ToString());
                            references.Add(name, readObjects[id]);
                        }
                        reader.Read();
                    }
                    IObjectXmlConverter converter = registry.LookUpConverter(Type.GetType(classType));
                    Object o = converter.GenerateInstance(primitives, references);
                    readObjects.Add(id, o);
                    if (readObject)
                        rootObjects.Add(o);
                }
            }
            return rootObjects;
        }

        /// <summary>
        /// Sets up of a XmlReader instance for the given file.
        /// </summary>
        /// <remarks>
        /// Sets up the Xml reader to ignore comments, white space, and processing instructions.
        /// The reader also validates based on the ObjextXml schema in ObjectXML.xsd.
        /// </remarks>
        /// <param name="file">The file to read from</param>
        /// <returns>A ObjectXml schema validating XmlReader</returns>
        private XmlReader SetupXmlReadStream(String file)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            settings.IgnoreProcessingInstructions = true;
            settings.IgnoreWhitespace = true;
            settings.ValidationFlags = System.Xml.Schema.XmlSchemaValidationFlags.ProcessSchemaLocation;
            settings.ValidationType = ValidationType.Schema;
            XmlReader reader = XmlReader.Create(file, settings);
            return reader;
        }

        /// <summary>
        /// Performs closing operations on the XmlReader stream specified
        /// </summary>
        /// <param name="reader">The stream to close</param>
        private void CloseXmlReadStream(XmlReader reader)
        {
            reader.Close();
        }
    }
}
