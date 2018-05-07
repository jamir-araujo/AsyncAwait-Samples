# async/await

## O que é?

É uma funcionalidade do c# para fazer continuação de I/O assíncronos dentro do escopo de um método.

``` csharp
private async Task CheckAvailability(Guid id)
{
    bool isAvailable = await _service.CheckAvailabilityAsync(id);

    //...
}
```

I/O assíncrono antes

```csharp
private void CheckAvailability(Guid id)
{
    _service.CheckAvailabilityCompleted += service_CheckAvailabilityCompleted;
    _service.CheckAvailabilityAsync(id);
}

private void service_CheckAvailabilityCompleted(object sender, CheckAvailabilityCompletedEventArgs e)
{
    _service.CheckAvailabilityCompleted -= service_CheckAvailabilityCompleted;
    //...
}
```

Um dos grandes problemas da forma clássica é perca do escopo original da chamada

```csharp
private void CheckAvailability(Guid id, string unavialableMessage)
{
    _service.CheckAvailabilityCompleted += service_CheckAvailabilityCompleted;
    _service.CheckAvailabilityAsync(id);
}

private void service_CheckAvailabilityCompleted(object sender, CheckAvailabilityCompletedEventArgs e)
{
    _service.CheckAvailabilityCompleted -= service_CheckAvailabilityCompleted;
    //...
}
```

Usando UserState

```csharp
public void CheckAvailabilityAsync(Guid id);
public void CheckAvailabilityAsync(Guid id, object userState);
```

```csharp
private void CheckAvailability(Guid id, string unavialableMessage)
{
    _service.CheckAvailabilityCompleted += service_CheckAvailabilityCompleted;
    _service.CheckAvailabilityAsync(id, unavialableMessage);
}

private void service_CheckAvailabilityCompleted(object sender, CheckAvailabilityCompletedEventArgs e)
{
    _service.CheckAvailabilityCompleted -= service_CheckAvailabilityCompleted;

    var unavialableMessage = (string)e.UserState;
    //...
}
```

e se eu quiser fazer isso:

```csharp
private void CheckAvailability(Guid id, string unavialableMessage, string availableMessage)
{
    _service.CheckAvailabilityCompleted += service_CheckAvailabilityCompleted;
    _service.CheckAvailabilityAsync(id, ...?);
}
```

```csharp
private void CheckAvailability(Guid id, string unavialableMessage, string availableMessage)
{
    _service.CheckAvailabilityCompleted += service_CheckAvailabilityCompleted;

    var userState = new MySpecialUserStateForThisCase(unavialableMessage, availableMessage)
    _service.CheckAvailabilityAsync(id, userState);
}
```

```csharp
private void CheckAvailability(Guid id, string unavialableMessage, string availableMessage)
{
    _service.CheckAvailabilityCompleted += (sender, args) =>
    {
        //os parâmetros estão disponivel aqui porque eles foram capturados pela expressão lambda
    };

    _service.CheckAvailabilityAsync(id);

    //E o evento fica assinado para sempre.
}
```

```csharp
private void CheckAvailability(Guid id, string unavialableMessage, string availableMessage)
{
    Action<object, CheckAvailabilityCompletedEventArgs> handler = null;
    handler = (sender, args) =>
    {
        _service.CheckAvailabilityCompleted -= handler;
        //parâmetros também foram capturados pela expressão lambda
    };

    _service.CheckAvailabilityCompleted += handler;
    _service.CheckAvailabilityAsync(id);
}
```

Ou evitamos usar I/O assíncrono quando possível.

```csharp
private void CheckAvailability(Guid id, string unavialableMessage, string availableMessage)
{
    //Thead reponsavel pela chamada vai ficar parada até que o método CheckAvailability retorne.
    bool isAvailable = _service.CheckAvailability(id);

    //...
}
```

Em código de server esse já o padrão para a maioria dos casos (veremos os problemas disso mais tarde).

Mas em código do client, seja Windows Forms, WPF ou UWP, isso pode ser um problemas.
Chamadas feitas nessas plataformas, normalmente são feitas pela main thread (ou UI Thread). Essa thread é responsável pela atualização dos elementos na tela, e quando essa thread executado código por muito tempo ou fica parada esperando uma operação de I/O síncrona, a tela da aplicação ficar irresponsiva.

## Voltando ao async/await

``` csharp
private async Task CheckAvailability(Guid id)
{
    bool isAvailable = await _service.CheckAvailabilityAsync(id);

    //...
}
```

### regras básicas

* Para usar `await` ao chamar um método, este método deve retornar uma `Task` ou `Task<T>`
* Para usar a palavra `await` dentro do escopo de um método, este método deve ser marcado como `async`
* Métodos marcados como `async` devem retornar uma `Task`, `Task<T>` ou `void` (`async void` é extremamente desencorajado - mais sobre isso mais tarde)
* É convenção colocar o sufixo _Async_ nos métodos que retornam `Task` ou `Task<T>`.

> Nota: Essas regras fazem o uso de `async`, `await` e `Tasks` bastante viral, assim como faz a refatoração para o uso de `async/await` bem trabalhosa.

### exemplos de uso

Não é necessário usar `await` logo na chamada do método. A `Task` pode ser colocada em umas variável local.

```csharp
Task<string> nameTask = GetNameAsync();
//...
string name = await nameTask;
```

Ou até mesmo em uma variável global

```csharp
public class Publisher
{
    private Task _pendingPublications;

    public void StartPublishing(IEnumerable<string> messages)
    {
        var publications = new List<Task>();
        foreach (var message in messages)
        {
            publications.Add(PublishMessageAsync(message));
        }

        _pendingPublications = Task.WhenAll(publications);
    }

    public Task WaitForPendingPublicationsAsync() => _pendingPublications;

    private Task PublishMessageAsync(string message) { /*...*/ }
}
```

Podemos usar no nosso c# do dia a dia.

dentro de if

``` csharp
if (await CheckConditionAsync())
{
    //...
}
```

como entrada de foreach e dentro de foreach

``` csharp
foreach (var value in await GetValuesAsync())
{
    //...
}

foreach(int id in resourceIds)
{
    var resource = await GetResourceAsync(id);

    //...
}
```

dentro de try/catch

```csharp
try
{
    await DoSomethingAsync();
}
catch (Exception e)
{
    await LogErrorAsync(e);
}
```

Acho que já deu para entender.

Porem temos uma limitação. Método marcados com `async` não podemos usar parâmetros `ref` ou `out`.

```csharp
    //Error CS1988 Async methods cannot have ref or out parameters
    private async Task PublishMessageAsync(string message, ref int index)
```

## Por que usar isso no meu código de server?

Melhor aproveitamento do threads.

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