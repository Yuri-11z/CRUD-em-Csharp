using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Seminario_POO2
{
    public class Veiculos : Conectando
    {
        public int id { get;set; }
        public string CPF { get; set; }
        public string Modelo { get; set; }
        public string Ano { get; set; }
        public string Placa { get; set; }


        public Veiculos(int id, string cpf, string modelo, string ano, string placa)
        {
            this.id = id;
            this.CPF = cpf;
            this.Modelo = modelo;
            this.Ano = ano;
            this.Placa = placa;
        }

        public static void InserirVeiculo()
        {
            try
            {
                Console.WriteLine("Insira o CPF do Cliente: ");
                string cpf = Console.ReadLine();

                Console.WriteLine("Insira o modelo do Veiculo: ");
                string modelo = Console.ReadLine();

                Console.WriteLine("Insira o Ano do Veiculo: ");
                string ano = Console.ReadLine();    

                Console.WriteLine("Insira a Placa do Veiculo: ");
                string placa = Console.ReadLine();

                using (var comando = conexaoBanco().CreateCommand())
                {
                    comando.CommandText = "INSERT INTO Veiculos (CPF_Cliente, Modelo_Veiculo, Ano_Veiculo, Placa_Veiculo)" +
                        "VALUES (@CPF_Cliente, @Modelo_Veiculo, @Ano_Veiculo, @Placa_Veiculo)";
                    comando.Parameters.AddWithValue("@CPF_Cliente", cpf);
                    comando.Parameters.AddWithValue("@Modelo_Veiculo", modelo);
                    comando.Parameters.AddWithValue("@Ano_Veiculo", ano);
                    comando.Parameters.AddWithValue("@Placa_Veiculo", placa);
                    comando.ExecuteNonQuery();

                }


            }
            catch(Exception ex)
            {
                Console.WriteLine($"ERRO!{ex}");
            }
        }

        public static void DeletarVeiculo()
        {
            try
            {
                Console.WriteLine("Informe o Id do Veiculo que voce deseja DELETAR: ");
                int id = int.Parse(Console.ReadLine());
                using (var comando = conexaoBanco().CreateCommand())
                {
                    comando.CommandText = "DELETE FROM Veiculos WHERE Id_Veiculo = @Id_Veiculo";
                    comando.Parameters.AddWithValue("@Id_Veiculo", id);
                    comando.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO!{ex}");
            }

        }

        public static List<Veiculos> ConsultarVeiculos()
        {
            List<Veiculos> veiculo = new List<Veiculos>();

            try
            {
                using (var comando = conexaoBanco().CreateCommand())
                {
                    comando.CommandText = "SELECT * FROM Veiculos";
                    using (var ler = comando.ExecuteReader())
                    {
                        while(ler.Read())
                        {
                            Veiculos veiculos = new Veiculos(Convert.ToInt32(ler["Id_Veiculo"]),
                               ler["CPF_Cliente"].ToString(),ler["Modelo_Veiculo"].ToString(),ler["Ano_Veiculo"].ToString(),
                                ler["Placa_Veiculo"].ToString());

                            veiculo.Add(veiculos);  
                        }
                    }
                }
                return veiculo;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"ERRO! {ex}");
            }
            return veiculo;
        }

       public static void MostrarVeiculos()
        {
            List<Veiculos> veiculo = Veiculos.ConsultarVeiculos();
            foreach(Veiculos veiculos in veiculo)
            {
                Console.WriteLine($"Id Veiculo: {veiculos.id}");
                Console.WriteLine($"CPF Cliente: {veiculos.CPF}");
                Console.WriteLine($"Modelo Veiculo: {veiculos.Modelo}");
                Console.WriteLine($"Ano Veiculo: {veiculos.Ano}");
                Console.WriteLine($"Placa Veiculo: {veiculos.Placa}");

            }
        }

        public static void EscolherOpcoesVeiculos()
        {
            Console.WriteLine("\nO que deseja modificar na tabela Veiculos?");

            Console.WriteLine("1. Inserir novo Veiculo?");
            Console.WriteLine("2. Deletar Veiculo?");
            Console.WriteLine("3. Consultar todos os Veiculos cadastrados?\n");

            int opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:

                    Veiculos.InserirVeiculo();
                    break;

                case 2:

                    Veiculos.DeletarVeiculo();
                    break;

                case 3:

                    Veiculos.ConsultarVeiculos();
                    Veiculos.MostrarVeiculos();
                    break;
            }
        }

    }
}
