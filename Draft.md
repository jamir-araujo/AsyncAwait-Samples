# async/await

## Como usar?

``` c#

 private async Task ShowNameAsync()
{
    string name = await GetNameAsync();

    //...
}

private Task<string> GetNameAsync() { //... }

```
