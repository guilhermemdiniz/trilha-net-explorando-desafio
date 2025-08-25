namespace DesafioProjetoHospedagem.Models;

public class Pessoa
{
    public Pessoa() { }

    public Pessoa(string nome)
    {
        Nome = nome;
    }

    public Pessoa(string nome, string sobrenome)
    {
        Nome = nome;
        Sobrenome = sobrenome;
    }

    private string _nome;
    private string _sobrenome;

    public string Nome
    {
        get => _nome;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("O nome não pode estar vazio ou nulo");
            }

            _nome = value;
        }
    }

    public string Sobrenome
    {
        get => _sobrenome;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("O sobrenome não pode estar vazio ou nulo");
            }

            _sobrenome = value;
        }
    }

    public string NomeCompleto => $"{Nome} {Sobrenome}".ToUpper();
}