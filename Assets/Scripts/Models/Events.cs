using RPS.Models;

namespace RPS.Models
{
    public struct RoundResolvedEvent
    {
        public Choice PlayerChoice;
        public Choice AIChoice;
        public RoundOutcome Outcome;
    }

    public struct ScoreChangedEvent
    {
        public int PlayerScore;
        public int AIScore;
    }

    public struct GameOverEvent
    {
        public bool PlayerWon;
    }
}
