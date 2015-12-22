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
            AutoMapper.Mapper.CreateMap<DtoMove, Move>();
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
                    return Response.AsJson(new DtoMessage() { Title = e.GetType().Name, Message = e.Message}, HttpStatusCode.BadRequest);
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
                    return Response.AsJson(new DtoMessage() { Title = e.GetType().Name, Message = e.Message }, HttpStatusCode.BadRequest);
                }

                return Response.AsJson(dtoSessionStatus);
            };

            Get["/connectfour/sessions/{id}/tokens"] = parameters =>
            {
                try
                {
                    string id = (string) parameters.id;
                    var moves = model.Moves(id);
                    var movesDto = AutoMapper.Mapper.Map<IEnumerable<DtoSessionStatus>>(moves);
                    return Response.AsJson(movesDto, HttpStatusCode.OK);
                }
                catch (Exception e)
                {
                    return Response.AsJson(new DtoMessage() { Title = e.GetType().Name, Message = e.Message }, HttpStatusCode.BadRequest);
                }
            };

            Post["/connectfour/sessions/{id}/tokens"] = parameters =>
            {
                try
                {
                    string id = (string) parameters.id;
                    var move = AutoMapper.Mapper.Map<Move>(this.Bind<DtoMove>());
                    var state = model.Play(id, move);

                    return Response.AsJson(new DtoMoveResult() { Status = state.ToString() }, HttpStatusCode.OK);
                }
                catch (GameException ge)
                {
                    return Response.AsJson(new DtoMessage() { Title = ge.GetType().Name, Message = ge.Message }, HttpStatusCode.BadRequest);
                }
                catch (Exception e)
                {
                    return Response.AsJson(new DtoMessage() { Title = e.GetType().Name, Message = e.Message }, HttpStatusCode.BadRequest);
                }
            };
        }
    }
}
