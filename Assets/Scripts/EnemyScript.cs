using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [Header("Enemy Info")]
    public string enemyName;
    public int enemyID;
    public int enemyAI;
    public string enemyPhase;
    private Rigidbody2D rb;
    [Header("Enemy Stats")]
    public int hp;
    public int dmg;
    public int maxHP;
    public float ViewRange;
    public float speed;
    public float knockBack;
    [Header("Special Enemy Behavior")]
    public float specialEnemyChance;
    [Header ("Other")]
    private Vector2 TargetPos = new Vector2(-1, -1);
    private Vector2 PlayerDir;
    private Vector2 TargetDir;
    private List<Collider2D> Colliders = new List<Collider2D>();
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        if (Random.Range(0, 100) <= specialEnemyChance * 100)
        {
            maxHP = Mathf.RoundToInt(maxHP * 10 * Random.Range(0.90f, 1.10f));
            transform.localScale = transform.localScale * 2f;
            dmg = Mathf.RoundToInt(dmg * 10 * Random.Range(0.90f, 1.10f));
            speed = Mathf.RoundToInt(speed / 2);
            
        }
        else
        {
            maxHP = Mathf.RoundToInt(maxHP * Random.Range(0.90f, 1.10f));
            dmg = Mathf.RoundToInt(dmg * Random.Range(0.90f, 1.10f));
        }
        hp = maxHP;

        

        
    }

    // Update is called once per frame
    void Update()
    {

        if (hp > 0)
        {
            AI(enemyID);
        }
        else
        {
            DropLoot();
        }
    }

    public void DropLoot()
    {

        Destroy(this.gameObject);
    }

    public void AI(int ID)
    {
        worldGen world = FindObjectOfType<worldGen>();
        GameObject Player = FindObjectOfType<PlayerController>().gameObject;
        RaycastHit2D[] hits = new RaycastHit2D[Mathf.RoundToInt(ViewRange)];

        if (ID == 1)//Fågel
            {
                if (TargetPos.x == -1 && TargetPos.y == -1)
                {
                    TargetPos = new Vector2(Random.Range(0, world.worldWidth), Random.Range(0, world.worldHeight));
                }
                TargetDir = new Vector2(TargetPos.x - transform.position.x, TargetPos.y - transform.position.y).normalized;
            transform.up = TargetDir * -1;

            Debug.Log(TargetPos);
            rb.velocity = TargetDir * speed;
                if (enemyPhase == "Patrol")
                {
                    if (Mathf.RoundToInt(TargetPos.x) == Mathf.RoundToInt(transform.position.x) && Mathf.RoundToInt(TargetPos.x) == Mathf.RoundToInt(transform.position.x))
                    {
                        Debug.Log("NyPunkt");
                        TargetPos = new Vector2(Random.Range(0, world.worldWidth), Random.Range(0, world.worldHeight));
                    }

                    if (Vector2.Distance(this.transform.position, Player.transform.position) <= ViewRange)
                    {
                        enemyPhase = "Chase";
                    }
                }
                else if (enemyPhase == "Chase")
                {

                    PlayerDir = new Vector2(Player.transform.position.x - this.transform.position.x, Player.transform.position.y - this.transform.position.y).normalized;
                    Debug.DrawRay(this.transform.position, TargetDir * ViewRange, Color.red);
                    int hitCount = Physics2D.RaycastNonAlloc(transform.position, PlayerDir, hits, ViewRange);

                    for (int i = 0; i < hitCount; i++)
                    {
                        if (!hits[i].collider.isTrigger)
                        {

                            if (hits[i].collider.CompareTag("Player"))
                            {
                                Debug.Log(hits[i].collider.name);
                                TargetPos = Player.transform.position;
                                TargetDir = PlayerDir;
                                i = hitCount;
                            }
                            else
                            {
                                i = hitCount;
                            }
                        }
                    }

                    //RaycastHit2D hit = Physics2D.Raycast(transform.position * PlayerDir.normalized, PlayerDir, ViewRange);
                    //if (hit.collider != null)
                    //{
                    //    Debug.Log(hit.collider.tag);
                    //    if (hit.collider.CompareTag("Player"))
                    //    {
                    //        TargetPos = Player.transform.position;
                    //        Debug.Log(TargetPos);
                    //    }
                    //}
                    if (Mathf.RoundToInt(TargetPos.x) == Mathf.RoundToInt(transform.position.x) && Mathf.RoundToInt(TargetPos.x) == Mathf.RoundToInt(transform.position.x))
                    {
                        enemyPhase = "Patrol";
                    }
                }
                else
                {
                    Debug.Log("not a phase, check code");
                    enemyPhase = "Patrol";
                }
            for (int i = 0; i < Colliders.Count; i++)
            {
                if (Colliders[i] != null && Colliders[i].GetComponent<PlayerController>())
                {
                    Colliders[i].GetComponent<Rigidbody2D>().velocity = Colliders[i].GetComponent<Rigidbody2D>().velocity + TargetDir * knockBack;
                    Colliders[i].GetComponent<PlayerController>().Health -= Mathf.RoundToInt(dmg * Random.Range(0.90f, 1.10f));

                }
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Colliders.Contains(collision))
        {
            Colliders.Add(collision);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (Colliders.Contains(collision))
        {
            Colliders.Remove(collision);
        }
    }
}
