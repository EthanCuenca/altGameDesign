using System.Collections;
using System.IO.Ports;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotaryEncoderInput : MonoBehaviour
{
    private SerialPort serialPort;
    private int previousEncoderPosition;
    [SerializeField] private GameObject background;
    public float rotationAmount = 5f; // Adjust this value as needed


    private IEnumerator Start()
    {
        // InputSystem.pollingFrequency = 120; // Poll gamepads at 120 Hz (adjust as needed)
        serialPort = new SerialPort("COM3", 9600);
        serialPort.Open();
        Debug.Log("Serial port opened");
        previousEncoderPosition = 0;

        // Start the asynchronous communication coroutine
        yield return StartCoroutine(ReadEncoderData());
    }

    private IEnumerator ReadEncoderData()
    {
        while (true)
        {
            if (serialPort.IsOpen)
            {
                string data = serialPort.ReadLine();
                if (int.TryParse(data, out int encoderPosition))
                {
                    int rotationDirection = encoderPosition - previousEncoderPosition;
                    if (rotationDirection > 0)
                    {
                        background.transform.Rotate(Vector3.back * rotationAmount);
                        // SimulateKeyPress('D');
                    }
                    else if (rotationDirection < 0)
                    {
                        // SimulateKeyPress('A');
                        background.transform.Rotate(Vector3.forward * rotationAmount);
                    }
                    previousEncoderPosition = encoderPosition;
                }

                else
                {
                    Debug.Log("Failed to parse encoder position");
                }
            }

            yield return new WaitForSeconds(0.02f); // Adjust the polling rate as needed
        }
    }


    // private void SimulateKeyPress(char key)
    // {
    //     // Implement your logic to simulate key presses in Unity
    //     // You can use Input.GetKey or other methods here
    //     Debug.Log($"Simulating key press: {key}");

    //     // Rotate the background based on the key
    //     float rotationAmount = 3f; // Adjust this value as needed
    //     if (key == 'A')
    //         background.transform.Rotate(Vector3.forward * rotationAmount);
    //     else if (key == 'D')
    //         background.transform.Rotate(Vector3.back * rotationAmount);
    // }

    private void OnDestroy()
    {
        // Close the serial port when the script is destroyed
        serialPort.Close();
    }
}
