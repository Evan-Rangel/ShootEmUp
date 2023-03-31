using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Movement Data", menuName = "Movement Data")]
public class MovementData : ScriptableObject
{

    [SerializeField] float velocity;
    [SerializeField] Vector2 direction;
    [SerializeField] int[] typeMovement;
    [SerializeField] float[] timeMovement;

    public int[] TypeMovemet {get{return typeMovement;}}
    public float[] TimeMovemet { get { return timeMovement; } }
    public float Velocity {get{return velocity;}}
}