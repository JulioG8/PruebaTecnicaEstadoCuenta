using EstadoDeCuenta.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EstadoDeCuenta.Web.Pages.Tarjetas.Compras
{
    public class CrearModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        [BindProperty]
        public CompraDto NuevaCompra { get; set; }

        public CrearModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public void OnGet()
        {
            NuevaCompra = new CompraDto();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = _httpClientFactory.CreateClient("EstadoDeCuentaApi");
            var response = await client.PostAsJsonAsync("Compras", NuevaCompra);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Tarjetas/Detalle", new { id = NuevaCompra.TarjetaCreditoId });
            }

            ModelState.AddModelError(string.Empty, "Hubo un error al registrar la compra.");
            return Page();
        }
    }

}
