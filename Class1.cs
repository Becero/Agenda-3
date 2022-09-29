using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validacao;

namespace AgendaLP2A4
{
    class Paciente
    {
        public string Nome;
        public string Telefone;
        public string Email;
        public string CPF;

        //Método Construtor
        public Paciente(string nome, string tel, string email, string cpf)
        {
           // Console.WriteLine(ValidaCPF.IsCpf(cpf));
            Validar(nome, tel, email, cpf);

        }

        public void Validar(string nome, string tel, string email, string cpf)
        {
            if (String.IsNullOrEmpty(nome))
                throw new ArgumentException("O texto fornecido para o campo listado abaixo é nulo ou vazio!", "nome");
            if (String.IsNullOrEmpty(tel))
                throw new ArgumentException("O texto fornecido para o campo listado abaixo é nulo ou vazio!", "telefone");
            if (String.IsNullOrEmpty(email))
                throw new ArgumentException("O texto fornecido para o campo listado abaixo é nulo ou vazio!", "email");
            if (!ValidaCPF.IsCpf(cpf))
                throw new ArgumentException("O texto fornecido para o campo listado abaixo é nulo ou vazio!", "cpf");

            Nome = nome;
            Telefone = tel;
            Email = email; 
            CPF = cpf;  
        }
        public string DadosParaArquivo()
        {
            return Nome + ";" +Telefone +";" + Email + ";" + CPF + ";"; 
        }
        public override string ToString()
        {
            return "NOME: " + Nome + "\nTELEFONE: " + Telefone + "\nEMAIL:" + Email + "\nCPF:" + CPF;
        }
    }
}
