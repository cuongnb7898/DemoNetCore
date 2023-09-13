namespace DemoNetCore
{
    internal class OptionsConfiguration
    {
        public string folder { get; set; }
        public string SecretKey { get; set; }
        public object ConnectionStrings { get; set; }
    }
}