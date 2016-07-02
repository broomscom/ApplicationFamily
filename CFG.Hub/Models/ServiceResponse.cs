// <copyright file="ServiceResponse.cs" company="Broomscom.com">Copyright (c) Broomscom.com</copyright>

namespace CFG.Hub.Models
{
    /// <summary>
    /// Response type enumerator
    /// </summary>
    public enum ResponseType
    {
        /// <summary>
        /// Success code
        /// </summary>
        Success,

        /// <summary>
        /// Error code
        /// </summary>
        Error
    }

    /// <summary>
    /// Service response model
    /// </summary>
    public class ServiceResponse
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public ResponseType Type { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the payload.
        /// </summary>
        /// <value>
        /// The payload.
        /// </value>
        public object Payload { get; set; }
    }
}