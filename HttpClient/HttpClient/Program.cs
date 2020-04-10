using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace HttpClients
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Client client = new Client();
            client.Init();

            string command;

            while (true)
            {
                command = Console.ReadLine();
                if (Regex.IsMatch(command, "^get(|$)", RegexOptions.IgnoreCase))
                {
                    RegexUtils.ReadGetRequest(command, client);
                }
                else if (Regex.IsMatch(command, "^post", RegexOptions.IgnoreCase))
                {
                    RegexUtils.ReadPostRequest(command, client);
                }
                else if (Regex.IsMatch(command, "^put", RegexOptions.IgnoreCase))
                {
                    RegexUtils.ReadPutRequest(command, client);
                }
                else if (Regex.IsMatch(command, "^delete", RegexOptions.IgnoreCase))
                {
                    RegexUtils.ReadDeleteRequest(command, client);
                }
            }
        }
    }
}
