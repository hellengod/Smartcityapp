using System.Net;
using Newtonsoft.Json.Linq;
using RestSharp;
using TechTalk.SpecFlow;
using Xunit;
using System.Net.Http;

namespace SmartCityApp.Steps
{
    [Binding]
    public class AuthSteps
    {
        private RestClient _client;
        private RestRequest _request;
        private RestResponse _response;
        private string _baseUrl = "https://localhost:5001/api/auth";

        [Given(@"que o usuário ""(.*)"" com senha ""(.*)"" existe")]
        public void DadoQueOUsuarioComSenhaExiste(string username, string password)
        {
            _client = new RestClient(_baseUrl);
            _request = new RestRequest("register", Method.Post);  // Alterado para 'Method.Post'

            _request.AddJsonBody(new { Username = username, Password = password, Email = $"{username}@exemplo.com" });
            _client.Execute(_request);
        }

        [When(@"o usuário tenta fazer login com ""(.*)"" e ""(.*)""")]
        public void QuandoOUsuarioTentaFazerLoginComE(string username, string password)
        {
            _client = new RestClient(_baseUrl);
            _request = new RestRequest("login", Method.Post);  // Alterado para 'Method.Post'
            _request.AddJsonBody(new { Username = username, Password = password });
            _response = _client.Execute(_request);
        }

        [Then(@"a resposta deve conter um token JWT")]
        public void EntaoARespostaDeveConterUmTokenJWT()
        {
            Assert.Equal(HttpStatusCode.OK, _response.StatusCode);
            var content = JObject.Parse(_response.Content);
            Assert.NotNull(content["token"]);
        }

        [Then(@"a resposta deve retornar status (.*) e os dados do usuário criado")]
        public void EntaoARespostaDeveRetornarStatusEosDadosDoUsuarioCriado(int statusCode)
        {
            Assert.Equal((HttpStatusCode)statusCode, _response.StatusCode);
            var content = JObject.Parse(_response.Content);
            Assert.NotNull(content["username"]);
            Assert.NotNull(content["email"]);
        }

        [Then(@"a resposta deve ser (.*) Unauthorized")]
        public void EntaoARespostaDeveSerUnauthorized(int statusCode)
        {
            Assert.Equal((HttpStatusCode)statusCode, _response.StatusCode);
        }
    }
}
