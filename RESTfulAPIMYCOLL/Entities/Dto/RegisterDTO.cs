namespace RESTfulAPIMYCOLL.Entities.Dto
{
    public class RegisterDTO
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string Nome { get; set; } = string.Empty;

        public string? NIF { get; set; }

        public string? TipoUtilizador { get; set; }

        public DateTime DataRegisto { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = false;
    }
}
