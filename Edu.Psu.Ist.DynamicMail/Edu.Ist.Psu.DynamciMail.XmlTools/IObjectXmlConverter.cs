using System;
using System.Collections.Generic;
using System.Text;

namespace Edu.Psu.Ist.DynamicMail
{
    /// <summary>
    /// This is the interface that all converters for object to ObjectXml transformation
    /// must adhere to for use with the ObjectXmlReader and ObjectXmlWriter classes.
    /// </summary>
    /// <remarks>
    /// All implementations of this class must be registered in the ObjectXmlConverterRegistry class.
    /// Future work should import all instances from a specified Xml file and loaded by reflection.
    /// Classes implementing this interface determine what information must be stored in Xml for a complete
    /// reconstruction of a particular class of objects.  They must also be able to reconstruct an object
    /// from the same information that they save.
    /// Future work on this interface should include generics.
    /// </remarks>
    public interface IObjectXmlConverter
    {
        /// <summary>
        /// This method must take an object that the converter recognizes and return a list of the
        /// primitives that must be saved for object reconstruction.
        /// </summary>
        /// <remarks>
        /// The method needs to return every primitive necessary to rebuild an exact copy of the object
        /// it is saving.  Primitive values include int, float, char, string, and derivative types.
        /// </remarks>
        /// <param name="o">
        /// The object that the converter will convert.  Must be a type appropriate
        /// for the converter implementation.
        /// </param>
        /// <returns>
        /// A List object containing KeyValuePair objects in which the key is the name of a parameter that
        /// needs to be saved for the object and the value is the parameter value represented as a String.
        /// </returns>
        Dictionary<String, String> GetPrimitives(Object o);
        /// <summary>
        /// This method must take an object that the converter recognizes and return a list of the
        /// objects that must be saved for object reconstruction.
        /// </summary>
        /// <remarks>
        /// The method needs to return every object necessary to rebuild an exact copy of the object
        /// it is saving.  All objects being saved must have an instance of IObjectXmlConverter registered
        /// in the ObjectXmlConverterRegistry in order to be saved.  Otherwise an error will be thrown.
        /// Any object that does not have a converter should be stored as a primitive or a converter
        /// should be created.
        /// </remarks>
        /// <param name="o">
        /// The object that the converter will convert.  Must be a type appropriate
        /// for the converter implementation.
        /// </param>
        /// <returns>
        /// A List object containing KeyValuePair objects in which the key is the name of a parameter that
        /// needs to be saved for the object and the value is the parameters value as an object.
        /// </returns>
        Dictionary<String, Object> GetReferences(Object o);
        /// <summary>
        /// This method generates an object based upon two lists of parameter names and values.
        /// </summary>
        /// <remarks>
        /// This method must take two lists of parameter names and values.  The first is a list of the
        /// primitives that have been stored as specified by the GetPrimitives method.  The second is
        /// a list of the object references that have been saved by the GetReferences method.  As those
        /// two methods must insure that all information required for reconstruction has been saved, this
        /// method must reconstruct the object from that data.
        /// </remarks>
        /// <param name="primtivies">
        /// A List of KeyValuePair obejcts in which the key is the name of a saved parameter with a
        /// primitive type and the value is the parameter value that has been saved.
        /// </param>
        /// <param name="references">
        /// A List of KeyValuePair objects in which the key is the name of a saved parameter with a
        /// object type and the value is the reconstructed object that was saved as the parameter value.
        /// </param>
        /// <returns>
        /// The reconstructed instance of the object that was saved with the primitives and references specified.
        /// </returns>
        Object GenerateInstance(Dictionary<String, String> primitives, Dictionary<String, Object> references);
    }
}
