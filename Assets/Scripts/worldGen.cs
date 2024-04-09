using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using TMPro;

public class worldGen : MonoBehaviour
{
    [Header("Tiles")]
    public GameObject Water;
    public GameObject grass;
    public GameObject sand;
    public GameObject sandTreasure;
    public GameObject ice;
    public GameObject iceBrick;
    public GameObject stone;
    public GameObject tree;
    //public Sprite leaf;

    [Header("Generation Settings")]
    public int chunkSize = 16;
    public int worldWidth;
    public int worldHeight;
    public float noiseFreq = 0.05f;
    public float biomeFreq;
    public float lootFreq;
    public float chunkDistance;

    private bool ready = false;

    [Header("Noise Settings")]
    public float worldChance;
    public float seed;
    public Texture2D worldTex;
    public Texture2D tempTex;

    private GameObject[,] worldChunks;
    private GameObject[,] worldTiles;
    public GameObject Player;

    public void Awake()
    {
        seed = MainMenuController.seed;
    }
    void Start()
    {
        //seed = Random.Range(-10000, 10000);
        GenerateNoiseTexture();
        GenerateBiomeTexture();
        createChunks();
        GenerateBaseTerrain();
        GenerateSecondTerrain();
        ready = true;   

    }

    // Update is called once per frame
    private void Update()
    {
        if (ready)
        {
            loadChunks();
        }
    }
    public void loadChunks()
    {
        for (int chunk = 0; chunk < transform.childCount; chunk++)
        {
            if (Vector2.Distance(Player.transform.position, transform.GetChild(chunk).transform.position) <= chunkDistance * chunkSize)
            {
                transform.GetChild(chunk).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(chunk).gameObject.SetActive(false);
            }
        }
    }

    public void createChunks()
    {
        int numchunksX = (worldWidth / chunkSize) + 1;
        int numchunksY = (worldHeight  / chunkSize) + 1;
        worldChunks = new GameObject[numchunksX + 1, numchunksY + 1];
        for (int i = 1; i <= numchunksX; i++)
        {
            for (int j = 1; j <= numchunksY; j++)
            {
                GameObject newchunk = new GameObject();
                newchunk.transform.parent = this.transform;
                newchunk.name = i.ToString() + ":" + j.ToString();
                worldChunks[i, j] = newchunk;
                newchunk.transform.position = new Vector2(i * chunkSize - chunkSize/2, j * chunkSize - chunkSize/2);
            }
        }

    }

