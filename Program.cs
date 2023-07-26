using System;
using System.Net;

class Program
{
    static void hello(HttpListenerResponse response)
    {
        string responseString = "Hello, World! See you in the future..." + DateTime.Now;
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
        response.ContentLength64 = buffer.Length;
        System.IO.Stream output = response.OutputStream;
        output.Write(buffer, 0, buffer.Length);
        output.Close();
    }

    static void Main()
    {
        HttpListener listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:8081/");
        listener.Start();
        Console.WriteLine("Listening on http://localhost:8081/");
        Console.WriteLine("Hello, World!");
 
        while (true)
        {
            HttpListenerContext context = listener.GetContext();
            HttpListenerResponse response = context.Response;
            hello(response);
        }
    }
}


