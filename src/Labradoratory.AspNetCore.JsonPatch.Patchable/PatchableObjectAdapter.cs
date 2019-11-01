using System;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Adapters;
using Newtonsoft.Json.Serialization;

namespace Labradoratory.AspNetCore.JsonPatch.Patchable
{
    /// <summary>
    /// An implementation of <see cref="IObjectAdapter"/> that looks for <see cref="PatchableAttribute"/>
    /// before allowing a patch operation to write to a property.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.JsonPatch.Adapters.ObjectAdapter" />
    public class PatchableObjectAdapter : ObjectAdapter
    {
        public PatchableObjectAdapter(IContractResolver contractResolver, Action<JsonPatchError> logErrorAction) 
            : base(contractResolver, logErrorAction, new PatchableObjectAdapterFactory())
        {}
    }
}
