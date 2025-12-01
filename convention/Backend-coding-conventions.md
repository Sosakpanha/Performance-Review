# Backend Coding Guidelines

## C# Guidelines

### Naming Conventions

#### Classes and Members
- **Public members, classes, and methods**: Use PascalCase (upper camel case)
  ```csharp
  public class ProviderResponseBase
  {
      public ErrorCodeEnum ErrorCode { get; set; }
      public string ErrorMessage { get; set; }
  }
  ```

- **Interfaces**: Start with capital 'I' followed by PascalCase
  ```csharp
  public interface IProviderHashService
  {
      // Interface members
  }
  ```

- **Private fields**: Start with underscore (_) followed by camelCase
  - Make private fields read-only if their values won't change
  ```csharp
  private readonly IGameInfoCache _gameInfoCache;
  private readonly ITransactionService _transactionService;
  ```

- **Local variables**: Use camelCase (dromedary case)
  ```csharp
  public void DoSomething()
  {
      var localVariableA = 0;
      var localVariableB = new Something();
  }
  ```

#### Specific Naming Patterns

- **Collections (Array/List)**: Add an "s" to indicate plurality
  ```csharp
  public class Company
  {
      public Person Manager { get; set; }
      public List<Person> Employees { get; set; }
  }
  ```

- **Boolean properties/methods**: Start with "Is"
  ```csharp
  public bool IsFailed()
  {
      return ErrorCode != ErrorCodeEnum.NoError;
  }
  
  public bool IsErrorMessageEmpty()
  {
      return string.IsNullOrEmpty(ErrorMessage);
  }
  ```

- **Base classes**: End with "Base"
  ```csharp
  public class ProviderResponseBase
  {
      // Base members
  }
  
  public class ProviderTransactionResponse : ProviderResponseBase
  {
      // Additional members
  }
  ```

- **Meaningful names**: All names should clearly indicate their purpose
  ```csharp
  // Good naming:
  var providerGetBalanceResponse
  var customerCurrency
  
  // Bad naming:
  var abc // what does it even mean?
  var currency // whose currency?
  ```

- **Use JSON property attributes** for provider-side property names
  ```csharp
  [JsonProperty("playerId")]
  public string Username { get; set; }
  
  [JsonProperty("gameId")]
  public string GameCode { get; set; }
  ```

### Method Guidelines

#### Naming
- Method names should clearly explain their purpose
- **Controller actions** should start with specific action types:
  - `Create` for inserting new data
  - `Update` for modifying data
  - `Get` for retrieving data

  ```csharp
  private string GetProviderLanguage(LanguageEnum lang)
  {
      // Logic to get provider language
  }
  
  public ApiResponse<BaseResponse> UpdatePaymentPassword(UpdatePaymentPasswordRequest req)
  {
      // Logic
  }
  ```

#### Method Design
- **Single Responsibility**: One method should only do one thing
  ```csharp
  // Bad practice:
  public void ConvertModelAndSendRequest(UnconvertedModel model)
  
  // Good practice:
  public ConvertedModel GetConvertedModel(UnconvertedModel model)
  public void SendRequest(ConvertedModel model)
  ```

- **Static methods**: Make private methods static if they don't use class fields/properties
  ```csharp
  private static void DoSomethingWithParamsOnly(/* params */)
  ```

- **Request/Response methods**: Should end with Request/Response
  ```csharp
  public TransactionRequest GetCustomerGetBalanceRequest(ProviderGetBalanceRequest request)
  public ProviderGetBalanceResponse GetProviderGetBalanceResponse(TransactionResponse response)
  ```

### Class Structure
- Organize class members in the following order:
  ```csharp
  public class SomeClass
  {
      // fields and properties
      // constructors
      // public methods
      // private methods
  }
  ```

### Best Practices

- **String comparison**: Ignore case when not necessary
  ```csharp
  someString.Equals(someOtherString, StringComparison.OrdinalIgnoreCase)
  ```

