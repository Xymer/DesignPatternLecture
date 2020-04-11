using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthListener : MonoBehaviour
{
    private const string m_PlayerHealth = "Player Health: ";
    private const string m_GameOver = "Game Over";

    [SerializeField] private Text m_PlayerHealthText;
    [SerializeField] private Text m_GameOverText;
    private Player m_Player;
    private IDisposable m_Subscription;

    private void Awake() //Construct myself 1 / 2
    {

    }

    private void OnEnable()
    {
        if (m_Player != null) // Is this the first OnEnable call?
        {
            m_Subscription = m_Player.Health.Subscribe(UpdateTextField);
        }
        m_GameOverText.gameObject.SetActive(false);
       
    }

    private void Start() //Construct myself 2 / 2
    {
        m_Player = FindObjectOfType<Player>();
        m_Subscription = m_Player.Health.Subscribe(UpdateTextField);
        // (intValue) =>
        // {
        //     Debug.Log("I still alive!");
        // };
    }

    private void OnDisable()
    {
        m_Subscription.Dispose();
    }

    private void UpdateTextField(int playerHealth)
    {
        if (playerHealth <= 0)
        {
            m_GameOverText.gameObject.SetActive(true);
        }
        else
        {
            m_GameOverText.gameObject.SetActive(false);
        }
        m_PlayerHealthText.text = m_PlayerHealth + playerHealth.ToString();
    }
}