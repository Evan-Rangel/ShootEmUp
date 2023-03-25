using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    [Header ("From UI")]
    [SerializeField] Text scoreText;
    [SerializeField] Text levelText;
    [SerializeField] Image[] buffsImages;
    [SerializeField] Image[] playerImage;
    [SerializeField] Image lifeBar;

    [Header("From Pause Canvas")]
    [SerializeField] GameObject generalPanel;
    [SerializeField] GameObject PanelUI;
    [SerializeField] GameObject PanelPause;
    [SerializeField] PlayerInput playerInput;
    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        for (int i = 0; i < buffsImages.Length; i++)
        {
            buffsImages[i].enabled = false;
        }
    }
    private void Update()
    {
        ActivePause();
    }

    public void ActivePause()
    {
        if (playerInput.actions["Pause"].WasPressedThisFrame())
        {
            Debug.Log("Pause...");
            generalPanel.GetComponent<Animator>().SetBool("Pause", !generalPanel.GetComponent<Animator>().GetBool("Pause")) ;

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
