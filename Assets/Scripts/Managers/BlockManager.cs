using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    #region Singleton
    private static BlockManager _instance;
    public static BlockManager Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    /*[System.Serializable]
    public class BallPool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }*/
    public int blockAmount;
    public bool maxBlocks = false;
    public GameObject blockPrefab;

    public GameObject[] blockSpaces;

    public Material[] basicMaterials;
    public Material[] specialMaterials;
    public Material[] capsuleMaterials;
    public float length;
    public float height;

    Vector3 startPosition1 = new Vector3(-3.8f, 5, -7);
    Quaternion startRotation1 = Quaternion.Euler(0, 0, 0);
    Vector3 startPosition2 = new Vector3(7, 5, -3.8f);
    Quaternion startRotation2 = Quaternion.Euler(0, -90, 0);
    Vector3 startPosition3 = new Vector3(3.8f, 5, 7);
    Quaternion startRotation3 = Quaternion.Euler(0, 180, 0);
    Vector3 startPosition4 = new Vector3(-7, 5, 3.8f);
    Quaternion startRotation4 = Quaternion.Euler(0, 90, 0);

    Vector3[] startPositions = { new Vector3(-3.8f, 5, -7), new Vector3(7, 5, -3.8f), new Vector3(3.8f, 5, 7), new Vector3(-7, 5, 3.8f) };
    Quaternion[] startRotations = { Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, -90, 0), Quaternion.Euler(0, 180, 0), Quaternion.Euler(0, 90, 0) };


    Vector3[] displaces = { new Vector3(+0.4f, 0, 0), new Vector3(0, 0, 0.4f), new Vector3(-0.4f, 0, 0), new Vector3(0, 0, -0.4f) };

    public float[] blockTypeProbabilities = { 70, 10, 5f, 2.5f, 5f, 2.5f, 2, 1, 2 };

    public List<GameObject> ActiveBlocks = new List<GameObject>();

    public List<GameObject>[] Blocks = new List<GameObject>[4];
    public bool[] movingSpace = { false, false, false, false };
    /*public List<GameObject> Blocks1 = new List<GameObject>();
    public List<GameObject> Blocks2 = new List<GameObject>();
    public List<GameObject> Blocks3 = new List<GameObject>();
    public List<GameObject> Blocks4 = new List<GameObject>();*/

    public Queue<GameObject> InactiveBlocks = new Queue<GameObject>();

    int currentSpawningSpace = 0;
    bool movingTimer = false;
    float time = 0;


    void Start()
    {
        //basicMaterials = Resources.LoadAll<Material>("Assets/Materials/Blocks/Basic");
        height = GameSceneManager.Instance.height;
        length = GameSceneManager.Instance.width;
        height -= 3f;
        Blocks[0] = new List<GameObject>();
        Blocks[1] = new List<GameObject>();
        Blocks[2] = new List<GameObject>();
        Blocks[3] = new List<GameObject>();

        SpawnRandomRow();
        TimerText.Instance.resetTextTimer(30);
        //Debug.Log(basicMaterials.Length);
        //for (int j = 0; j < 4; j++) {
        /*Vector3 positionIter = startPosition1;
        for (int i = 0; i < blockAmount; i++)
        {
            GameObject obj = Instantiate(blockPrefab, positionIter, startRotation1, blockSpaces[0].transform);
            //Debug.Log(basicMaterials.Length);
            Material mat;
            mat = basicMaterials[Random.Range(0, 8)];
            float dice = Random.Range(0.0f, 100.0f);
            //Debug.Log(dice);
            if (dice < blockTypeProbabilities[1])
            {
                obj.GetComponent<BlockScript>().type = 1;
                mat = specialMaterials[0];
            }
            else if (dice < blockTypeProbabilities[2] + blockTypeProbabilities[1])
            {
                obj.GetComponent<BlockScript>().type = 2;
                mat = specialMaterials[1];
            }
            else if (dice < blockTypeProbabilities[3] + blockTypeProbabilities[2] + blockTypeProbabilities[1])
            {
                obj.GetComponent<BlockScript>().type = 3;
                mat = specialMaterials[2];
            }
            else if (dice < blockTypeProbabilities[4] + blockTypeProbabilities[3] + blockTypeProbabilities[2] + blockTypeProbabilities[1])
            {
                obj.GetComponent<BlockScript>().type = 4;
                mat = specialMaterials[3];
            }
            else if (dice < blockTypeProbabilities[5] + blockTypeProbabilities[4] + blockTypeProbabilities[3] + blockTypeProbabilities[2] + blockTypeProbabilities[1])
            {
                obj.GetComponent<BlockScript>().type = 5;
                mat = specialMaterials[4];
            }
            else if (dice < blockTypeProbabilities[6] + blockTypeProbabilities[5] + blockTypeProbabilities[4] + blockTypeProbabilities[3] + blockTypeProbabilities[2] + blockTypeProbabilities[1])
            {
                obj.GetComponent<BlockScript>().type = 6;
                mat = specialMaterials[5];
            }
            else if (dice < blockTypeProbabilities[7] + blockTypeProbabilities[6] + blockTypeProbabilities[5] + blockTypeProbabilities[4] + blockTypeProbabilities[3] + blockTypeProbabilities[2] + blockTypeProbabilities[1])
            {
                obj.GetComponent<BlockScript>().type = 7;
                mat = specialMaterials[6];
            }
            else if (dice < blockTypeProbabilities[8] + blockTypeProbabilities[7] + blockTypeProbabilities[6] + blockTypeProbabilities[5] + blockTypeProbabilities[4] + blockTypeProbabilities[3] + blockTypeProbabilities[2] + blockTypeProbabilities[1])
            {
                obj.GetComponent<BlockScript>().type = 8;
                mat = specialMaterials[7];
            }
            obj.GetComponent<MeshRenderer>().material = mat;

            ActiveBlocks.Add(obj);
            Blocks[0].Add(obj);
            //Debug.Log(obj.GetComponent<BlockScript>().type);

            if (positionIter.x + 0.4f <= startPosition1.x + length - 0.2f)
            {
                positionIter.x += 0.4f;
            }
            else
            {
                positionIter.x = startPosition1.x;
                positionIter.y -= 0.15f;
            }

            //obj.SetActive(false);
            //InactiveBalls.Enqueue(obj);
            //obj.GetComponent<BallScript>().activeIndex = i;
        }

        positionIter = startPosition2;
        for (int i = 0; i < blockAmount; i++)
        {
            GameObject obj = Instantiate(blockPrefab, positionIter, startRotation2, blockSpaces[1].transform);
            //Debug.Log(basicMaterials.Length);
            Material mat;
            mat = basicMaterials[Random.Range(0, 8)];
            float dice = Random.Range(0.0f, 100.0f);
            if (dice < blockTypeProbabilities[1])
            {
                obj.GetComponent<BlockScript>().type = 1;
                mat = specialMaterials[0];
            }
            else if (dice < blockTypeProbabilities[2] + blockTypeProbabilities[1])
            {
                obj.GetComponent<BlockScript>().type = 2;
                mat = specialMaterials[1];
            }
            else if (dice < blockTypeProbabilities[3] + blockTypeProbabilities[2] + blockTypeProbabilities[1])
            {
                obj.GetComponent<BlockScript>().type = 3;
                mat = specialMaterials[2];
            }
            else if (dice < blockTypeProbabilities[4] + blockTypeProbabilities[3] + blockTypeProbabilities[2] + blockTypeProbabilities[1])
            {
                obj.GetComponent<BlockScript>().type = 4;
                mat = specialMaterials[3];
            }
            else if (dice < blockTypeProbabilities[5] + blockTypeProbabilities[4] + blockTypeProbabilities[3] + blockTypeProbabilities[2] + blockTypeProbabilities[1])
            {
                obj.GetComponent<BlockScript>().type = 5;
                mat = specialMaterials[4];
            }
            else if (dice < blockTypeProbabilities[6] + blockTypeProbabilities[5] + blockTypeProbabilities[4] + blockTypeProbabilities[3] + blockTypeProbabilities[2] + blockTypeProbabilities[1])
            {
                obj.GetComponent<BlockScript>().type = 6;
                mat = specialMaterials[5];
            }
            else if (dice < blockTypeProbabilities[7] + blockTypeProbabilities[6] + blockTypeProbabilities[5] + blockTypeProbabilities[4] + blockTypeProbabilities[3] + blockTypeProbabilities[2] + blockTypeProbabilities[1])
            {
                obj.GetComponent<BlockScript>().type = 7;
                mat = specialMaterials[6];
            }
            else if (dice < blockTypeProbabilities[8] + blockTypeProbabilities[7] + blockTypeProbabilities[6] + blockTypeProbabilities[5] + blockTypeProbabilities[4] + blockTypeProbabilities[3] + blockTypeProbabilities[2] + blockTypeProbabilities[1])
            {
                obj.GetComponent<BlockScript>().type = 8;
                mat = specialMaterials[7];
            }
            else
            {
                obj.GetComponent<BlockScript>().type = 0;
            }
            obj.GetComponent<MeshRenderer>().material = mat;
            ActiveBlocks.Add(obj);
            Blocks[0].Add(obj);

            if (positionIter.z + 0.4f <= startPosition2.z + length - 0.2f)
            {
                positionIter.z += 0.4f;
            }
            else
            {
                positionIter.z = startPosition2.z;
                positionIter.y -= 0.15f;
            }

            //obj.SetActive(false);
            //InactiveBalls.Enqueue(obj);
            //obj.GetComponent<BallScript>().activeIndex = i;
        }
        positionIter = startPosition3;
        for (int i = 0; i < blockAmount; i++)
        {
            GameObject obj = Instantiate(blockPrefab, positionIter, startRotation3, blockSpaces[2].transform);
            //Debug.Log(basicMaterials.Length);
            Material mat;
            mat = basicMaterials[Random.Range(0, 8)];
            float dice = Random.Range(0.0f, 100.0f);
            if (dice < blockTypeProbabilities[1])
            {
                obj.GetComponent<BlockScript>().type = 1;
                mat = specialMaterials[0];
            }
            else if (dice < blockTypeProbabilities[2] + blockTypeProbabilities[1])
            {
                obj.GetComponent<BlockScript>().type = 2;
                mat = specialMaterials[1];
            }
            else if (dice < blockTypeProbabilities[3] + blockTypeProbabilities[2] + blockTypeProbabilities[1])
            {
                obj.GetComponent<BlockScript>().type = 3;
                mat = specialMaterials[2];
            }
            else if (dice < blockTypeProbabilities[4] + blockTypeProbabilities[3] + blockTypeProbabilities[2] + blockTypeProbabilities[1])
            {
                obj.GetComponent<BlockScript>().type = 4;
                mat = specialMaterials[3];
            }
            else if (dice < blockTypeProbabilities[5] + blockTypeProbabilities[4] + blockTypeProbabilities[3] + blockTypeProbabilities[2] + blockTypeProbabilities[1])
            {
                obj.GetComponent<BlockScript>().type = 5;
                mat = specialMaterials[4];
            }
            else if (dice < blockTypeProbabilities[6] + blockTypeProbabilities[5] + blockTypeProbabilities[4] + blockTypeProbabilities[3] + blockTypeProbabilities[2] + blockTypeProbabilities[1])
            {
                obj.GetComponent<BlockScript>().type = 6;
                mat = specialMaterials[5];
            }
            else if (dice < blockTypeProbabilities[7] + blockTypeProbabilities[6] + blockTypeProbabilities[5] + blockTypeProbabilities[4] + blockTypeProbabilities[3] + blockTypeProbabilities[2] + blockTypeProbabilities[1])
            {
                obj.GetComponent<BlockScript>().type = 7;
                mat = specialMaterials[6];
            }
            else if (dice < blockTypeProbabilities[8] + blockTypeProbabilities[7] + blockTypeProbabilities[6] + blockTypeProbabilities[5] + blockTypeProbabilities[4] + blockTypeProbabilities[3] + blockTypeProbabilities[2] + blockTypeProbabilities[1])
            {
                obj.GetComponent<BlockScript>().type = 8;
                mat = specialMaterials[7];
            }
            else
            {
                obj.GetComponent<BlockScript>().type = 0;
            }
            obj.GetComponent<MeshRenderer>().material = mat;
            ActiveBlocks.Add(obj);
            Blocks[0].Add(obj);

            if (positionIter.x - 0.4f >= startPosition3.x - length + 0.2f)
            {
                positionIter.x -= 0.4f;
            }
            else
            {
                positionIter.x = startPosition3.x;
                positionIter.y -= 0.15f;
            }

            //obj.SetActive(false);
            //InactiveBalls.Enqueue(obj);
            //obj.GetComponent<BallScript>().activeIndex = i;
        }
        positionIter = startPosition4;
        for (int i = 0; i < blockAmount; i++)
        {
            GameObject obj = Instantiate(blockPrefab, positionIter, startRotation4, blockSpaces[3].transform);
            //Debug.Log(basicMaterials.Length);
            Material mat;
            mat = basicMaterials[Random.Range(0, 8)];
            float dice = Random.Range(0.0f, 100.0f);
            if (dice < blockTypeProbabilities[1])
            {
                obj.GetComponent<BlockScript>().type = 1;
                mat = specialMaterials[0];
            }
            else if (dice < blockTypeProbabilities[2] + blockTypeProbabilities[1])
            {
                obj.GetComponent<BlockScript>().type = 2;
                mat = specialMaterials[1];
            }
            else if (dice < blockTypeProbabilities[3] + blockTypeProbabilities[2] + blockTypeProbabilities[1])
            {
                obj.GetComponent<BlockScript>().type = 3;
                mat = specialMaterials[2];
            }
            else if (dice < blockTypeProbabilities[4] + blockTypeProbabilities[3] + blockTypeProbabilities[2] + blockTypeProbabilities[1])
            {
                obj.GetComponent<BlockScript>().type = 4;
                mat = specialMaterials[3];
            }
            else if (dice < blockTypeProbabilities[5] + blockTypeProbabilities[4] + blockTypeProbabilities[3] + blockTypeProbabilities[2] + blockTypeProbabilities[1])
            {
                obj.GetComponent<BlockScript>().type = 5;
                mat = specialMaterials[4];
            }
            else if (dice < blockTypeProbabilities[6] + blockTypeProbabilities[5] + blockTypeProbabilities[4] + blockTypeProbabilities[3] + blockTypeProbabilities[2] + blockTypeProbabilities[1])
            {
                obj.GetComponent<BlockScript>().type = 6;
                mat = specialMaterials[5];
            }
            else if (dice < blockTypeProbabilities[7] + blockTypeProbabilities[6] + blockTypeProbabilities[5] + blockTypeProbabilities[4] + blockTypeProbabilities[3] + blockTypeProbabilities[2] + blockTypeProbabilities[1])
            {
                obj.GetComponent<BlockScript>().type = 7;
                mat = specialMaterials[6];
            }
            else if (dice < blockTypeProbabilities[8] + blockTypeProbabilities[7] + blockTypeProbabilities[6] + blockTypeProbabilities[5] + blockTypeProbabilities[4] + blockTypeProbabilities[3] + blockTypeProbabilities[2] + blockTypeProbabilities[1])
            {
                obj.GetComponent<BlockScript>().type = 8;
                mat = specialMaterials[7];
            }
            else
            {
                obj.GetComponent<BlockScript>().type = 0;
            }
            obj.GetComponent<MeshRenderer>().material = mat;
            ActiveBlocks.Add(obj);
            Blocks[0].Add(obj);

            if (positionIter.z - 0.4f >= startPosition4.z - length + 0.2f)
            {
                positionIter.z -= 0.4f;
            }
            else
            {
                positionIter.z = startPosition4.z;
                positionIter.y -= 0.15f;
            }

            //obj.SetActive(false);
            //InactiveBalls.Enqueue(obj);
            //obj.GetComponent<BallScript>().activeIndex = i;
        }
        //}*/
    }

    /*public GameObject ActivateBall()
    {
        GameObject ball = InactiveBalls.Dequeue();
        ActiveBalls.Add(ball);
        ball.SetActive(true);
        return ball;

    }
    */
    public void DeactivateBlock(GameObject block)
    {
        if (ActiveBlocks.Count > 0)
        {
            ActiveBlocks.Remove(block);
            if (Blocks[0].Remove(block))
            {
                resetMovement(0);
            }
            else if (Blocks[1].Remove(block))
            {
                resetMovement(1);
            }
            else if (Blocks[2].Remove(block))
            {
                resetMovement(2);
            }
            else if (Blocks[3].Remove(block))
            {
                resetMovement(3);
            }
            block.transform.parent = null;
            InactiveBlocks.Enqueue(block);
            block.SetActive(false);

        }
    }

    public void DeactivateAll()
    {
        for (int i = ActiveBlocks.Count - 1; i >= 0; i--)
        {
            GameObject block = ActiveBlocks[i];
            ActiveBlocks.Remove(block);
            Blocks[0].Remove(block);
            Blocks[1].Remove(block);
            Blocks[2].Remove(block);
            Blocks[3].Remove(block);
            block.transform.parent = null;
            InactiveBlocks.Enqueue(block);
            block.SetActive(false);
        }
        resetMovement(0);
        resetMovement(1);
        resetMovement(2);
        resetMovement(3);
    }

    public void SpawnRandomRow()
    {
        SpawnRow(Random.Range(0, 3));
    }

    public void SpawnRow(int space)
    {
        Vector3 positionIter = startPosition1;

        movingSpace[space] = true;
        for (int i = 0; i < 20; i++)
        {
            positionIter = startPositions[space] + displaces[space] * i;
            GameObject block = SpawnBlock(positionIter, startRotations[space], blockSpaces[space].transform, Blocks[space]);
            resetBlockFunction(block);
        }
    }
    /*
    public GameObject SpawnBlock()
    {
        if (InactiveBlocks.Count > 0)
        {
            GameObject block = InactiveBlocks.Dequeue();
            ActiveBlocks.Add(block);
            block.SetActive(true);
            return block;
        }
        else
        {
            GameObject block = Instantiate(blockPrefab);
            block.SetActive(true);
            return block;
        }
    }*/

    public GameObject SpawnBlock(Vector3 position, Quaternion rotation, Transform parent, List<GameObject> Space)
    {
        if (InactiveBlocks.Count > 0)
        {
            GameObject block = InactiveBlocks.Dequeue();
            block.SetActive(true);
            block.transform.position = position;
            block.transform.rotation = rotation;
            block.transform.parent = parent;
            resetBlockFunction(block);
            ActiveBlocks.Add(block);
            Space.Add(block);
            return block;
        }
        else
        {
            GameObject block = Instantiate(blockPrefab, position, rotation, parent);
            block.SetActive(true);
            resetBlockFunction(block);
            ActiveBlocks.Add(block);
            Space.Add(block);
            return block;
        }

    }

    public GameObject resetBlockFunction(GameObject obj)
    {
        Material mat;
        mat = basicMaterials[Random.Range(0, 8)];
        float dice = Random.Range(0.0f, 100.0f);
        if (dice < blockTypeProbabilities[1])
        {
            obj.GetComponent<BlockScript>().type = 1;
            mat = specialMaterials[0];
        }
        else if (dice < blockTypeProbabilities[2] + blockTypeProbabilities[1])
        {
            obj.GetComponent<BlockScript>().type = 2;
            mat = specialMaterials[1];
        }
        else if (dice < blockTypeProbabilities[3] + blockTypeProbabilities[2] + blockTypeProbabilities[1])
        {
            obj.GetComponent<BlockScript>().type = 3;
            mat = specialMaterials[2];
        }
        else if (dice < blockTypeProbabilities[4] + blockTypeProbabilities[3] + blockTypeProbabilities[2] + blockTypeProbabilities[1])
        {
            obj.GetComponent<BlockScript>().type = 4;
            mat = specialMaterials[3];
        }
        else if (dice < blockTypeProbabilities[5] + blockTypeProbabilities[4] + blockTypeProbabilities[3] + blockTypeProbabilities[2] + blockTypeProbabilities[1])
        {
            obj.GetComponent<BlockScript>().type = 5;
            mat = specialMaterials[4];
        }
        else if (dice < blockTypeProbabilities[6] + blockTypeProbabilities[5] + blockTypeProbabilities[4] + blockTypeProbabilities[3] + blockTypeProbabilities[2] + blockTypeProbabilities[1])
        {
            obj.GetComponent<BlockScript>().type = 6;
            mat = specialMaterials[5];
        }
        else if (dice < blockTypeProbabilities[7] + blockTypeProbabilities[6] + blockTypeProbabilities[5] + blockTypeProbabilities[4] + blockTypeProbabilities[3] + blockTypeProbabilities[2] + blockTypeProbabilities[1])
        {
            obj.GetComponent<BlockScript>().type = 7;
            mat = specialMaterials[6];
        }
        else if (dice < blockTypeProbabilities[8] + blockTypeProbabilities[7] + blockTypeProbabilities[6] + blockTypeProbabilities[5] + blockTypeProbabilities[4] + blockTypeProbabilities[3] + blockTypeProbabilities[2] + blockTypeProbabilities[1])
        {
            obj.GetComponent<BlockScript>().type = 8;
            mat = specialMaterials[7];
        }
        else
        {
            obj.GetComponent<BlockScript>().type = 0;
        }
        obj.GetComponent<MeshRenderer>().material = mat;

        return obj;
    }

    private void Update()
    {
        if (movingTimer && TimerText.Instance.timerOn)
        {
            if (movingSpace[0])
            {
                blockSpaces[0].transform.Translate(new Vector3(0, -Time.deltaTime / 8, 0));
            }
            if (movingSpace[1])
            {
                blockSpaces[1].transform.Translate(new Vector3(0, -Time.deltaTime / 8, 0));
            }
            if (movingSpace[2])
            {
                blockSpaces[2].transform.Translate(new Vector3(0, -Time.deltaTime / 8, 0));
            }
            if (movingSpace[3])
            {
                blockSpaces[3].transform.Translate(new Vector3(0, -Time.deltaTime / 8, 0));
            }
        }

        if (TimerText.Instance.checkTextTime() <= 0 )
        {
            if (movingTimer)
            {
                SpawnRandomRow();
                movingTimer = false;
                TimerText.Instance.resetTextTimer(30f);
                //time = 0;
            }
            else
            {
                movingTimer = true;
                TimerText.Instance.resetTimer(1.2f);
                //time = 0;
            }
        }
        

       // time += Time.deltaTime;

        if (Input.GetButtonDown("DebugButton9"))
        {
            Debug.Log("Debug button9");
            DeactivateAll();
        }
    }

    public void resetMovement(int blockSpace)
    {
        if (Blocks[blockSpace].Count == 0)
        {
            movingSpace[blockSpace] = false;

            blockSpaces[blockSpace].transform.position = Vector3.zero;
        }
    }


}

