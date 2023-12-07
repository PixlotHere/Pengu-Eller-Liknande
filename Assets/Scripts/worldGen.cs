using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldGen : MonoBehaviour
{
    [Header("Tiles")]
    public Sprite Water;
    public Sprite stone;
    public Sprite sand;
    public Sprite sandTreasure;
    public Sprite ice;
    public Sprite iceBrick;

    [Header("Generation Settings")]
    public int chunkSize = 16;
    public int worldWidth;
    public int worldHeight;
    public float noiseFreq = 0.05f;
    public float biomeFreq;
    public float lootFreq;
    public float chunkDistance;

    [Header("Noise Settings")]
    public float worldChance;
    public int seed;
    public Texture2D noiseTex;
    public Texture2D tempText;

    private GameObject[,] worldChunks;
    private List<Vector2> worldTiles = new List<Vector2>();
    public GameObject Player;
    void Start()
    {
        //seed = Random.Range(-10000, 10000);
        GenerateNoiseTexture();
        GenerateBiomeTexture();
        createChunks();
        GenerateTerrain();
    }

    // Update is called once per frame
    void Update()
    {
        loadChunks();
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

    public void GenerateTerrain()
    {
        for (int x = 1; x < worldWidth; x++)
        {
            for (int y = 1; y < worldHeight; y++)
            {
                if (noiseTex.GetPixel(x, y).r > worldChance)
                {
                    if (tempText.GetPixel(x,y).r > 0.6)//desert
                    {
                        if (tempText.GetPixel(x,y).g > 0.95f)
                        {
                            Debug.Log("Special");
                            placeTile(sandTreasure, x, y, true);
                        }
                        else
                        {
                            placeTile(sand, x, y, false);
                        }
                    }
                    else if (tempText.GetPixel(x, y).r > 0.3f)//StoneLands
                    {
                        placeTile(stone, x, y,false);
                    }
                    else //iceLands
                    {
                        if (tempText.GetPixel(x, y).g > 0.85f)
                        {
                            Debug.Log("SpecialIce");
                            placeTile(iceBrick, x, y, true);
                        }
                        else
                        {
                            placeTile(ice, x, y, false);
                        }
                    }

                }
                else if (noiseTex.GetPixel(x, y).r > worldChance - 0.045f)
                {
                    if (tempText.GetPixel(x, y).r > 0.5)
                    {
                        if (tempText.GetPixel(x, y).g > 0.95f)
                        {
                            Debug.Log("Special");
                            placeTile(sandTreasure, x, y,true);
                        }
                        else
                        {
                            placeTile(sand, x, y,false);
                        }
                    }
                    else if (tempText.GetPixel(x, y).r > 0.2)
                    {
                        placeTile(Water, x, y,false);
                    }
                    else
                    {
                        if (tempText.GetPixel(x, y).g > 0.85f)
                        {
                            Debug.Log("SpecialIce");
                            placeTile(iceBrick, x, y,true);
                        }
                        else
                        {
                            placeTile(ice, x, y,false);
                        }
                    }


                }

                else
                {
                    if (tempText.GetPixel(x, y).r > 0.6)
                    {
                        if (tempText.GetPixel(x, y).g > 0.95f)
                        {
                            Debug.Log("Special");
                            placeTile(sandTreasure, x, y,true);
                        }
                        else
                        {
                            placeTile(sand, x, y,false);
                        }
                    }
                    else if (tempText.GetPixel(x, y).r > 0.2)
                    {
                        placeTile(Water, x, y,false);
                    }
                    else
                    {
                        if (tempText.GetPixel(x, y).g > 0.85f)
                        {
                            Debug.Log("SpecialIce");
                            placeTile(iceBrick, x, y,true);
                        }
                        else
                        {
                            placeTile(ice, x, y,false);
                        }

                    }
                    
                }
                
            }
        }
    }

    public void placeTile(Sprite tileSprite, float x, float y, bool Collide)
    {
        GameObject newTile = new GameObject();
        newTile.isStatic = true;
        Vector2 ChunkCord = new Vector2((x/chunkSize), (y / chunkSize));
        //ChunkCord.x /= chunkSize;
        //ChunkCord.y /= chunkSize;
        newTile.transform.parent = worldChunks[Mathf.CeilToInt(ChunkCord.x), Mathf.CeilToInt(ChunkCord.y)].transform;
        
        newTile.name = "tile";
        newTile.AddComponent<SpriteRenderer>();
        newTile.GetComponent<SpriteRenderer>().sprite = tileSprite;
        if (Collide)
        {
            newTile.AddComponent<BoxCollider2D>();
        }
        if (Random.Range(0,2) == 1)
        {
            newTile.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (Random.Range(0, 2) == 1)
        {
            newTile.GetComponent<SpriteRenderer>().flipY = true;
        }
        newTile.transform.position = new Vector2(x - 1.5f, y - 1.5f);
        //newTile.transform.parent;
        worldTiles.Add(newTile.transform.position);
    }

    public void GenerateNoiseTexture()
    {
        noiseTex = new Texture2D(worldWidth,worldHeight);

        for (int x = 0; x < noiseTex.width; x++)
        {
            for (int y = 0; y < noiseTex.height; y++)
            {
                float v = Mathf.PerlinNoise((x + seed) * noiseFreq, (y + seed) * noiseFreq);
                noiseTex.SetPixel(x, y, new Color(v, v, v));
            }
        }
        noiseTex.Apply(); 
    }

    public void GenerateBiomeTexture()
    {
        tempText = new Texture2D(worldWidth, worldHeight);
        for (int x = 0; x < tempText.width; x++)
        {
            for (int y = 0; y < tempText.height; y++)
            {
                float v = Mathf.PerlinNoise((x - seed) * biomeFreq, (y - seed) * biomeFreq);
                float w = Mathf.PerlinNoise(x * (Mathf.Sqrt(seed) + 1) * lootFreq, y * (Mathf.Sqrt(seed) + 1) * lootFreq);
                tempText.SetPixel(x, y, new Color(v, w, 1-v));
            }
        }
        tempText.Apply();
    }
}
