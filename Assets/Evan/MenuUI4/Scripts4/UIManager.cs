using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] Text levelText;
    [SerializeField] Image[] buffsImages;
    [SerializeField] Image[] playerImage;
    [SerializeField] Image lifeBar;

    private void Start()
    {
        for (int i = 0; i < buffsImages.Length; i++)
        {
            buffsImages[i].enabled = false;
        }
    }
    public void ActiveImage(Image imageToPuth)
    {
        for (int i = 0; i < buffsImages.Length; i++)
        {
            if (!buffsImages[i].enabled)
            {
                buffsImages[i].enabled = true;
                buffsImages[i] = imageToPuth;
            }
        }
    }
    public void TakeDamage(float totalHealth, float currentHealth)
    {
        float amount = currentHealth / totalHealth;
        lifeBar.fillAmount = amount;
    }
    public void UpdateScore(float newScore)
    {
        scoreText.text = "SCORE: " + newScore.ToString();
    }

    public void UpdateLevel(string nameLevel)
    {
        levelText.text = nameLevel;
    }
}
