using UnityEngine;
using System.IO.Ports;

public class ArduinoReader : MonoBehaviour
{
    private SerialPort serialPort;

    [SerializeField]
    private string portName = "COM11";

    [SerializeField]
    private int baudRate = 9600;

    private bool arduinoConnected = false;

    private void Start()
    {
        try
        {
            serialPort = new SerialPort(portName, baudRate);
            serialPort.ReadTimeout = 50;
            serialPort.Open();

            arduinoConnected = true;

            Debug.Log("Arduino verbunden auf " + portName);
        }
        catch
        {
            Debug.Log("Arduino nicht gefunden. Tastatur-Fallback aktiv.");
        }
    }

    private void Update()
{
    if (!arduinoConnected)
        return;

    try
    {
        string data = serialPort.ReadLine().Trim();

        Debug.Log("Arduino Daten: [" + data + "]");

        if (int.TryParse(data, out int keyId))
        {
            Debug.Log("Sende Taste: " + keyId);

            SoundboardManager.Instance?.OnButtonPressed(keyId);
        }
        else
        {
            Debug.Log("Konnte nicht parsen: " + data);
        }
    }
    catch (System.TimeoutException)
    {
    }
}

    private void OnApplicationQuit()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}