using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace Edu.Psu.Ist.DynamicMail
{
    /// <summary>
    /// This class is a singleton registry of all IObjectXmlConverter implementation
    /// for use in writing and reading ObjectXml.
    /// </summary>
    /// <remarks>
    /// This class contains lists of all ObjectXml converters.  Each converter must be registered 
    /// through the LoadConvertersFromStatic method.  Future work should include extending
    /// registration through reading a xml file.  Classes wishing to access the registry should
    /// use the Instance property which will hold the singleton isntance.
    /// </remarks>
    public class ObjectXmlConverterRegistry
    {
        /// <summary>
        /// Stores the singleton instance of the ObjectXmlConverterRegistry
        /// </summary>
        private static ObjectXmlConverterRegistry instance = null;
        /// <summary>
        /// Stroes all registered converters in a Dictionary with a key of
        /// the type on which the converter is used with a value of an
        /// instance of the converter.
        /// </summary>
        private Dictionary<Type, IObjectXmlConverter> converters;

        /// <summary>
        /// Singleton instance property.
        /// </summary>
        /// <value>
        /// The value of this property is the singleton instance of this class.
        /// If the instance has not been instantiated it generates the instance.
        /// </value>
        public static ObjectXmlConverterRegistry Instance
        {
            get
            {
                if (instance == null)
                    instance = new ObjectXmlConverterRegistry();
                    return instance;
            }
        }

        /// <summary>
        /// Constructor for the ObjectXmlConverterRegistry class.  Loads all converter isntances.
        /// </summary>
        /// <remarks>
        /// The constructor instantiates the converter Dictionary instance and loads all
        /// ObjectXml converter instances.
        /// </remarks>
        private ObjectXmlConverterRegistry()
        {
            converters = new Dictionary<Type, IObjectXmlConverter>();
            LoadConverters();
        }

        /// <summary>
        /// This method allows access to the registry to other classes.
        /// </summary>
        /// <remarks>
        /// This method exposes the Dictionary instance to all other classes.
        /// A converter instance is returned based on the type passed to the
        /// method.  If the object type returned has no converter registered
        /// a NoConverterAvailable exception is thrown.
        /// </remarks>
        /// <param name="type">The type the converter recognizes</param>
        /// <returns>A converter recognizing the passed type</returns>
        /// <exception cref="NoConverterAvailable">
        /// Thrown when no converter for the specified type has been registered.
        /// </exception>
        public IObjectXmlConverter LookUpConverter(Type type)
        {
            if (!converters.ContainsKey(type)) throw new NoConverterAvailable(String.Format("No converter exists for type \"{0}\"", type.FullName));
            return converters[type];
        }

        /// <summary>
        /// Registers all converters.
        /// </summary>
        /// <remarks>
        /// This method abstracts how the converters are loaded for easy updating from a static loading
        /// of known classes implementing IObjectXmlConverter and future work involving the loading
        /// of converters from an Xml file or a combination of static and Xml.
        /// </remarks>
        private void LoadConverters()
        {
            LoadConvertersFromStatic();
        }

        /// <summary>
        /// Stub function for future update with loading converters from an Xml file.
        /// </summary>
        private void LoadConvertersFromXml()
        {

        }

        /// <summary>
        /// Method that will register converters.
        /// </summary>
        /// <remarks>
        /// This method must be updated with each new converter that is generated.
        /// </remarks>
        private void LoadConvertersFromStatic()
        {
            converters.Add(typeof(Hashtable), new IndexObjectXMLConverter());
            converters.Add(typeof(ArrayList), new ArrayListObjectXMLConverter());
        }

        public void RegisterConverter(Type t, IObjectXmlConverter converter)
        {
            if (converters.ContainsKey(t))
                if (converters[t].GetType() == converter.GetType())
                    return;
            converters.Add(t, converter);
        }
    }
}
