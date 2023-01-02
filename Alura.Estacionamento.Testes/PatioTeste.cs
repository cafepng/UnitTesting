using Alura.Estacionamento.Alura.Estacionamento.Modelos;
using Alura.Estacionamento.Modelos;
using Xunit;
using Xunit.Abstractions;

namespace Alura.Estacionamento.Testes;

public class PatioTeste : IDisposable
{
    private Veiculo veiculo = new();
    private Operador operador = new();
    public ITestOutputHelper _testOutputHelper;

    public PatioTeste(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _testOutputHelper.WriteLine("Construtor invocado");
        operador.Nome = "Operador01";
    }

    [Fact]
    public void TestaFaturamento()
    {
        Patio patio = new();
        patio.OperadorPatio = operador;

        veiculo = new()
        {
            Proprietario = "Proprietario01",
            Tipo = TipoVeiculo.Automovel,
            Cor = "Cor01",
            Modelo = "Modelo01",
            Placa = "ASD-0001"
        };

        patio.RegistrarEntradaVeiculo(veiculo);
        patio.RegistrarSaidaVeiculo(veiculo.Placa);

        double faturamento = patio.TotalFaturado();

        Assert.Equal(2, faturamento);
    }

    [Theory]
    [InlineData("Proprietario01", "ASD-0001", "Cor01", "Carro01")]
    [InlineData("Proprietario02", "ASD-0002", "Cor02", "Carro02")]
    [InlineData("Proprietario03", "ASD-0003", "Cor03", "Carro03")]
    [InlineData("Proprietario04", "ASD-0004", "Cor04", "Carro04")]
    public void TestaFaturamentoComVariosVeiculos(string proprietario, string placa, string cor, string modelo)
    {
        Patio patio = new();
        patio.OperadorPatio = operador;

        veiculo = new()
        {
            Proprietario = proprietario,
            Cor = cor,
            Modelo = modelo,
            Placa = placa
        };

        patio.RegistrarEntradaVeiculo(veiculo);
        patio.RegistrarSaidaVeiculo(veiculo.Placa);

        double faturamento = patio.TotalFaturado();

        Assert.Equal(2, faturamento);
    }

    [Theory]
    [InlineData("Proprietario01", "ASD-0001", "Cor01", "Carro01")]
    [InlineData("Proprietario02", "ASD-0002", "Cor02", "Carro02")]
    [InlineData("Proprietario03", "ASD-0003", "Cor03", "Carro03")]
    [InlineData("Proprietario04", "ASD-0004", "Cor04", "Carro04")]
    public void LocalizaVeiculoPatio(string proprietario, string placa, string cor, string modelo)
    {
        Patio patio = new();
        patio.OperadorPatio = operador;

        veiculo = new()
        {
            Proprietario = proprietario,
            Cor = cor,
            Modelo = modelo,
            Placa = placa
        };

        patio.RegistrarEntradaVeiculo(veiculo);

        var consultado = patio.PesquisaVeiculo(veiculo.IdTicket);

        Assert.Contains("### Ticket Estacionamento Alura ###", consultado.Ticket);
    }

    [Fact]
    public void AlterarDadosVeiculo()
    {
        Patio patio = new();
        patio.OperadorPatio = operador;

        veiculo = new()
        {
            Proprietario = "Proprietario01",
            Tipo = TipoVeiculo.Automovel,
            Cor = "Cor01",
            Modelo = "Modelo01",
            Placa = "ASD-0001"
        };

        patio.RegistrarEntradaVeiculo(veiculo);

        Veiculo veiculo2 = new()
        {
            Proprietario = "Proprietario01",
            Tipo = TipoVeiculo.Automovel,
            Cor = "Cor02",
            Modelo = "Modelo01",
            Placa = "ASD-0001"
        };

        Veiculo alterado = patio.AlteraDadosVeiculo(veiculo2);

        Assert.Equal(alterado.Cor, veiculo2.Cor);
    }

    public void Dispose()
    {
        _testOutputHelper.WriteLine("Dispose invocado");
    }
}
