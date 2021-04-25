using UnityEngine;
using System.Collections.Generic;

public class Bank : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Transform m_bankObject = null;
    [SerializeField]
    private GameObject m_interactIcon = null;
    [SerializeField]
    private Transform[] m_spawns = { };

    [Header("Values")]
    [SerializeField, Tooltip("Does not update at runtime")]
    private float m_maxDistanceFromPlayer = 2.0f;

    private Transform m_playerTransform = null;
    private List<Transform> m_shuffleBagSpawns = new List<Transform>();

    private void Start()
    {
        m_playerTransform = PlayerMovement.Instance.transform;

        m_maxDistanceFromPlayer = m_maxDistanceFromPlayer * m_maxDistanceFromPlayer;

        CreateShuffleBag();
    }

    private void Update()
    {
        float distance = (m_playerTransform.position - m_bankObject.position).sqrMagnitude;

        if (distance <= m_maxDistanceFromPlayer)
        {
            // Update icon
            if (!m_interactIcon.activeSelf)
            {
                m_interactIcon.SetActive(true);
            }

            // Check for interaction
            if (Input.GetKeyDown(KeyCode.E))
            {
                TakeLoan();
            }
        }
        else if (distance > m_maxDistanceFromPlayer)
        {
            // Update icon
            if (m_interactIcon.activeSelf)
            {
                m_interactIcon.SetActive(false);
            }
        }
    }

    private void TakeLoan()
    {
        Loan.Instance.TakeLoan();

        m_bankObject.position = m_shuffleBagSpawns[0].position;

        if (m_shuffleBagSpawns.Count <= 1)
        {
            CreateShuffleBag();
        }
        else
        {
            m_shuffleBagSpawns.RemoveAt(0);
        }
    }

    private void CreateShuffleBag()
    {
        List<Transform> spawns = new List<Transform>();
        Transform previousPoint = (m_shuffleBagSpawns.Count == 1) ? m_shuffleBagSpawns[0] : null;

        // Get spawns except previous one
        for (int i = 0; i < m_spawns.Length; i++)
        {
            if (previousPoint == null || previousPoint != m_spawns[i])
            {
                spawns.Add(m_spawns[i]);
            }
        }

        m_shuffleBagSpawns.Clear();

        // Go through all the spawns and randomly shuffle them into the bag
        for (int i = 0; i < m_spawns.Length; i++)
        {
            int index = Random.Range(0, spawns.Count);

            m_shuffleBagSpawns.Add(spawns[index]);
            spawns.RemoveAt(index);

            // Add previous point back into the bag
            if (i == 0 && previousPoint != null)
            {
                spawns.Add(previousPoint);
            }
        }
    }
}
