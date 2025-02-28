using EstadoDeCuenta.Web.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EstadoDeCuenta.Web.Pages.Tarjetas
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public List<TarjetaCreditoDto> Tarjetas { get; set; } = new List<TarjetaCreditoDto>();

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task OnGetAsync()
        {
            try
            {

                var client = _httpClientFactory.CreateClient("EstadoDeCuentaApi");
                ViewData["BaseUrl"] = client.BaseAddress.ToString(); // Pasa la URL base a la vista
                var response = await client.GetAsync("Tarjetas").ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    Tarjetas = JsonConvert.DeserializeObject<List<TarjetaCreditoDto>>(json);
                }
                else
                {
                    // Manejo de errores de respuesta
                    ModelState.AddModelError(string.Empty, "Error al cargar las tarjetas de crédito.");
                }
            }
            catch (HttpRequestException ex)
            {
                // Manejo de excepciones de red
                ModelState.AddModelError(string.Empty, $"Error de conexión: {ex.Message}");
            }
        }
    }
}


//using EstadoDeCuenta.Web.Models;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Newtonsoft.Json;
//using System.Collections.Generic;
//using System.Net.Http;
//using System.Threading.Tasks;

//namespace EstadoDeCuenta.Web.Pages.Tarjetas
//{
//    public class IndexModel : PageModel
//    {
//        private readonly IHttpClientFactory _httpClientFactory;
//        public List<TarjetaCreditoDto> Tarjetas { get; set; }

//        public IndexModel(IHttpClientFactory httpClientFactory)
//        {
//            _httpClientFactory = httpClientFactory;
//        }

//        public async Task OnGetAsync()
//        {
//            var client = _httpClientFactory.CreateClient("EstadoDeCuentaApi");
//            var response = await client.GetAsync("Tarjetas");

//            if (response.IsSuccessStatusCode)
//            {
//                var json = await response.Content.ReadAsStringAsync();
//                Tarjetas = JsonConvert.DeserializeObject<List<TarjetaCreditoDto>>(json);
//            }
//            else
//            {
//                Tarjetas = new List<TarjetaCreditoDto>();
//            }
//        }
//    }
//}
