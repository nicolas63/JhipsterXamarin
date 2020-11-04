using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace JhipsterXamarin.Models
{
    public class JwtToken
    {
        [JsonPropertyName("id_token")]
        public string IdToken { get; set; }
    }
}