- **Use enums** for comparing integers like error codes
  ```csharp
  // Bad: 
  ErrorCode != 0;
  
  // Good: 
  ErrorCode != ErrorCodeEnum.NoError;
  ```

- **Business logic**: Should be in services, not controllers
- **Idempotence**: Handle in filters, not services
- **Organization**: Classes with different purposes should be in their own folders
- **DateTime format**: Use "yyyy-MM-ddTHH:mm:ss.fffffffzzz" for customer-facing responses

### Concurrency and Asynchronous Programming

#### Locking (Preventing Race Conditions)
- Use the `lock` statement to synchronize access to shared resources
  ```csharp
  public T Get(string key)
  {
      if (Contains(key))
      {
          return (T)_cache[$"{CacheKey}_{key}"];
      }
      
      lock (_lockKey)
      {
          if (_cache.Contains($"{CacheKey}_{key}"))
          {
              return (T)_cache[$"{CacheKey}_{key}"];
          }
          
          var result = ReloadFromDb(key);
          _cache.Set($"{CacheKey}_{key}", result, GetItemPolicy());
          return result;
      }
  }
  ```
- **Benefit**: Prevents multiple threads from executing the same critical section simultaneously, avoiding duplicate DB calls

#### Async/Await Pattern
- Use `async` and `await` to make asynchronous code more readable and maintainable
- Apply consistently to all `Task`-returning methods
- Makes execution ordered without blocking threads

```csharp
public async Task UploadFile(HttpRequest request)
{
    if (!request.HasFormContentType || !request.ContentType.ToLower().Contains("multipart/form-data"))
    {
        throw new WLException(HttpStatusCode.UnsupportedMediaType.ToString());
    }
    
    var form = await request.ReadFormAsync();
    
    foreach (var file in form.Files.Where(f => f.Length > 0))
    {
        var buffer = new byte[file.Length];
        using (var fileStream = file.OpenReadStream())
        {
            await fileStream.ReadAsync(buffer, 0, (int)file.Length);
        }
        
        var fileName = file.FileName.Trim('"');
        await UploadFileToLocalServer(fileName, buffer, request.HttpContext);
    }
}
```
- **Benefit**: Makes asynchronous code appear synchronous (readable) while maintaining non-blocking behavior

## SQL Guidelines

### Naming Conventions

- **Tables and Columns**: Use PascalCase wrapped in square brackets
  ```sql
  CREATE TABLE [dbo].[ProviderInfo]
  (
      [ProviderId] INT NOT NULL PRIMARY KEY,
      [ProviderName] NVARCHAR(50) NOT NULL
  )
  ```

- **Local variables**: Use camelCase
  ```sql
  DECLARE @resultTable TABLE([WebId] INT, [UserId] INT);
  DECLARE @providerId INT;
  ```

- **Variable size**: Should match table schema size
  ```sql
  -- If column is NVARCHAR(500)
  DECLARE @remark NVARCHAR(500) -- Good
  DECLARE @remark NVARCHAR(50)  -- Bad
  ```

### Stored Procedures

#### Naming
- Format: `[ProjectName_FeatureName_Version]`
- Version format: `[MajorChange].[LogicChange].[BugFix]`
- Example: `[dbo].[Coloris_GetAllCashAgentName_1.0.0]`

#### Version Updates
- **First number**: Changed when modifying I/O (inputs/outputs)
- **Second number**: Changed when modifying logic without changing I/O
- **Third number**: Changed when fixing bugs
- Reset subsequent numbers to zero when incrementing a higher-level number:
  - `1.1.2` → `1.2.0`
  - `1.1.2` → `2.0.0`

#### SP File Naming
- File name should not contain version (e.g., `HRMS_Login.sql`)
- SP ID and file name must match

#### Parameters
- SP parameters must start with lowercase:
  ```sql
  CREATE PROCEDURE [dbo].[Coloris_Example_1.0.0]
      @customerId INT, 
      @webId INT
  AS
  ```

#### Helper SPs
- SPs used within other SPs should start with "Sp":
  ```sql
  [Sp_Promotion_AutoCompletePromotionWallet_2.0.0]
  ```

