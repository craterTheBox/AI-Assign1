/**************************************************
*   AI for Games - Assignment 1
*
*   Carter Menary, 100700587
*   2022-02-06
**************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnyPickup : MonoBehaviour
{
    [SerializeField] float spinSpeed = 0.4f;

    void Update()
    {
        transform.Rotate(0.0f, spinSpeed, 0.0f); //this just rotates the object to juice it up
    }
}
