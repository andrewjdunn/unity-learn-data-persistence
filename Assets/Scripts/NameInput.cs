using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameInput : MonoBehaviour
{
    CurrentPlayer player;

    public void Start()
    {
        player = GameObject.Find("DataObject").GetComponent<CurrentPlayer>();
    }

    public void OnNameChanged(string name)
    {
        player.CurrentPlayerName = name;
    }
}
