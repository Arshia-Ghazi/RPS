using RPS.Models;

namespace RPS.AI
{
    public interface IAIStrategy
    {
        Choice GetNextMove();
    }
}
