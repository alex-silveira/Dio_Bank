using Dio_Bank_MVC.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Dio_Bank_MVC.Models
{
    public class Transferencia
    {
        [Key]
        public int IdTransferencia { get; set; }
       
        [ForeignKey("IdConta")]
        public int Conta_IdConta { get; set; }

        [Display(Name = "Agência")]
        public int Agencia { get; set; }

        [Display(Name = "Conta")]
        public int Conta { get; set; }

        [Display(Name = "Dv")]
        public int DigitoVerificador { get; set; }

        [Display(Name = "Tipo")]
        public int TipoConta { get; set; }

        [Display(Name = "Valor transferido")]
        public double Valor { get; set; }

        [Display(Name = "Data")]
        public string Data { get; set; }

        [Display(Name = "Repetir")]
        public bool RepetirProximosMeses { get; set; }

        public bool Sacar(double valorSaque, Dio_Bank_MVCContext _context, string userName)
        {
            var saque = _context.Conta
               .Where(b => b.UserName == userName)
               .FirstOrDefault();

            if(saque.Saldo < valorSaque)
            {
                return false;
            }
            
            saque.Saldo -= valorSaque;
            _context.Update(saque);

            return true;
        }

        public void Transferir(double valorTransferencia, Dio_Bank_MVCContext _context, string userName)
        {
            if (this.Sacar(valorTransferencia, _context, userName))
            {
                Depositar(valorTransferencia, _context);
            }
        }

        public void Depositar(double valorDeposito, Dio_Bank_MVCContext _context)
        {
            var deposito = _context.Conta
           .Where(
                b => b.Agencia == this.Agencia
                    && b.NumeroConta == this.Conta
                    && b.DigitoVerificador == this.DigitoVerificador
                    && b.Tipo == this.TipoConta
                )
           .FirstOrDefault();

            deposito.Saldo += valorDeposito;

            _context.Update(deposito);
        }
    }
}
