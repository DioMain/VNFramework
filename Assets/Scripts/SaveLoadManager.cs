using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public AudioClip Clip;

    private void Start()
    {
        Debug.Log(JsonUtility.ToJson(Clip.GetInstanceID()));

        
    }
}
