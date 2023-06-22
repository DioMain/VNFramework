using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public Dictionary<string, int> IntValues = new Dictionary<string, int>();
    public Dictionary<string, float> FloatValues = new Dictionary<string, float>();
    public Dictionary<string, bool> BoolValues = new Dictionary<string, bool>();
    public Dictionary<string, string> StringValues = new Dictionary<string, string>();
}
