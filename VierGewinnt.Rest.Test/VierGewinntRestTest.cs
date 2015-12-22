using Nancy;
using Nancy.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VierGewinnt.Rest.DTO;
using VierGewinnt.Rest.logic;
using Xunit;

namespace VierGewinnt.Rest.Test
{
    public class VierGewinntRestTest
    {
        IVierGewinnt model;
        Browser browser;
        public VierGewinntRestTest()
        {

            // Given
            model = new RestLogic();
            var bootstrapper = new DefaultNancyBootstrapper();
            browser = new Browser(with => with.Module(new VierGewinntRestModule(model)));
        }

        [Fact]
        public void Should_Create_A_Session_Game_And_Return_SessionId()
        {

            // When
            var response = browser.Post("/connectfour/sessions", with => {
                with.HttpRequest();
                with.FormValue("PlayerA", "A");
                with.FormValue("PlayerB", "B");
                with.FormValue("BoardWidth", "10");
                with.FormValue("BoardHeight", "20");
            });

            // Then
            var sessionId = JsonConvert.DeserializeObject<DtoSessionId>(response.Body.AsString());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(sessionId.Id);

        }

        [Fact]
        public void Should_Create_A_Session_Game_And_Return_SessionId_Although_BoardWidth_Is_Not_Set()
        {

            // When
            var response = browser.Post("/connectfour/sessions", with => {
                with.HttpRequest();
                with.FormValue("PlayerA", "A");
                with.FormValue("PlayerB", "B");
                with.FormValue("BoardHeight", "20");
            });

            // Then
            var sessionId = JsonConvert.DeserializeObject<DtoSessionId>(response.Body.AsString());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(sessionId.Id);
        }

        [Fact]
        public void Should_Create_A_Session_Game_And_Return_SessionId_Although_BoardHeight_Is_Not_Set()
        {

            // When
            var response = browser.Post("/connectfour/sessions", with => {
                with.HttpRequest();
                with.FormValue("PlayerA", "A");
                with.FormValue("PlayerB", "B");
                with.FormValue("BoardWidth", "10");
            });

            // Then
            var sessionId = JsonConvert.DeserializeObject<DtoSessionId>(response.Body.AsString());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(sessionId.Id);
        }

        [Fact]
        public void Should_Create_A_Session_Game_And_Return_SessionId_Although_PlayerB_Not_Exist_In_DtoSessionStart()
        {

            // When
            var response = browser.Post("/connectfour/sessions", with => {
                with.HttpRequest();
                with.FormValue("PlayerA", "A");
                with.FormValue("BoardWidth", "10");
                with.FormValue("BoardHeight", "20");
            });

            // Then
            var sessionId = JsonConvert.DeserializeObject<DtoSessionId>(response.Body.AsString());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(sessionId.Id);
        }

        [Fact]
        public void Should_Create_A_Session_Game_And_Return_SessionId_Although_PlayerA_Not_Exist()
        {

            // When
            var response = browser.Post("/connectfour/sessions", with => {
                with.HttpRequest();
                with.FormValue("PlayerB", "B");
                with.FormValue("BoardWidth", "10");
                with.FormValue("BoardHeight", "20");
            });

            // Then
            var sessionId = JsonConvert.DeserializeObject<DtoSessionId>(response.Body.AsString());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(sessionId.Id);
        }




        //------------------------------------------------------------------------------------------------------------------------


        [Fact]
        public void Should_Create_A_Session_Game_Although_PlayerA_Not_Set_And_PlayerAName_Is_SpielerA()
        {

            // When
            var response = browser.Post("/connectfour/sessions", with => {
                with.HttpRequest();
                with.FormValue("PlayerB", "B");
                with.FormValue("BoardWidth", "10");
                with.FormValue("BoardHeight", "20");
            });


            var sessionId = JsonConvert.DeserializeObject<DtoSessionId>(response.Body.AsString());

            var responseStatus = browser.Get("/connectfour/sessions/" + sessionId.Id, with => {
                with.HttpRequest();
            });

            // Then
            var dtoSessionStatus = JsonConvert.DeserializeObject<DtoSessionStatus>(responseStatus.Body.AsString());
            Assert.Equal(HttpStatusCode.OK, responseStatus.StatusCode);
            Assert.Equal("Spieler A", dtoSessionStatus.PlayerA);
            Assert.Equal("B", dtoSessionStatus.PlayerB);

        }

        [Fact]
        public void Should_Create_A_Session_Game_Although_PlayerB_Not_Set_And_PlayerAName_Is_SpielerB()
        {

            // When
            var response = browser.Post("/connectfour/sessions", with => {
                with.HttpRequest();
                with.FormValue("PlayerA", "A");
                with.FormValue("BoardWidth", "10");
                with.FormValue("BoardHeight", "20");
            });


            var sessionId = JsonConvert.DeserializeObject<DtoSessionId>(response.Body.AsString());

            var responseStatus = browser.Get("/connectfour/sessions/" + sessionId.Id, with => {
                with.HttpRequest();
            });

            // Then
            var dtoSessionStatus = JsonConvert.DeserializeObject<DtoSessionStatus>(responseStatus.Body.AsString());
            Assert.Equal(HttpStatusCode.OK, responseStatus.StatusCode);
            Assert.Equal("Spieler B", dtoSessionStatus.PlayerB);

        }


       /* [Theory]
        [InlineData(5)]
        [InlineData(501)]
        public void Should_Throw_A_Error_Message_Because_Width_Not_Between_7_499(int width)
        {

            // When
            var response = browser.Post("/connectfour/sessions", with => {
                with.HttpRequest();
                with.FormValue("PlayerA", "A");
                with.FormValue("PlayerB", "B");
                with.FormValue("BoardWidth", ""+ width);
                with.FormValue("BoardHeight", "20");
            });


            // Then
            var message = JsonConvert.DeserializeObject<DtoMessage>(response.Body.AsString());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("The width must be >= 7", message.Message);
            Assert.Equal("LogicException", message.Title);

        }*/

    }

}
