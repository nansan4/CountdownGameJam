using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DrawerManager : MonoBehaviour
{
    private const string EmptyPlaceholder = "__";

    #region Variables
    [Header("Buttons")]
    [SerializeField] private Button resistorButton;
    [SerializeField] private Button capacitorButton;
    [SerializeField] private Button transformerButton;
    [SerializeField] private Button integratedCircuitButton;
    [Header("Input Fields")]
    [SerializeField] private TMP_InputField resistorInputField;
    [SerializeField] private TMP_InputField capacitorInputField;
    [SerializeField] private TMP_InputField transformerInputField;
    [SerializeField] private TMP_InputField integratedCircuitInputField;
    #endregion

    #region Singleton
    public static DrawerManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    #region Button Callbacks
    public void OnResistorButtonClick()
    {
        string resistorValue = resistorInputField.text;
        if (resistorValue.Length == 1)
        {
            resistorValue = "0" + resistorValue; // Prepend a "0" if the value is a single digit
        }
        if (resistorValue == EmptyPlaceholder)
        {
            Debug.Log("Resistor value is empty.");
        }
        else
        {
            Debug.Log("Resistor value: " + resistorValue);
        }
        resistorInputField.text = EmptyPlaceholder; // Reset the input field to "__"
    }

    public void OnCapacitorButtonClick()
    {
        string capacitorValue = capacitorInputField.text;
        if (capacitorValue.Length == 1)
        {
            capacitorValue = "0" + capacitorValue; // Prepend a "0" if the value is a single digit
        }
        if (capacitorValue == EmptyPlaceholder)
        {
            Debug.Log("Capacitor value is empty.");
        }
        else
        {
            Debug.Log("Capacitor value: " + capacitorValue);
        }
        capacitorInputField.text = EmptyPlaceholder; // Reset the input field to "__"
    }

    public void OnTransformerButtonClick()
    {
        string transformerValue = transformerInputField.text;
        if (transformerValue.Length == 1)
        {
            transformerValue = "0" + transformerValue; // Prepend a "0" if the value is a single digit
        }
        if (transformerValue == EmptyPlaceholder)
        {
            Debug.Log("Transformer value is empty.");
        }
        else
        {
            Debug.Log("Transformer value: " + transformerValue);
        }
        transformerInputField.text = EmptyPlaceholder; // Reset the input field to "__"
    }

    public void OnIntegratedCircuitButtonClick()
    {
        string integratedCircuitValue = integratedCircuitInputField.text;
        if (integratedCircuitValue.Length == 1)
        {
            integratedCircuitValue = "0" + integratedCircuitValue; // Prepend a "0" if the value is a single digit
        }
        if (integratedCircuitValue == EmptyPlaceholder)
        {
            Debug.Log("Integrated Circuit value is empty.");
        }
        else
        {
            Debug.Log("Integrated Circuit value: " + integratedCircuitValue);
        }
        integratedCircuitInputField.text = EmptyPlaceholder; // Reset the input field to "__"
    }
    #endregion
}