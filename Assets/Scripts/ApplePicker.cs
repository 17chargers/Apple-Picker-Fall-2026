using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ApplePicker : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject basketPrefab;
    public int numBaskets = 4;
    public float basketBottomY = -14f;
    public float basketSpacingY = 2f;
    public List<GameObject> basketList;
    public GameObject gameOverPanel;
    public Text roundText;
    public int totalRound = 4;
    public float roundDuration = 10f;
    public float baseGravity = -9.81f;
    public Text gameOverTitle;

    public float[] gravityMult = {1f, 1.2f, 1.4f, 1.6f};
    public float[] dropDelay = {1f, 0.8f, 0.65f, 0.5f};
    public float[] treeSpeed = {1f, 1.25f, 1.5f, 2f};

    private AppleTree tree;
    private float baseTreeSpeed, baseDropDelay, roundTimer;
    private int currentRound;
    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        basketList = new List<GameObject>();
        for (int i = 0; i < numBaskets; i++)
        {
            GameObject tBasketGO = Instantiate<GameObject>( basketPrefab);
            Vector3 pos = Vector3.zero;
            pos.y = basketBottomY + (basketSpacingY * i );
            tBasketGO.transform.position = pos;
            basketList.Add(tBasketGO);
        }    

        Time.timeScale = 1f;
        gameOverPanel.SetActive(false);

        tree = FindObjectOfType<AppleTree>();
        baseTreeSpeed = Mathf.Abs(tree.speed);
        baseDropDelay = tree.appleDropDelay;
        StartRound(1);
    }

    public void AppleMissed() {
        GameObject[] appleArray = GameObject.FindGameObjectsWithTag("Apple");
        foreach ( GameObject tempGO in appleArray ) {
            Destroy(tempGO);
        }

        GameObject[] poisonArray = GameObject.FindGameObjectsWithTag("PoisonApple");
        foreach ( GameObject tempGO in poisonArray ) {
            Destroy(tempGO);
        }

        int basketIndex = basketList.Count -1;
        GameObject basketGO = basketList[basketIndex];
        basketList.RemoveAt(basketIndex);
        Destroy(basketGO);

        if ( basketList.Count == 0 ) {
            EndGame(false);
        }
    }

    public void PoisonCaught(){
        if(gameOver) return;
        EndGame(false);
    }

    void Update(){
        if (gameOver) return;

        roundTimer -= Time.deltaTime;
        if (roundTimer <= 0f){
            if (currentRound >= totalRound) EndGame(true);
            else StartRound(currentRound + 1);
        }

        if (currentRound >= totalRound) EndGame(true);
    }

    void StartRound(int round){
        currentRound = round;
        roundTimer = roundDuration;
        int i = round -1;

        Physics.gravity = new Vector3(0f, baseGravity * gravityMult[i], 0f );
        tree.speed = Mathf.Sign(tree.speed) * baseTreeSpeed * treeSpeed[i];
        tree.appleDropDelay = baseDropDelay * dropDelay[i];

        roundText.text = "Round " + round + " / " + totalRound;
    }

    void EndGame(bool won){
        gameOver = true;
        gameOverTitle.text = won ? "You Win!" : "Game Over!";
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

}
