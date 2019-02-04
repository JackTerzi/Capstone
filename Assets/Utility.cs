using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Utility : MonoBehaviour {

    public static bool IsDefined(GameObject obj){
        if(obj != null){
            return true;
        }else{
            return false;
        }
    }
    public static bool IsDefined(string obj)
    {
        if (obj != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool IsDefined(Transform obj)
    {
        if (obj != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool IsDefined(Text obj)
    {
        if (obj != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }



}
