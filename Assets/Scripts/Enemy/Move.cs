using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        if (h > 0) transform.localScale = new Vector3(-1, 1, 1);
        else if (h < 0) transform.localScale = new Vector3(1, 1, 1);
        transform.Translate(new Vector3(h,0,0) * Time.deltaTime);
    }
}
