using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{


    [SerializeField] TextMeshProUGUI clicksText;
    public int clicks;

    //// Referencias a objetos privados visibles desde el Inspector
    [SerializeField] GameObject HPBar; // Para referenciar el relleno de la barra de vida

    private Image img;

    // Variable pública para modificar desde cualquier script
    public static float life = 100; // Vida que tiene el jugador en un momento determinado
    public static float maxLife = 100; // Vida máxima del jugador

    private bool canClick = true;


    // Start is called before the first frame update
    void Start()
    {
        // Actualizamos la información
        UpdateHUD();
        img = HPBar.GetComponent<Image>();
        life = SCManager.instance.currentLife;
        VaciarBarraVida();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Método para restar vida al Player
    public void TakeDamage(float damage)
    {
        life = Mathf.Clamp(life - damage, 0, maxLife);
        if(life == 0)
        {
            SCManager.instance.LoadSceneByName("GameOver");
        }
    }

    public void UpdateHUD()
    {
        clicksText.text = "Clicks: " + clicks;
    }
    public void VaciarBarraVida()
    {
        img.fillAmount = life / maxLife;
    }
    public void DisableClick()
    {
        canClick = false;
    }
    public void EnableClick()
    {
        canClick = true;
    }
    public bool GetClickingStatus()
    {
        return canClick;
    }

}
