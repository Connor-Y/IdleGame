using UnityEngine;
using System.Collections;

public class FloorExpansion : MonoBehaviour {
    // Use this for initialization
    public GameObject Cubicle;
	void Start () {
        Debug.Log("Creating a floor at " + gameObject.transform.position + new Vector3(0, 0, 2.5F));
        for (int i=0;i<3;i++)
        {
            for (int j=0;j<3;j++)
            {
                if (!(i == 0 && j == 0))
                {
                    Instantiate(Cubicle, gameObject.transform.position + new Vector3(2.5f*i, 0, 2.5f*j), gameObject.transform.rotation);
                }
            }
        }
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