### SQL Best Practices

- **Decimal type**: Use `DECIMAL(19,6)` for amounts
- **SELECT statements**:
  - Explicitly list columns instead of using `*`
  - Always use `WITH(NOLOCK)`
  - Specify join types explicitly
  ```sql
  -- Good
  SELECT [ProviderId], [ProviderName]
  FROM [dbo].[ProviderInfo] WITH(NOLOCK)
  INNER JOIN [dbo].[AnotherTable] WITH(NOLOCK)
  ON [ProviderInfo].[Id] = [AnotherTable].[ProviderId]
  ```

- **COUNT operations**: Use `COUNT(1)` instead of `COUNT(*)`
  ```sql
  -- Bad
  SELECT COUNT(*) FROM [dbo].[Users] WITH(NOLOCK)
  
  -- Good
  SELECT COUNT(1) FROM [dbo].[Users] WITH(NOLOCK)
  ```

- **SET NOCOUNT ON**: Include at the beginning of each SP
  ```sql
  CREATE PROCEDURE [dbo].[ExampleProcedure_1.0.0]
  AS
  BEGIN
      SET NOCOUNT ON;
      -- Procedure logic
  END
  ```

- **Readability**: Add a new line for SQL keywords (except AND/OR)
  ```sql
  -- Good
  SELECT [ProviderId], [CustomerCurrency]
  FROM [dbo].[ProviderCurrencyInfo] WITH(NOLOCK)
  WHERE [ProviderId] = @providerId AND [IsEnabled] = 1
  ```

- **Temporary tables**: Always drop them when done
- **Table references**: Always use square brackets around table names
  ```sql
  -- Good
  SELECT [CustomerId] FROM [Customer] WITH(NOLOCK)
  -- Bad
  SELECT [CustomerId] FROM Customer WITH(NOLOCK)
  ```
- **Data organization**: Separate large data inserts by provider using the `GO` keyword

### Transaction Locks (Preventing Race Conditions)

- Use `sp_getapplock` and `sp_releaseapplock` to prevent concurrent transactions
  ```sql
  SET NOCOUNT ON;
  BEGIN TRANSACTION
  
  DECLARE @targetId BIGINT = 0
  DECLARE @SpName NVARCHAR(100) = OBJECT_NAME(@@PROCID)
  DECLARE @resName NVARCHAR(100);
  DECLARE @res INT;
  
  SET @resName = 'LockSettle' + CAST(@webId AS NVARCHAR(50)) + '_' + @username;
  
  EXEC @res = sp_getapplock 
      @Resource = @resName, 
      @LockMode = 'Exclusive', 
      @LockOwner = 'Transaction', 
      @LockTimeout = 5000
  
  -- Unable to Acquire Lock
  -- 0 and 1 are valid return values
  IF @res NOT IN (0, 1)
  BEGIN
      SELECT -1 ErrorCode, 
             'Transaction Lock Error, lock stats :' + CAST(@res AS NVARCHAR(50)) AS ErrorMessage
      ROLLBACK
      RETURN
  END
  
  -- Do SP Logic
  
  EXEC @res = sp_releaseapplock 
      @Resource = @resName, 
      @LockOwner = 'Transaction'
  
  COMMIT
  SELECT 0 AS ErrorCode
  ```

- **Benefits**:
  - Reduces deadlocks
  - Prevents duplicate data insertion (when merging/inserting)
  
- **Disadvantages**:
  - Multiple transactions must execute sequentially, potentially causing slowdowns
  - If the lock timeout is reached (e.g., 5 seconds), waiting processes will terminate

### General SQL Rules

- Avoid using cursors or recursion (discuss with team lead if necessary)
- Every SP hotfix must include a commit
- Log files should only contain Error, Fatal, and Information levels
- Code must build successfully before committing to git
- Consider constraints when designing tables

**After modifying a stored procedure version, you must also update the corresponding Value in InsertData.sql to point to the new SP version. The ModifiedBy field should be set to your username. This applies to all SPs, including (but not limited to)
