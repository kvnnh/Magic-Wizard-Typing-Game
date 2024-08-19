using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public TMP_Text skillCooldownText;
    [SerializeField] private bool canUseSkill = true;
    [SerializeField] private float skillCooldown;
    [SerializeField] private float timerCooldown;
    [SerializeField] public GameObject gameOver;
    [SerializeField] public GameObject pauseMenu;
    [SerializeField] private PauseMenu pause;
    private bool isPaused;

    private int kill;
    public TMP_Text kills;
    private Animator anim;
    private SpriteRenderer sr;

    public SpriteRenderer healthbarRenderer;
    public Sprite[] healthSprites;

    [SerializeField] private int maxHealth;
    [SerializeField] private int healthRemaining;
    
    private float timeSurvived;
    public TMP_Text score;
    public bool isKnocked;
    private bool canBeKnocked = true;
    public bool isDead;

    public bool isCastSpell;

    public EnemySpawner spawnEnemy;
    private List<Enemy> enemies = new List<Enemy>();
    private string currentInput = "";

    // Start is called before the first frame update
    void Start()
    {
        timeSurvived = 0;
        kill = 0;
        score.text = "Score: " + timeSurvived;
        kills.text = "Kills: " + kill;
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        healthRemaining = maxHealth;
        timerCooldown = skillCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        isPaused = pauseMenu.activeSelf;

        checkHealthBar();

        animationController();

        if (isDead)
            return;
        
        checkCooldown();

        if(healthRemaining <= 0)
        {
            isDead = true;
        }

        if(isDead && canBeKnocked)
        {
            die();
        }

        timeSurvived += Time.deltaTime;
        updateScore();

        checkInput();

    }

    private void checkCooldown()
    {
        timerCooldown -= Time.deltaTime;
        if (timerCooldown <= 0 && canUseSkill == false)
        {
            skillCooldownText.text = "Skill ready: type \"killall\"";
            canUseSkill = true;
        }
        else
        {
            if(canUseSkill == true)
            {
                skillCooldownText.text = "Skill ready: type \"killall\"";
            }
            else
            {
                skillCooldownText.text = "Skill Cooldown: " + timerCooldown.ToString("F1");
            }
        }
    }

    private void updateScore()
    {

        score.text = "Time survived: " + timeSurvived;
        kills.text = "Kills: " + kill;
    }

    private void checkHealthBar()
    {
        for(int i = 0; i < 5; i++)
        {
            if(i == healthRemaining)
            {
                healthbarRenderer.sprite = healthSprites[i];
            }else if(isDead == true)
            {
                healthbarRenderer.sprite = healthSprites[0];
            }
        }
    }

    private void CheckEnemyName(string input)
    {
        List<Enemy> enemiesToRemove = new List<Enemy>();

        for (int i = 0; i < enemies.Count; i++)
        {
            
            if (enemies[i].enemyName.Equals(input, System.StringComparison.OrdinalIgnoreCase))
            {
                isCastSpell = true;
                isKnocked = false;
                enemiesToRemove.Add(enemies[i]);
                kill += 1;
                Debug.Log("Total Kill: " + kill);
            }
        }

        foreach (Enemy enemy in enemiesToRemove) 
        {
            enemy.die();
            enemies.Remove(enemy);
        }
    }


    public void RegisterEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
    }

    public void knocked()
    {
        if(!canBeKnocked)
            return;

        healthRemaining -= 1;
        StartCoroutine(Invicibility());
        isKnocked = true;

    }

    private void die()
    {
        isKnocked = false;
        isDead = true;
        canBeKnocked = false;
        anim.SetBool("isDead", true);
        Debug.Log("Your Score is: " + timeSurvived); 
    }

    private IEnumerator Invicibility()
    {
        if (healthRemaining > 0)
        {
            Color originalColor = sr.color;
            Color darkenColor = new Color(sr.color.r, sr.color.g, sr.color.b, .5f);

            canBeKnocked = false;

            sr.color = darkenColor;
            yield return new WaitForSeconds(.1f);

            sr.color = originalColor;
            yield return new WaitForSeconds(.1f);

            sr.color = darkenColor;
            yield return new WaitForSeconds(.15f);

            sr.color = originalColor;
            yield return new WaitForSeconds(.15f);

            sr.color = darkenColor;
            yield return new WaitForSeconds(.25f);

            sr.color = originalColor;
            yield return new WaitForSeconds(.25f);

            sr.color = darkenColor;
            yield return new WaitForSeconds(.3f);

            sr.color = originalColor;
            yield return new WaitForSeconds(.35f);

            sr.color = darkenColor;
            yield return new WaitForSeconds(.4f);

            sr.color = originalColor;

            canBeKnocked = true;

        }

    }

    private void cancelKnockback() => isKnocked = false;

    private void animationController()
    {
        anim.SetBool("isKnocked", isKnocked);
        anim.SetBool("isDead", isDead);
        anim.SetBool("isCastSpell", isCastSpell);
    }

    private void setGameOver()
    {
            gameOver.SetActive(true);
    }

    private void checkInput()

    {
        if(isDead == true)
        {
            Invoke("setGameOver", 3f);
            return;
        }

        if (isPaused)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pause.Pause();
        }

        foreach (char c in Input.inputString)
        {
            if (c == '\b') // Backspace
            {
                if (currentInput.Length != 0)
                {
                    currentInput = currentInput.Substring(0, currentInput.Length - 1);
                    Debug.Log("Current input: " + currentInput);
                }
            }
            else if (c == '\n' || c == '\r') // Enter
            {
                if (currentInput.ToLower() == "killall" && canUseSkill == true)
                {
                    canUseSkill = false;
                    timerCooldown = skillCooldown;
                    List<Enemy> enemiesToRemove = new List<Enemy>();

                    for (int i = 0; i < enemies.Count; i++)
                    {
                        canUseSkill = false;
                        isCastSpell = true;
                        isKnocked = false;
                        enemiesToRemove.Add(enemies[i]);
                        kill += 1;
                        Debug.Log("Total Kill: " + kill);
                    }

                    foreach (Enemy enemy in enemiesToRemove)
                    {
                        enemy.die();
                        enemies.Remove(enemy);
                    }
                    currentInput = "";
                }
                else
                {
                    Debug.Log("Checking enemy name: " + currentInput);
                    CheckEnemyName(currentInput);
                    currentInput = "";
                }
            }
           
            else
            {
                currentInput += c;
                Debug.Log("Current input: " + currentInput);
            }
        }
    }

    private void readyToCastSpell()
    {
        isCastSpell = false;
    }
}
