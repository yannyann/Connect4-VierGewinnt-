using Nancy;
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
            AutoMapper.Mapper.CreateMap<SessionStatus, DtoSessionStatus>();
            AutoMapper.Mapper.AssertConfigurationIsValid();

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
                    return Response.AsJson(new DtoMessage() { Title = e.GetType().Name, Message = e.Message});
                }


                return Response.AsJson(new DtoSessionId() { Id = sessionId });
            };

            Get["/connectfour/sessions/{id}"] = parameters =>
            {
                DtoSessionStatus dtoSessionStatus;
                try
                {
                    SessionStatus sessionStatus = model.Status(parameters.id);
                    dtoSessionStatus = AutoMapper.Mapper.Map<DtoSessionStatus>(sessionStatus);
                }
                catch (Exception e)
                {
                    return Response.AsJson(new DtoMessage() { Title = e.GetType().Name, Message = e.Message });
                }

                return Response.AsJson(dtoSessionStatus);
            };

        }

    }
}
