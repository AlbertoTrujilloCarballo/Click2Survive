using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    // Referencias privadas accesibles desde el Inspector
    [SerializeField] GameObject bulletPrefab; // Prefab con la bala
    [SerializeField] GameObject spawnPoint; // Posici�n desde la que instanciar balas


    // Variables privadas
    Vector2 playerDirection; // Para almacenar el vector de direcci�n que apunta al player

    // Variables privadas accesibles desde el Inspector
    [SerializeField] float bulletSpeed = 10;

    private PlayerController player;
    private bool hasShoot = false;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Hacemos que el GameObject apunte a la posici�n del mouse
        // calculando el vector que contiene la direcci�n hacia el mouse
        playerDirection = player.transform.position - transform.position;


        // Ajustamos la rotaci�n del objeto para que apunte hacia la direcci�n del player
        // Recordatorio: un vector normalizado es un vector que tiene magnitud 1
        transform.up = playerDirection.normalized;
        //StartCoroutine(FireAndWait());
        if (!hasShoot)
        {
            StartCoroutine(FireAndWait());
            hasShoot = true;
            AudioManager.instance.PlaySFX("shot");
            //AudioManager.instance.PlaySFX("Shot");
            // Instanciamos la bala en la posici�n de spawn
            GameObject bullet = Instantiate(bulletPrefab, spawnPoint.transform);

            // Eliminamos la dependencia del objeto padre spawnPoint
            bullet.transform.SetParent(null);

            // Configuramos la velocidad de movimiento
            bullet.GetComponent<Rigidbody2D>().velocity = playerDirection.normalized * bulletSpeed;           
            StartCoroutine(FireAndWait());
        }
    }

    IEnumerator FireAndWait()
    {
        yield return new WaitForSeconds(3f);
        hasShoot = false;
    }

}
