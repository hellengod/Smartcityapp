using System.Net;
using Newtonsoft.Json.Linq;
using RestSharp;
using TechTalk.SpecFlow;
using Xunit;

namespace SmartCityApp.Steps
{
    [Binding]
    public class ReportsSteps
    {
        private RestClient _client;
        private RestRequest _request;
        private RestResponse _response;
        private string _baseUrl = "https://localhost:5001/api/reports";

        [Given(@"que existem relatórios cadastrados no sistema")]
        public void DadoQueExistemRelatoriosCadastradosNoSistema()
        {
            // Pré-condição: Certifique-se de que há relatórios no banco de dados para os testes
            // Isso pode ser feito via seed ou inserção direta no banco de dados de teste
        }

        [When(@"o usuário solicita a lista de relatórios na página (.*) com pageSize (.*)")]
        public void QuandoOUsuarioSolicitaAListaDeRelatoriosNaPaginaComPageSize(int page, int pageSize)
        {
            _client = new RestClient(_baseUrl);
            _request = new RestRequest("GET");  // Alterado para string "GET"
            _request.AddParameter("page", page);
            _request.AddParameter("pageSize", pageSize);
            _response = _client.Execute(_request);
        }

        [Then(@"a resposta deve retornar status (.*) e uma lista de relatórios")]
        public void EntaoARespostaDeveRetornarStatusEUmaListaDeRelatorios(int statusCode)
        {
            Assert.Equal((HttpStatusCode)statusCode, _response.StatusCode);

            if (string.IsNullOrEmpty(_response.Content))
            {
                throw new InvalidOperationException("A resposta está vazia ou nula.");
            }

            var content = JObject.Parse(_response.Content);
            Assert.NotNull(content["reports"]);
        }

        [Then(@"a resposta deve retornar status (.*) com mensagem de erro apropriada")]
        public void EntaoARespostaDeveRetornarStatusComMensagemDeErroApropriada(int statusCode)
        {
            Assert.Equal((HttpStatusCode)statusCode, _response.StatusCode);

            if (string.IsNullOrEmpty(_response.Content))
            {
                throw new InvalidOperationException("A resposta está vazia ou nula.");
            }

            var content = JObject.Parse(_response.Content);
            Assert.NotNull(content["message"]);
        }
    }
}
