using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator anim;
    public float moveSpeed;
    public string enemyName;

    private bool isDead;
    public Player mainCharacter;

    [SerializeField] private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
            mainCharacter = playerObject.GetComponent<Player>();
            Debug.Log("Player object found: " + playerObject.name);
        }
        else
        {
            Debug.LogError("Player object not found. Make sure the player object has the tag 'Player'.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && !isDead)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }


    }

    public void setTarget(Transform target)
    {
        this.player = target;
    }

    public string getName()
    {
        return enemyName;
    }


    public void die()
    {
        isDead = true;
        moveSpeed = 0;
        anim.SetBool("isDead", isDead);
        Invoke("destroyEnemy", 3f);
    }

    public void destroyEnemy()
    {
        Debug.Log("Enemy " + enemyName + " destroyed.");
        Destroy(gameObject);
    }

    internal void StartCoroutine(object v)
    {
        throw new NotImplementedException();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            moveSpeed = 0;
            if (isDead)
            {
                return;
            }
            else
            {
                mainCharacter.knocked();
                Debug.Log("player Knocked");
            }
        }
    }
}
