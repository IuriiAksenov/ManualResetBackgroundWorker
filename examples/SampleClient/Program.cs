using System;
using System.Net.Http;

const string serverUrl = "http://localhost:5000/home";

var httpClient = new HttpClient();
ConsoleKey key;

do
{
    Console.WriteLine("Press 'R' to restart integers on the server");
    Console.WriteLine("Press 'Enter' to exit");
    var readKey = Console.ReadKey();
    Console.WriteLine();
    key = readKey.Key;
    if (key == ConsoleKey.R)
    {
        try
        {
            await httpClient.GetAsync(serverUrl);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
           
        }
    }

} while (key != ConsoleKey.Enter);