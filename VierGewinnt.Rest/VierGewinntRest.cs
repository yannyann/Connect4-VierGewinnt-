﻿using Nancy;
using Nancy.Hosting.Self;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VierGewinnt.Model;
using VierGewinnt.Rest.DTO;
using VierGewinnt.Rest.logic;

namespace VierGewinnt.Rest
{
    class VierGewinntRest
    {
        static void Main(string[] args)
        {
            var config = new HostConfiguration() { UrlReservations = new UrlReservations() { CreateAutomatically = true } };

            using (var host = new NancyHost(config, new Uri("http://localhost:1234")))
            {
                host.Start();
                Console.ReadLine();
            }
        }
    }


    public class VierGewinntRestModule : NancyModule
    {

        public VierGewinntRestModule(IVierGewinnt model)
        {
            Post["/connectfour/sessions"] = parameters =>
            {
                var sessionStart = this.Bind<DtoSessionStart>();
                string sessionId = null;
                try
                {
                    sessionId = model.CreateSession(sessionStart);
                }
                catch (Exception e)
                {
                    return Response.AsJson(new DtoMessage(e.GetType().Name, e.Message));
                }


                return Response.AsJson(new DtoSessionId(sessionId));
            };

            Get["/connectfour/sessions/{id}"] = parameters =>
            {
                DtoSessionStatus dtoSessionStatus;
                try
                {
                    SessionStatus sessionStatus = model.Status(parameters.id);
                    dtoSessionStatus = new DtoSessionStatus(sessionStatus.PlayerA, sessionStatus.PlayerB, sessionStatus.BoardWidth, sessionStatus.BoardHeight, Enum.GetName(typeof(SessionStatus.State), sessionStatus.Status));
                }
                catch (Exception e)
                {
                    return Response.AsJson(new DtoMessage(e.GetType().Name, e.Message));
                }

                return Response.AsJson(dtoSessionStatus);
            };

        }

    }
}