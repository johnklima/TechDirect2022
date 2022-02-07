using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public TriggerNextLevel trigger;

    public Animator transition;

    public float transitionTime = 1f;

    //void Update()
    // Using this update, the transision will happen when the mousebutton is pressed
    //{
    //    if(Input.GetMouseButtonDown(0))
    //    {
    //        LoadNextLevel();
    //    }
    //}

    void Update()
    {
        if (trigger.isTrigger == true)
        {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        //play animation
        transition.SetTrigger("Start");

        //wait
        yield return new WaitForSeconds(transitionTime);

        //load scene
        SceneManager.LoadScene(levelIndex);
    }
}
