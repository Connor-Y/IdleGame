using UnityEngine;
using System.Collections;

public class UpgradeController : MonoBehaviour {


    private StatTracker stats;
    // Use this for initialization
    void Awake()
    {
        stats = StatTracker.Instance;
    }


    
}
