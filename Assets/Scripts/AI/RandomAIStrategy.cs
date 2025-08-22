using RPS.Models;
using UnityEngine;
using System.Collections.Generic;


namespace RPS.AI
{
    public class RandomAIStrategy : IAIStrategy
    {
        private List<Choice> availableChoices;

        public RandomAIStrategy(List<Choice> choices)
        {
            availableChoices = choices;
        }

        public Choice GetNextMove()
        {
            int index = Random.Range(0, availableChoices.Count);
            return availableChoices[index];
        }
    }
}
