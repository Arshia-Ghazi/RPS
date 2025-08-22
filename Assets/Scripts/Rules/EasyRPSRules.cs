using RPS.Models;
using System.Collections.Generic;

namespace RPS.Rules
{
    public class EasyRPSRules : IRulesStrategy
    {
        public List<Choice> GetChoices()
        {
            return new List<Choice> { Choice.Rock, Choice.Paper, Choice.Scissor, Choice.Flower };
        }

        public RoundOutcome GetOutcome(Choice playerChoice, Choice aiChoice)
        {
            // Flower always wins
            if (playerChoice == Choice.Flower || aiChoice == Choice.Flower)
                return RoundOutcome.PlayerWin;

            if (playerChoice == aiChoice)
                return RoundOutcome.Draw;

            if ((playerChoice == Choice.Rock && aiChoice == Choice.Scissor) ||
                (playerChoice == Choice.Scissor && aiChoice == Choice.Paper) ||
                (playerChoice == Choice.Paper && aiChoice == Choice.Rock))
            {
                return RoundOutcome.PlayerWin;
            }

            return RoundOutcome.AIWin;
        }
    }
}
