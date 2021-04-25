using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    private const float I_FRAME_TIME = 1.0f;

    [SerializeField]
    private Image m_healthBarImage = null;

    [SerializeField]
    private TextMeshProUGUI m_amountText = null;

    [SerializeField]
    private TextMeshProUGUI m_maxHealthText = null;

    public int Health
    {
        get { return m_health; }
        set
        {
            if (m_iFrameTimer > 0.0f)
            {
                return;
            }

            m_health = Mathf.Clamp(value, 0, m_maxHealth);
            m_amountText.text = m_health.ToString();
            UpdateBar();

            m_iFrameTimer = I_FRAME_TIME;
        }
    }

    public int MaxHealth
    {
        get { return m_maxHealth; }
        set
        {
            m_maxHealth = Mathf.Clamp(value, 1, int.MaxValue);
            m_maxHealthText.text = m_maxHealth.ToString();
            UpdateBar();
        }
    }

    public static HealthBar Instance { get { return s_instance; } }

    private int m_health = 99;
    private int m_maxHealth = 99;
    private float m_iFrameTimer = 0.0f;

    private static HealthBar s_instance = null;

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
        if (m_iFrameTimer > 0.0f)
        {
            m_iFrameTimer -= Time.deltaTime;
        }
    }

    private void UpdateBar()
    {
        m_healthBarImage.fillAmount = (float)m_health / (float)m_maxHealth;

        if (m_health == 0)
        {
            PlayerPrefs.SetInt("LoanTaken", Loan.Instance.LoanTaken);
            PlayerPrefs.SetInt("TimeAlive", Mathf.FloorToInt(Timer.Instance.TimeAlive));
            SceneManager.LoadScene("end_scene");
        }
    }
}
