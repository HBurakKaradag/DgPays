using System;

namespace DgPays.Domain.ConfigurationModels
{
    public class RabbitSettings : BaseSettingsModel
    {
        public string? Host { get; set; }
        public int Port { get; set; }
        public bool Ssl { get; set; } = false;
    }
}
