using TMPro;
using UnityEngine;

public class ProgressDisplay : MonoBehaviour
{
    public TMP_Text progressText; // 진행도 표시 텍스트

    void Start()
    {
        if (progressText == null)
        {
            Debug.LogError("Progress Text is not assigned!");
        }
    }

    void Update()
    {
        if (progressText != null)
        {
            float progressPercentage = (float)MapManager.Instance.CurrentMapTrash.Count / MapManager.Instance.Map.GetComponent<GarbageSpawner>().GarbageCount * 100f;
            progressText.text = "진행도: " + progressPercentage.ToString("F1") + "%";
        }
    }
}
