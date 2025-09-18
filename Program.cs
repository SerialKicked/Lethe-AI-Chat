using LetheAISharp.LLM;
using Newtonsoft.Json;
using System.IO;
using LetheAIChat.Files;

namespace LetheAIChat
{
    internal static class Program
    {
        public static MainForm? BigForm { get; private set; }
        public static LetheChatSettings Settings { get; set; } = new LetheChatSettings();

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            if (!File.Exists("settings.json"))
            {
                Settings = new LetheChatSettings();
                File.WriteAllText("settings.json", JsonConvert.SerializeObject(Settings, Formatting.Indented));
            }
            var str = File.ReadAllText("settings.json");
            Settings = JsonConvert.DeserializeObject<LetheChatSettings>(str)!;
            LLMEngine.Settings = Settings;

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            DataFiles.LoadDB();
            BigForm = new MainForm();
            Application.Run(BigForm);
        }
    }
}