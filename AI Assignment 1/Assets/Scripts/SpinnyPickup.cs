using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnyPickup : MonoBehaviour
{
    [SerializeField] float spinSpeed = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f, spinSpeed, 0.0f);

        //if collides with player, do something
    }
}
