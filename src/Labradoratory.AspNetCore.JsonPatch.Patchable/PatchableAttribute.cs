using System;

namespace Labradoratory.AspNetCore.JsonPatch.Patchable
{
    /// <summary>
    /// Marks a property as being patchable.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property)]
    public class PatchableAttribute : Attribute
    {}
}
