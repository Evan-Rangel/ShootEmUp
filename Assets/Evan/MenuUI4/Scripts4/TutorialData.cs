using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[CreateAssetMenu(fileName = "New Tutorial Data", menuName = "Tutorial Data")]

public class TutorialData : ScriptableObject
{
    //[SerializeField] UnityEditor.Animations.AnimatorController animator;
    [SerializeField] AnimationClip tutorialanimation;
    [SerializeField, TextArea(0, 2)] string tutorialText;
    [SerializeField] string tutorialName;
    [SerializeField] Sprite tutorialsprite;
    //[SerializeField] int tutorialOrder;

   // public UnityEditor.Animations.AnimatorController Animator { get { return animator; } }
    public string TutorialText { get { return tutorialText; } }
    public string TutorialName { get { return tutorialName; } }
    public Sprite TutorialSprite { get { return tutorialsprite; } }
    public AnimationClip TutorialAnimation { get { return tutorialanimation; } }
    //public int TutorialOrder { get { return tutorialOrder; } }
}
