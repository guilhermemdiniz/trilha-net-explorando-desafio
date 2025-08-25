using System.Text;
using DesafioProjetoHospedagem.Models;

Console.OutputEncoding = Encoding.UTF8;

List<Suite> suitesCadastradas = new List<Suite>();

InitializeSystem();

/* MENU INTERATIVO */
void InitializeSystem()
{
    int option = 0;

    while (option != 4)
    {
        Console.WriteLine("\n==============================");
        Console.WriteLine("      SISTEMA DE HOTELARIA     ");
        Console.WriteLine("==============================\n");
        Console.WriteLine("Escolha uma das opções abaixo: ");
        Console.WriteLine(
            "1 - Cadastrar nova suíte\n" +
            "2 - Listar suítes cadastradas\n" +
            "3 - Realizar nova reserva\n" +
            "4 - Sair do sistema");

        Console.Write("\nDigite sua opção: ");
        option = Convert.ToInt16(Console.ReadLine());
        Console.WriteLine();

        switch (option)
        {
            case 1:
                CadastrarSuiteMenu();
                break;
            case 2:
                Console.WriteLine("Lista de suítes cadastradas:\n");
                ListarSuites();
                break;
            case 3:
                RealizarReservaMenu();
                break;
            case 4:
                Console.WriteLine("Encerrando o sistema... Até logo!");
                break;
            default:
                Console.WriteLine("Opção inválida! Por favor, escolha uma opção entre 1 e 4.");
                break;
        }
    }
}

// MENU CADASTRAR SUÍTE
void CadastrarSuiteMenu()
{
    Console.Clear();
    bool finished = false;
    while (!finished)
    {
        Console.WriteLine("\n--- CADASTRAR NOVA SUÍTE ---");
        Console.WriteLine("Informe os dados da suíte:\n");

        Console.Write("Tipo de suíte (Simples/Luxo/Presidencial): ");
        string tipo = Console.ReadLine();

        Console.Write("Capacidade máxima de hóspedes: ");
        int capacidade = Convert.ToInt16(Console.ReadLine());

        Console.Write("Valor da diária (em R$): ");
        decimal valorDiaria = Convert.ToDecimal(Console.ReadLine());

        try
        {
            Suite novaSuite = new Suite(tipo, capacidade, valorDiaria);
            suitesCadastradas.Add(novaSuite);
            Console.WriteLine("\nSuíte cadastrada com sucesso!");
            finished = true;
        }
        catch (ArgumentException e)
        {
            Console.WriteLine($"\nErro: {e.Message}");
            Console.WriteLine("Deseja tentar novamente?\n1 - SIM\n0 - NÃO");
            finished = Console.ReadLine() != "1";
        }
    }
}

// MENU REALIZAR RESERVA
void RealizarReservaMenu()
{
    Console.Clear();
    bool finished = false;
    while (!finished)
    {
        Console.WriteLine("\n--- REALIZAR NOVA RESERVA ---\n");

        Console.WriteLine("Informe os dados dos hóspedes:");
        List<Pessoa> hospedes = AdicionarHospedesMenu();

        Console.WriteLine($"\nReserva para {hospedes.Count} hóspede(s).");
        Console.WriteLine("Escolha uma das suítes disponíveis:\n");

        ListarSuites();
        Console.Write("Digite o número da suíte desejada: ");
        int indexSuiteEscolhida = Convert.ToInt16(Console.ReadLine()) - 1;

        Console.Write("Informe a quantidade de dias da reserva: ");
        int diasReservados = Convert.ToInt16(Console.ReadLine());

        // Confirmação
        Console.WriteLine("\n--- RESUMO DA RESERVA ---");
        Console.WriteLine("Hóspedes:\n" + ObterDadosHospedes(hospedes));
        Console.WriteLine(ObterDadosSuite(suitesCadastradas[indexSuiteEscolhida]));
        Console.WriteLine($"Dias reservados: {diasReservados}");
        Console.WriteLine("---------------------------");
        Console.WriteLine("Confirmar reserva?\n1 - SIM\n0 - NÃO");

        finished = Console.ReadLine() == "1";

        if (finished)
        {
            try
            {
                Reserva reserva = new Reserva(diasReservados);
                reserva.CadastrarSuite(suitesCadastradas[indexSuiteEscolhida]);
                reserva.CadastrarHospedes(hospedes);
                Console.WriteLine("\nReserva realizada com sucesso!");
                ImprimirDetalhesDaReserva(reserva);
                finished = true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"\nErro ao realizar a reserva: {e.Message}");
                Console.WriteLine("Deseja tentar novamente?\n1 - SIM\n0 - NÃO");
                finished = Console.ReadLine() != "1";
            }
        }
    }
}

