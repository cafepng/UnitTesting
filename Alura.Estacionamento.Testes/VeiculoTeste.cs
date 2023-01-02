using Alura.Estacionamento.Alura.Estacionamento.Modelos;
using Alura.Estacionamento.Modelos;
using Xunit;
using Xunit.Abstractions;

namespace Alura.Estacionamento.Testes;

public class VeiculoTeste : IDisposable
{
    private Veiculo veiculo = new();
    public ITestOutputHelper _testOutputHelper;

    public VeiculoTeste(ITestOutputHelper consoleTest)
    {
        _testOutputHelper = consoleTest;
        _testOutputHelper.WriteLine("Construtor invocado");

    }

    [Fact(DisplayName = "Acelerar")]
    [Trait("Funcionalidade", "Acelerar")]
    public void TestaVeiculoAcelerar()
    {
        veiculo.Acelerar(10);
        Assert.Equal(100, veiculo.VelocidadeAtual);
    }

    [Fact(DisplayName = "Frear")]
    [Trait("Funcionalidade", "Acelerar")]
    public void TestaVeiculoFrear()
    {
        veiculo.Frear(10);
        Assert.Equal(-150, veiculo.VelocidadeAtual);
    }

    [Fact]
    public void DadosVeiculo()
    {
        veiculo = new()
        {
            Proprietario = "Proprietario01",
            Tipo = TipoVeiculo.Automovel,
            Cor = "Cor01",
            Modelo = "Modelo01",
            Placa = "ASD-0001"
        };

        string dados = veiculo.ToString();

        Assert.Contains("Ficha do Veiculo", dados);
    }

    [Fact]
    public void TestaExcecaoNomeProprietario()
    {
        string nomeProprietario = "Ab";

        Assert.Throws<System.FormatException>(
            () => new Veiculo(nomeProprietario)
        );
    }

    [Fact]
    public void TestaExcecaoPlaca()
    {
        string placa = "ASDF0001";

        var mensagem = Assert.Throws<System.FormatException>(
            () => new Veiculo().Placa = placa
        );

        Assert.Equal("O 4° caractere deve ser um hífen", mensagem.Message);
    }

    public void Dispose()
    {
        _testOutputHelper.WriteLine("Dispose invocado");
    }
}