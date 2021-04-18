using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dio_Bank_MVC.Models
{
    public class Conta
    {
        [Key]
        public int IdConta { get; set; }

        [ForeignKey("UserName")]
        public string UserName { get; set; }
        public int Agencia { get; set; }
        public int NumeroConta { get; set; }
        public int DigitoVerificador { get; set; }
        public int Tipo { get; set; }
        public double Saldo { get; set; }
        public string DataCriacao { get; set; }
    }
}
