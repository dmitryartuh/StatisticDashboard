using System.Collections.Generic;

namespace Models.DtoModels
{
    public class ResponseModel<T>
    {
        public string Status { get; set; }

        public MetaDto Meta { get; set; }

        public IEnumerable<T> Data { get; set; }
    }
}
