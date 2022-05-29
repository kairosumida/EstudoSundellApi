using System;
using Newtonsoft.Json;

namespace EstudoSundellApi.Models
{
	public class Artigo
	{
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "nome")]
        public string? Nome { get; set; }
        [JsonProperty(PropertyName = "dataInicio")]
        public DateTime DataInicio { get; set; }
        [JsonProperty(PropertyName = "dataTermino")]
        public DateTime? DataTermino { get; set; }
    }
}

