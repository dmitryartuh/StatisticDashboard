namespace Models.DtoModels
{
    public class ResponseModel<T>
    {
        public string Status { get; set; }

        public MetaDto Meta { get; set; }

        public T Data { get; set; }
    }
}
