namespace AutomataServer.Models
{
    public class BaseModel
    {
        public bool hasError { get; set; } = false;
        public string? error { get; set; } = null;

        public BaseModel() { }

    }
}
