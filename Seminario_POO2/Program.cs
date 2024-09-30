// See https://aka.ms/new-console-template for more information
using Seminario_POO2;


while (true)
{
    Console.WriteLine("\nQual tabela deseja modificar?");
    Console.WriteLine("1. Tabela Cliente");
    Console.WriteLine("2. Tabela Ordem de Serviço");
    Console.WriteLine("3. Tabela Serviço");
    Console.WriteLine("4. Tabela Veiculos");
    Console.WriteLine("5. Sair\n");

    Console.WriteLine("Escolha apenas uma opção: ");
    int opcao = int.Parse(Console.ReadLine());

    switch (opcao)
    {
        case 1:

            Cliente.EscolherOpcoesCliente();
            break;

        case 2:

            OrdemServico.EscolherOpcoesOrdemServico();
            break;

        case 3:

            Servicos.EscolherOpcoesServico();
            break;

        case 4:

            Veiculos.EscolherOpcoesVeiculos();
            break;

        case 5:
            Environment.Exit(0);
            break;


    }

}
