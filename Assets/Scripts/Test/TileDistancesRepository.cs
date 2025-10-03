using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class TileDistancesRepository : MonoBehaviour
{

    private const string FILE_NAME = "distances.bin";

    private byte[] _distances;
    private int _vertexCount;
    private void Awake()
    {
        StartCoroutine(LoadStaticData());
    }

    public int GetDistance(int from, int to)
    {
        return _distances[from * _vertexCount + to];
    }

    private IEnumerator LoadStaticData()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, FILE_NAME);

        // ��� ������ �������� ������ ������ ������
#if UNITY_EDITOR || UNITY_STANDALONE
        // � Editor � Standalone ������ ��������
        _distances = LoadFromFile(filePath);


#elif UNITY_ANDROID || UNITY_WEBGL
        yield return StartCoroutine(LoadFromWebRequest(filePath));
        
#endif

        if (_distances != null)
        {
            yield return null;
        }
    }

    private byte[] LoadFromFile(string path)
    {
        if (!File.Exists(path))
        {
            Debug.LogError($"���� �� ������: {path}");
            return null;
        }

        using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
        {
            int distancesCount = reader.ReadInt32();
            _vertexCount = reader.ReadInt32();
            return reader.ReadBytes(distancesCount);
        }
    }

    private IEnumerator LoadFromWebRequest(string path)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(path))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"������ ��������: {www.error}");
                yield break;
            }

            // �������� ����� �� ����������
            byte[] data = www.downloadHandler.data;

            // ������ ����� ���������� ���� ������
            using (MemoryStream stream = new MemoryStream(data))
            using (BinaryReader reader = new BinaryReader(stream))
            {
                int count = reader.ReadInt32();
                byte[] numbers = reader.ReadBytes(count);
                // ����������� numbers �����
                Debug.Log($"��������� ����� WebRequest: {numbers.Length} �����");
            }
        }
    }
}
