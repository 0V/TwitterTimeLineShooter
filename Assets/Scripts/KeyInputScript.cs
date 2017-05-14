using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInputScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }
    }
}
