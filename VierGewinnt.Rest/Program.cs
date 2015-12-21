using Nancy;
using Nancy.Hosting.Self;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VierGewinnt.Rest
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new HostConfiguration() { UrlReservations = new UrlReservations() { CreateAutomatically = true} };

            using (var host = new NancyHost(config, new Uri("http://localhost:1234")))
            {
                host.Start();
                Console.ReadLine();
            }
        }
    }

    public interface IGameState
    {
        string Text { get; set; }
    }

    public class GameState : IGameState
    {
        public string Text { get; set; } = "Hallo";
    }

    public class HelloModule : NancyModule
    {
        public HelloModule(IGameState gameState)
        {
            Before += ctx =>
            {
                return null;   
            };

            After += ctx =>
            {
            };

            Get["/"] = parameters => gameState.Text;

            Get["/{id:int}"] = parameters =>
            {
                int id = (int)parameters.id;
                gameState.Text = string.Format("Hello world {0}", id);
                return gameState.Text;
            };
        }
    }
}
