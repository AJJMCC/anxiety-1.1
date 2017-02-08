using UnityEngine;
using System.Collections;

public class raydetect : MonoBehaviour {

    public static raydetect Instance;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private Texture2D image;


    private float lookrange = 2f;


    private Rect rect = new Rect(Screen.width / 2, Screen.height / 2, 15, 10);
    private string message;
    public bool showbox = false;

    void Start ()
    {
        Cursor.lockState = CursorLockMode.Locked;
         Instance = this;
        message = "demo text";
	}
	
	void Update ()
    {
        HitObject();
        //Debug.Log(HitObject());

        objectChecks(HitObject());

    }

    string HitObject()
    {
        //raycast forward from camera to detect if we are close enough to an object to interact with it
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, lookrange))
        {
            return hit.collider.name.ToString();
        }
        else return null;
    }

    void objectChecks(string name)
    {
        //first check what we are looking at, then designate the GUI box and what happens if the player clicks. 
        if (name == "door")
        {

            if (EventManager.Instance.dooropen == true)
            {
                showbox = true;
                message = "close door";
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("clicked mouse");
                    EventManager.Instance.Closedoor();
                }
            }
           // else showbox = false;
        }

        else if (name == "box")
        {
            showbox = true;

        }

        else if (name == "glass")
        {
            
            showbox = true;
        }

        else if (name == "mattress")
        {
            showbox = true;
            message = "shift mattress";
            if (Input.GetMouseButtonDown(0))
            {
                EventManager.Instance.Shiftmattress();
            }
        }
        else { showbox = false; }

    }

    private void OnGUI()
    {
        //place cursor in centre
        GUI.DrawTexture(rect, image, ScaleMode.StretchToFill);
        if (showbox)
        { GUI.Box(new Rect((Screen.width / 2) + 25, (Screen.height / 2) - 5, 100, 30), message); }

    }
}