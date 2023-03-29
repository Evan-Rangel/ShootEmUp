using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    [SerializeField] TutorialData[] tutorialData;

    string currentTag;
    
    [SerializeField] Text holderText;

    [SerializeField] Image holderImage;
    [SerializeField] bool change;
    [SerializeField] GameObject tutorialPanel;

    int triggerNumber;

    


    private void Awake()
    {
        change = false;
        triggerNumber = -1;
    }
    private void Start()
    {
        UpdateTutorial();
    }
    private void Update()
    {
        if (change==true)
        {
            change = false;
            UpdateTutorial();
        }
    }
    public void UpdateTutorial()
    {
        triggerNumber++;
        if (triggerNumber>= tutorialData.Length)
        {
            StopCoroutine(Animation());
            tutorialPanel.SetActive(false);
            Debug.Log("parando");
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(currentTag))
        {
            UpdateTutorial();
        }
    }

}
