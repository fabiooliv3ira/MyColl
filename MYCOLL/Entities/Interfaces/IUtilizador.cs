namespace MYCOLL.Entities.Interfaces
{
    public interface IUtilizador
    {
        enum TipoUtilizador
        {
            anonimo = 1,
            cliente = 2,
            fornecedor = 3
        }
        int id { get; set; }
        int tipoUtilizador { get; }
    }
}
