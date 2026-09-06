namespace model
{
    public abstract class Gate
    {
        public string Id { get; }
        protected Gate(string id) { Id = id; }
    }
}