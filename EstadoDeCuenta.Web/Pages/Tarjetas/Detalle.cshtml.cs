using EstadoDeCuenta.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;

namespace EstadoDeCuenta.Web.Pages.Tarjetas
{
    public class DetalleModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TarjetaCreditoDto Tarjeta { get; set; }
        public decimal InteresBonificable { get; set; }
        public decimal CuotaMinimaPagar { get; set; }
        public decimal MontoTotalPagar { get; set; }
        public List<CompraDto> Compras { get; set; }
        public List<PagoDto> Pagos { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public DetalleModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            Tarjeta = new TarjetaCreditoDto(); // Inicializa para evitar null
        }


        public IActionResult OnGetExportarPDF(int id, int Mes, int Anio)
        {
            // Inicializa el cliente HTTP
            var client = _httpClientFactory.CreateClient("EstadoDeCuentaApi");

            // Obtiene la información de la tarjeta desde el API
            var response = client.GetAsync($"EstadoCuenta/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                Tarjeta = JsonConvert.DeserializeObject<TarjetaCreditoDto>(json);
            }

            // Obtiene las compras
            var comprasResponse = client.GetAsync($"Compras/{id}?mes={Mes}&anio={Anio}").Result;
            if (comprasResponse.IsSuccessStatusCode)
            {
                var jsonCompras = comprasResponse.Content.ReadAsStringAsync().Result;
                Compras = JsonConvert.DeserializeObject<List<CompraDto>>(jsonCompras);
            }

            // Obtiene los pagos
            var pagosResponse = client.GetAsync($"Pagos/{id}?mes={Mes}&anio={Anio}").Result;
            if (pagosResponse.IsSuccessStatusCode)
            {
                var jsonPagos = pagosResponse.Content.ReadAsStringAsync().Result;
                Pagos = JsonConvert.DeserializeObject<List<PagoDto>>(jsonPagos);
            }

            // Genera el PDF
            var estadoDeCuenta = new MemoryStream();
            var doc = new Document(PageSize.A4, 20f, 20f, 20f, 20f);
            PdfWriter.GetInstance(doc, estadoDeCuenta);
            doc.Open();

            // Título
            var titleFont = FontFactory.GetFont("Arial", 20, Font.BOLD, BaseColor.Blue);
            var title = new Paragraph("Detalle del Estado de Cuenta", titleFont)
            {
                Alignment = Element.ALIGN_CENTER
            };
            doc.Add(title);
            doc.Add(new Paragraph("\n"));

            // Información de la Tarjeta
            var subTitleFont = FontFactory.GetFont("Arial", 16, Font.BOLD, BaseColor.White);
            var normalFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.Black);

            // Sección: Información de la Tarjeta
            PdfPTable infoTable = new PdfPTable(2)
            {
                WidthPercentage = 100
            };
            infoTable.AddCell(new PdfPCell(new Phrase("Información de la Tarjeta", subTitleFont))
            {
                Colspan = 2,
                BackgroundColor = BaseColor.Blue,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 5
            });
            infoTable.AddCell(new Phrase("Nombre del Titular:", normalFont));
            infoTable.AddCell(new Phrase(Tarjeta.NombreTitular, normalFont));
            infoTable.AddCell(new Phrase("Número de Tarjeta:", normalFont));
            infoTable.AddCell(new Phrase(Tarjeta.NumeroTarjeta, normalFont));
            infoTable.AddCell(new Phrase("Saldo Actual:", normalFont));
            infoTable.AddCell(new Phrase($"${Tarjeta.SaldoActual}", normalFont));
            infoTable.AddCell(new Phrase("Saldo Disponible:", normalFont));
            infoTable.AddCell(new Phrase($"${Tarjeta.SaldoDisponible}", normalFont));
            doc.Add(infoTable);
            doc.Add(new Paragraph("\n"));

