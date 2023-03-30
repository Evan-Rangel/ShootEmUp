using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class TutorialScript : MonoBehaviour
{
    [SerializeField] TutorialData[] tutorialData;

    [SerializeField] Text holderText;

    [SerializeField] Image holderImage;
    [SerializeField] GameObject tutorialPanel;

    [SerializeField] int triggerNumber;
    [SerializeField] PlayerInput playerInput;
    //[SerializeField] PlayerInput playerInput;

    bool canChangeTutorial;
    float timer;
    float maxTimer;

    private void Awake()
    {
        maxTimer = 2;
        canChangeTutorial=true;
        triggerNumber = -1;
        playerInput =GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
    }
    private void Start()
    {
        UpdateTutorial();
    }
    private void Update()
    {
        GetInputs();
    }

    void GetInputs()
    {
        Debug.Log(playerInput.actions["Fire"].WasPressedThisFrame());

        switch (triggerNumber)
        {
            case 0:
                if (playerInput.actions["Move"].ReadValue<Vector2>() != Vector2.zero && timer == 0)
                {
                    UpdateTutorial();
                    timer = 2;
                }
                break;
            case 1:
                if (playerInput.actions["Fire"].WasPressedThisFrame() && timer == 0)
                {
                    UpdateTutorial();

                    timer = 2;
                }
                break;
            default:
                break;
        }

        if (timer<=maxTimer && timer>=0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = 0;
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


}
