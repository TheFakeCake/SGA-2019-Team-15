using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public string destinationScene;
    public string inputAxe = "Fire1";

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(inputAxe)) {
            Switch();
        }
    }

    public void Switch()
    {
        SceneManager.LoadScene(destinationScene);
    }
}
