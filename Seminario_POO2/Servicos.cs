using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Seminario_POO2
{
    public class Servicos : Conectando
    {
        public int idServico { get; set; }
        public string descricaoServico { get; set; }   
        public double precoServico { get; set; }

        public Servicos(int id, string descricaoS, double precos)
        {
            this.idServico = id;    
            this.descricaoServico = descricaoS;
            this.precoServico = precos;
        }




        public static void InserirServico()
        {
            try
            {
                Console.WriteLine("Insira a descrição do Serviço realizado: ");
                string descricao = Console.ReadLine();

                Console.WriteLine("Insira o preço total do Servico realizado: ");
                double preco = double.Parse(Console.ReadLine());

                using (var comando = conexaoBanco().CreateCommand())
                {
                    comando.CommandText = "INSERT INTO Servicos (Descricao_Servico, Preco_Servico) " +
                        "VALUES (@Descricao_Servico, @Preco_Servico)";
                    comando.Parameters.AddWithValue("@Descricao_Servico", descricao);
                    comando.Parameters.AddWithValue("Preco_Servico", preco);
                    comando.ExecuteNonQuery();
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERRO!" + ex);
            }
        }

        public static void DeletarServico()
        {
            try
            {
                Console.WriteLine("Informe o Id do Serviço que deseja DELETAR: ");
                int id = int.Parse(Console.ReadLine());

                using (var comando = conexaoBanco().CreateCommand())
                {
                    comando.CommandText = "DELETE FROM Servicos WHERE Id_Servico = @Id_Servico";
                    comando.Parameters.AddWithValue("@Id_Servico", id);
                    comando.ExecuteNonQuery();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"ERRO!{ex}");
            }
        }

        public static List<Servicos> ConsultarTabelaServico()
        {
            List<Servicos> Servico = new List<Servicos>();  
            try
            {
               using (var comando = conexaoBanco().CreateCommand())
                {
                    comando.CommandText = "SELECT * FROM Servicos";
                    using (var ler = comando.ExecuteReader())
                    {
                        while(ler.Read())
                        {
                            Servicos servicos = new Servicos(Convert.ToInt32(ler["Id_Servico"]), 
                                ler["Descricao_Servico"].ToString(), Convert.ToDouble(ler["Preco_Servico"]));
                            Servico.Add(servicos);
                        }
                    }

                }   
               return Servico;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"ERRO! {ex}");
            }
            return Servico;
        }

        public static void MostrarServicos()
        {
            List<Servicos> servico = Servicos.ConsultarTabelaServico();

            foreach(Servicos servicos in servico)
            {
                Console.WriteLine($"Id Serviço: {servicos.idServico}");
                Console.WriteLine($"Descrição Serviço: {servicos.descricaoServico}");
                Console.WriteLine($"Preço Serviço: {servicos.precoServico.ToString("F2", CultureInfo.InvariantCulture)}\n");
                
            }
        }

        public static void EscolherOpcoesServico()
        {
            Console.WriteLine("\nO que deseja modificar na tabela Serviço?");

            Console.WriteLine("1. Inserir novo Serviço?");
            Console.WriteLine("2. Deletar Serviço?");
            Console.WriteLine("3. Consultar todos os Serviços cadastrados?\n");

            int opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    Servicos.InserirServico();
                    break;

                case 2:
                    Servicos.DeletarServico();
                    break;

                case 3:
                    Servicos.ConsultarTabelaServico();
                    Servicos.MostrarServicos();
                    break;
            }
        }

    }
}
