using System.Threading.Tasks;

namespace ServerForAndroidProject
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Server server = new Server(1123);
            await server.startServerAsync();
        }
    }
}
