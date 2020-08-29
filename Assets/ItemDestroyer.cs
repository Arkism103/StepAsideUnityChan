using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ItemDestroyer : MonoBehaviour
{
    public GameObject unitychan;
    
    // Start is called before the first frame update
    void Start()
    {

        

    }

    // Update is called once per frame
    void Update()
    {
        this.unitychan = GameObject.Find("unitychan");

        if (unitychan.transform.position.z - 10 > this.gameObject.transform.position.z)
        {
            Destroy(this.gameObject);
        }
    }
}
