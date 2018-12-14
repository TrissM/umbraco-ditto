﻿using System;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;

namespace Our.Umbraco.Ditto
{
    /// <summary>
    /// The current content processor attribute.
    /// Used for providing Ditto with the current <see cref="IPublishedContent"/> object from Umbraco.
    /// </summary>
    public class CurrentContentAsAttribute : DittoProcessorAttribute
    {
        /// <summary>
        /// Processes the value.
        /// </summary>
        /// <returns>
        /// The <see cref="object" /> representing the processed value.
        /// </returns>
        public override object ProcessValue()
        {
            // NOTE: [LK] In order to prevent an infinite loop / stack-overflow, we check if the
            // property's type matches the containing model's type, then we throw an exception.
            if (this.Context.PropertyInfo.PropertyType == this.Context.TargetType)
            {
                throw new InvalidOperationException($"Unable to process property type '{this.Context.TargetType.Name}', it is the same as the containing model type.");
            }

            return this.Context.Content;
        }
    }
}