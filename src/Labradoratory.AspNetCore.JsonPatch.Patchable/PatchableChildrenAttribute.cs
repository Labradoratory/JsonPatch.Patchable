using System;

namespace Labradoratory.AspNetCore.JsonPatch.Patchable
{
    /// <summary>
    /// Marks a property as having children that are patchable, while not itself being patchable.
    /// </summary>
    /// <remarks>
    /// This attribute can be used to allow the contents of a collection or the child properties of
    /// an object to be patched, while blocking the actual object from being replaced or deleted as
    /// part of a patch.
    /// </remarks>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property)]
    public class PatchableChildrenAttribute : Attribute
    {
    }
}
