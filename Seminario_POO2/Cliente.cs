using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminario_POO2
{
    public class Cliente : Conectando
    {
        public string cpf { get; set; }
        public string name { get; set; }    
        public string endereco { get; set; }
        public string telefone {  get; set; }   

        public Cliente(string cpf,string nome, string endereco, string telefone)
        { 
            this.cpf = cpf;
            this.name = nome;
            this.endereco = endereco;
            this.telefone = telefone;
        }    
        
        public static void InserirCliente()
        {
            try
            {
                Console.WriteLine("Insira o CPF do Cliente: ");
                string cpf = Console.ReadLine();    

                Console.WriteLine("Informe o Nome do cliente: ");
                string nome = Console.ReadLine();

                Console.WriteLine("Informe o endereço do cliente: ");
                string endereco = Console.ReadLine();

                Console.WriteLine("Informe o telefone do cliente: ");
                long telefone = long.Parse(Console.ReadLine());

                using (var comando = conexaoBanco().CreateCommand())
                {
                    comando.CommandText = "INSERT INTO Cliente (CPF_Cliente, Nome_Cliente, Endereco_Cliente, Telefone_Cliente)" +
                        " VALUES (@CPF_Cliente, @Nome_Cliente, @Endereco_Cliente, @Telefone_Cliente)";
                    comando.Parameters.AddWithValue("@CPF_Cliente", cpf);
                    comando.Parameters.AddWithValue("@Nome_Cliente", nome);
                    comando.Parameters.AddWithValue("@Endereco_Cliente", endereco);
                    comando.Parameters.AddWithValue("@Telefone_Cliente", telefone);
                    comando.ExecuteNonQuery();

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("ERRO!" + ex);
            }
        }

        public static void DeletarCliente()
        {
            try
            {
                Console.WriteLine("Informe o CPF da pessoa que deseja deletar do banco de dados.");
                string CPF = Console.ReadLine();

                using (var comando = conexaoBanco().CreateCommand())
                {
                    comando.CommandText = "DELETE FROM Cliente WHERE CPF_Cliente = @CPF_Cliente";
                    comando.Parameters.AddWithValue("@CPF_Cliente", CPF);
                    comando.ExecuteReader();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("ERRO!" + ex);
            }
        }

        public static List<Cliente> ConsultarTabelaCliente()
        {
            List<Cliente> clientes = new List<Cliente>();

            try
            {
                using (var comando = conexaoBanco().CreateCommand())
                {
                    comando.CommandText = "SELECT * FROM Cliente";
                    using(var ler = comando.ExecuteReader())
                    {
                        while(ler.Read())
                        {
                            Cliente cliente = new Cliente(ler["CPF_Cliente"].ToString(),
                                ler["Nome_Cliente"].ToString(),
                                ler["Endereco_Cliente"].ToString(),
                                ler["Telefone_Cliente"].ToString()); ;

                            clientes.Add(cliente);
                        }
                    }
                    
                }
                return clientes;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO! {ex}");
            }
            return clientes;   
        }

        public static void MostrarClientes()
        {
            List<Cliente> clientes = Cliente.ConsultarTabelaCliente();
            foreach (Cliente cliente in clientes) 
            {
                Console.WriteLine($"CPF Cliente: {cliente.cpf}");
                Console.WriteLine($"Nome Cliente: {cliente.name}");
                Console.WriteLine($"Endereço Cliente: {cliente.endereco}");
                Console.WriteLine($"Telefone Cliente: {cliente.telefone}\n");
            }
        }

        public static void AtualizarCliente()
        {

            try
            {
                Console.WriteLine("Informe o CPF");
                string cpf = Console.ReadLine();

                Console.WriteLine("Digite o novo Endereço do Cliente: ");
                string endereco = Console.ReadLine();

                Console.WriteLine("Digite o novo Telefone do Cliente: ");
                string telefone = Console.ReadLine();

                using (var comando = conexaoBanco().CreateCommand())
                {

                    comando.CommandText = "UPDATE Cliente SET Endereco_Cliente = @Endereco_Cliente," +
                        "Telefone_Cliente = @Telefone_Cliente WHERE CPF_Cliente = @CPF_Cliente";
                 
                    comando.Parameters.AddWithValue("@CPF_Cliente", cpf);
                    comando.Parameters.AddWithValue("@Endereco_Cliente", endereco);
                    comando.Parameters.AddWithValue("@Telefone_Cliente", telefone);
      
                    comando.ExecuteNonQuery();
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }

        public static void EscolherOpcoesCliente()
        {
            Console.WriteLine("\nO que deseja modificar na tabela Cliente?");

            Console.WriteLine("1. Inserir novo Cliente?");
            Console.WriteLine("2. Deletar cliente?");
            Console.WriteLine("3. Consultar todos os clientes cadastrados?");
            Console.WriteLine("4. Atualizar dados do cliente?\n");

            int opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:

                    Cliente.InserirCliente();
                    break;

                case 2:

                    Cliente.DeletarCliente();
                    break;

                case 3:

                    Cliente.ConsultarTabelaCliente();
                    Cliente.MostrarClientes();
                    break;

                case 4:

                    Cliente.AtualizarCliente();
                    break;
            }
            
        }



    }
}
