using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
    [SerializeField] GameObject creditsPanel;
    [SerializeField] GameObject creditsPanelAnim;
    [SerializeField] GameObject creditsTargetAnim;
    [SerializeField] Text finalLoseScoreText;
    [SerializeField] Text finalWinScoreText;

    [SerializeField] PlayerInput playerInput;

    [SerializeField] Button backToGameButton;
    [SerializeField] Button retryButton;
    [SerializeField] Transform leftPanelPosTarget;
    [SerializeField] Transform rightPanelPosTarget;
    [SerializeField] Transform PausePanelPosTarget;

    [SerializeField] Vector3 leftPanelPos;
    [SerializeField] Vector3 rightPanelPos;
    [SerializeField] Vector3 PausetPanelPos;

    public int totalScore=0;



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
        playerInput.enabled = false;
        levelText.text = SceneManager.GetActiveScene().name;
    }
    private void Start()
    {
        Time.timeScale = 1;
        isPause = true;
        rightPanelPos = rightPanel.transform.position;
        leftPanelPos = leftPanel.transform.position;
        PausetPanelPos = panelPause.transform.position;
        for (int i = 0; i < buffsImages.Length; i++)
        {
            buffsImages[i].enabled = false;
        }
        //SetHeats(3);
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
                backToGameButton.gameObject.SetActive(true);
                backToGameButton.Select();

                StartCoroutine(SetTimeScale());
            }
            else
            {

                backToGameButton.Select();
                backToGameButton.gameObject.SetActive(false);
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
        yield return new WaitForSeconds(0.25f);
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
            hearts[i].gameObject.SetActive(true);
            hearts[i].GetComponent<Image>().enabled = true ;
            hearts[i].GetComponent<Image>().sprite = heartImage;
            hearts[i].rectTransform.anchoredPosition = posImages;
            posImages.x += 50;
        }   
    }
    public void TakeDamage()
    {
        hearts.RemoveAt(hearts.Count-1);
    }

    public void SetDamae(int health, int score)
    {
        if (health<=0)
        {
            LoseScreen();
            finalLoseScoreText.text = score.ToString();
        }
        for (int i = 0; i < health; i++)
        {
            hearts[i].gameObject.SetActive(false);
        }
    }

    public void LoseScreen()
    {
        loseScreen.SetActive(true);
        retryButton.Select();
        Time.timeScale = 0;
    }
    public void WinScreen()
    {
        if (SceneManager.GetActiveScene().name == "UITest")
        {
            /*Time.timeScale = 1;
            winScreen.SetActive(true);
            finalWinScoreText.text = totalScore.ToString();
            StartCoroutine(DelayCredits());*/
        }
        else
        {
            StartCoroutine(UltimoNivel());
        }
    }

    public void WinScreen2()
    {
        Time.timeScale = 1;
        winScreen.SetActive(true);
        finalWinScoreText.text = totalScore.ToString();
        StartCoroutine(DelayCredits());
    }

    IEnumerator UltimoNivel()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("UITest");
    }
    IEnumerator DelayCredits()
    {
        yield return new WaitForSeconds(1);
        creditsPanelAnim.transform.LeanMove(creditsTargetAnim.transform.position, 20);
    }
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");

    }
    public void BackToGame()
    {
        backToGameButton.Select();
        backToGameButton.gameObject.SetActive(false);

        Time.timeScale = 1;

        panelPause.transform.LeanMove(PausetPanelPos, 0.2f);
        panelPause.transform.LeanScale(Vector3.zero, 0.2f);
        leftPanel.transform.LeanMove(leftPanelPos, 0.2f);
        rightPanel.transform.LeanMove(rightPanelPos, 0.2f);
        isPause = !isPause;
    }
    public void UpdateScore(int newScore)
    {
        if (totalScore>=800)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player117>().shotLevel = 3;
            buffsImages[1].gameObject.SetActive(true);

        }
        else
        if (totalScore >= 400)
        {
            buffsImages[0].gameObject.SetActive(true);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player117>().shotLevel=2;
        }
        totalScore += newScore;
        scoreText.text = totalScore.ToString();
    }

    public void UpdateLevel(int nameLevel)
    {
        levelText.text = nameLevel.ToString();
    }
}
