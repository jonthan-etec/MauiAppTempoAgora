namespace MauiAppTempoAgora.Models
{
    internal class Tempo
    {
        // Localização
        public double? lon { get; set; }
        public double? lat { get; set; }

        // Visibilidade
        public int? visibility { get; set; }

        // Temperatura
        public double? temp_min { get; set; }
        public double? temp_max { get; set; }

        // Condições do clima
        public string? main { get; set; }
        public string? description { get; set; }

        // Vento
        public double? speed { get; set; }

        // Astronomia
        public string? sunrise { get; set; }
        public string? sunset { get; set; }
    }
}
