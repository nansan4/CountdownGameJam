using UnityEngine;

public enum ComponentType
{
    None,
    Resistor,
    Capacitor,
    Transformer,
    IntegratedCircuit
}

public class ComponentScript : MonoBehaviour
{
    public ComponentType componentType;
    public string componentName;
}
