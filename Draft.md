# async/await

## Como usar?

``` c#

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

private Task<string> GetNameAsync(int id) { //... }

```

``` c#
[CompilerGenerated]
private sealed class ShowNameAsync_StateMachine : IAsyncStateMachine
{
    public int _state;
    public AsyncTaskMethodBuilder _builder;
    public Draft _this;
    public int id;
    private string name;
    private string result;
    private TaskAwaiter<string> _awaiter;

    private void MoveNext()
    {
        int num = _state;
        try
        {
            TaskAwaiter<string> awaiter;
            if (num != 0)
            {
                awaiter = _this.GetNameAsync(id).GetAwaiter();
                if (!awaiter.IsCompleted)
                {
                    num = (_state = 0);
                    _awaiter = awaiter;
                    ShowNameAsync_StateMachine @this = this;
                    _builder.AwaitUnsafeOnCompleted(ref awaiter, ref @this);
                    return;
                }
            }
            else
            {
                awaiter = _awaiter;
                _awaiter = default(TaskAwaiter<string>);
                num = (_state = -1);
            }
            result = awaiter.GetResult();
            name = result;
            result = null;
            if (string.IsNullOrWhiteSpace(name))
            {
                name = "NOT FOUND";
            }
            else
            {
                name = name.ToUpper();
            }
            _this.SetName(name);
        }
        catch (Exception exception)
        {
            _state = -2;
            _builder.SetException(exception);
            return;
        }
        _state = -2;
        _builder.SetResult();
    }

    void IAsyncStateMachine.MoveNext()
    {
        //ILSpy generated this explicit interface implementation from .override directive in MoveNext
        this.MoveNext();
    }

    [DebuggerHidden]
    private void SetStateMachine(IAsyncStateMachine stateMachine)
    {
    }

    void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
    {
        //ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
        this.SetStateMachine(stateMachine);
    }
}

[AsyncStateMachine(typeof(ShowNameAsync_StateMachine))]
[DebuggerStepThrough]
private Task ShowNameAsync(int id)
{
    ShowNameAsync_StateMachine stateMachine = new ShowNameAsync_StateMachine();
    stateMachine._this = this;
    stateMachine.id = id;
    stateMachine._builder = AsyncTaskMethodBuilder.Create();
    stateMachine._state = -1;
    AsyncTaskMethodBuilder builder = stateMachine._builder;
    builder.Start(ref stateMachine);
    return stateMachine._builder.Task;
}
```
