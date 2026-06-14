using UnityEngine;
using System.IO.Ports;

public class ArduinoReader : MonoBehaviour
{
    private SerialPort serialPort;

    [SerializeField]
    private string portName = "COM11";

    [SerializeField]
    private int baudRate = 9600;

    private void Start()
    {
        try
        {
            serialPort = new SerialPort(portName, baudRate);
            serialPort.ReadTimeout = 50;
            serialPort.Open();

            Debug.Log("Arduino verbunden auf " + portName);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Arduino Fehler: " + e.Message);
        }
    }

    private void Update()
{
    if (serialPort == null || !serialPort.IsOpen)
        return;

    try
    {
        string data = serialPort.ReadLine().Trim();

        if (int.TryParse(data, out int keyId))
        {
            SoundboardManager.Instance?.OnButtonPressed(keyId);
        }
    }
    catch (System.TimeoutException)
    {
        // normal, ignorieren
    }
    catch (System.Exception e)
    {
        Debug.LogWarning(e.Message);
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