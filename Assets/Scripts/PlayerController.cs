using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    #region Variables
    [Header("Parametros")]
    [SerializeField]
    private float speed;
    [SerializeField] ParticleSystem bloodParticles;

    // Variables privadas
    Vector2 mousePosition; // Para almacenar las coordenadas en píxeles del mouse
    Vector2 worldMousePosition; // Para almacenar las coordenadas equivalentes del mundo
    //Vector2 Destination;

    // Variables privadas
    Vector2 pointDirection; // Para almacenar el vector de dirección que apunta al mouse

    SpriteRenderer sr;
    private Rigidbody2D rig;

    private int sceneIndex;
    GameManager gameManager;

    private bool playerCanClick;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rig = GetComponent<Rigidbody2D>();
        sceneIndex = SCManager.instance.GetActualSceneIndex(SceneManager.GetActiveScene().buildIndex);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //Destination = transform.position;
        sceneIndex = SCManager.instance.GetActualSceneIndex(SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    void Update()
    {
        // Obtenemos la posición del mouse
        mousePosition = Input.mousePosition; // Coordenadas de pantalla

        // Transformamos las coordenadas de pantalla en coordenadas del mundo
        worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //Debug.Log(worldMousePosition);

        // Hacemos que el GameObject apunte a la posición del mouse
        // calculando el vector que contiene la dirección hacia el mouse
        pointDirection = worldMousePosition - (Vector2)transform.position;


        if (Input.GetMouseButtonDown(0))
        {
            playerCanClick = gameManager.GetClickingStatus();
            if (playerCanClick && gameManager.clicks > 0)
            {
                AudioManager.instance.PlaySFX("click");
                gameManager.clicks = Mathf.Clamp(gameManager.clicks - 1, 0, gameManager.clicks); ;
                gameManager.UpdateHUD();
                if (pointDirection.normalized.x > 0)
                {
                    sr.flipX = false;
                }
                else
                {
                    sr.flipX = true;
                }
                rig.velocity = pointDirection.normalized * speed;
                //rig.velocity = (transform.right * speed * pointDirection.normalized.x +
                //    (transform.up * speed * pointDirection.normalized.y));
                //
            }

          
        }
        ////Metodo para mover al personaje donde se hace click
        //transform.position = Vector2.MoveTowards(transform.position, Destination, speed * Time.deltaTime);

        if (gameManager.clicks == 0)
        {
            StartCoroutine(LoseLife());
        }
        else if (gameManager.clicks > 0)
        {
            StopCoroutine(LoseLife());
        }

        gameManager.EnableClick();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            rig.velocity = Vector2.zero;
            if (collision.collider.CompareTag("Trap"))
            {
                AudioManager.instance.PlaySFX("damage");                
                bloodParticles.Play();
                SCManager.instance.LoadSceneByName("GameOver");
            }
            if (collision.collider.CompareTag("Exit"))
            {
                AudioManager.instance.PlaySFX("nextLevel");
                sceneIndex++;
                SCManager.instance.LoadSceneByIndex(sceneIndex);
            }
        }
    }

    IEnumerator LoseLife()
    {
        gameManager.TakeDamage(0.1f);
        gameManager.VaciarBarraVida();
        yield return new WaitForSeconds(5);
    }

}