using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RPS.Models;
using RPS.Core;
using System.Collections;
using UnityEngine.SceneManagement;

namespace RPS.UI
{
    public class UIManager : MonoBehaviour
    {
        [Header("Round History")]
        public Transform roundHistoryContent;
        public GameObject roundHistoryEntryPrefab;

        private int roundCounter = 0;

        [Header("UI References")]
        public Transform buttonContainer;
        public Transform PlayerChoiceVisual, AIChoiceVisual, GameOverPopup, GameOverBlocker;
        public GameObject buttonPrefab;
        public TMP_Text scoreText;
        public TMP_Text PlayerScore;
        public TMP_Text AIScore;
        public OpponentAnimatorController opponentAnimatorController;

        private GameManager gameManager;

        public void Setup(GameManager manager)
        {
            gameManager = manager;

            gameManager.OnRoundResolved -= HandleRoundResolved;
            gameManager.OnScoreChanged -= HandleScoreChanged;
            gameManager.OnGameOver -= HandleGameOver;

            gameManager.OnRoundResolved += HandleRoundResolved;
            gameManager.OnScoreChanged += HandleScoreChanged;
            gameManager.OnGameOver += HandleGameOver;

            GenerateChoiceButtons(gameManager.GetAvailableChoices());
            ResetRoundHistory();
        }

        void GenerateChoiceButtons(Choice[] choices)
        {
            foreach (Transform child in buttonContainer)
                Destroy(child.gameObject);

            foreach (Choice choice in choices)
            {
                GameObject btnObj = Instantiate(buttonPrefab, buttonContainer);
                Button button = btnObj.GetComponent<Button>();
                TMP_Text btnText = btnObj.GetComponentInChildren<TMP_Text>();
                btnText.text = choice.ToString();
                button.onClick.AddListener(() => gameManager.PlayRound(choice));
            }
        }

        void HandleRoundResolved(RoundResolvedEvent e)
        {
            AddRoundHistoryEntry(e.PlayerChoice, e.AIChoice, e.Outcome);
            StartCoroutine(PlayChoicesWithAnimation(e.PlayerChoice, e.AIChoice));

            if (opponentAnimatorController != null)
                StartCoroutine(PlayAIAnimationSequence(e.AIChoice, e.Outcome));
        }

        public void AddRoundHistoryEntry(Choice playerChoice, Choice aiChoice, RoundOutcome outcome)
        {
            if (roundHistoryEntryPrefab == null || roundHistoryContent == null) return;

            roundCounter++;

            GameObject entryObj = Instantiate(roundHistoryEntryPrefab, roundHistoryContent);
            entryObj.SetActive(true);

            TMP_Text entryText = entryObj.GetComponentInChildren<TMP_Text>();
            if (entryText != null)
            {
                entryText.text = $"Round {roundCounter}: You: {playerChoice} | AI: {aiChoice} → {outcome}";
            }

            Canvas.ForceUpdateCanvases();
            ScrollRect scrollRect = roundHistoryContent.GetComponentInParent<ScrollRect>();
            if (scrollRect != null)
            {
                scrollRect.verticalNormalizedPosition = 0f;
            }
        }
        public void ResetRoundHistory()
        {
            roundCounter = 0;

            foreach (Transform child in roundHistoryContent)
            {
                Destroy(child.gameObject);
            }
        }

        IEnumerator PlayChoicesWithAnimation(Choice playerChoice, Choice aiChoice)
        {
            SetButtonsInteractable(false);

            IEnumerator playerAnim = ShowChoiceWithEffect(playerChoice, PlayerChoiceVisual);
            IEnumerator aiAnim = ShowChoiceWithEffect(aiChoice, AIChoiceVisual);

            yield return StartCoroutine(RunSimultaneously(playerAnim, aiAnim));

            SetButtonsInteractable(true);
        }

        IEnumerator RunSimultaneously(IEnumerator first, IEnumerator second)
        {
            Coroutine c1 = StartCoroutine(first);
            Coroutine c2 = StartCoroutine(second);
            yield return c1;
            yield return c2;
        }

        void HandleScoreChanged(ScoreChangedEvent e)
        {
            PlayerScore.text = e.PlayerScore.ToString();
            AIScore.text = e.AIScore.ToString();
        }

        void HandleGameOver(GameOverEvent e)
        {
            GameOverPopup.gameObject.SetActive(true);
            GameOverBlocker.gameObject.SetActive(true);
            TMP_Text GameResults = GameOverPopup.GetComponentInChildren<TMP_Text>();
            GameResults.text = e.PlayerWon ? "You Won the Game!" : "AI Won the Game!";
        }

        IEnumerator ShowChoiceWithEffect(Choice choice, Transform parent)
        {
            parent.gameObject.SetActive(true);
            foreach (Transform child in parent)
                child.gameObject.SetActive(choice.ToString() == child.tag);

            Animator animator = parent.GetComponent<Animator>();
            if (animator != null)
            {
                animator.enabled = true;
                yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
            }

            foreach (Transform child in parent)
                child.gameObject.SetActive(false);
        }

        void SetButtonsInteractable(bool interactable)
        {
            foreach (Transform child in buttonContainer)
            {
                Button button = child.GetComponent<Button>();
                if (button != null)
                    button.interactable = interactable;
            }
        }

        public void GotoMainMenu()
        {
            SceneManager.LoadScene("Menu");
        }

        public void Retry()
        {
            SceneManager.LoadScene("Game");
        }
        IEnumerator PlayAIAnimationSequence(Choice aiChoice, RoundOutcome outcome)
        {
            //SetButtonsInteractable(false);

            yield return opponentAnimatorController.PlayAIAnimation(aiChoice, outcome);

            //SetButtonsInteractable(true);
        }
    }
}
