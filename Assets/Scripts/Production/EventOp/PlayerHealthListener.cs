using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthListener : MonoBehaviour
{
    [SerializeField] private Text textField;

    private Player player;
    private IDisposable subscription;

    private void Awake() //Construct myself 1 / 2
    {

    }

    private void OnEnable()
    {
        if (player != null) // Is this the first OnEnable call?
        {
            subscription = player.m_Health.Subscribe(UpdateTextField);
        }
    }

    private void Start() //Construct myself 2 / 2
    {
        player = FindObjectOfType<Player>();
        subscription = player.m_Health.Subscribe(UpdateTextField);
        // (intValue) =>
        // {
        //     Debug.Log("I still alive!");
        // };
    }

    private void OnDisable()
    {
        subscription.Dispose();
    }

    private void UpdateTextField(int playerHealth)
    {
        textField.text = playerHealth.ToString();
    }
}