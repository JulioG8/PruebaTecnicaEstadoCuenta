using EstadoDeCuenta.Web.Models;
using FluentValidation;

namespace EstadoDeCuenta.Web.Validators
{
    public class PagoDtoValidator : AbstractValidator<PagoDto>
    {
        public PagoDtoValidator()
        {
            RuleFor(x => x.Monto)
                .GreaterThan(0).WithMessage("El monto debe ser mayor a cero.");
        }
    }

}
