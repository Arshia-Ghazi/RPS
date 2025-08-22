using System.Collections;
using UnityEngine;
using RPS.Models;

namespace RPS.Core
{
    public class OpponentAnimatorController : MonoBehaviour
    {
        private Animator animator;

        public IEnumerator PlayAIAnimation(Choice aiChoice, RoundOutcome outcome)
        {
            animator = GetComponentInChildren<Animator>();
            if (animator == null)
                yield break;
            animator.SetTrigger(aiChoice.ToString());

            if (outcome == RoundOutcome.AIWin)
                animator.SetTrigger("Win");
            else if (outcome == RoundOutcome.PlayerWin)
                animator.SetTrigger("Lose");
            else if (outcome == RoundOutcome.Draw)
                animator.SetTrigger("Draw");

            yield return WaitForCurrentAnimation();
        }

        private IEnumerator WaitForCurrentAnimation()
        {
            yield return null;

            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            float timer = 0f;

            while (timer < stateInfo.length)
            {
                timer += Time.deltaTime;
                yield return null;
            }
        }
    }
}
