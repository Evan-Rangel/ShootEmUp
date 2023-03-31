using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{
    private enum PanelSelector
    {
        none,
        Principal,
        Settings,
        Credits,
        Resolution,
        Quality,
        Music,
        Volume
    }

    [SerializeField] PlayerInput playerInput;

    [Header("Panels")]
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
    [SerializeField] Button musicButton;
    [SerializeField] Button volumeButton;
    [SerializeField] Button firstResButton;
    [SerializeField] Button firstQuaButton;
   
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider volumeSlider;




    [Header("Texts")]
    public Text resolutionText;
    public Text qualityText;
    [SerializeField] Text musicText;
    [SerializeField] Text volumeText;



    PanelSelector panelSelector = PanelSelector.Principal;
    Vector2 resValue;
    public string resolution;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }
    private void Start()
    {
        volumeSlider.value = volumeSlider.maxValue;
        volumeText.text = volumeSlider.value.ToString();
        musicSlider.value = musicSlider.maxValue;
        musicText.text = musicSlider.value.ToString();

        resolution = Screen.currentResolution.ToString();
        SetQualityText();
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
                    panelSelector = PanelSelector.Principal;
                    break;
                case PanelSelector.Credits:
                    menuPanel.SetActive(true);
                    creditsPanel.SetActive(false);
                    creditsButton.Select();
                    panelSelector = PanelSelector.Principal;
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
                case PanelSelector.Music:
                    musicButton.Select();
                    panelSelector = PanelSelector.Settings;
                    musicText.text = musicSlider.value.ToString();

                    break;
                case PanelSelector.Volume:
                    volumeButton.Select();
                    panelSelector = PanelSelector.Settings;
                    volumeText.text = volumeSlider.value.ToString();

                    break;
            }
        }
    }
    void SetQualityText()
    {
        switch (QualitySettings.GetQualityLevel())
        {
            case 0:
                qualityText.text = "Very Low Quality";
                break;
            case 1:
                qualityText.text = "Low Quality";
                break;
            case 2:
                qualityText.text = "Medium Quality";
                break;
            case 3:
                qualityText.text = "High Quality";
                break;
            case 4:
                qualityText.text = "Very High Quality";
                break;
            case 5:
                qualityText.text = "Ultra Quality";
                break;
            default:
                break;
        }
    }
    public void StartGameButton()
    {
        SceneManager.LoadScene("Level1");
    }
    public void SettingsButton()
    {
        settigsPanel.SetActive(true);
        menuPanel.SetActive(false);
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
        Application.Quit();
        Debug.Log("Exit...");
    }  
    public void ResolutionButton()
    {
        resolutionPanel.SetActive(true);
        resolutionText.gameObject.SetActive(false);
        firstResButton.Select();
        panelSelector = PanelSelector.Resolution;
        Debug.Log("Resolution...");
    }
    public void QualityButton()
    {
        qualityPanel.SetActive(true);
        qualityText.gameObject.SetActive(false);
        panelSelector = PanelSelector.Quality;
        firstQuaButton.Select();
        Debug.Log("Quality...");
    }
    public void SetQuality()
    {
        QualitySettings.SetQualityLevel(int.Parse (EventSystem.current.currentSelectedGameObject.name), true);
        Debug.Log(QualitySettings.GetQualityLevel());
        qualityPanel.SetActive(false);
        qualityButton.Select();
        qualityText.gameObject.SetActive(true);
        SetQualityText();
        panelSelector = PanelSelector.Settings;

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
    public void VolumeButtonPress()
    {
        volumeSlider.Select();
        panelSelector = PanelSelector.Volume;
    }
    public void MusicButtonPress()
    {
        musicSlider.Select();
        panelSelector = PanelSelector.Music;
    }
    public void VolumeSliderMod()
    {
        volumeText.text = volumeSlider.value.ToString();
    }
    public void MusicSliderMod()
    {
        musicText.text = musicSlider.value.ToString();
    }
}
