using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    [SerializeField] TutorialData[] tutorialData;

    [SerializeField] string[] collisionTags;
    string currentTag;
    
    [SerializeField, TextArea(1,10)] string[] tutorialText;
    [SerializeField] Text holderText;

    [SerializeField] Sprite[] tutorialImages;
    [SerializeField] Image holderImage;
    int triggerNumber;



    private void Awake()
    {
        triggerNumber = 0;
    }
    private void Start()
    {
        UpdateTutorial();
    }
    public void UpdateTutorial()
    {
        holderImage.sprite = tutorialData[triggerNumber].TutorialSprite;
       // holderImage.gameObject.GetComponent<Animation>().AddClip(tutorialData[triggerNumber].TutorialAnimation, tutorialData[triggerNumber].TutorialName);// = tutorialData[triggerNumber].TutorialAnimation;
       // holderImage.gameObject.GetComponent<Animation>().Play();
        holderText.text = tutorialData[triggerNumber].TutorialText;
        triggerNumber++;
        /*
        holderImage.sprite = tutorialImages[triggerNumber];
        holderText.text = tutorialText[triggerNumber];
        currentTag = collisionTags[triggerNumber];
        triggerNumber++;*/
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(currentTag))
        {
            UpdateTutorial();
        }
    }

}
