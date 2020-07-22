namespace IngresoSwatch.ModelApi
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class ContenedorModel
    {
        [JsonProperty("idcontenedor")]
        public long Idcontenedor { get; set; }

        [JsonProperty("contenedor")]
        public string Contenedor { get; set; }
    }

    public partial class ContenedorModel
    {
        public static List<ContenedorModel> FromJson(string json) => JsonConvert.DeserializeObject<List<ContenedorModel>>(json, ConverterContenedor.Settings);
    }

    public static class SerializeContenedor
    {
        public static string ToJson(this List<ContenedorModel> self) => JsonConvert.SerializeObject(self, ConverterContenedor.Settings);
    }

    internal static class ConverterContenedor
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
