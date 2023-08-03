namespace IsaacWebAPI.Models;

public class Game
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public bool IsComplete { get; set; }
}