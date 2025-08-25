namespace DesafioProjetoHospedagem.Models
{
    public class Reserva
    {
        private List<Pessoa> _hospedes;
        private Suite _suite;
        private int _diasReservados;

        public List<Pessoa> Hospedes
        {
            get => _hospedes;
        }

        public Suite Suite
        {
            get => _suite;
        }

        public int DiasReservados
        {
            get => _diasReservados;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("O número de dias deve ser maior que zero");
                }

                _diasReservados = value;
            }
        }

        public Reserva() { }

        public Reserva(int diasReservados)
        {
            DiasReservados = diasReservados;
        }

        public void CadastrarHospedes(List<Pessoa> hospedes)
        {
            // TODO: Verificar se a capacidade é maior ou igual ao número de hóspedes sendo recebido
            // *IMPLEMENTADO*
            if (_suite == null)
            {
                throw new InvalidOperationException("Não foi encontrada nenhuma suite. Primeiro adicione uma suite para cadastrar um hóspede.");
            }
            else
            {
                if (_suite.Capacidade < hospedes.Count)
                {
                    throw new ArgumentException($"O número de hóspedes excede a capacidade máxima de {_suite.Capacidade} da suite.");
                }

                else if (hospedes.Count < 1)
                {
                    throw new ArgumentException("Deve haver pelo menos 1 hóspede na lista de hóspedes.");
                }

                _hospedes = hospedes;
            }
        }

        public void CadastrarSuite(Suite suite)
        {
            _suite = suite;
        }

        public int ObterQuantidadeHospedes()
        {
            // TODO: Retorna a quantidade de hóspedes (propriedade Hospedes)
            // *IMPLEMENTADO*
            if (_hospedes == null)
            {
                return 0;
            }

            return _hospedes.Count;
        }

        public decimal CalcularValorDiaria()
        {
            // TODO: Retorna o valor da diária
            // Cálculo: DiasReservados X Suite.ValorDiaria
            // *IMPLEMENTADO*
            decimal valor = _diasReservados * _suite.ValorDiaria;

            // Regra: Caso os dias reservados forem maior ou igual a 10, conceder um desconto de 10%
            // *IMPLEMENTADO*
            if (_diasReservados >= 10)
            {
                valor *= 0.9M;
            }

            return valor;
        }
    }
}