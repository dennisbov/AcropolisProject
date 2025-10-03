using System.IO;
using UnityEngine;

public class BinaryArrayCreator : MonoBehaviour
{
    [SerializeField] private bool createBinaryFile;

    
    public void SaveNumbersBinary(byte[] numbers)
    {
        string path = Path.Combine(Application.persistentDataPath, "distances.bin");
        using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
        {
            writer.Write(numbers.Length);
            foreach (byte number in numbers)
            {
                writer.Write(number);
            }
        }
    }

    public byte[] LoadNumbersBinary()
    {
        string path = Path.Combine(Application.persistentDataPath, "numbers.bin");
        if (!File.Exists(path)) return null;

        using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
        {
            int count = reader.ReadInt32();
            byte[] numbers = new byte[count];
            for (int i = 0; i < count; i++)
            {
                numbers[i] = reader.ReadByte();
            }
            return numbers;
        }
    }
}
