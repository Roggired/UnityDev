using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : MonoBehaviour
{
    public GameObject character;


    public void DoOnClick()
    {
        character.GetComponent<CharacterMoveController>().Restart();
    }
}
