using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    private int currentCharacter;
    public Animator characterAnim;
    public Button[] CharactersButton;
    void Start()
    {
        currentCharacter = PlayerPrefs.GetInt("Character", 0);
        CharacterSelected(currentCharacter);
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
        PlayerPrefs.SetInt("Character", index);
    }
}
