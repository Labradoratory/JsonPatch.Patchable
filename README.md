[![Build Status](https://labradoratory.visualstudio.com/LabradoratoryGitHub/_apis/build/status/Labradoratory.JsonPatch.Patchable?branchName=master)](https://labradoratory.visualstudio.com/LabradoratoryGitHub/_build/latest?definitionId=1&branchName=master)

# JsonPatch.Patchable
Extends the Microsoft.AspNetCore.JsonPatch library by adding a PatchableAttribute and JsonPatchDocument.ApplyToIfPatchable method which restricts a JSON patch to only be applied to properties which are assigned the attribute.


Placing the `PatchableAttribute` on a property of a model object marks allows it to be updated when a `JsonPatchDocument` is applied.  If there is no `PatchableAttribute`, then a JsonPatchDocument that attempts to change the property will cause an error.

```csharp
public class MyModel
{
    // Can NOT be updated via patch.
    public int Id { get; set; }

    // Can be updated via patch
    [Patchable]
    public string Value { get; set; }
}
```

In order to take advantage of the `PatchableAttribute`, you must use the specialized `ApplyIfPatchable` extension method included in this library.

```csharp
[HttpPatch, Route("{id}")]
public async Task<IActionResult> Update(int id, [FromBody]JsonPatchDocument<MyModel> patch, CancellationToken cancellationToken)
{
    var model = await Repostiory.GetAsync<MyModel>(id);
    
    var errors = new List<JsonPatchError>();
    patch.ApplyToIfPatchable(model, error => errors.Add(error));

    ...
}
```