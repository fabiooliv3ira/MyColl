namespace MYCOLL.Entities.Interfaces
{
    public interface Utilizador
    {
        enum TipoUtilizador
        {
            anonimo = 1,
            cliente = 2,
            fornecedor = 3
        }
        int tipoUtilizador { get; }
    }
}
