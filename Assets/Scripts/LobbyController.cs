using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyController : MonoBehaviour
{
    [SerializeField] private Button buttonPlay;
    [SerializeField] private Button buttonInstructions;
    [SerializeField] private Button buttonCoop;
    [SerializeField] private Button buttonQuit;
    [SerializeField] private Button buttonBack;

    [SerializeField] private GameObject InstructionsMenu;

    private void Awake()
    {
        //SoundManager.Instance.PlayBgMusic(Sounds.LobbyMusic);

        InstructionsMenu.SetActive(false);

        buttonPlay.onClick.AddListener(PlayGame);
        buttonCoop.onClick.AddListener(PlayCoop);
        buttonInstructions.onClick.AddListener(ShowInstructions);
        buttonQuit.onClick.AddListener(QuitGame);
        buttonBack.onClick.AddListener(GoBack);
        
    }

    private void Start()
    {
        SoundManager.Instance.PlayBgMusic(Sounds.LobbyMusic);
    }
    private void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SoundManager.Instance.Play(Sounds.GameStart);
        //InstructionsMenu.SetActive(true);
    }

    private void PlayCoop()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        SoundManager.Instance.Play(Sounds.GameStart);
    }

    private void ShowInstructions()
    {
        SoundManager.Instance.Play(Sounds.ButtonClick);
        InstructionsMenu.SetActive(true);
    }
    void GoBack()
    {
        SoundManager.Instance.Play(Sounds.ButtonBack);
        InstructionsMenu.SetActive(false);
    }

    private void QuitGame()
    {
        SoundManager.Instance.Play(Sounds.ButtonBack);
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
