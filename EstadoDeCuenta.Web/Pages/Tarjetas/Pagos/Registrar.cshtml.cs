using EstadoDeCuenta.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EstadoDeCuenta.Web.Pages.Tarjetas.Pagos
{
    public class RegistrarModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        [BindProperty]
        public PagoDto NuevoPago { get; set; }

        public RegistrarModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public void OnGet()
        {
            NuevoPago = new PagoDto();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = _httpClientFactory.CreateClient("EstadoDeCuentaApi");
            var response = await client.PostAsJsonAsync("Pagos", NuevoPago);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Tarjetas/Detalle", new { id = NuevoPago.TarjetaCreditoId });
            }

            ModelState.AddModelError(string.Empty, "Hubo un error al registrar el pago.");
            return Page();
        }
    }

}
