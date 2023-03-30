using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    [Header("From UI")]
    [SerializeField] Text scoreText;
    [SerializeField] Text levelText;
    [SerializeField] Image[] buffsImages;
    [SerializeField] Image[] playerImage;
    [SerializeField] List <Image> hearts;
    [SerializeField] Sprite heartImage;
    [SerializeField] Vector2 posImages;
    [SerializeField]Image rem;

    [Header("From Pause Canvas")]
    [SerializeField] GameObject generalPanel;
    [SerializeField] GameObject panelUI;
    [SerializeField] GameObject panelPause;
    [SerializeField] GameObject leftPanel;
    [SerializeField] GameObject rightPanel;
    [SerializeField] GameObject healthPanel;

    [Header ("Win Lose Screens")]
    [SerializeField] GameObject loseScreen;
    [SerializeField] GameObject winScreen;

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
        playerInput = gameObject.GetComponent<PlayerInput>();
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
        SetHeats(3);
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
                //Time.timeScale = 0;
                panelPause.transform.LeanScale(Vector3.one, 0.2f);
                panelPause.transform.LeanMove(PausePanelPosTarget.position,0.2f);
                leftPanel.transform.LeanMove(leftPanelPosTarget.position, 0.2f);
                rightPanel.transform.LeanMove(rightPanelPosTarget.position, 0.2f);
                backToGameButton.Select();
                StartCoroutine(SetTimeScale());
            }
            else
            {
                Time.timeScale = 1;

                panelPause.transform.LeanMove(PausetPanelPos,0.2f);
                panelPause.transform.LeanScale(Vector3.zero, 0.2f);
                leftPanel.transform.LeanMove(leftPanelPos, 0.2f);
                rightPanel.transform.LeanMove(rightPanelPos, 0.2f);
            }
            isPause = !isPause;
        }
    }
    IEnumerator SetTimeScale()
    {
        yield return new WaitForSeconds(0.2f);
        Time.timeScale = 0;
        StopCoroutine(SetTimeScale());

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
    public void SetHeats(int healt)
    {
        for (int i = 0; i < healt; i++)
        {
            hearts.Add(Instantiate(rem, healthPanel.transform));
            hearts[i].GetComponent<Image>().enabled = true ;

            hearts[i].sprite = heartImage;
            hearts[i].rectTransform.anchoredPosition = posImages;
            posImages.x += 50;
        }   
    }

    public void SetDamae(int health)
    {
        if (health<=0)
        {
            LoseScreen();
        }
        for (int i = 0; i < health; i++)
        {
            hearts[i].gameObject.SetActive(false);
        }
    }

    private void LoseScreen()
    {
        loseScreen.SetActive(true);
        Time.timeScale = 0;
    }
    private void WinScreen()
    {
        winScreen.SetActive(true);
        Time.timeScale = 0;
    }
    public void UpdateScore(float newScore)
    {

        scoreText.text = newScore.ToString();
    }

    public void UpdateLevel(int nameLevel)
    {
        levelText.text = nameLevel.ToString();
    }
}
