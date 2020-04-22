using System.Collections.Generic;
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
        public void TryGetJsonProperty_IListMarkedPatchableCollectionNotPatchable()
        {
            var target = new Test_PatchablePocoAdapter();
            var test = new Test();

            target.Test_TryGetJsonProperty(test, new DefaultContractResolver(), "List1", out var jsonProperty);
            Assert.False(jsonProperty.Writable);
        }

        [Fact]
        public void TryTraverse_IListMarkedPatchableCollectionCanTraverse()
        {
            var target = new PatchablePocoAdapter();
            var test = new Test();

            var result = target.TryTraverse(test, "List1", new DefaultContractResolver(), out var value, out var errorMessage);
            Assert.True(result);
        }

        [Fact]
        public void TryTraverse_IListMarkedPatchableCanTraverse()
        {
            var target = new PatchablePocoAdapter();
            var test = new Test();

            var result = target.TryTraverse(test, "List2", new DefaultContractResolver(), out var value, out var errorMessage);
            Assert.True(result);
        }

        [Fact]
        public void TryTraverse_IListNotMarkedPatchableOrPatchableCollectionCanNotTraverse()
        {
            var target = new PatchablePocoAdapter();
            var test = new Test();
            var result = target.TryTraverse(test, "List3", new DefaultContractResolver(), out var value, out var errorMessage);
            Assert.False(result);
        }

        [Fact]
        public void TryGetJsonProperty_IDictionaryMarkedPatchableCollectionNotPatchable()
        {
            var target = new Test_PatchablePocoAdapter();
            var test = new Test();

            target.Test_TryGetJsonProperty(test, new DefaultContractResolver(), "Dictionary1", out var jsonProperty);
            Assert.False(jsonProperty.Writable);
        }

        [Fact]
        public void TryTraverse_IDictionaryMarkedPatchableCollectionCanTraverse()
        {
            var target = new PatchablePocoAdapter();
            var test = new Test();

            var result = target.TryTraverse(test, "Dictionary1", new DefaultContractResolver(), out var value, out var errorMessage);
            Assert.True(result);
        }

        [Fact]
        public void TryTraverse_IDictionaryMarkedPatchableCanTraverse()
        {
            var target = new PatchablePocoAdapter();
            var test = new Test();

            var result = target.TryTraverse(test, "Dictionary2", new DefaultContractResolver(), out var value, out var errorMessage);
            Assert.True(result);
        }

        [Fact]
        public void TryTraverse_IDictionaryNotMarkedPatchableOrPatchableCollectionCanNotTraverse()
        {
            var target = new PatchablePocoAdapter();
            var test = new Test();
            var result = target.TryTraverse(test, "Dictionary3", new DefaultContractResolver(), out var value, out var errorMessage);
            Assert.False(result);
        }

        [Fact]
        public void TryGetJsonProperty_ArrayMarkedPatchableCollectionNotPatchable()
        {
            var target = new Test_PatchablePocoAdapter();
            var test = new Test();

            target.Test_TryGetJsonProperty(test, new DefaultContractResolver(), "Array1", out var jsonProperty);
            Assert.False(jsonProperty.Writable);
        }

        [Fact]
        public void TryTraverse_ArrayMarkedPatchableCollectionCanTraverse()
        {
            var target = new PatchablePocoAdapter();
            var test = new Test();

            var result = target.TryTraverse(test, "Array1", new DefaultContractResolver(), out var value, out var errorMessage);
            Assert.True(result);
        }

        [Fact]
        public void TryTraverse_ArrayMarkedPatchableCanTraverse()
        {
            var target = new PatchablePocoAdapter();
            var test = new Test();

            var result = target.TryTraverse(test, "Array2", new DefaultContractResolver(), out var value, out var errorMessage);
            Assert.True(result);
        }

        [Fact]
        public void TryTraverse_ArrayNotMarkedPatchableOrPatchableCollectionCanNotTraverse()
        {
            var target = new PatchablePocoAdapter();
            var test = new Test();
            var result = target.TryTraverse(test, "Array3", new DefaultContractResolver(), out var value, out var errorMessage);
            Assert.False(result);
        }



        private class Test
        {
            public string StringValue1 { get; set; }

            [Patchable]
            public string StringValue2 { get; set; }

            public string StringValue3 { get; }

            [PatchableCollection]
            public Dictionary<string, string> Dictionary1 { get; set; } = new Dictionary<string, string>();

            [Patchable]
            public Dictionary<string, string> Dictionary2 { get; set; } = new Dictionary<string, string>();

            public Dictionary<string, string> Dictionary3 { get; set; } = new Dictionary<string, string>();

            [PatchableCollection]
            public List<string> List1 { get; set; } = new List<string>();

            [Patchable]
            public List<string> List2 { get; set; } = new List<string>();

            public List<string> List3 { get; set; } = new List<string>();

            [PatchableCollection]
            public string[] Array1 { get; set; } = new string[0];

            [Patchable]
            public string[] Array2 { get; set; } = new string[0];

            public string[] Array3 { get; set; } = new string[0];
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
