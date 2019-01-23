using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

/// <summary>
/// Level creator. Define the levle size and initialize a base grid of blocks. 
/// Use the editor to edit and remove blocks.
/// Then save out the level.
/// </summary>
public class LevelCreator : MonoBehaviour
{
    // to be updated in the Editor
    public int width;
    public int height;

    /// <summary>
    /// The prefab wall block.
    /// </summary>
    public GameObject pf_wallBlock;

    public LevelData levelToLoadFrom;
    public LevelData levelToSaveTo;

    private int gap;

    /// <summary>
    /// Takes the current blocks and saves it into an array of BlockData.
    /// Only saves the data if levelToSaveTo is wired up.
    /// </summary>
    public void save()
    {
        if(levelToSaveTo != null)
        {
            // get all the blocks in the level
            List<GameObject> go_blocks = getBlocks();
            if(go_blocks.Count > 0)
            {
                List<Vector3> blockData = new List<Vector3>();
                foreach(GameObject go_block in go_blocks)
                {
                    blockData.Add(go_block.GetComponent<BlockCreator>().getData());
                }
                levelToSaveTo.blockPositions = blockData;
            }
            else
            {
                Debug.LogError("Failed to save created level, no blocks in the level gameobject");
            }

        }
        else
        {
            // must first create an instance of the LevelData scriptable object
            // and wire it to the levelToSaveTo in the editor
            Debug.LogError("Failed to save created level, levelToSaveTo is null");
        }
    }

    /// <summary>
    /// Loads the wired in LevelData.
    /// </summary>
    public void load()
    {
        if(levelToLoadFrom != null && pf_wallBlock != null)
        {
            int counter = 0;
            foreach(Vector3 blockData in levelToLoadFrom.blockPositions)
            {
                GameObject block = Instantiate(pf_wallBlock);
                block.name = "block" + counter;
                block.transform.parent = this.transform;
                block.GetComponent<BlockCreator>().setPosition(new Vector3(blockData.x, blockData.y, blockData.z));
            }
        } 
        else
        {
            /// should be wired in the editor.
            throw new UnityException("Failed to load level, levelToLoadFrom is null");
        }
    }

    /// <summary>
    /// Returns all existing gameobject blocks under the Level.
    /// </summary>
    /// <returns>The blocks.</returns>
    private List<GameObject> getBlocks()
    {
        List<GameObject> go_blocks = new List<GameObject>();
        foreach (Transform child in transform)
        {
            if (child.gameObject.tag == "BlockTag")
            {
                go_blocks.Add(child.gameObject);
            }
        }
        return go_blocks;
    }

    /// <summary>
    /// Clears all existing blocks under this Level.
    /// </summary>
    public void clear()
    {
        List<GameObject> go_blocks = getBlocks();

        if (go_blocks.Count > 0)
        {
            for (int i=go_blocks.Count-1; i>=0; i--)
            {
                Object.DestroyImmediate(go_blocks[i]);
                ///Destroy(go_blocks[i]);
            }
        }
        else
        {
            Debug.Log("Failed to clear, count was " + go_blocks.Count);
        }

    }

    /// <summary>
    /// Generates a grid block based on width and height params.
    /// </summary>
    public void generateCleanBlocks()
    {
        if (pf_wallBlock != null)
        {
            this.gap = 1;

            if (width > 0 && height > 0)
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        Vector3 pos = new Vector3(i * gap, 0, j * gap);
                        GameObject block = Instantiate(pf_wallBlock);
                        block.name = "block" + (i + j);
                        block.transform.parent = this.transform;
                        block.GetComponent<BlockCreator>().setPosition(pos);
                    }
                }
            }
            else
            {
                throw new UnityException("Failed to init level, Width or Height is zero.");
            }

        }
        else
        {
            throw new UnityException("Failed to init level, pf_block is null");
        }
    }




}
