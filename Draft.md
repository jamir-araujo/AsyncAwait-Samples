# async/await

## Como usar?

``` csharp
private async Task ShowNameAsync()
{
    string name = await GetNameAsync();

    //...
}

private Task<string> GetNameAsync() { /*...*/ }
```

### regras básicas

* Para usar `await` ao chamar um método, este método deve retornar uma `Task` ou `Task<T>`
    ``` csharp
    private Task ShowNameAsync();
    private Task<string> GetNameAsync();
    ```
* Para usar a palavra `await` dentro do escopo de um método, este método deve ser marcado como `async`
    ``` csharp
    private async Task ShowNameAsync()
    {
        string name = await GetNameAsync();

        //...
    }
    ```
* Métodos marcados como `async` devem retornar uma `Task`, `Task<T>` ou `void` (`async void` é extremamente desencorajado - mais sobre isso mais tarde)

> Com essas regras já da pra ver que esse negócio de `async`, `await` e `Task` são meio viral.

### regras de uso


## Como funciona?

``` csharp
private async Task ShowNameAsync()
{
    string name = await GetNameAsync(id);

    if (string.IsNullOrWhiteSpace(name))
    {
        name = "NOT FOUND";
    }
    else
    {
        name = name.ToUpper();
    }

    //...
}

private Task<string> GetNameAsync(int id) { /*...*/ }

```

### Código gerado

``` csharp
[AsyncStateMachine(typeof(ShowNameAsync_StateMachine))]
[DebuggerStepThrough]
private Task ShowNameAsync(int id)
{
    ShowNameAsync_StateMachine stateMachine = new ShowNameAsync_StateMachine();
    stateMachine.scope = this;
    stateMachine.id = id;
    stateMachine.buidler = AsyncTaskMethodBuilder.Create();
    stateMachine.state = -1;
    AsyncTaskMethodBuilder builder = stateMachine.buidler;
    builder.Start(ref stateMachine);
    return stateMachine.buidler.Task;
}

[CompilerGenerated]
private sealed class ShowNameAsync_StateMachine : IAsyncStateMachine
{
    public int state;
    public AsyncTaskMethodBuilder buidler;
    public Draft scope;
    public int id;
    private string _name;
    private string _result;
    private TaskAwaiter<string> _awaiter;

    public void MoveNext()
    {
        int num = state;
        try
        {
            TaskAwaiter<string> awaiter;
            if (num != 0)
            {
                awaiter = scope.GetNameAsync(id).GetAwaiter();
                if (!awaiter.IsCompleted)
                {
                    num = (state = 0);
                    _awaiter = awaiter;
                    ShowNameAsync_StateMachine thisMachine = this;
                    buidler.AwaitUnsafeOnCompleted(ref awaiter, ref thisMachine);
                    return;
                }
            }
            else
            {
                awaiter = _awaiter;
                _awaiter = default(TaskAwaiter<string>);
                num = (state = -1);
            }
            _result = awaiter.GetResult();
            _name = _result;
            _result = null;
            if (string.IsNullOrWhiteSpace(_name))
            {
                _name = "NOT FOUND";
            }
            else
            {
                _name = _name.ToUpper();
            }
            scope.SetName(_name);
        }
        catch (Exception exception)
        {
            state = -2;
            buidler.SetException(exception);
            return;
        }
        state = -2;
        buidler.SetResult();
    }

    [DebuggerHidden]
    public void SetStateMachine(IAsyncStateMachine stateMachine)
    {
    }
}
```

### Mais de um await no método

Código escrito

``` csharp
private async Task<string> GetFullNameAsync(int id)
{
    string firstName = await GetFirsNameAsync(id);
    string lastName = await GetLastNameAsync(id);

    return $"{firstName} {lastName}";
}

private Task<string> GetFirsNameAsync(int id) { //... }

private Task<string> GetLastNameAsync(int id) { //... }

```

Código gerado

``` csharp
[AsyncStateMachine(typeof(GetFullNameAsync_StateMachine))]
[DebuggerStepThrough]
private Task<string> GetFullNameAsync(int id)
{
    GetFullNameAsync_StateMachine stateMachine = new GetFullNameAsync_StateMachine();
    stateMachine.scope = this;
    stateMachine.id = id;
    stateMachine.builder = AsyncTaskMethodBuilder<string>.Create();
    stateMachine.state = -1;
    AsyncTaskMethodBuilder<string> _builder = stateMachine.builder;
    _builder.Start(ref stateMachine);
    return stateMachine.builder.Task;
}

[CompilerGenerated]
private sealed class GetFullNameAsync_StateMachine : IAsyncStateMachine
{
    public int state;
    public AsyncTaskMethodBuilder<string> builder;
    public int id;
    public Draft scope;
    private string _firstName;
    private string _lastName;
    private string _result1;
    private string _result2;
    private TaskAwaiter<string> _awaiter;

    public void MoveNext()
    {
        int num = state;
        string result;
        try
        {
            TaskAwaiter<string> awaiter2;
            GetFullNameAsync_StateMachine thisMachine;
            TaskAwaiter<string> awaiter;
            switch (num)
            {
                default:
                    awaiter2 = scope.GetFirsNameAsync(id).GetAwaiter();
                    if (!awaiter2.IsCompleted)
                    {
                        num = (state = 0);
                        _awaiter = awaiter2;
                        thisMachine = this;
                        builder.AwaitUnsafeOnCompleted(ref awaiter2, ref thisMachine);
                        return;
                    }
                    goto IL_007d;
                case 0:
                    awaiter2 = _awaiter;
                    _awaiter = default(TaskAwaiter<string>);
                    num = (state = -1);
                    goto IL_007d;
                case 1:
                    {
                        awaiter = _awaiter;
                        _awaiter = default(TaskAwaiter<string>);
                        num = (state = -1);
                        break;
                    }
                    IL_007d:
                    _result1 = awaiter2.GetResult();
                    _firstName = _result1;
                    _result1 = null;
                    awaiter = scope.GetLastNameAsync(id).GetAwaiter();
                    if (awaiter.IsCompleted)
                    {
                        break;
                    }
                    num = (state = 1);
                    _awaiter = awaiter;
                    thisMachine = this;
                    builder.AwaitUnsafeOnCompleted(ref awaiter, ref thisMachine);
                    return;
            }
            _result2 = awaiter.GetResult();
            _lastName = _result2;
            _result2 = null;
            result = string.Format("{0} {1}", _firstName, _lastName);
        }
        catch (Exception exception)
        {
            state = -2;
            builder.SetException(exception);
            return;
        }
        state = -2;
        builder.SetResult(result);
    }

    [DebuggerHidden]
    public void SetStateMachine(IAsyncStateMachine stateMachine)
    {
    }
}
```

## async/await além da Task