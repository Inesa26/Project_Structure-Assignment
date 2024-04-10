namespace Domain
{
    public class Department : Entity
    {
        public string Name { get; set; }

        public override string? ToString()
        {
            return $"ID: {Id}, Name: {Name}";
        }
    }
}
