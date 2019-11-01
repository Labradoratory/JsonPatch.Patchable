using Newtonsoft.Json.Serialization;
using Xunit;

namespace Labradoratory.AspNetCore.JsonPatch.Patchable.Test
{
    public class PatchableObjectAdapter_Tests
    {
        [Fact]
        public void Constructor_AdapterFactoryIsPatchable()
        {
            var target = new PatchableObjectAdapter(new DefaultContractResolver(), error => { });
            Assert.IsAssignableFrom<PatchableObjectAdapterFactory>(target.AdapterFactory);
        }
    }
}
