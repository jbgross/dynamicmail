using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Edu.Psu.Ist.DynamicMail
{
    /// <summary>
    /// This class generates ObjectXml Xml files for the specified object(s) in the
    /// specified location.  The output uses the ObjectXML.xsd schema for validation.
    /// </summary>
    /// <remarks>
    /// The ObjectXmlWriter is one of the two key classes of the ObjectXml project.
    /// This class is responsible for all actions related to generating XML from an
    /// object or a list of objects.  It is responsible for setting up the write
    /// stream.  It is responsible for recursivly saving all objects referenced by 
    /// the object it is writing.  This implementation generates the XML inline
    /// and does not buffer any XML to memory during execution.  All referenced
    /// objects must be written before the current object is written in this
    /// implementation.  Two lists for every object being saved are generated
    /// to hold all primitive values and object references of an object being saved.
    /// These lists are comprised of KeyValuePair objects that are created at the time
    /// of writer execution.  These lists also contain String names for each parameter.
    /// Additionally, a list is generated of every object being saved with a
    /// corresponding integer id.  This class is thread safe.
    /// </remarks>
    public class ObjectXmlWriter
    {
        /// <summary>
        /// This is the file location of the ObjectXML.xsd schema.
        /// </summary>
        private static String SCHEMA_LOCATION = "ObjectXML.xsd";

        /// <summary>
        /// This method sets up a XmlWriter to write all XML generated.
        /// </summary>
        /// <remarks>
        /// XmlWriters are generated using the XmlTextWriter implementation
        /// of XmlWriter.  Standard XML header information is written with
        /// the document start and attribute string methods.
        /// </remarks>
        /// <param name="outputLocation">The location of the output file.</param>
        /// <returns>An XmlWriter isntance for streaming generated XML.</returns>
        private XmlWriter SetupXmlWriteStream(String outputLocation)
        {
            XmlTextWriter writeStream = new XmlTextWriter(outputLocation, Encoding.ASCII);
            writeStream.Formatting = Formatting.Indented;
            writeStream.Indentation = 1;
            writeStream.IndentChar = '\t';
            writeStream.WriteStartDocument();
            writeStream.WriteStartElement("SavedObjects");
            writeStream.WriteAttributeString("xmlns", "http://www.w3schools.com");
            writeStream.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            writeStream.WriteAttributeString("xsi:schemaLocation", SCHEMA_LOCATION);
            return writeStream;
        }

        /// <summary>
        /// Closes the XmlWriter stream that has been used to export all objects as XML.
        /// </summary>
        /// <remarks>
        /// The WriteEndDocument method handles closing all open tags including the root element.
        /// </remarks>
        /// <param name="writeStream">The stream to close.</param>
        private void CloseXmlWriteStream(XmlWriter writeStream)
        {
            writeStream.WriteEndDocument();
            writeStream.Close();
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// This is one of two methods publicly accessible for writing objects to
        /// ObjectXml.  This method takes a single base object to output while
        /// the other takes a list of objects.  This method should be used
        /// if a single base objects is to be stored in a file.  If mutliple base
        /// objects need to be saved the other method signature should
        /// be used.  (A base object is an explicit object to save as opposed to
        /// an object saved due to a dependency for object reconstruction.)
        /// <param name="objectToWrite">The object to write out to ObjectXml.</param>
        /// <param name="outputLocation">The location where the output file should be created.</param>
        public void WriteObjectXml(Object objectToWrite, String outputLocation)
        {
            List<Object> temp = new List<Object>();
            temp.Add(objectToWrite);
            WriteObjectXml(temp, outputLocation);
        }


        /// <summary>
        /// Writes a lists of objects as ObejctXml in the specified output file.
        /// </summary>
        /// <remarks>
        /// This is one of two methods publicly accessible for writing objects to
        /// ObjectXml.  This method takes multiple base objects to output while
        /// the other takes only a single object.  This method should be used
        /// if multiple base objects are to be stored in the same file.  If only a
        /// single base object needs to be saved the other method signature should
        /// be used.  (A base object is an explicit object to save as opposed to
        /// an object saved due to a dependency for object reconstruction.)  A
        /// Dictionary is created to contain all objects that are written to 
        /// ObjectXml and their respective ObjectIDs so that other objects may
        /// reference them in the XML file.
        /// </remarks>
        /// <param name="objectsToWrite">The List of objects to write out to ObjectXml.</param>
        /// <param name="outputLocation">The location where the output file should be created.</param>
        public void WriteObjectXml(List<Object> objectsToWrite, String outputLocation)
        {
            XmlWriter writeStream = SetupXmlWriteStream(outputLocation);
            Int32 id = 1;
            Dictionary<Object, Int32> objectIds = new Dictionary<Object, Int32>();
            foreach (Object objectToWrite in objectsToWrite)
                WriteObjectXml(objectsToWrite, objectToWrite, writeStream, objectIds, ref id);
            CloseXmlWriteStream(writeStream);
        }

        /// <summary>
        /// Writes a given object to ObjectXML using the given stream, objectID, and objectID
        /// lookup table for other saved objects.
        /// </summary>
        /// <remarks>
        /// This method is the first of two methods used for writing objects to ObjectXML.
        /// This method recursivly writes every object that the current object is dependent
        /// on for reconstruction unless the object is already written.  If the object has
        /// allready been written then it has been assigned an ObjectID for reference.
        /// Circular dependencies are detercted by checking that the ObjectID does not
        /// equal 0 for any object who has already been added to the lookup table.
        /// A ObjectID of 0 signifies that the object must be an ancestor since only
        /// objects that have been seen and not written will be in the table and have
        /// an ID of 0.  If the object was written then the ID would not be 0.  Additionally
        /// if the object was not an ancestor than either the object has not been seen before
        /// or the object belongs to a Depth First branch that has been entirely written.
        /// However if the object has not been seen then it cannot be in the table.  Also if the
        /// object had belonged to another Depth First branch then it would have to have been written
        /// and could not have ID 0.  Circular dependencies are not handled.  They are just
        /// identified.  Future implementations may resolve this issue.  Note that a List object
        /// is created for every object that the current object is dependent on in order to
        /// contain the objects that the dependent objects are dependent on.  A converter is
        /// retrieved from the registry for each object.  The id is passed by reference
        /// in order to make this object thread safe.  The root objects are objects that are
        /// explicitly being saved as opposed to objects incidently being saved due to
        /// reconstruction dependencies.
        /// </remarks>
        /// <param name="rootObjects">The root objects being written</param>
        /// <param name="objectToWrite">The object to write</param>
        /// <param name="writeStream">The XML stream to output to.</param>
        /// <param name="objectIDLookupTable">A lookup table with all previously seen objects and their reference ID.</param>
        /// <param name="id">The current ID counter.</param>
        /// <exception cref="CircularDependencyException">
        /// Thrown when a circular dependency is detected in the objects to be written.
        /// </exception>
        private void WriteObjectXml(List<Object> rootObjects, Object objectToWrite, XmlWriter writeStream, Dictionary<Object, Int32> objectIDLookupTable, ref Int32 id)
        {
            objectIDLookupTable.Add(objectToWrite, 0);
            IObjectXmlConverter converter = ObjectXmlConverterRegistry.Instance.LookUpConverter(objectToWrite.GetType());
            Dictionary<String, Object> references = converter.GetReferences(objectToWrite);
            foreach (String key in references.Keys)
                if (objectIDLookupTable.ContainsKey(references[key]))
                { if (objectIDLookupTable[references[key]] == 0) throw new CircularDependencyException(String.Format("A circular dependcy exists in the objects to write.  Object circularly dependent is: {0}", references[key].ToString())); }
                else
                    WriteObjectXml(rootObjects, references[key], writeStream, objectIDLookupTable, ref id);
            objectIDLookupTable[objectToWrite] = id++;
            Dictionary<String, String> primitives = converter.GetPrimitives(objectToWrite);
            WriteObjectXml(rootObjects, objectToWrite, writeStream, objectIDLookupTable, primitives, references);
        }

        /// <summary>
        /// Writes a given object to ObjectXML using the given stream, objectID lookup table
        /// for other saved objects, a list of primitives associated with the object to be 
        /// written, and a list of objects on which the object to be written is dependent.
        /// </summary>
        /// <remarks>
        /// This method is the second of two methods used for writing objects to ObjectXML.
        /// This method is the method actually responsible for the physical writing of an object
        /// to file.  A list of all primitives to write to the object as well as a list of
        /// objects that the object to be written references must be provided.  The method
        /// begins by writting the &lt;Object&gt; tag with the Class attribute which is the
        /// type of the object to be written.  Next all primitives must be output as a
        /// singleton &lt;Primitive&gt; tag with a variable name and value attribute.  Then
        /// all object references are output in &lt;Reference&gt; tags containing two
        /// attributes containing the variable name and the ObjectID assigned to the
        /// referenced object.  The root objects are objects that are
        /// explicitly being saved as opposed to objects incidently being saved due to
        /// reconstruction dependencies.
        /// </remarks>
        /// <param name="rootObjects">The root objects being written</param>
        /// <param name="objectToWrite">The object to write</param>
        /// <param name="writeStream">The stream to write to.</param>
        /// <param name="objectIDLookupTable">A lookup table with all previously seen objects and their reference ID.</param>
        /// <param name="primitives">A List of primitives need to reconstruct the object being written</param>
        /// <param name="references">A List of objects needed to reconstruct the object being written</param>
        private void WriteObjectXml(List<Object> rootObjects, Object objectToWrite, XmlWriter writeStream, Dictionary<Object, Int32> objectIDLookupTable,
            Dictionary<String, String> primitives, Dictionary<String, Object> references)
        {
            writeStream.WriteStartElement("Object");
            writeStream.WriteAttributeString("Class", objectToWrite.GetType().AssemblyQualifiedName);
            writeStream.WriteAttributeString("ObjectID", objectIDLookupTable[objectToWrite].ToString());
            writeStream.WriteAttributeString("RootObject", rootObjects.Contains(objectToWrite).ToString());
            foreach (String key in primitives.Keys)
            {
                writeStream.WriteStartElement("Primitive");
                writeStream.WriteAttributeString("VariableName", key);
                writeStream.WriteAttributeString("Value", primitives[key].ToString());
                writeStream.WriteEndElement();
            }
            foreach (String key in references.Keys)
            {
                writeStream.WriteStartElement("Reference");
                writeStream.WriteAttributeString("VariableName", key);
                writeStream.WriteAttributeString("ObjectID", objectIDLookupTable[references[key]].ToString());
                writeStream.WriteEndElement();
            }
            writeStream.WriteEndElement();
        }
    }
}
