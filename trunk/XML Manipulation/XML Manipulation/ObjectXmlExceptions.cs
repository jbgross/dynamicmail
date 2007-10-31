using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ObjectXml
{
    /// <summary>
    /// This is the base exception for all ObjectXml exceptions
    /// </summary>
    public abstract class ObjectXmlException : Exception
    {
        public ObjectXmlException() : base() { }
        public ObjectXmlException(String message) : base(message) { }
        public ObjectXmlException(String message, Exception innerException) : base(message, innerException) { }
        public ObjectXmlException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    /// <summary>
    /// This is the base exception for all ObjectXml Converter exceptions
    /// </summary>
    public abstract class ObjectXmlConverterException : ObjectXmlException
    {
        public ObjectXmlConverterException() : base() { }
        public ObjectXmlConverterException(String message) : base(message) { }
        public ObjectXmlConverterException(String message, Exception innerException) : base(message, innerException) { }
        public ObjectXmlConverterException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    /// <summary>
    /// This is the base exception for all ObjectXml Writer exceptions
    /// </summary>
    public abstract class ObjectXmlWriterException : ObjectXmlException
    {
        public ObjectXmlWriterException() : base() { }
        public ObjectXmlWriterException(String message) : base(message) { }
        public ObjectXmlWriterException(String message, Exception innerException) : base(message, innerException) { }
        public ObjectXmlWriterException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    /// <summary>
    /// This is the base exception for all ObjectXml Reader exceptions
    /// </summary>
    public abstract class ObjectXmlReaderException : ObjectXmlException
    {
        public ObjectXmlReaderException() : base() { }
        public ObjectXmlReaderException(String message) : base(message) { }
        public ObjectXmlReaderException(String message, Exception innerException) : base(message, innerException) { }
        public ObjectXmlReaderException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    /// <summary>
    /// This exception is thrown when an IObjectXmlConverter instance cannot be found for a specific type.
    /// </summary>
    public class NoConverterAvailable : ObjectXmlConverterException
    {
        public NoConverterAvailable() : base() { }
        public NoConverterAvailable(String message) : base(message) { }
        public NoConverterAvailable(String message, Exception innerException) : base(message, innerException) { }
        public NoConverterAvailable(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    /// <summary>
    /// This exception is thrown when a circular dependency is discovered while attempting to write objects
    /// to ObjectXml.  A circular dependency occurs when the object to be written has a decendant who reference
    /// the object to eb written.
    /// </summary>
    public class CircularDependencyException : ObjectXmlWriterException
    {
        public CircularDependencyException() : base() { }
        public CircularDependencyException(String message) : base(message) { }
        public CircularDependencyException(String message, Exception innerException) : base(message, innerException) { }
        public CircularDependencyException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    /// <summary>
    /// This exception is thrown when an ObjectXml stream does not follow the schema
    /// </summary>
    public class MalformedXmlException : ObjectXmlReaderException
    {
        public MalformedXmlException() : base() { }
        public MalformedXmlException(String message) : base(message) { }
        public MalformedXmlException(String message, Exception innerException) : base(message, innerException) { }
        public MalformedXmlException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    /// <summary>
    /// This exception is thrown when an object references another object that has not already been read
    /// from the ObjectXml reader stream.
    /// </summary>
    public class InvalidObjectIDException : ObjectXmlReaderException
    {
        public InvalidObjectIDException() : base() { }
        public InvalidObjectIDException(String message) : base(message) { }
        public InvalidObjectIDException(String message, Exception innerException) : base(message, innerException) { }
        public InvalidObjectIDException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    /// <summary>
    /// This exception is thrown when an error occurs while reconstructing an object in a converter.
    /// </summary>
    public class ReconstructionException : ObjectXmlConverterException
    {
        public ReconstructionException() : base() { }
        public ReconstructionException(String message) : base(message) { }
        public ReconstructionException(String message, Exception innerException) : base(message, innerException) { }
        public ReconstructionException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
