using RPS.UI;
using UnityEngine;

public class ChoiceAnimationHandler : MonoBehaviour
{
    public void HideAfterAnimation()
    {
        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.SetActive(false);
        }

        gameObject.SetActive(false);
    }
}
