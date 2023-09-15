using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    public int indexToScene;
    public GameObject animFade;
    public Animator playerAnim;
    void Start()
    {
        SoundManager.Instance.PlayBGM("BGM - Regular");
        StartCoroutine(WaitingLoading());

        if(PlayerPrefs.GetInt("Character") == 0)
        {
            playerAnim.Play("Player 1 Run");
        }
        else if (PlayerPrefs.GetInt("Character") == 1)
        {
            playerAnim.Play("Player 2 Run");
        }
        else if (PlayerPrefs.GetInt("Character") == 2)
        {
            playerAnim.Play("Player 3 Run");
        }
    }

    private IEnumerator WaitingLoading()
    {
        animFade.GetComponent<Animator>().Play("fade out");
        animFade.SetActive(true);

        yield return new WaitForSeconds(4.5f);

        animFade.GetComponent<Animator>().Play("fade in");

        yield return new WaitForSeconds(1.5f);
        SoundManager.Instance.StopBGM("BGM - Regular");
        SceneManager.LoadScene(indexToScene);
    }
}
