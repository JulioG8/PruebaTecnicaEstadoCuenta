using EstadoDeCuenta.Web.Models;
using FluentValidation;

namespace EstadoDeCuenta.Web.Validators
{
    public class CompraDtoValidator : AbstractValidator<CompraDto>
    {
        public CompraDtoValidator()
        {
            RuleFor(x => x.Descripcion)
                .NotEmpty().WithMessage("La descripción es obligatoria.")
                .MaximumLength(100).WithMessage("La descripción no puede tener más de 100 caracteres.");

            RuleFor(x => x.Monto)
                .GreaterThan(0).WithMessage("El monto debe ser mayor a cero.");
        }
    }

}
