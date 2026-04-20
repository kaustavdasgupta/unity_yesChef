using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] Image fillImage; 
    [SerializeField] GameObject uiContainer;

    void Start()
    {
        HideBar();
    }

    public void UpdateProgress(float currentValue, float maxValue)
    {
        if (!uiContainer.activeSelf)
        {
            uiContainer.SetActive(true);
        }

        fillImage.fillAmount = 1f - (currentValue / maxValue);
    }

    public void HideBar()
    {
        if (uiContainer != null)
        {
            uiContainer.SetActive(false);
            fillImage.fillAmount = 1f;
        }
    }
}


