namespace MYCOLL.Entities
{
    public class TipoUtilizador
    {
        static public List<string> GetAllTipos()
        {
            return new List<string> { cliente, fornecedor, funcionario, admin};
        }
        static public List<string> GetTiposAtivos()
        {
            return new List<string> { cliente, fornecedor, funcionario, admin };
        }

        public const String cliente = "Cliente";
        public const String fornecedor = "Fornecedor";
        public const String funcionario = "Funcionario";
        public const String fInativo = "Funcionario Inativo";

        public const String admin = "Admin";
    }
}
