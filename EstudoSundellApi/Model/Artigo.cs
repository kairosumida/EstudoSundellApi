using System.Text.Json;
using System.Text.Json.Serialization;

namespace EstudoSundellApi.Model
{
	public class Artigo
	{
		[JsonPropertyName("id")]
		public string Id { get; set; }
		public string Nome { get; set; }
		public DateTime DataInicio { get; set; }
		public DateTime DataTermino { get; set; }
	}
}

