using System;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json.Serialization;

namespace Labradoratory.AspNetCore.JsonPatch.Patchable
{
    public static class JsonPatchDocumentExtensions
    {
        /// <summary>
        /// Applies the <see cref="JsonPatchDocument"/> to the provided object, only allowing writes
        /// to properties that are marked with the <see cref="PatchableAttribute"/>.
        /// </summary>
        /// <param name="document">The patch to apply.</param>
        /// <param name="objectToApplyTo">The object to apply the patch to.</param>
        /// <param name="logErrorAction">An action to handle logging any errors.</param>
        public static void ApplyToIfPatchable(
            this JsonPatchDocument document,
            object objectToApplyTo,
            Action<JsonPatchError> logErrorAction = null)
        {
            // If there is no provided error logging, just use an empty method.
            if (logErrorAction == null)
                logErrorAction = _ => { };

            var adapter = new PatchableObjectAdapter(new DefaultContractResolver(), logErrorAction);
            document.ApplyTo(objectToApplyTo, adapter, logErrorAction);
        }
    }
}
