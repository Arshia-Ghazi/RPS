using RPS.Models;
using System.Collections.Generic;

namespace RPS.Rules
{
    public class ClassicRPSRules : IRulesStrategy
    {
        public List<Choice> GetChoices()
        {
            return new List<Choice> { Choice.Rock, Choice.Paper, Choice.Scissor };
        }

        public RoundOutcome GetOutcome(Choice playerChoice, Choice aiChoice)
        {
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
