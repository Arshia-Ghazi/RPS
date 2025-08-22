using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameInitializer : MonoBehaviour
{
    [Header("3D Setup")]
    public GameObject environment3D;
    public Transform aiSpawnPoint;
    public List<GameObject> characterPrefabs;

    [Header("2D Setup")]
    public GameObject backgroundPanel;
    public List<Sprite> backgroundImages;
    public Image backgroundRenderer;

    private void Start()
    {
        ApplySelection();
    }

    void ApplySelection()
    {
        AudioListener.volume = GameSettings.Instance.volume;
        if (GameSettings.Instance.is3DMode)
        {
            environment3D.SetActive(true);
            backgroundPanel.SetActive(false);

            int index = GameSettings.Instance.selectedIndex;
            if (index >= 0 && index < characterPrefabs.Count)
            {
                Instantiate(characterPrefabs[index], aiSpawnPoint.position, aiSpawnPoint.rotation, aiSpawnPoint);
            }
        }
        else
        {
            environment3D.SetActive(false);
            backgroundPanel.SetActive(true);

            int index = GameSettings.Instance.selectedIndex;
            if (index >= 0 && index < backgroundImages.Count)
            {
                backgroundRenderer.sprite = backgroundImages[index];
            }
        }
    }
}
