using System;

namespace Labradoratory.AspNetCore.JsonPatch.Patchable
{
    /// <summary>
    /// Marks a property as being patchable.
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property)]
    public class PatchableAttribute : Attribute
    {
    }
}
