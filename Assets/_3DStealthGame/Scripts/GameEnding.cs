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

    public AudioSource exitAudio;     // Referenca ka zvuku koji se reprodukuje kada se igra uspješno završi
    public AudioSource caughtAudio;    // Referenca ka zvuku  koji se reprodukuje kada je igrač uhvaćen
    bool audioPlayed;    // Flag koji govori da li je zvuk kraja nivoa/hvatanja već reprodukovan

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
            EndLevel(endScreen, false, exitAudio);    // Ako je igrač stigao do kraja nivoa prikaži odgovarajuću sliku i završi igru
        }
        else if (playerCaught)
        {
            EndLevel(caughtScreen, true, caughtAudio);    // Ako je igrač uhvaćen prikaži odgovarajuću sliku i restartuj nivo
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)    // Da li je objekat koji je prošao kroz collider kraja nivoa igrač?
        {
            playerAtExit = true;    // Postavi odgovarajući flag
        }
    }

    void EndLevel(VisualElement element, bool restart, AudioSource audioSource)
    {
        if (!audioPlayed)    // Da li je zvuk već reprodukovan?
        {
            audioSource.Play();    // Ako nije, reprodukuj željeni zvuk
            audioPlayed = true;    // Postavi flag, da se u sljedećem pozivu funkcije ne bi ponovo pokrenula reprodukcija od početka
        }

        timer += Time.deltaTime;    // Uvećaj tajmer za vrijeme proteklo od prethodnog frejma
        element.style.opacity = timer / fadeDuration;    // Podesi transparenciju završne slike u skladu sa proteklim vremenom

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
