using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScoreList", menuName = "Whales/ScoreList", order = 1)]
public class ScoreList : ScriptableObject
{
    public List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>();
}
