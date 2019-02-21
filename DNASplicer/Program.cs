
using System;
using System.IO;
using System.Net.Sockets;

class Solution
{
    static void Main(string[] args)
    {
        // 52.49.91.111:3241, 
        Connect("52.49.91.111", 3241, "TEST");
        Console.ReadLine();
    }

    static void Connect(String server, int port, String message)
    {
        try
        {
            // Create a TcpClient.
            // Note, for this client to work you need to have a TcpServer 
            // connected to the same address as specified by the server, port
            // combination.
            //Int32 port = 13000;
            TcpClient client = new TcpClient(server, port);

            // Translate the passed message into ASCII and store it as a Byte array.
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

            // Get a client stream for reading and writing.
            //  Stream stream = client.GetStream();

            NetworkStream stream = client.GetStream();
            // Send the message to the connected TcpServer. 
            //stream.Write(data, 0, data.Length);

            //Console.WriteLine("Sent: {0}", message);

            // Receive the TcpServer.response.

            // Buffer to store the response bytes.
            data = new Byte[256];

            // String to store the response ASCII representation.
            String responseData = String.Empty;

            // Read the first batch of the TcpServer response bytes.
            Int32 bytes = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            Console.WriteLine("Received: {0}", responseData);

            bytes = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            Console.WriteLine("Received: {0}", responseData);

            stream.Write(data, 0, data.Length);
            Console.WriteLine("Sent: {0}", message);

            bytes = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            Console.WriteLine("Received: {0}", responseData);


            // Close everything.
            stream.Close();
            client.Close();
        }
        catch (ArgumentNullException e)
        {
            Console.WriteLine("ArgumentNullException: {0}", e);
        }
        catch (SocketException e)
        {
            Console.WriteLine("SocketException: {0}", e);
        }

        Console.WriteLine("\n Press Enter to continue...");
        Console.Read();
    }
}

public class StreamConnectionManager
{
    private string server;
    private int port;
    private StreamReader reader;
    private StreamWriter writer;
    private TcpClient client;
    private NetworkStream stream;

    public StreamConnectionManager(string server , int port)
    {
        this.server = server;
        this.port = port;
    }

    public bool IsConnected { get; private set; }

    public bool Connect ()
    {
        try
        {
            client = new TcpClient(server, port);
            stream = client.GetStream();
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);
        }
        catch (Exception exception)
        {
            IsConnected = false;
            return false;
        }
        IsConnected = true;
        return true;
    }

    public char Read()
    {
        try
        {
            if (!IsConnected)
            {
                Connect();
            }

            return (char)reader.Read();
        }
        catch (Exception ee)
        {
            IsConnected = false;
            return 'y';
        }
    }

    public string ReadLine()
    {
        try
        {
            if(!IsConnected)
            {
                Connect();
            }
            if (!reader.EndOfStream)
                return reader.ReadLine();
            return null;
        }
        catch(Exception ee)
        {
            IsConnected = false;
            return null;
        }
    }

    public void WriteLine(string line)
    {
        try
        {
            if (!IsConnected)
            {
                Connect();
            }

            writer.WriteLine(line);
        }
        catch (Exception ee)
        {
            IsConnected = false;
        }
    }

    public void Write(string line)
    {
        try
        {
            if (!IsConnected)
            {
                Connect();
            }

            writer.Write(line);
        }
        catch (Exception ee)
        {
            IsConnected = false;
        }
    }
}

