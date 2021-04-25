using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_minutesText = null;

    [SerializeField]
    private TextMeshProUGUI m_secondsText = null;

    [SerializeField]
    private TextMeshProUGUI m_loanTakenText = null;

    private void Start()
    {
        int minutes = Mathf.FloorToInt(PlayerPrefs.GetInt("TimeAlive", 0) / 60.0f);
        int seconds = Mathf.FloorToInt(PlayerPrefs.GetInt("TimeAlive", 0) % 60.0f);

        m_minutesText.text = minutes.ToString();
        m_secondsText.text = (seconds < 10) ? "0" + seconds.ToString() : seconds.ToString();

        m_loanTakenText.text = "Loan Taken: $" + PlayerPrefs.GetInt("LoanTaken", 0).ToString();
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("menu_scene");
    }
}
