using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidLogs : MonoBehaviour
{
    private int i = 0;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {   
            if(i == 0)
            {
                Debug.Log("Click Log");
            }
            else if(i == 1)
            {   
                Debug.LogWarning("Click Warning");
            }
            else if(i == 2)
            {
                Debug.LogError("Click Error");
            }
            else
            {
                Debug.Assert(false, "Assert");
            }

            i++;

            i %= 4;
        }
    }
}
