using ConsultorioAPI.Models;

namespace ConsultorioAPI.Services
{
    public class ViaCepServices
    {
        private readonly HttpClient _httpClient;
        public ViaCepServices(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri("https://viacep.com.br/ws/");
        }

        public async Task<ViaCepResponse> BuscarEnderecoAsync(string cep)
        {
            var endereco = await _httpClient.GetFromJsonAsync<ViaCepResponse>($"https://viacep.com.br/ws/{cep}/json/");
            return endereco;

        }
    }
}
