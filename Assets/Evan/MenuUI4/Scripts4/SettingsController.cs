using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Animations;
public class SettingsController : MonoBehaviour
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
}
