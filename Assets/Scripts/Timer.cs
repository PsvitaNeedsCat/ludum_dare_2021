using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public static Timer Instance { get { return s_instance; } }

    public float TimeAlive { get; private set; } = 0.0f;

    [SerializeField]
    private TextMeshProUGUI m_minutesText = null;
    [SerializeField]
    private TextMeshProUGUI m_secondsText = null;

    private static Timer s_instance = null;

    private void Awake()
    {
        if (s_instance != null && s_instance != this)
        {
            Destroy(this);
            return;
        }

        s_instance = this;
    }

    private void Update()
    {
        TimeAlive += Time.deltaTime;

        int minutes = Mathf.FloorToInt(TimeAlive / 60.0f);
        int seconds = Mathf.FloorToInt(TimeAlive % 60.0f);

        m_minutesText.text = minutes.ToString();
        m_secondsText.text = (seconds < 10) ? "0" + seconds.ToString() : seconds.ToString();
    }
}
