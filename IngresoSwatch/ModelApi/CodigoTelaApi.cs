
namespace IngresoSwatch.ModelApi
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class CodigoTelaModel
    {
        [JsonProperty("idtpc")]
        public long Idtpc { get; set; }

        [JsonProperty("procod")]
        public string Procod { get; set; }

        [JsonProperty("idcontenedor")]
        public long Idcontenedor { get; set; }
    }

    public partial class CodigoTelaModel
    {
        public static List<CodigoTelaModel> FromJson(string json) => JsonConvert.DeserializeObject<List<CodigoTelaModel>>(json, ConverterCodigo.Settings);
    }

    public static class SerializeCodigo
    {
        public static string ToJson(this List<CodigoTelaModel> self) => JsonConvert.SerializeObject(self, ConverterCodigo.Settings);
    }

    internal static class ConverterCodigo
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
