using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Newtonsoft.Json.Serialization;
using Xunit;

namespace Labradoratory.AspNetCore.JsonPatch.Patchable.Test
{
    public class JsonPatchDocumentExtensions_Tests
    {
        [Fact]
        public void ApplyToIfPatchable_Property_AddWhenPatchableAttribute()
        {
            var expectedValue = "Value Patched";
            var target = new TestTarget()
            {
                StringValueP = "Patchable Value"
            };

            var patch = new JsonPatchDocument(
                new List<Operation> 
                {
                    new Operation("add", "StringValueP", null, expectedValue)
                },
                new DefaultContractResolver());

            patch.ApplyToIfPatchable(target);
            Assert.Equal(expectedValue, target.StringValueP);
        }

        [Fact]
        public void ApplyToIfPatchable_Property_NoAddWhenNoPatchableAttribute()
        {
            var expectedError = "The property at path 'StringValueNP' could not be updated.";
            var expectedValue = "Not Patchable Value";
            var target = new TestTarget()
            {
                StringValueNP = expectedValue
            };

            var patch = new JsonPatchDocument(
                new List<Operation>
                {
                    new Operation("add", "StringValueNP", null, "Value Patched")
                },
                new DefaultContractResolver());

            var errors = new List<JsonPatchError>();
            patch.ApplyToIfPatchable(target, error => errors.Add(error));
            Assert.Equal(expectedValue, target.StringValueNP);
            Assert.Single(errors);
            var error = errors[0];
            Assert.Equal(target, error.AffectedObject);
            Assert.Equal(patch.Operations[0], error.Operation);
            Assert.Equal(expectedError, error.ErrorMessage);
        }

        [Fact]
        public void ApplyToIfPatchable_Collection_AddWhenPatchableAttribute()
        {
            var expectedValue = "Value Patched";
            var target = new TestTarget();

            var patch = new JsonPatchDocument(
                new List<Operation>
                {
                    new Operation("add", "StringCollectionP/-", null, expectedValue)
                },
                new DefaultContractResolver());

            patch.ApplyToIfPatchable(target);
            Assert.Single(target.StringCollectionP);
            Assert.Equal(expectedValue, target.StringCollectionP[0]);
        }

        [Fact]
        public void ApplyToIfPatchable_Collection_NoAddWhenNoPatchableAttribute()
        {
            var expectedError = "The property at path 'StringCollectionNP' could not be updated.";
            var target = new TestTarget();

            var patch = new JsonPatchDocument(
                new List<Operation>
                {
                    new Operation("add", "StringCollectionNP/-", null, "Value Patched")
                },
                new DefaultContractResolver());

            var errors = new List<JsonPatchError>();
            patch.ApplyToIfPatchable(target, error => errors.Add(error));
            Assert.Empty(target.StringCollectionNP);
            Assert.Single(errors);
            var error = errors[0];
            Assert.Equal(target, error.AffectedObject);
            Assert.Equal(patch.Operations[0], error.Operation);
            Assert.Equal(expectedError, error.ErrorMessage);
        }

        [Fact]
        public void ApplyToIfPatchable_Property_RemoveWhenPatchableAttribute()
        {;
            var target = new TestTarget()
            {
                StringValueP = "Patchable Value"
            };

            var patch = new JsonPatchDocument(
                new List<Operation>
                {
                    new Operation("remove", "StringValueP", null)
                },
                new DefaultContractResolver());

            patch.ApplyToIfPatchable(target);
            Assert.Null(target.StringValueP);
        }

        [Fact]
        public void ApplyToIfPatchable_Property_NoRemoveWhenNoPatchableAttribute()
        {
            var expectedError = "The property at path 'StringValueNP' could not be updated.";
            var target = new TestTarget()
            {
                StringValueNP = "Not Patched Value"
            };

            var patch = new JsonPatchDocument(
                new List<Operation>
                {
                    new Operation("remove", "StringValueNP", null)
                },
                new DefaultContractResolver());

            var errors = new List<JsonPatchError>();
            patch.ApplyToIfPatchable(target, error => errors.Add(error));
            Assert.NotNull(target.StringValueNP);
            Assert.Single(errors);
            var error = errors[0];
            Assert.Equal(target, error.AffectedObject);
            Assert.Equal(patch.Operations[0], error.Operation);
            Assert.Equal(expectedError, error.ErrorMessage);
        }

        private class TestTarget
        {
            [Patchable]
            public string StringValueP { get; set; }
            public string StringValueNP { get; set; }

            [Patchable]
            public Collection<string> StringCollectionP { get; set; } = new Collection<string>();
            public Collection<string> StringCollectionNP { get; set; } = new Collection<string>();
        }
    }
}
