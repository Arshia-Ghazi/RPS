using RPS.Models;
using System.Collections.Generic;

namespace RPS.Rules
{
    public class HardRPSRules : IRulesStrategy
    {
        public List<Choice> GetChoices()
        {
            return new List<Choice> { Choice.Rock, Choice.Paper, Choice.Scissor, Choice.Gun };
        }

        public RoundOutcome GetOutcome(Choice playerChoice, Choice aiChoice)
        {

            // Gun always makes AI win
            if (playerChoice == Choice.Gun || aiChoice == Choice.Gun)
                return RoundOutcome.AIWin;

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
