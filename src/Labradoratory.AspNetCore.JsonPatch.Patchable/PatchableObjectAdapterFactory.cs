using Microsoft.AspNetCore.JsonPatch.Adapters;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Newtonsoft.Json.Serialization;

namespace Labradoratory.AspNetCore.JsonPatch.Patchable
{
    /// <summary>
    /// An implementation of <see cref="AdapterFactory"/> replaces the standard <see cref="PocoAdapter"/>
    /// with a <see cref="PatchablePocoAdapter"/>.
    /// </summary>
    /// <seealso cref="AdapterFactory" />
    public class PatchableObjectAdapterFactory : AdapterFactory
    {
        /// <inheritdoc />
        public override IAdapter Create(object target, IContractResolver contractResolver)
        {
            var adapter = base.Create(target, contractResolver);
            if (adapter is PocoAdapter)
                adapter = new PatchablePocoAdapter();

            return adapter;
        }
    }
}
