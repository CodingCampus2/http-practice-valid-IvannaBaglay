using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace HttpClients
{
    static class RegexUtils
    {
        private const string getExample = "Get usage: get [--sorted (True/False)] [--id (dataId)]";
        private const string postExample = "Post usage: post {\"dataId\": \"someId\", \"weight\": 42}";
        private const string putExample = "Put usage: put {id} {\"dataId\": \"someId\", \"weight\": 42}";
        private const string deleteExample = "Delete usage: delete {id}"; 

        public static async void ReadGetRequest(string command, Client client)
        {
            if (Regex.IsMatch(command, "^get *$", RegexOptions.IgnoreCase))
            {
                await client.GetAllDataRequest(false);
            }
            else if (Regex.IsMatch(command, "^get --sorted (True|False) *$", RegexOptions.IgnoreCase))
            {
                await client.GetAllDataRequest(bool.Parse(command.Substring(13).Trim())); // Read from from 13 char to end
            }
            else if (Regex.IsMatch(command, "^get --id .+ *$", RegexOptions.IgnoreCase))
            {
                await client.GetDataByIdRequest(command.Substring(9).Trim());
            }
            else
            {
                Console.WriteLine(getExample);
            }
        }
        public static async void ReadPostRequest(string command, Client client)
        {
            if (Regex.IsMatch(command, "^post {.+} *$", RegexOptions.IgnoreCase))
            {
                await client.PostRequest(command.Substring(5).TrimEnd());
            }
            else
            {
                Console.WriteLine(postExample);
            }

        }
        public static async void ReadPutRequest(string command, Client client)
        {
            if (Regex.IsMatch(command, "^put \\S+ {.+} *$", RegexOptions.IgnoreCase))
            {
                string[] commandArgs = command.Split(' ');
                await client.PutRequest(commandArgs[1], command.Substring(commandArgs[0].Length + commandArgs[1].Length + 2));
            }
            else
            {
                Console.WriteLine(putExample);
            }
        }
        public static async void ReadDeleteRequest(string command, Client client)
        {
            if (Regex.IsMatch(command, "^delete \\S+ *$", RegexOptions.IgnoreCase))
            {
                await client.DeleteDataByIdRequest(command.Substring(7).Trim());
            }
            else
            {
                Console.WriteLine(deleteExample);
            }
        }
    }
}
