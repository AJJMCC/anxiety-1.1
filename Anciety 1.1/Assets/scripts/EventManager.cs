using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {
    public static EventManager Instance;
    [SerializeField]
    private GameObject mattress;
    private float smoothing = 7f;

    public bool holdingglass = false;
    public bool holdingbox = false;

    public bool dooropen = false;
    private float timer;

    public Animator dooranim;
    void Start ()
    {
        Instance = this;
        timer = Random.Range(5,8);
    }
	

	void Update ()
    {
        opendoor();
        dooranim.SetBool("dooropen", dooropen);
    }


    public void opendoor()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            dooropen = true;
        }
        else dooropen = false;
    }

    public void Closedoor()
    {
        //reset the timer, tell the ray detect to stop showing the tooltip.
        timer = Random.Range(5, 8);
        raydetect.Instance.showbox = false;
        Debug.Log("door closed");
    }



    public void Shiftmattress()
    {
        float mattressmove = 0.1f;
        if (mattress.transform.position.x >= 0)
        { mattressmove = Random.Range(-0.02f, -0.04f); }

        else if (mattress.transform.position.x <= 0)
        { mattressmove = Random.Range(0.02f, 0.04f); }


        Vector3 newpos = new Vector3(mattressmove, mattress.transform.position.y,  mattress.transform.position.z);
        StopCoroutine("Movement");
        StartCoroutine("Movement", newpos);
        //mattress.transform.position = Vector3.Lerp(mattress.transform.position, newpos, 1f);

        Debug.Log("shifted mattress");
    }



    public void pickupglass()
    {

    }



    public void pickupbox()
    {

    }

    IEnumerator Movement(Vector3 target)
    {
        while (Vector3.Distance(mattress.transform.position, target) > 0.001f)
        {
            mattress.transform.position = Vector3.Lerp(mattress.transform.position, target, smoothing * Time.deltaTime);

            yield return null;
        }
    }
}