            // Compras Realizadas
            PdfPTable comprasTable = new PdfPTable(3)
            {
                WidthPercentage = 100
            };
            comprasTable.AddCell(new PdfPCell(new Phrase("Compras Realizadas", subTitleFont))
            {
                Colspan = 3,
                BackgroundColor = BaseColor.Yellow,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 5
            });
            comprasTable.AddCell(new Phrase("Fecha", normalFont));
            comprasTable.AddCell(new Phrase("Descripción", normalFont));
            comprasTable.AddCell(new Phrase("Monto", normalFont));

            foreach (var compra in Compras)
            {
                comprasTable.AddCell(new Phrase(compra.Fecha.ToShortDateString(), normalFont));
                comprasTable.AddCell(new Phrase(compra.Descripcion, normalFont));
                comprasTable.AddCell(new Phrase($"${compra.Monto}", normalFont));
            }
            doc.Add(comprasTable);
            doc.Add(new Paragraph("\n"));

            // Pagos Realizados
            PdfPTable pagosTable = new PdfPTable(2)
            {
                WidthPercentage = 100
            };
            pagosTable.AddCell(new PdfPCell(new Phrase("Pagos Realizados", subTitleFont))
            {
                Colspan = 2,
                BackgroundColor = BaseColor.Red,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 5
            });
            pagosTable.AddCell(new Phrase("Fecha", normalFont));
            pagosTable.AddCell(new Phrase("Monto", normalFont));

            foreach (var pago in Pagos)
            {
                pagosTable.AddCell(new Phrase(pago.Fecha.ToShortDateString(), normalFont));
                pagosTable.AddCell(new Phrase($"${pago.Monto}", normalFont));
            }
            doc.Add(pagosTable);

            doc.Close();
            var pdfBytes = estadoDeCuenta.ToArray();

            return File(pdfBytes, "application/pdf", "EstadoDeCuenta.pdf");
        }

        public async Task OnGetAsync(int id, int? mes, int? anio)
        {

            var client = _httpClientFactory.CreateClient("EstadoDeCuentaApi");

            // Obtener el estado de cuenta de la tarjeta de crédito
            var response = await client.GetAsync($"EstadoCuenta/{Id}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"JSON Recibido: {json}"); // Para depuración
                Tarjeta = JsonConvert.DeserializeObject<TarjetaCreditoDto>(json);
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
                Tarjeta = new TarjetaCreditoDto();
            }

            //var client = _httpClientFactory.CreateClient("EstadoDeCuentaApi");

            //// Obtener el estado de cuenta de la tarjeta de crédito
            //var response = await client.GetAsync($"EstadoDeCuenta/{Id}");

            //if (response.IsSuccessStatusCode)
            //{
            //    var json = await response.Content.ReadAsStringAsync();
            //    Tarjeta = JsonConvert.DeserializeObject<TarjetaCreditoDto>(json);
            //}
            //else
            //{
            //    Tarjeta = new TarjetaCreditoDto();
            //}

            // Obtener Compras
            var comprasUrl = $"Compras/{id}";
            if (mes.HasValue && anio.HasValue)
            {
                comprasUrl += $"?mes={mes.Value}&anio={anio.Value}";
            }

            var comprasResponse = await client.GetAsync(comprasUrl);
            if (comprasResponse.IsSuccessStatusCode)
            {
                var jsonCompras = await comprasResponse.Content.ReadAsStringAsync();
                Compras = JsonConvert.DeserializeObject<List<CompraDto>>(jsonCompras);
            }
            else
            {
                Compras = new List<CompraDto>();
            }

            // Obtener Pagos
            var pagosUrl = $"Pagos/{id}";
            if (mes.HasValue && anio.HasValue)
            {
                pagosUrl += $"?mes={mes.Value}&anio={anio.Value}";
            }

            var pagosResponse = await client.GetAsync(pagosUrl);
            if (pagosResponse.IsSuccessStatusCode)
            {
                var jsonPagos = await pagosResponse.Content.ReadAsStringAsync();
                Pagos = JsonConvert.DeserializeObject<List<PagoDto>>(jsonPagos);
            }
            else
            {
                Pagos = new List<PagoDto>();
            }
        }

    }
}
