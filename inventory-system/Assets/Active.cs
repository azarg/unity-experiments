using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active : MonoBehaviour
{
    public GameObject inactive;

    // Start is called before the first frame update
    void Start()
    {
        inactive.GetComponent<Inactive>().Initialize();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
