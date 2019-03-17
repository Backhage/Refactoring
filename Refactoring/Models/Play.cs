namespace Refactoring.Models
{
    public class Play
    {
        public string PlayId { get; }
        public string Name { get; }
        public string Type { get; }

        public Play(string playId, string name, string type)
        {
            PlayId = playId;
            Name = name;
            Type = type;
        }
    }
}
