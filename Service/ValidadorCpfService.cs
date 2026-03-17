using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ConsultorioAPI.Service
{
    public class ValidadorCpfService
    {

        //protected override ValidationResult isValid(object value, ValidationContext validation)
        //{
        //    { var cpf = value as string;

        //        if (string.IsNullOrEmpty(cpf)) return new ValidationResult("O CPF é obrigatório.");

        //        cpf = Regex.Replace(cpf, "[^0-9]", "");

        //        if(cpf.Length != 11) return new ValidationResult("O CPF deve conter 11 dígitos.");
        //        return ValidationResult.Success;

        //    }
    }
}
