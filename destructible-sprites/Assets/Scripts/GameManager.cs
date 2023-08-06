using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1)) {
            var b = Instantiate(bullet);
            b.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            b.transform.up = Vector3.zero - b.transform.position;
        }
        if(Input.GetKeyUp(KeyCode.Space)) {
            var b = Instantiate(bullet);
            b.transform.up = Quaternion.Euler(0, 0, Random.Range(0, 360)) * Vector3.up;
            b.transform.Translate(0, -3, 0);
            //var b = Instantiate(bullet);
            //b.transform.up = Quaternion.Euler(0, 0, 90) * Vector3.up;
            //b.transform.Translate(0, -3, 0);
        }
    }
}
