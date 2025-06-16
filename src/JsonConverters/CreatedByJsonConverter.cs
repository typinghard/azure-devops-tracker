using AzureDevopsTracker.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AzureDevopsTracker.JsonConverters
{
    internal class UserInfo
    {
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

    }
    internal class CreatedByJsonConverter : JsonConverter<string>
    {
        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var userString = reader.GetString();
                return userString.ExtractEmail(); // Reutiliza seu método de extensão
            }
            else if (reader.TokenType == JsonTokenType.StartObject)
            {
                // Desserializa o objeto para UserInfo
                var userInfo = JsonSerializer.Deserialize<UserInfo>(ref reader, options);
                return userInfo?.DisplayName;
            }

            // Se não for nem string nem objeto, retorne null ou lance uma exceção
            return null;
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            // Este conversor é primariamente para leitura (desserialização).
            // Se você precisar serializar, precisará definir como o valor string deve ser escrito.
            // Para o seu caso de uso (extrair dados do JSON), não é estritamente necessário.
            writer.WriteStringValue(value);
        }
    }
}
