using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

[System.Serializable]
public class CharacterOption
{
    public GameObject prefab;
    public Sprite thumbnail;
}

public class OptionsMenuManager : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Dropdown difficultyDropdown;
    public Slider volumeSlider;
    public Transform optionListContainer;
    public GameObject optionButtonPrefab;
    public Button mode2DButton;
    public Button mode3DButton;

    [Header("Assets")]
    public List<Sprite> backgroundImages;
    public List<CharacterOption> characterOptions;

    void Start()
    {
        if (difficultyDropdown != null)
        {
            difficultyDropdown.onValueChanged.AddListener(SetDifficulty);
            switch (GameSettings.Instance.difficulty)
            {
                case "Easy": difficultyDropdown.value = 0; break;
                case "Normal": difficultyDropdown.value = 1; break;
                case "Hard": difficultyDropdown.value = 2; break;
            }
        }

        if (mode2DButton != null)
            mode2DButton.onClick.AddListener(Set2DMode);

        if (mode3DButton != null)
            mode3DButton.onClick.AddListener(Set3DMode);

        if (volumeSlider != null)
            volumeSlider.onValueChanged.AddListener(SetVolume);

        if (GameSettings.Instance == null)
        {
            GameObject go = new GameObject("GameSettings");
            go.AddComponent<GameSettings>();
        }

        volumeSlider.value = GameSettings.Instance.volume;

        RefreshOptionList();
    }
    void SetDifficulty(int value)
    {
        switch (value)
        {
            case 0: GameSettings.Instance.difficulty = "Easy"; break;
            case 1: GameSettings.Instance.difficulty = "Normal"; break;
            case 2: GameSettings.Instance.difficulty = "Hard"; break;
        }

        RefreshOptionList();
    }
    void SetVolume(float value)
    {
        AudioListener.volume = value;
        GameSettings.Instance.volume = value;
    }

    public void Set2DMode()
    {
        GameSettings.Instance.is3DMode = false;
        GameSettings.Instance.selectedIndex = 0;
        RefreshOptionList();
    }

    public void Set3DMode()
    {
        GameSettings.Instance.is3DMode = true;
        GameSettings.Instance.selectedIndex = 0;
        RefreshOptionList();
    }

    void RefreshOptionList()
    {
        foreach (Transform child in optionListContainer)
            Destroy(child.gameObject);

        if (GameSettings.Instance.is3DMode)
        {
            for (int i = 0; i < characterOptions.Count; i++)
            {
                int index = i;
                GameObject btn = Instantiate(optionButtonPrefab, optionListContainer);

                Image img = btn.GetComponent<Image>();
                if (img != null && characterOptions[i].thumbnail != null)
                    img.sprite = characterOptions[i].thumbnail;

                btn.GetComponent<Button>().onClick.AddListener(() => SelectOption(index));
            }
        }
        else
        {
            for (int i = 0; i < backgroundImages.Count; i++)
            {
                int index = i;
                GameObject btn = Instantiate(optionButtonPrefab, optionListContainer);

                Image img = btn.GetComponent<Image>();
                if (img != null)
                    img.sprite = backgroundImages[i];

                btn.GetComponent<Button>().onClick.AddListener(() => SelectOption(index));
            }
        }
    }

    void SelectOption(int index)
    {
        GameSettings.Instance.selectedIndex = index;
    }

    public Sprite GetSelectedBackground() =>
        (!GameSettings.Instance.is3DMode && GameSettings.Instance.selectedIndex < backgroundImages.Count) 
            ? backgroundImages[GameSettings.Instance.selectedIndex] 
            : null;

    public GameObject GetSelectedPrefab() =>
        (GameSettings.Instance.is3DMode && GameSettings.Instance.selectedIndex < characterOptions.Count) 
            ? characterOptions[GameSettings.Instance.selectedIndex].prefab 
            : null;
}
