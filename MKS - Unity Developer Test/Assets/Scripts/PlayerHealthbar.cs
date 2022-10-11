using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthbar : MonoBehaviour
{
    private Vector3 localScale;
    private Transform playerTransfom;
    // Start is called before the first frame update
    void Start()
    {
        playerTransfom = GameObject.FindWithTag("Player").transform;
        localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = new Vector3(playerTransfom.transform.position.x, playerTransfom.transform.position.y + 0.8f, playerTransfom.transform.position.z);
        localScale.x = Player.health / 3;
        transform.localScale = localScale;
    }
}
