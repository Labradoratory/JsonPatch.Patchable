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
            var target = new Test_PatchablePocoAdapter();
            var test = new Test();

            target.Test_TryGetJsonProperty(test, new DefaultContractResolver(), "StringValue1", out var jsonProperty);
            Assert.False(jsonProperty.Writable);
        }

        [Fact]
        public void TryGetJsonProperty_WritableWhenPatchable()
        {
            var target = new Test_PatchablePocoAdapter();
            var test = new Test();

            target.Test_TryGetJsonProperty(test, new DefaultContractResolver(), "StringValue2", out var jsonProperty);
            Assert.True(jsonProperty.Writable);
        }

        [Fact]
        public void TryGetJsonProperty_NotWritableWhenNotWritable()
        {
            var target = new Test_PatchablePocoAdapter();
            var test = new Test();

            target.Test_TryGetJsonProperty(test, new DefaultContractResolver(), "StringValue3", out var jsonProperty);
            Assert.False(jsonProperty.Writable);
        }

        [Fact]
        public void TryGetJsonProperty_TrueWhenPoco()
        {
            var target = new Test_PatchablePocoAdapter();
            var test = new Test();

            var result = target.Test_TryGetJsonProperty(test, new DefaultContractResolver(), "StringValue3", out var jsonProperty);
            Assert.True(result);
        }

        [Fact]
        public void TryGetJsonProperty_ReturnsFalseWhenNotPoco()
        {
            var target = new Test_PatchablePocoAdapter();
            var test = new List<string>();

            var result = target.Test_TryGetJsonProperty(test, new DefaultContractResolver(), "StringValue3", out var jsonProperty);
            Assert.False(result);
        }

        [Fact]
        public void TryGetJsonProperty_IListMarkedPatchableChildrenNotPatchable()
        {
            var target = new Test_PatchablePocoAdapter();
            var test = new Test();

            target.Test_TryGetJsonProperty(test, new DefaultContractResolver(), "Dictionary1", out var jsonProperty);
            Assert.False(jsonProperty.Writable);
        }

        [Fact]
        public void TryTraverse_IListMarkedPatchableChildrenCanTraverse()
        {
            var target = new PatchablePocoAdapter();
            var test = new Test();

            var result = target.TryTraverse(test, "Dictionary1", new DefaultContractResolver(), out var value, out var errorMessage);
            Assert.True(result);
        }

        [Fact]
        public void TryTraverse_IListMarkedPatchableCanTraverse()
        {
            var target = new PatchablePocoAdapter();
            var test = new Test();

            var result = target.TryTraverse(test, "Dictionary2", new DefaultContractResolver(), out var value, out var errorMessage);
            Assert.True(result);
        }

        [Fact]
        public void TryTraverse_IListNotMarkedPatchableOrPatchableChildrenCanNotTraverse()
        {
            var target = new PatchablePocoAdapter();
            var test = new Test();
            var result = target.TryTraverse(test, "Dictionary3", new DefaultContractResolver(), out var value, out var errorMessage);
            Assert.False(result);
        }

        private class Test
        {
            public string StringValue1 { get; set; }

            [Patchable]
            public string StringValue2 { get; set; }

            public string StringValue3 { get; }

            [PatchableChildren]
            public Dictionary<string, string> Dictionary1 { get; set; } = new Dictionary<string, string> { { "1", "value1" } };

            [Patchable]
            public Dictionary<string, string> Dictionary2 { get; set; } = new Dictionary<string, string> { { "2", "value2" } };

            public Dictionary<string, string> Dictionary3 { get; set; } = new Dictionary<string, string> { { "3", "value3" } };
        }

        private class Test_PatchablePocoAdapter : PatchablePocoAdapter
        {
            public bool Test_TryGetJsonProperty(object target, IContractResolver contractResolver, string segment, out JsonProperty jsonProperty)
            {
                return TryGetJsonProperty(target, contractResolver, segment, out jsonProperty);
            }
        }
    }
}
