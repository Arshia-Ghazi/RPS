using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance;

    public float volume = 1f;
    public bool is3DMode = true;
    public int selectedIndex = 0; // Index of the chosen background/prefab based on the mode we've chosen (2D/3D)
    public string difficulty = "Normal";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
