using PipesAndFilters.Filters;
using PipesAndFilters.Messages;
using PipesAndFilters.Pipes;
using System.Collections.Generic;

namespace PipesAndFilters
{
    static class ServerEnvironment
    {
        private static List<User> Users { get; set; }
        public static User CurrentUser { get; private set; }

        private static IPipe IncomingPipe { get; set; }
        private static IPipe OutgoingPipe { get; set; }

        // ✔ As instructed: loop through users and set CurrentUser
        public static bool SetCurrentUser(int id)
        {
            foreach (var user in Users)
            {
                if (user.ID == id)
                {
                    CurrentUser = user;
                    return true;
                }
            }
            return false;
        }

        // ✔ As instructed: initialise pipes and register filters
        public static void Setup()
        {
            // Users
            Users = new List<User>();
            Users.Add(new User { ID = 1, Name = "Test User" });

            // Pipes
            IncomingPipe = new Pipe();
            OutgoingPipe = new Pipe();

            // Incoming pipeline filters
            IncomingPipe.RegisterFilter(new AuthenticateFilter());
            IncomingPipe.RegisterFilter(new TranslateFilter());

            // Outgoing pipeline filters
            OutgoingPipe.RegisterFilter(new TranslateFilter());
            OutgoingPipe.RegisterFilter(new TimestampFilter());
        }

        public static IMessage SendRequest(IMessage message)
        {
            // Incoming pipeline
            message = IncomingPipe.ProcessMessage(message);

            // Endpoint
            HelloWorldEndpoint endpoint = new HelloWorldEndpoint();
            message = endpoint.Execute(message);

            // Outgoing pipeline
            message = OutgoingPipe.ProcessMessage(message);

            return message;
        }
    }
}
