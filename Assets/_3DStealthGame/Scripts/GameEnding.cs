using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
#if UNITY_EDITOR    // Direktiva kompajleru: Da li je igra pokrenuta iz Unity editora?
using UnityEditor;    // Ako jeste, uvrsti odgovarajuću biblioteku da bi se omogućilo pristup klasi EditorApplication
#endif    // Kraj direktive kompajleru

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f;    // Trajanje fade efekta u sekundama
    public float displayImageDuration = 1f;    // Vrijeme prikazivanja završne slike, nakon završetka fade efekta
    public GameObject player;    // Referenca ka objektu igrača
    public UIDocument uiDocument;    // Referenca ka UI objektu

    bool playerAtExit;    // Flag koji govori da li je igrač stigao do kraja nivoa
    bool playerCaught;    // Flag koji govori da li je igrač uhvaćen

    float timer = 0f;    // Tajmer za fade i kraj/restart nivoa

    private VisualElement endScreen;    // Referenca UI elementu koji se prikazuje kada se igra uspješno završi
    private VisualElement caughtScreen;    // Referenca UI elementu koji se prikazuje kada je igrač uhvaćen

    public void CaughtPlayer()  // Javna metoda koja omogućava da se igrač proglasi uhvaćenim
    {
        playerCaught = true;
    }

    void Start()
    {
        endScreen = uiDocument.rootVisualElement.Q<VisualElement>("EndScreen");    // Preuzimanje UI elementa za uspješan završetak
        caughtScreen = uiDocument.rootVisualElement.Q<VisualElement>("CaughtScreen");    // Preuzimanje UI elementa za neuspješan završetak
    }

    void Update()
    {
        if (playerAtExit)
        {
            EndLevel(endScreen, false);    // Ako je igrač stigao do kraja nivoa prikaži odgovarajuću sliku i završi igru
        }
        else if (playerCaught)
        {
            EndLevel(caughtScreen, true);    // Ako je igrač uhvaćen prikaži odgovarajuću sliku i restartuj nivo
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)    // Da li je objekat koji je prošao kroz collider kraja nivoa igrač?
        {
            playerAtExit = true;    // Postavi odgovarajući flag
        }
    }

    void EndLevel(VisualElement element, bool restart)
    {
        timer += Time.deltaTime;    // Uvećaj tajmer za vrijeme proteklo od prethodnog frejma
        element.style.opacity = timer / fadeDuration;

        if (timer > fadeDuration + displayImageDuration)    // Da li je slika prikazana u skladu sa zadatim trajanjima?
        {
            if (restart)    // Da li je zatražen restart scene
            {
                SceneManager.LoadScene("MainScene");    // Restartuj scenu
            }
            else    // U suprotnom završii igru
            {
#if UNITY_EDITOR    // Direktiva kompajleru: Da li je igra pokrenuta iz Unity editora?
                EditorApplication.isPlaying = false;    // Vrati se u editor
#else    // Direktiva kompajleru: U suprotnom
                Application.Quit();    // Zatvori aplikaciju
#endif    // Kraj direktive kompajleru
            }
        }
    }
}
