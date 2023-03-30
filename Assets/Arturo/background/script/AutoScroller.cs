using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScroller : MonoBehaviour
{
    public float scrollSpeed;
    [SerializeField] private MeshRenderer mesh;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = new Vector2(0, Time.time * scrollSpeed);
        mesh.material.mainTextureOffset = offset;
    }
}
