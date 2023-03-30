using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class evasive : MonoBehaviour
{
    public float dodge;

    public float speed;



    public Boundary lim;
    public Vector2 startWait;
    public Vector2 manobrearTime;
    public Vector2 manobrearWait;



    private Rigidbody2D rb;
    private float target;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Evade());
    }

    private void FixedUpdate()
    {
        float newManiobra = Mathf.MoveTowards(rb.velocity.x, target, speed);
        rb.velocity = new Vector2(newManiobra, rb.velocity.y);

        rb.position = new Vector2(Mathf.Clamp(rb.position.x, lim.xMin, lim.xMax), Mathf.Clamp(rb.position.y, lim.yMin, lim.yMax));

    }

    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));
        while (true)
        {
            target = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
            yield return new WaitForSeconds(Random.Range(manobrearTime.x, manobrearTime.y));
            target = 0;
            yield return new WaitForSeconds(Random.Range(manobrearWait.x, manobrearWait.y));
        }
    }
}
