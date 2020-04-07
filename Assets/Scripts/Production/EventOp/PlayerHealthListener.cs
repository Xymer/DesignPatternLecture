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
            subscription = player.health.Subscribe(UpdateTextField);
        }
    }

    private void Start() //Construct myself 2 / 2
    {
        player = FindObjectOfType<Player>();
        subscription = player.health.Subscribe(UpdateTextField);
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