    public void GenerateBaseTerrain()
    {
        worldTiles = new GameObject[worldWidth + 1, worldHeight + 1];
        for (int x = 1; x < worldWidth; x++)
        {
            for (int y = 1; y < worldHeight; y++)
            {
                if (worldTex.GetPixel(x, y).r > worldChance)
                {
                    if (tempTex.GetPixel(x,y).r > 0.6)//desert
                    {
                        placeTile(sand, x, y, false);
                    }
                    else if (tempTex.GetPixel(x, y).r > 0.2f)//GrassLands
                    {
                        placeTile(grass, x, y,false);
                    }
                    else //iceLands
                    {
                            placeTile(ice, x, y, false);
                    }

                }
                else if (worldTex.GetPixel(x, y).r > worldChance - 0.045f)
                {
                    if (tempTex.GetPixel(x, y).r > 0.5)
                    {
                            placeTile(sand, x, y,false);
                    }
                    else if (tempTex.GetPixel(x, y).r > 0.2)
                    {
                        placeTile(Water, x, y,false);
                    }
                    else
                    {
                            placeTile(ice, x, y,false);   
                    }


                }

                else
                {
                    if (tempTex.GetPixel(x, y).r > 0.6)
                    {
                        placeTile(sand, x, y, false);
                    }
                    else if (tempTex.GetPixel(x, y).r > 0.2)
                    {
                        placeTile(Water, x, y,false);
                    }
                    else
                    {
                        placeTile(ice, x, y, false);
                    }
                    
                }
                
            }
        }
    }
    public void GenerateSecondTerrain()
    {
        for (int x = 1; x < worldWidth; x++)
        {
            for (int y = 1; y < worldHeight; y++)
            {
                if (worldTex.GetPixel(x, y).r > worldChance)
                {
                    if (tempTex.GetPixel(x, y).r > 0.6)//desert
                    {
                        if (Random.Range(0.00f, 1.00f) > 0.99f)
                        {
                            Debug.Log("Special");
                            placeTile(sandTreasure, x, y, true);
                        }
                    }
                    else if (tempTex.GetPixel(x, y).r > 0.2f)//GrassLands
                    {
                        if (Random.Range(0.00f, 1.00f) > 0.99f)
                        {
                            placeTile(tree, x, y, true);
                            //Instantiate(tree, new Vector3(x,y,0), this.transform.rotation);
                        }
                        else if (Random.Range(0.00f, 1.00f) < 0.03f)
                        {
                            Debug.Log("SpecialStone");
                            placeTile(stone, x, y, true);
                        }
                    }
                    else //iceLands
                    {
                        if (Random.Range(0.00f, 1.00f) > 0.85f)
                        {
                            Debug.Log("SpecialIce");
                            placeTile(iceBrick, x, y, true);
                        }
                    }

                }
                else if (worldTex.GetPixel(x, y).r > worldChance - 0.045f)
                {
                    if (tempTex.GetPixel(x, y).r > 0.5)
                    {
                        if (Random.Range(0.00f, 1.00f) > 0.99f)
                        {
                            Debug.Log("Special");
                            placeTile(sandTreasure, x, y, true);
                        }
                    }
                    else if (tempTex.GetPixel(x, y).r < 0.2)
                    {
                        if (Random.Range(0.00f, 1.00f) > 0.9f)
                        {
                            Debug.Log("SpecialIce");
                            placeTile(iceBrick, x, y, true);
                        }
                    }


                }

                else
                {
                    if (tempTex.GetPixel(x, y).r > 0.6)
                    {
                        if (Random.Range(0.00f, 1.00f) > 0.99f)
                        {
                            Debug.Log("Special");
                            placeTile(sandTreasure, x, y, true);
                        }
                    }
                    else if (tempTex.GetPixel(x, y).r < 0.2)
                    {
                        if (Random.Range(0.00f, 1.00f) > 0.95f)
                        {
                            Debug.Log("SpecialIce");
                            placeTile(iceBrick, x, y, true);
                        }
                    }

                }
                //placeTile(stone, x, y, true);
                //Debug.Log("stone");

            }
        }
    }

    public void placeTile(GameObject tileGO, float x, float y, bool Minable)
    {
        GameObject newTile = Instantiate(tileGO, new Vector3(x, y, 0), this.transform.rotation);
        newTile.isStatic = true;
        newTile.name = tileGO.name;
        Vector2 ChunkCord = new Vector2((x/chunkSize), (y / chunkSize));
        newTile.transform.parent = worldChunks[Mathf.CeilToInt(ChunkCord.x), Mathf.CeilToInt(ChunkCord.y)].transform;
        newTile.transform.position = new Vector2(x - 1.5f, y - 1.5f);

        

        newTile.tag = "tile";
        if (Random.Range(0,2) == 1)
        {
            newTile.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (Random.Range(0, 2) == 1)
        {
            newTile.GetComponent<SpriteRenderer>().flipY = true;
        }

        worldTiles[Mathf.CeilToInt(x), Mathf.CeilToInt(y)] = newTile;
        //newTile.transform.parent;
    }
    public void GenerateNoiseTexture()
    {
        worldTex = new Texture2D(worldWidth,worldHeight);

        for (int x = 0; x < worldTex.width; x++)
        {
            for (int y = 0; y < worldTex.height; y++)
            {
                float v = Mathf.PerlinNoise(((x + Mathf.Sqrt(Mathf.Abs(seed)))* (seed + 0.5f)) * noiseFreq, ((y + Mathf.Sqrt(Mathf.Abs(seed)))* (seed + 0.5f)) * noiseFreq);
                worldTex.SetPixel(x, y, new Color(v, v, v));
            }

        }
        worldTex.Apply(); 
    }

    public void GenerateBiomeTexture()
    {
        tempTex = new Texture2D(worldWidth, worldHeight);
        for (int x = 0; x < tempTex.width; x++)
        {
            for (int y = 0; y < tempTex.height; y++)
            {
                float v = Mathf.PerlinNoise((x - seed) * biomeFreq, (y - seed) * biomeFreq);
                tempTex.SetPixel(x, y, new Color(v, 0, 1-v));
            }
        }
        tempTex.Apply();
    }
}