void ImprimirDetalhesDaReserva(Reserva reserva)
{
    if (reserva == null)
    {
        Console.WriteLine("Nenhuma reserva encontrada.");
        return;
    }

    Console.WriteLine("\n==============================");
    Console.WriteLine("      DETALHES DA RESERVA     ");
    Console.WriteLine("==============================\n");

    // Hóspedes
    if (reserva.Hospedes != null && reserva.Hospedes.Count > 0)
    {
        Console.WriteLine("Hóspedes:");
        int index = 0;
        foreach (var h in reserva.Hospedes)
        {
            index++;
            Console.WriteLine($"[{index}] - {h.NomeCompleto}");
        }
        Console.WriteLine($"Total de hóspedes: {reserva.Hospedes.Count}\n");
    }
    else
    {
        Console.WriteLine("Nenhum hóspede cadastrado.\n");
    }

    // Suíte
    if (reserva.Suite != null)
    {
        Console.WriteLine("Suíte:");
        Console.WriteLine($"Tipo: {reserva.Suite.TipoSuite}");
        Console.WriteLine($"Capacidade: {reserva.Suite.Capacidade}");
        Console.WriteLine($"Valor da diária: {reserva.Suite.ValorDiaria:C}\n");
    }
    else
    {
        Console.WriteLine("Nenhuma suíte cadastrada para esta reserva.\n");
    }

    // Dias e valor total
    Console.WriteLine($"Dias reservados: {reserva.DiasReservados}");
    Console.WriteLine($"Valor total da reserva: R{reserva.CalcularValorDiaria():C}");
    Console.WriteLine("\n==============================\n");
}

// MENU ADICIONAR HÓSPEDES
List<Pessoa> AdicionarHospedesMenu()
{
    List<Pessoa> hospedes = new List<Pessoa>();

    int hospedesCount = 0;
    bool finished = false;
    while (!finished)
    {
        hospedesCount++;
        Console.WriteLine($"\n--- Hóspede {hospedesCount} ---");

        Console.Write("Nome: ");
        string nome = Console.ReadLine();

        Console.Write("Sobrenome: ");
        string sobrenome = Console.ReadLine();
        Pessoa hospede = new Pessoa(nome, sobrenome);
        hospedes.Add(hospede);

        Console.WriteLine("Deseja adicionar outro hóspede?\n1 - SIM\n0 - NÃO");
        finished = Console.ReadLine() != "1";
    }

    return hospedes;
}

void ListarSuites()
{
    Console.Clear();
    if (suitesCadastradas == null || suitesCadastradas.Count < 1)
    {
        Console.WriteLine("Não há suítes cadastradas.");
    }
    else
    {
        int index = 0;
        foreach (Suite s in suitesCadastradas)
        {
            index++;
            Console.WriteLine($"[{index}] - Tipo: {s.TipoSuite}, capacidade: {s.Capacidade}, valor da diária: {s.ValorDiaria:C}");
        }
    }
}

string ObterDadosHospedes(List<Pessoa> hospedes)
{
    string dados = "";
    int index = 0;
    foreach (Pessoa h in hospedes)
    {
        index++;
        dados += $"[{index}] - {h.NomeCompleto}\n";
    }
    dados += $"\nTotal: {hospedes.Count}";

    return dados;
}

string ObterDadosSuite(Suite suite)

{
    return $"Tipo da suíte: {suite.TipoSuite}\nCapacidade da suite: {suite.Capacidade}";
}