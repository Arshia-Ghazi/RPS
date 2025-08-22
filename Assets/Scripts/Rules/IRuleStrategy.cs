using RPS.Models;
using System.Collections.Generic;


namespace RPS.Rules
{
    public interface IRulesStrategy
    {
        List<Choice> GetChoices();
        RoundOutcome GetOutcome(Choice player, Choice ai);
    }

}
