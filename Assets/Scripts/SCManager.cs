using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SCManager : MonoBehaviour
{
    // Creamos una variable estática para almacenar la única instancia
    public static SCManager instance;
    // Referencia al sprite que contiene la imagen para el cursor
    [SerializeField] Texture2D cursorTarget;

    public int lastScene;

    public float currentLife;

    // Método Awake que se llama al inicio antes de que se active el objeto. Útil para inicializar
    // variables u objetos que serán llamados por otros scripts (game managers, clases singleton, etc).
    private void Awake()
    {

        // ----------------------------------------------------------------
        // AQUÍ ES DONDE SE DEFINE EL COMPORTAMIENTO DE LA CLASE SINGLETON
        // Garantizamos que solo exista una instancia del SCManager
        // Si no hay instancias previas se asigna la actual
        // Si hay instancias se destruye la nueva
        if (instance == null) instance = this;
        else { Destroy(gameObject); return; }
        // ----------------------------------------------------------------

        // No destruimos el SceneManager aunque se cambie de escena
        DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {
        // Configuramos el cursor 
        //Vector2 hotspot = new Vector2(cursorTarget.width / 2, cursorTarget.height / 2);
        Vector2 hotspot = new Vector2(3,3);
        Cursor.SetCursor(cursorTarget, hotspot, CursorMode.Auto);
        Debug.Log("Awake:" + SceneManager.GetActiveScene().buildIndex);
    }

    // Método para cargar una nueva escena por nombre
    public void LoadSceneByName(string sceneName)
    {
        currentLife = 100;
        SceneManager.LoadScene(sceneName);
        //Para añadir una nueva escena a la actual, como por ejemplo un inventario o un mapa
        //SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    public void LoadSceneByIndex(int sceneIndex)
    {
        if(GameManager.life == 0)
        {
            currentLife = 100;
        }
        else
        {
            currentLife = GameManager.life;
        }
        SceneManager.LoadScene(sceneIndex);
        //Para añadir una nueva escena a la actual, como por ejemplo un inventario o un mapa
        //SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    public void LoadSceneAdditive(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    public void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    public string GetActualSceneName(string ActualSceneName)
    {
        currentLife = GameManager.life;
        return ActualSceneName;
    }

    public int GetActualSceneIndex(int ActualSceneIndex)
    {
        currentLife = GameManager.life;
        lastScene = ActualSceneIndex;
        return ActualSceneIndex;
    }

}

