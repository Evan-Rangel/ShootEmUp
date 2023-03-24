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
    [SerializeField] GameObject PanelUI;
    [SerializeField] GameObject PanelPause;
    [SerializeField] PlayerInput playerInput;

    [SerializeField] Button backToGameBTN;
    bool pause=false;
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
            pause = !pause;
            Debug.Log("Pause...");
            PanelUI.GetComponent<Animator>().SetBool("Pause", !PanelUI.GetComponent<Animator>().GetBool("Pause"));
            PanelPause.GetComponent<Animator>().enabled = true;

            if (!pause)
            {
                PanelPause.GetComponent<Animator>().Play("PausePaneOutAnim");
            }
            else
            {
                PanelPause.GetComponent<Animator>().Play("PausePanelInAnim");
                backToGameBTN.Select();
            }
            PanelPause.GetComponent<Animator>().SetBool("Pause", !pause);

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
