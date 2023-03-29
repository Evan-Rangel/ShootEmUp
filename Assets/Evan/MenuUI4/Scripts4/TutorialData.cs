using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[CreateAssetMenu(fileName = "New Tutorial Data", menuName = "Tutorial Data")]

public class TutorialData : ScriptableObject
{
    [SerializeField, TextArea(0, 2)] string tutorialText;
    [SerializeField] Sprite[] tutorialsprite;

    public string TutorialText { get { return tutorialText; } }
    public Sprite[] TutorialSprite { get { return tutorialsprite; } }
}
