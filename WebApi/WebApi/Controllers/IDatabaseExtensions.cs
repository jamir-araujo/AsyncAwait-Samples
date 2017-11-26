using System.Threading.Tasks;

namespace StackExchange.Redis
{
    public static class IDatabaseExtensions
    {
        const string SCRIPT = @"
            local aTempKey = 'a-temp-key'
            local cycles
            redis.call('SET',aTempKey,'1')
            redis.call('PEXPIRE',aTempKey, ARGV[1])
            while true do
               local apttl = redis.call('PTTL', aTempKey)

               if apttl == 0 then
                    break;
                end
            end
            return cycles";

        public static void ExecuteOperation(this IDatabase database, long miliseconds)
        {
            database.ScriptEvaluate(SCRIPT, values: new RedisValue[] { miliseconds });
        }

        public static Task ExecuteOperationAsync(this IDatabase database, long miliseconds)
        {
            return database.ScriptEvaluateAsync(SCRIPT, values: new RedisValue[] { miliseconds });
        }
    }
}