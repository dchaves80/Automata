namespace AutomataServer.Models.recordsmodels
{
    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public DateTime creationdate { get; set; }
        public bool banned { get; set; }
    }
}
