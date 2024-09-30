using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Seminario_POO2
{
    public class OrdemServico : Conectando
    {
        public int id { get; set; }
        public string cpf { get; set; }
        public int idVeiculo { get; set; }
        public string dataOrdem { get; set; }
        public double totalOrdem { get; set; }
       

        public OrdemServico(int id, string cpf, int idveiculo, string data, double total)
        {
            this.id = id;
            this.cpf = cpf;
            this.idVeiculo = idveiculo;
            this.dataOrdem = data;
            this.totalOrdem = total;
        }
        public static void InserirOrdem()
        {

            try
            {
                Console.WriteLine("Insira o CPF do Cliente: ");
                string cpf = Console.ReadLine();

                Console.WriteLine("Insira o Id do Veiculo: ");
                int idVeiculo = int.Parse(Console.ReadLine());

                Console.WriteLine("Insira a data da Ordem de Serviço: ");
                string dataOrdem = Console.ReadLine();

                Console.WriteLine("Insira o valor total da Ordem de Serviço realizada: ");
                double totalOrdem = double.Parse(Console.ReadLine());

                using (var comando = conexaoBanco().CreateCommand())
                {
                    comando.CommandText = "INSERT INTO OrdemServico (CPF_Cliente, Id_Veiculo, Data_Ordem, Total_Ordem) VALUES (@CPF_Cliente, @Id_Veiculo, @Data_Ordem, @Total_Ordem)";
                    comando.Parameters.AddWithValue("@CPF_Cliente", cpf);
                    comando.Parameters.AddWithValue("@Id_Veiculo", idVeiculo);
                    comando.Parameters.AddWithValue("@Data_Ordem", dataOrdem);
                    comando.Parameters.AddWithValue("@Total_Ordem", totalOrdem);
                    comando.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERRO!" + ex);
            }

        }



        public static void DeletarOrdem()
        {
            try
            {
                Console.WriteLine("Informe o Id da Ordem de Serviço que deseja DELETAR: ");
                int id = int.Parse(Console.ReadLine());

                using (var comando = conexaoBanco().CreateCommand())
                {
                    comando.CommandText = "DELETE FROM OrdemServico WHERE Id_Ordem = @Id_Ordem";
                    comando.Parameters.AddWithValue("@Id_Ordem", id);
                    comando.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERRO!" + ex);
            }
        }

        public static List<OrdemServico> ConsultarTabelaOrdem()
        {
            List<OrdemServico> OrdemServicos = new List<OrdemServico>();

            try
            {
                using (var comando = conexaoBanco().CreateCommand())
                {
                    comando.CommandText = "SELECT * FROM OrdemServico";
                    using (var ler = comando.ExecuteReader())
                    {
                        while (ler.Read())
                        {
                            OrdemServico ordem = new OrdemServico(Convert.ToInt32(ler["Id_Ordem"]), ler["CPF_Cliente"].ToString(),
                                Convert.ToInt32(ler["Id_Veiculo"]),ler["Data_Ordem"].ToString(), Convert.ToDouble(ler["Total_Ordem"]));

                            OrdemServicos.Add(ordem);   
                        }
                    }

                }
                return OrdemServicos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO! {ex}");
            }
            return OrdemServicos;
        }

        public static void MostrarOrdem()
        {
            List<OrdemServico> OrdemServicos = OrdemServico.ConsultarTabelaOrdem();
            foreach (OrdemServico OrdemServico in OrdemServicos)
            {
                Cliente.MostrarClientes();
                Console.WriteLine($"Id Ordem: {OrdemServico.id}");
                Console.WriteLine($"Nome Cliente: {OrdemServico.cpf}");
                Console.WriteLine($"Id Ordem: {OrdemServico.idVeiculo}");
                Console.WriteLine($"Data Ordem: {OrdemServico.dataOrdem}");
                Console.WriteLine($"Total Ordem: {OrdemServico.totalOrdem}\n");
                
            }
        }

        public static void AtualizarOrdemServico()
        {

            try
            {
                Console.WriteLine("Informe o Id da Ordem de Serviço: ");
                int id = int.Parse(Console.ReadLine());

                Console.WriteLine("Digite a nova data da Ordem de Serviço ");
                string data = Console.ReadLine();

                Console.WriteLine("Digite o novo total da Ordem de Serviço: ");
                string total = Console.ReadLine();

                using (var comando = conexaoBanco().CreateCommand())
                {

                    comando.CommandText = "UPDATE OrdemServico SET Data_Ordem = @Data_Ordem," +
                        "Total_Ordem = @Total_Ordem WHERE Id_Ordem = @Id_Ordem";

                    comando.Parameters.AddWithValue("@Id_Ordem", id);
                    comando.Parameters.AddWithValue("@Data_Ordem", data);
                    comando.Parameters.AddWithValue("@Total_Ordem", total);

                    comando.ExecuteNonQuery();
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }






        public static void EscolherOpcoesOrdemServico()
        {
            Console.WriteLine("\nO que deseja modificar na tabela OrdemServiço?");

            Console.WriteLine("1. Inserir nova Ordem?");
            Console.WriteLine("2. Deletar Ordem?");
            Console.WriteLine("3. Consultar todos as Ordens de Serviço cadastradas?");
            Console.WriteLine("4. Atualizar dados da Ordem de Serviço?\n");

            int opcao = int.Parse(Console.ReadLine());

            switch(opcao)
            {
                case 1:

                    OrdemServico.InserirOrdem();
                    break;

                case 2:

                    OrdemServico.DeletarOrdem();
                    break;

                case 3:

                    OrdemServico.ConsultarTabelaOrdem();
                    OrdemServico.MostrarOrdem();
                    break;

                case 4:

                    OrdemServico.AtualizarOrdemServico();
                    break;
            }
        }

    }
}
