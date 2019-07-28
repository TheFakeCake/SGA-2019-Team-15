using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public string inputAxe = "Cancel";

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(inputAxe)) {
            Application.Quit();
        }
    }
}
