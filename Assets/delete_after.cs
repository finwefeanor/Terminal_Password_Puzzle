using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delete_after : MonoBehaviour {

    enum Day { Sun, Mon, Tue, Wed, Thu, Fri, Sat };

    void Start () 
    {
        int x = (int)Day.Sun;
        int y = (int)Day.Fri;
        Debug.Log(Day.Mon);
        Debug.Log(Day.Thu);
        Debug.Log(y);
    }
}
