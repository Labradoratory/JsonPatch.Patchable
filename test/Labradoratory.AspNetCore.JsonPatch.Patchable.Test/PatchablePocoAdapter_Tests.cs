using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json.Serialization;
using Xunit;

namespace Labradoratory.AspNetCore.JsonPatch.Patchable.Test
{
    public class PatchablePocoAdapter_Tests
    {
        [Fact]
        public void TryGetJsonProperty_NotWritableWhenMissingPatchable()
        {
            var target = new TestAdapter();
            var test = new Test();

            target.Test_TryGetJsonProperty(test, new DefaultContractResolver(), "StringValue1", out var jsonProperty);
            Assert.False(jsonProperty.Writable);
        }

        [Fact]
        public void TryGetJsonProperty_WritableWhenPatchable()
        {
            var target = new TestAdapter();
            var test = new Test();

            target.Test_TryGetJsonProperty(test, new DefaultContractResolver(), "StringValue2", out var jsonProperty);
            Assert.True(jsonProperty.Writable);
        }

        [Fact]
        public void TryGetJsonProperty_NotWritableWhenNotWritable()
        {
            var target = new TestAdapter();
            var test = new Test();

            target.Test_TryGetJsonProperty(test, new DefaultContractResolver(), "StringValue3", out var jsonProperty);
            Assert.False(jsonProperty.Writable);
        }

        private class Test
        {
            public string StringValue1 { get; set; }

            [Patchable]
            public string StringValue2 { get; set; }

            public string StringValue3 { get; }
        }

        private class TestAdapter : PatchablePocoAdapter
        {
            public bool Test_TryGetJsonProperty(object target, IContractResolver contractResolver, string segment, out JsonProperty jsonProperty)
            {
                return TryGetJsonProperty(target, contractResolver, segment, out jsonProperty);
            }
        }
    }
}
