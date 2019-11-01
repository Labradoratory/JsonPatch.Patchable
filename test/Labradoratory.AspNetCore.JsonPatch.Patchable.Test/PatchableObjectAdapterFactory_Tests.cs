using Newtonsoft.Json.Serialization;
using Xunit;

namespace Labradoratory.AspNetCore.JsonPatch.Patchable.Test
{
    public class PatchableObjectAdapterFactory_Tests
    {
        [Fact]
        public void Create_ReturnsPatchablePocoAdapter()
        {
            var target = new PatchableObjectAdapterFactory();
            var result = target.Create(new Test(), new DefaultContractResolver());

            Assert.IsAssignableFrom<PatchablePocoAdapter>(result);
        }

        private class Test
        {
            public string StringValue { get; set; }
        }
    }
}
