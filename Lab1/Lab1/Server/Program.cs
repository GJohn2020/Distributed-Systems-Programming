using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
TcpListener tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 5000);
tcpListener.Start();
TcpClient tcpClient = tcpListener.AcceptTcpClient();
NetworkStream nStream =tcpClient.GetStream();
string message = ReadFromStream(nStream);
Console.WriteLine("Received: \"" + message + "\"");



string translatedMessage = Translate(message);
Console.WriteLine("Translated: \"" + translatedMessage + "\"");

// Serialize translated message and send back to client
byte[] response = Serialize(translatedMessage);
nStream.Write(response, 0, response.Length);
// TODO: Serialize the translated message and write it to the stream

Console.ReadKey(); // Wait for keypress before exit
static string Translate(string message)
{
   StringBuilder translatedmessage = new StringBuilder();//???????
    //string translatedmessage = "TEST RESPONSE";
    string[] words = message.Split(' ');
    foreach (string word in words)
    {
        // TODO: Perform translation#
        int index = 0;
        if (IsVowel(word[0]))
        {
            translatedmessage.Append(word + "way ");
        }

        else
        {
            // Count leading consonants
            while (index < word.Length && !IsVowel(word[index]))
            {
                index++;
            }

            // Rule 1 & 2: move consonant(s) to end + "ay"
            string translated =
                word.Substring(index) +
                word.Substring(0, index) +
                "ay";

            translatedmessage.Append(translated + " ");
        }
    }
    return translatedmessage.ToString().Trim();
}


static bool IsVowel(char c)
{
    return "aeiouAEIOU".IndexOf(c) >= 0;
}
static string ReadFromStream(NetworkStream stream)
{
    int messageLength = stream.ReadByte();
    byte[] messageBytes = new byte[messageLength];
    stream.Read(messageBytes, 0, messageLength);
    return Encoding.ASCII.GetString(messageBytes);
}


////???????
static byte[] Serialize(string response)
{
    byte[] responseBytes = Encoding.ASCII.GetBytes(response);
    byte responseLength = (byte)responseBytes.Length;

    byte[] rawData = new byte[responseLength + 1];
    rawData[0] = responseLength;
    responseBytes.CopyTo(rawData, 1);

    return rawData;
}