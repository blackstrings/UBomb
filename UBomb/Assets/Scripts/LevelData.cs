using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelData : ScriptableObject
{

    [SerializeField]
    public List<Vector3> blockPositions;

    // other position locations for other spawned stuff
}
