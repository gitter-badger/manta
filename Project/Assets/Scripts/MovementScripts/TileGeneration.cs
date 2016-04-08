using UnityEngine;
using System.Collections;

public class TileGeneration : MonoBehaviour
{
    public GameObject tileObject;
    public int gridSize;
    public float tileScale;

	// Use this for initialization
	void Start ()
    {
        generateTiles();
	}

    public void generateTiles()
    {
        int x = 0;
        int z = 0;

        for (int i = 0; i < (gridSize * gridSize); i++)
        {
            if (x == gridSize)
            {
                x = 0;
                z++;
            }
            GameObject t = Instantiate(tileObject, new Vector3(x, 0, z), Quaternion.identity)as GameObject;
            t.transform.parent = GameObject.FindGameObjectWithTag("Tiles").transform;
            x++;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
