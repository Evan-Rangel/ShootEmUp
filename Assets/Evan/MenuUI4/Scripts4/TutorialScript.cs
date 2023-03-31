using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TutorialScript : MonoBehaviour
{
    [SerializeField] TutorialData[] tutorialData;
    [SerializeField] Text holderText;
    [SerializeField] Image holderImage;
    [SerializeField] GameObject tutorialPanel;
    [SerializeField] int triggerNumber;
    [SerializeField] PlayerInput playerInput;

    bool canChangeTutorial;
    float timer;
    [SerializeField] float maxTimer;


    private void Awake()
    {
        

        timer = maxTimer;
        canChangeTutorial=false;
        triggerNumber = -1;
        playerInput =GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().name=="UITest")
        {
            triggerNumber = tutorialData.Length;
            UpdateTutorial();
        }
        UpdateTutorial();
    }
    private void Update()
    {
        GetInputs();
    }
    private void FixedUpdate()
    {
        if (canChangeTutorial)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0)
        {
            canChangeTutorial = false;
            timer = maxTimer;
            UpdateTutorial();
        }
    }

    void GetInputs()
    {
        switch (triggerNumber)
        {
            case 0:
                if (playerInput.actions["Move"].ReadValue<Vector2>() != Vector2.zero && timer == maxTimer)
                {
                    canChangeTutorial = true;
                }
                break;
            case 1:
                if (playerInput.actions["Fire"].WasPressedThisFrame() && timer == maxTimer)
                {
                    canChangeTutorial = true;
                }
                break;
            default:
                break;
        }
    }
   
    public void UpdateTutorial()
    {
        triggerNumber++;
        if (triggerNumber>= tutorialData.Length)
        {
            StopCoroutine(Animation());
            tutorialPanel.SetActive(false);
        }
        else
        {
            StartCoroutine(Animation());
            holderText.text = tutorialData[triggerNumber].TutorialText;
        }
    }
    
    IEnumerator Animation()
    {
        if (tutorialData[triggerNumber].TutorialSprite.Length>0)
        {

            for (int i = 0; i < tutorialData[triggerNumber].TutorialSprite.Length; i++)
            {
                holderImage.sprite = tutorialData[triggerNumber].TutorialSprite[i];
                if (i== tutorialData[triggerNumber].TutorialSprite.Length-1)
                {
                    i = 0;
                }
                yield return new WaitForSeconds(0.15f);
            }
        }
        else
        {
            yield return null;
        }
    }
}
