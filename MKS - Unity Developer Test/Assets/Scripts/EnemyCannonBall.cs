using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCannonBall : MonoBehaviour
{
    [SerializeField] private float speed, ballRange;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Range());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            player.TakeDamage();
            Destroy(gameObject);
        }
    }

    IEnumerator Range()
    {
        yield return new WaitForSeconds(ballRange);
        Destroy(this.gameObject);
    }
}
