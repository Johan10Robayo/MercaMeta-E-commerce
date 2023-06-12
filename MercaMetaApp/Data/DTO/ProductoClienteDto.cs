namespace MercaMetaApp.Data.DTO
{
    public class ProductoClienteDto
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public string Descripcion { get; set; }
        public string UrlImagen { get; set; }
        public long TelefonoEmpresa { get; set; }
        public int Cantidad { get; set; }
        public string UnidadMedida { get; set; }
    }
}
