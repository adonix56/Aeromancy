using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sway : MonoBehaviour
{
    private const string START = "Start";
    private const string BOUNCE = "Bounce";

    [SerializeField] private bool bouncable;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = Random.Range(0.8f, 1.2f);
        StartCoroutine(WaitToStart());
        if (bouncable) StartCoroutine(WaitToBounce());
    }

    IEnumerator WaitToStart() {
        float timeToBounce = Time.time + Random.Range(0f, 0.5f);
        yield return new WaitUntil(() => Check(timeToBounce));
        anim.SetTrigger(START);
    }

    IEnumerator WaitToBounce() {
        while (true) {
            float timeToBounce = Time.time + Random.Range(2f, 3f);
            yield return new WaitUntil(() => Check(timeToBounce));
            anim.SetTrigger(BOUNCE);
        }
    }

    private bool Check(float timeToBounce) {
        return (Time.time > timeToBounce); 
    }


    // Update is called once per frame
    private void Update() {
        
    }
}
