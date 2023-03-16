using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class MenuController : MonoBehaviour
{
    private enum PanelSelector
    {
        none,
        Principal,
        Settings,
        Credits,
        Resolution,
        Quality
    }

    [SerializeField] PlayerInput playerInput;

    [Header("Panels")]
    [SerializeField] GameObject volumeValue;
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject settigsPanel;
    [SerializeField] GameObject creditsPanel;
    [SerializeField] GameObject resolutionPanel;
    [SerializeField] GameObject qualityPanel;

    [Header("Buttons")]
    [SerializeField] Button resolutionButton;
    [SerializeField] Button qualityButton;
    [SerializeField] Button settingsButton;
    [SerializeField] Button creditsButton;

    [SerializeField] Button firstResButton;


    [Header("Texts")]
    public  Text resolutionText;

    PanelSelector panelSelector= PanelSelector.Principal;
    GameObject previousPanel;
    GameObject currentPanel;
    Vector2 resValue;
    public string resolution;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }
    private void Start()
    {
        resolution = Screen.currentResolution.ToString();
    }
    private void Update()
    {
        if (playerInput.actions["Cancel"].WasPressedThisFrame())
        {
            switch (panelSelector)
            {
                case PanelSelector.Settings:
                    menuPanel.SetActive(true);
                    settigsPanel.SetActive(false);
                    settingsButton.Select();
                    break;
                case PanelSelector.Credits:
                    menuPanel.SetActive(true);
                    creditsPanel.SetActive(false);
                    creditsButton.Select();
                    break;
                case PanelSelector.Resolution:
                    resolutionPanel.SetActive(false);
                    resolutionButton.Select();
                    panelSelector = PanelSelector.Settings;
                    break;
                case PanelSelector.Quality:
                    qualityPanel.SetActive(false);
                    qualityButton.Select();
                    panelSelector = PanelSelector.Settings;
                    break;
            }
        }
    }

    public void StartGameButton()
    {
        Debug.Log("Loading Game...");
    }
    public void SettingsButton()
    {
        settigsPanel.SetActive(true);
        menuPanel.SetActive(false);
        previousPanel = menuPanel;
        currentPanel = settigsPanel;
        resolutionButton.Select();
        resolutionText.text = resolution;
        panelSelector = PanelSelector.Settings;
        Debug.Log("Settings...");
    }
    public void CreditsButton()
    {
        Debug.Log("Credits...");
    }
   
    public void ExitButton()
    {
        Debug.Log("Exit...");
    }  
    
    public void ResolutionButton()
    {
        resolutionPanel.SetActive(true);
        previousPanel = settigsPanel;
        currentPanel = resolutionPanel;
        resolutionText.gameObject.SetActive(false);
        firstResButton.Select();
        panelSelector = PanelSelector.Resolution;
        Debug.Log("Resolution...");
    }
    public void QualityButton()
    {
    }
    public void Resolution01()
    {
        resValue = new Vector2(854, 480);
        SetResolution();
    }
    public void Resolution02()
    {
        resValue = new Vector2(1280, 720);
        SetResolution();
    }
    public void Resolution03()
    {
        resValue = new Vector2(1920, 1080);
        SetResolution();
    }
    public void Resolution04()
    {
        resValue = new Vector2(2560, 1440);
        SetResolution();
    }
    public void Resolution05()
    {
        resValue = new Vector2(3840, 2160);
        SetResolution();
    }
    public void SetResolution()
    {
        Debug.Log(resValue);
        Screen.SetResolution((int)resValue.x, (int)resValue.y, false);
        resolutionPanel.SetActive(false);
        resolutionButton.Select();
        resolutionText.gameObject.SetActive(true);
        resolution = Screen.currentResolution.ToString();
        resolutionText.text = resolution;
        panelSelector = PanelSelector.Settings;
    }
}
