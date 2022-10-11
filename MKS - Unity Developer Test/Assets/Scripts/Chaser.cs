using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour
{
    [SerializeField] private GameObject chaser, explode, hpBar;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float moveSpeed, health = 3;
    private Vector3 localScale;
    [SerializeField] private Animator chaserAnim, explosionAnim;
    [SerializeField] private bool fadeOut = false;
    private SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        health = 3;
        chaserAnim = gameObject.GetComponent<Animator>();
        playerTransform = GameObject.FindWithTag("Player").transform;
        rend = gameObject.GetComponent<SpriteRenderer>();
        localScale = hpBar.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        //FADE OUT
        if (fadeOut == true)
        {
            fadeOut = false;
            StartCoroutine(FadeOut());
        }

        //HP BAR
        hpBar.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z);
        localScale.x = health / 3;
        hpBar.transform.localScale = localScale;

        //HP ANIM
        chaserAnim.SetInteger("Health", (int)health);

        //FOLLOW PLAYER
        if (health > 0 && Player.health > 0 && GameManager.survived == false)
        {
            Vector2 direction = playerTransform.transform.position - transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;

            transform.position = Vector2.MoveTowards(this.transform.position, playerTransform.transform.position, moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
        //DEATH TRIGGER
        else if(health <= 0)
        {
            Death();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            player.TakeDamage();
            Hit();
        }
    }

    public void TakeDamage()
    {
        health -= 1;
        if (health <= 0)
        {
            GameManager.score += 1;
            fadeOut = true;
            Death();
        }
    }

    public void Hit()
    {
        health = 0;
        explode.SetActive(true);
        if (health <= 0)
        {
            fadeOut = true;
            Death();
        }
    }

    public void Death()
    {
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        health = 0;
        explode.SetActive(true);
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(1f);
        for (float f = 1f; f >= -0.05f; f -= 0.04f)
        {
            Color c = rend.material.color;
            c.a = f;
            rend.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
        Destroy(chaser);
    }
}
