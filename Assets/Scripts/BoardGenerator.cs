using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    [Header("Board Settings")]
    public GameObject tilePrefab;
    public GameObject snakePrefab;
    public GameObject ladderPrefab;
    public int boardSize = 100;
    public float tileSpacing = 1f;
    
    [Header("Visual Settings")]
    public Color lightTileColor = Color.white;
    public Color darkTileColor = Color.gray;
    public Material snakeMaterial;
    public Material ladderMaterial;
    
    private Transform[] tilePositions;
    
    void Start()
    {
        GenerateBoard();
        PlaceSnakesAndLadders();
    }
    
    void GenerateBoard()
    {
        tilePositions = new Transform[boardSize + 1];
        
        int tilesPerRow = 10;
        bool leftToRight = true;
        
        for (int i = 1; i <= boardSize; i++)
        {
            int row = (i - 1) / tilesPerRow;
            int col = (i - 1) % tilesPerRow;
            
            // Alternate direction for snake-ladder board pattern
            if (!leftToRight)
                col = tilesPerRow - 1 - col;
            
            Vector3 position = new Vector3(col * tileSpacing, row * tileSpacing, 0);
            GameObject tile = Instantiate(tilePrefab, position, Quaternion.identity, transform);
            
            // Set tile color (checkerboard pattern)
            Renderer tileRenderer = tile.GetComponent<Renderer>();
            if (tileRenderer != null)
            {
                tileRenderer.material.color = ((row + col) % 2 == 0) ? lightTileColor : darkTileColor;
            }
            
            // Add tile number
            GameObject numberText = new GameObject("TileNumber");
            numberText.transform.SetParent(tile.transform);
            numberText.transform.localPosition = Vector3.zero;
            
            TextMesh textMesh = numberText.AddComponent<TextMesh>();
            textMesh.text = i.ToString();
            textMesh.fontSize = 20;
            textMesh.color = Color.black;
            textMesh.anchor = TextAnchor.MiddleCenter;
            
            tilePositions[i] = tile.transform;
            
            // Switch direction at end of row
            if (i % tilesPerRow == 0)
                leftToRight = !leftToRight;
        }
        
        // Position 0 (start position)
        GameObject startTile = Instantiate(tilePrefab, new Vector3(-tileSpacing, -tileSpacing, 0), Quaternion.identity, transform);
        startTile.GetComponent<Renderer>().material.color = Color.green;
        tilePositions[0] = startTile.transform;
        
        // Add "START" text
        GameObject startText = new GameObject("StartText");
        startText.transform.SetParent(startTile.transform);
        startText.transform.localPosition = Vector3.zero;
        
        TextMesh startTextMesh = startText.AddComponent<TextMesh>();
        startTextMesh.text = "START";
        startTextMesh.fontSize = 15;
        startTextMesh.color = Color.white;
        startTextMesh.anchor = TextAnchor.MiddleCenter;
    }
    
    void PlaceSnakesAndLadders()
    {
        // Snake positions (head -> tail)
        int[,] snakes = new int[,] {
            {99, 78}, {95, 75}, {92, 88}, {87, 24}, {64, 60},
            {62, 19}, {56, 53}, {49, 11}, {47, 26}, {16, 6}
        };
        
        // Ladder positions (bottom -> top)
        int[,] ladders = new int[,] {
            {1, 38}, {4, 14}, {9, 31}, {21, 42}, {28, 84},
            {36, 44}, {51, 67}, {71, 91}, {80, 100}
        };
        
        // Place snakes
        for (int i = 0; i < snakes.GetLength(0); i++)
        {
            int head = snakes[i, 0];
            int tail = snakes[i, 1];
            
            Vector3 headPos = tilePositions[head].position;
            Vector3 tailPos = tilePositions[tail].position;
            
            GameObject snake = Instantiate(snakePrefab, (headPos + tailPos) / 2, Quaternion.identity, transform);
            
            // Orient snake from head to tail
            Vector3 direction = tailPos - headPos;
            snake.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            
            // Scale snake to fit distance
            float distance = Vector3.Distance(headPos, tailPos);
            snake.transform.localScale = new Vector3(0.5f, distance, 1f);
            
            if (snakeMaterial != null)
                snake.GetComponent<Renderer>().material = snakeMaterial;
        }
        
        // Place ladders
        for (int i = 0; i < ladders.GetLength(0); i++)
        {
            int bottom = ladders[i, 0];
            int top = ladders[i, 1];
            
            Vector3 bottomPos = tilePositions[bottom].position;
            Vector3 topPos = tilePositions[top].position;
            
            GameObject ladder = Instantiate(ladderPrefab, (bottomPos + topPos) / 2, Quaternion.identity, transform);
            
            // Orient ladder from bottom to top
            Vector3 direction = topPos - bottomPos;
            ladder.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            
            // Scale ladder to fit distance
            float distance = Vector3.Distance(bottomPos, topPos);
            ladder.transform.localScale = new Vector3(0.3f, distance, 1f);
            
            if (ladderMaterial != null)
                ladder.GetComponent<Renderer>().material = ladderMaterial;
        }
    }
    
    public Transform[] GetTilePositions()
    {
        return tilePositions;
    }
}