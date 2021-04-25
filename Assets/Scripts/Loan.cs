using UnityEngine;
using TMPro;

public class Loan : MonoBehaviour
{
    public int LoanTaken 
    {
        get { return m_loanTaken; } 
        set
        {
            m_loanTaken = value;
            UpdateUI();
        }
    }

    public static Loan Instance { get { return s_instance; } }

    [Header("References")]
    [SerializeField]
    private TextMeshProUGUI m_amountText = null;

    [Header("Values")]
    [SerializeField]
    private int m_moneyPerLoan = 100;
    [SerializeField]
    private int m_healthPerLoam = 1;

    private int m_loanTaken = 0;

    private static Loan s_instance = null;

    private void Awake()
    {
        if (s_instance != null && s_instance != this)
        {
            Destroy(this);
            return;
        }

        s_instance = this;
    }

    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        // Temp
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeLoan();
        }
    }

    public void TakeLoan()
    {
        LoanTaken += m_moneyPerLoan;

        HealthBar.Instance.Health += m_healthPerLoam;
    }

    private void UpdateUI()
    {
        m_amountText.text = m_loanTaken.ToString();
    }
}
