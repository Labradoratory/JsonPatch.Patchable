using System;
using System.Collections;
using System.Linq;
using System.Resources;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Newtonsoft.Json.Serialization;

namespace Labradoratory.AspNetCore.JsonPatch.Patchable
{
    /// <summary>
    /// An implementation of <see cref="PocoAdapter"/> that looks for <see cref="PatchableAttribute"/>
    /// to determine if a property is writable.
    /// </summary>
    /// <seealso cref="PocoAdapter" />
    public class PatchablePocoAdapter : PocoAdapter
    {
        /// <inheritdoc />
        public override bool TryTraverse(object target, string segment, IContractResolver contractResolver, out object value, out string errorMessage)
        {
            if (!base.TryTraverse(target, segment, contractResolver, out value, out errorMessage))
                return false;

            // If the value is a list or dictionary, we need to check that the collection can be patched.
            if (value is IList || value is IDictionary)
            {
                // If the property is not patchable, we'll just do a fake add so we can generate the correct message.
                // Using the embeded resources file is a PITA and not much more efficient.
                if (!CanPatchChildren(target, segment, contractResolver))
                    return TryAdd(target, segment, contractResolver, value, out errorMessage);
            }

            return true;
        }

        /// <summary>
        /// Determines whether the property represented by the segment can have its children patched.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="segment">The segment.</param>
        /// <param name="contractResolver">The contract resolver.</param>
        /// <returns>
        ///   <c>true</c> if [is patchable list] [the specified target]; otherwise, <c>false</c>.
        /// </returns>
        protected virtual bool CanPatchChildren(object target, string segment, IContractResolver contractResolver)
        {
            if (contractResolver.ResolveContract(target.GetType()) is JsonObjectContract jsonObjectContract)
            {
                var pocoProperty = jsonObjectContract
                    .Properties
                    .FirstOrDefault(p => string.Equals(p.PropertyName, segment, StringComparison.OrdinalIgnoreCase));

                // Property must have PatchableAttribute or patch operation cannot be applied.
                if (pocoProperty.AttributeProvider.GetAttributes(false).Any(a => a is PatchableAttribute || a is PatchableCollectionAttribute))
                    return true;
            }

            return false;
        }

        /// <inheritdoc />
        protected override bool TryGetJsonProperty(object target, IContractResolver contractResolver, string segment, out JsonProperty jsonProperty)
        {
            if (contractResolver.ResolveContract(target.GetType()) is JsonObjectContract jsonObjectContract)
            {
                var pocoProperty = jsonObjectContract
                    .Properties
                    .FirstOrDefault(p => string.Equals(p.PropertyName, segment, StringComparison.OrdinalIgnoreCase));

                // Property must have PatchableAttribute or patch operation cannot be applied.
                if (pocoProperty != null)
                {
                    jsonProperty = pocoProperty;
                    // If the property is writable, but it doesn't have the PatchableAttribute, then make it not writable.
                    if (jsonProperty.Writable && !pocoProperty.AttributeProvider.GetAttributes(false).Any(a => a is PatchableAttribute))
                        jsonProperty.Writable = false;

                    return true;
                }
            }

            jsonProperty = null;
            return false;
        }
    }
}