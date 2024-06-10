using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            Destroy(gameObject);
        }
    }

    // Cuando las balas salen de la escena se autodestruyen
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
