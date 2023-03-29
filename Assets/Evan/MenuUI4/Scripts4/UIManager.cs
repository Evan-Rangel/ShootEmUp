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
    [SerializeField] GameObject panelUI;
    [SerializeField] GameObject panelPause;
    [SerializeField] GameObject leftPanel;
    [SerializeField] GameObject rightPanel;

    [SerializeField] PlayerInput playerInput;
    [SerializeField] Button backToGameButton;

    [SerializeField] Transform leftPanelPosTarget;
    [SerializeField] Transform rightPanelPosTarget;
    [SerializeField] Transform PausePanelPosTarget;



    [SerializeField] Vector3 leftPanelPos;
    [SerializeField] Vector3 rightPanelPos;
    [SerializeField] Vector3 PausetPanelPos;


    bool isPause;

    public static UIManager instance;
    public static UIManager Instance { get { return instance; } }
    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        playerInput =gameObject.GetComponent<PlayerInput>();

    }
    private void Start()
    {
        isPause = true;
        rightPanelPos = rightPanel.transform.position;
        leftPanelPos = leftPanel.transform.position;
        PausetPanelPos = panelPause.transform.position;
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
            if (isPause)
            {
                panelPause.transform.LeanScale(Vector3.one, 0.2f);
                panelPause.transform.LeanMove(PausePanelPosTarget.position,0.2f);
                leftPanel.transform.LeanMove(leftPanelPosTarget.position, 0.2f);
                rightPanel.transform.LeanMove(rightPanelPosTarget.position, 0.2f);

            }
            else
            {
                panelPause.transform.LeanMove(PausetPanelPos,0.2f);
                panelPause.transform.LeanScale(Vector3.zero, 0.2f);
                leftPanel.transform.LeanMove(leftPanelPos, 0.2f);
                rightPanel.transform.LeanMove(rightPanelPos, 0.2f);
            }
            isPause = !isPause;
            backToGameButton.Select();
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
