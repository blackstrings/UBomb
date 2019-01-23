using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Helps assist editing blocks when creating level.
/// </summary>
public class BlockCreator : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    /// <summary>
    /// Sets the coordinates for later extraction.
    /// </summary>
    public void setPosition(Vector3 position)
    {
        // position the gameobject
        this.transform.position = position;
    }

    /// <summary>
    /// Gets the coordinates of the block location.
    /// </summary>
    /// <returns>The coordinates.</returns>
    public Vector3 getData()
    {
        ///BlockData data = ScriptableObject.CreateInstance<BlockData>();

        return new Vector3((int)transform.position.x,
            (int)transform.position.y,
            (int)transform.position.z);
    }

}
