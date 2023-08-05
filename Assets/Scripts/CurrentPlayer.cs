using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentPlayer : MonoBehaviour
{
    public delegate void CurrentPlayerUpdate(CurrentPlayer sender, EventArgs e);

    private string _currentPlayerName;
    public event CurrentPlayerUpdate CurrentPlayerUpdated;
    
    public string CurrentPlayerName
    {
        set 
        { 
            _currentPlayerName = value; 
            if(CurrentPlayerUpdated != null)
            {
                CurrentPlayerUpdated.Invoke(this, EventArgs.Empty);
            }
        }
        get 
        { 
            return _currentPlayerName; 
        }
    }
}
