using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    public int indexToScene;
    public GameObject animFade;
    void Start()
    {
        StartCoroutine(WaitingLoading());
    }

    private IEnumerator WaitingLoading()
    {
        animFade.GetComponent<Animator>().Play("fade out");
        animFade.SetActive(true);

        yield return new WaitForSeconds(4.5f);

        animFade.GetComponent<Animator>().Play("fade in");

        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(indexToScene);
    }
}
