using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class blinkingcursor : MonoBehaviour {
    TextMesh txt;
    float timer = 0.5f;
	// Use this for initialization
	void Start () {
        txt = gameObject.GetComponent<TextMesh>();
        txt.text = ">";
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            if (txt.text == ">")
            {
                txt.text = ">_";
            }
            else
            {
                txt.text = ">";
            }
            timer = 0.5f;
        }
	}
}
