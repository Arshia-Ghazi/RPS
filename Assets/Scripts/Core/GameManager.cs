using UnityEngine;
using UnityEngine.SceneManagement;
using RPS.Models;
using RPS.Rules;
using RPS.AI;
using RPS.UI;
using System;

namespace RPS.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Header("Game Settings")]
        public int winningScore = 5;

        private IRulesStrategy ruleStrategy;
        private IAIStrategy aiStrategy;

        private int playerScore;
        private int aiScore;

        public event Action<RoundResolvedEvent> OnRoundResolved;
        public event Action<ScoreChangedEvent> OnScoreChanged;
        public event Action<GameOverEvent> OnGameOver;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        void Start()
        {
            SetupUIManager();
            ResetGame();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "Game")
            {
                ResetGame();
                InitializeRulesAndAI();
            }
            SetupUIManager();
        }

        private void OnSceneUnloaded(Scene scene)
        {
            OnRoundResolved = null;
            OnScoreChanged = null;
            OnGameOver = null;
        }

        private void SetupUIManager()
        {
            var uiManager = FindAnyObjectByType<UIManager>();
            if (uiManager != null)
                uiManager.Setup(this);
        }


        private void InitializeRulesAndAI()
        {
            // This decides what RPS rules to use based on difficulty
            // Easy adds Flower, Hard adds Gun, Normal is classic
            string difficulty = "Normal";

            if (GameSettings.Instance != null)
                difficulty = GameSettings.Instance.difficulty;

            switch (difficulty)
            {
                case "Easy":
                    ruleStrategy = new EasyRPSRules();
                    break;
                case "Hard":
                    ruleStrategy = new HardRPSRules();
                    break;
                default:
                    ruleStrategy = new ClassicRPSRules();
                    break;
            }

            aiStrategy = new RandomAIStrategy(ruleStrategy.GetChoices());
        }

        public void PlayRound(Choice playerChoice)
        {
            Choice aiChoice = aiStrategy.GetNextMove();
            RoundOutcome outcome = ruleStrategy.GetOutcome(playerChoice, aiChoice);

            if (outcome == RoundOutcome.PlayerWin) playerScore++;
            else if (outcome == RoundOutcome.AIWin) aiScore++;

            OnRoundResolved?.Invoke(new RoundResolvedEvent
            {
                PlayerChoice = playerChoice,
                AIChoice = aiChoice,
                Outcome = outcome
            });

            OnScoreChanged?.Invoke(new ScoreChangedEvent
            {
                PlayerScore = playerScore,
                AIScore = aiScore
            });

            if (playerScore >= winningScore || aiScore >= winningScore)
            {
                bool playerWon = playerScore >= winningScore;
                OnGameOver?.Invoke(new GameOverEvent { PlayerWon = playerWon });
            }
        }

        public Choice[] GetAvailableChoices()
        {
            return ruleStrategy.GetChoices().ToArray();
        }

        private void ResetGame()
        {
            playerScore = 0;
            aiScore = 0;

            OnScoreChanged?.Invoke(new ScoreChangedEvent
            {
                PlayerScore = playerScore,
                AIScore = aiScore
            });
        }
    }
}
