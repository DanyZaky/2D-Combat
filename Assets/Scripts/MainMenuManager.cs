using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private int currentCharacter;
    public Animator characterAnim;
    public Button[] CharactersButton;

    public Button[] LevelsButton;

    void Start()
    {
        currentCharacter = PlayerPrefs.GetInt("Character", 0);
        CharacterSelected(currentCharacter);

        int currentProgress = PlayerPrefs.GetInt("Progress Level", 1);

        for (int i = 0; i < LevelsButton.Length; i++)
        {
            LevelsButton[i].interactable = false;
        }

        for (int i = 0; i < currentProgress; i++)
        {
            LevelsButton[i].interactable = true;
        }

        SoundManager.Instance.PlayBGM("BGM - MainMenu");
    }

    void Update()
    {
        CheckingCharacterSwitch();
    }

    private void CheckingCharacterSwitch()
    {
        if (currentCharacter == 0)
        {
            characterAnim.Play("chara 1");
        }
        else if (currentCharacter == 1)
        {
            characterAnim.Play("chara 2");
        }
        else if (currentCharacter == 2)
        {
            characterAnim.Play("chara 3");
        }
    }

    public void CharacterSelected(int index)
    {
        for (int i = 0; i < CharactersButton.Length; i++)
        {
            CharactersButton[i].interactable = true;
        }

        CharactersButton[index].interactable = false;

        currentCharacter = index;
        SoundManager.Instance.PlaySFX("SFX - Button");
        PlayerPrefs.SetInt("Character", index);
    }

    public void LoadToLevel(string name)
    {
        SceneManager.LoadScene(name);
        SoundManager.Instance.PlaySFX("SFX - Button");
        SoundManager.Instance.StopBGM("BGM - MainMenu");
    }

    public void AudioButton()
    {
        SoundManager.Instance.PlaySFX("SFX - Button");
    }
}
