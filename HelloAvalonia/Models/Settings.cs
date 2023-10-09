using System;
using System.IO;
using System.Text.Json;

namespace HelloAvalonia.Models
{
    public class Setting
    {
        public Setting()
        {
            Script = new ScriptInfo();
        }

        public ScriptInfo Script { get; set; }
        public class ScriptInfo
        {
            public string Ext { get; set; } = string.Empty;
        }
    }

    public sealed class Settings
    {
        private static readonly Lazy<Settings> _instance =
            new Lazy<Settings>();
        public static Settings Instance => _instance.Value;

        private readonly string _filepath = @"Resources\Settings.json";

        public Settings()
        {
            var json = File.ReadAllText(_filepath);
            _jo = JsonSerializer.Deserialize<Setting>(json);
        }

        private Setting? _jo;
        public Setting Read() => _jo ?? new Setting();

    }
}