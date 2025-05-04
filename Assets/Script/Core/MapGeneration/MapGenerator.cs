using System.Collections;
using UnityEngine;

namespace dang
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private int mapWidth, mapHeight;
        [SerializeField] private GameObject tileReference;
        [SerializeField] private Sprite emptyTile, downPath, leftRight, leftDown, rightDown, downLeft, downRight;

        [SerializeField] private Sprite[] decorationSprites; // Mảng các sprite trang trí
        [SerializeField] private GameObject[] treePrefabs; // Mảng các prefab cây

        private Color pathColor = new Color(229f / 255f, 170f / 255f, 36f / 255f);

        private int curX;
        private int curY;
        private Sprite spriteToUse;
        private bool forceDirectionChange = false;
        private bool continueLeft = false;
        private bool continueRight = false;
        private int currentCount = 0;

        private enum CurrentDirection
        {
            LEFT,
            RIGHT,
            DOWN,
            UP
        };
        private CurrentDirection curDirection = CurrentDirection.DOWN;

        public struct TileData
        {
            public Transform transform;
            public SpriteRenderer spriteRenderer;
            public int tileID;
            public GameObject decoration; // Thêm trường decoration cho mỗi tile
            public bool isPath; // Trạng thái tile có phải là path không
        }

        TileData[,] tileData;

        void Awake()
        {
            Init();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Init(); // Gọi lại khi nhấn phím Space
            }
        }

        void Init()
        {
            ClearMap(); // Xoá tile cũ (nếu có)

            tileData = new TileData[mapWidth, mapHeight];
            GenerateMap(); // Tạo bản đồ mới
            StopAllCoroutines(); // Dừng các Coroutine trước đó (nếu có)
            StartCoroutine(GeneratePath()); // Tạo đường đi mới
        }

        void ClearMap()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }

        void GenerateMap()
        {
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    float xOffset = x;
                    float yOffset = y;
                    GameObject newTile = Instantiate(tileReference, new Vector2(xOffset, yOffset), Quaternion.identity);
                    newTile.transform.parent = this.transform;
                    tileData[x, y].spriteRenderer = newTile.GetComponent<SpriteRenderer>();
                    tileData[x, y].tileID = 0;
                    tileData[x, y].spriteRenderer.sprite = emptyTile;
                    tileData[x, y].spriteRenderer.color = Color.white;
                    tileData[x, y].transform = newTile.transform;
                    tileData[x, y].isPath = false; // Ban đầu chưa có path
                }
            }
        }

        // Hàm tạo cây sau khi path hoàn thành
        void CreateTrees()
        {
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    bool isEdgeTile = (x == 0 || x == mapWidth - 1 || y == 0 || y == mapHeight - 1);
                    bool isEmpty = !tileData[x, y].isPath && tileData[x, y].decoration == null;

                    if (isEdgeTile && isEmpty && Random.value < 1f) // Tăng xác suất cây lên 40%
                    {
                        int treeIndex = Random.Range(0, treePrefabs.Length);
                        GameObject tree = Instantiate(treePrefabs[treeIndex], tileData[x, y].transform.position, Quaternion.identity);
                        tree.transform.parent = tileData[x, y].transform;

                        // Đảm bảo cây ở vị trí đúng trong render
                        var sr = tree.GetComponent<SpriteRenderer>();
                        sr.sortingOrder = -(int)tileData[x, y].transform.position.y;

                        tileData[x, y].decoration = tree;
                    }
                }
            }
        }

        // Hàm tạo trang trí sau khi cây đã được sinh ra
        void CreateDecorations()
        {
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    // Chỉ tạo trang trí nếu tile chưa có cây
                    if (tileData[x, y].decoration == null && !tileData[x, y].isPath)
                    {
                        CreateDecoration(x, y, tileData[x, y].transform);
                    }
                }
            }
        }

        // Hàm tạo một decoration ngẫu nhiên cho mỗi tile
        void CreateDecoration(int x, int y, Transform parentTransform)
        {
            // Đặt một tỉ lệ xuất hiện trang trí (ví dụ, 30% xác suất)
            if (Random.value < 0.1f) // Tăng xác suất lên 30%
            {
                GameObject decoration = new GameObject("Decoration");
                decoration.transform.parent = parentTransform; // Trang trí là con của tile trống
                decoration.transform.localPosition = Vector3.zero; // Đặt ở vị trí trung tâm của tile

                // Chọn sprite ngẫu nhiên từ mảng decorationSprites
                SpriteRenderer decorationRenderer = decoration.AddComponent<SpriteRenderer>();
                decorationRenderer.sprite = GetRandomDecorationSprite();
                tileData[x, y].decoration = decoration; // Lưu lại đối tượng decoration cho tile này
            }
        }

        // Lấy sprite trang trí ngẫu nhiên
        Sprite GetRandomDecorationSprite()
        {
            int randomIndex = Mathf.FloorToInt(Random.Range(0, decorationSprites.Length));
            return decorationSprites[randomIndex];
        }

        IEnumerator GeneratePath()
        {
            curX = Random.Range(1, mapWidth - 1);
            curY = 0;
            spriteToUse = downPath;

            while (curY <= mapHeight - 1)
            {
                CheckCurrentDirections();
                ChooseDirection();

                if (curY <= mapHeight - 1)
                {
                    UpdateMap(curX, curY, spriteToUse);
                }

                if (curDirection == CurrentDirection.DOWN)
                {
                    curY++;
                }

                yield return new WaitForSeconds(0.05f);
            }

            // Sau khi path hoàn thành, tạo cây
            CreateTrees();

            // Sau khi cây hoàn thành, tạo trang trí
            CreateDecorations();
        }

        private void UpdateMap(int mapX, int mapY, Sprite spriteToUse)
        {
            tileData[mapX, mapY].tileID = 1;
            tileData[mapX, mapY].spriteRenderer.sprite = spriteToUse;
            tileData[mapX, mapY].spriteRenderer.color = pathColor;
            tileData[mapX, mapY].isPath = true; // Đánh dấu tile là path
        }

        private void CheckCurrentDirections()
        {
            if (curDirection == CurrentDirection.LEFT && curX - 1 >= 0 && tileData[curX - 1, curY].tileID == 0)
            {
                curX--;
            }
            else if (curDirection == CurrentDirection.RIGHT && curX + 1 < mapWidth && tileData[curX + 1, curY].tileID == 0)
            {
                curX++;
            }
            else if (curDirection == CurrentDirection.UP && curY - 1 >= 0 && tileData[curX, curY - 1].tileID == 0)
            {
                if ((continueLeft && curX - 1 >= 0 && tileData[curX - 1, curY - 1].tileID == 0) ||
                    (continueRight && curX + 1 < mapWidth && tileData[curX + 1, curY - 1].tileID == 0))
                {
                    curY--;
                }
                else
                {
                    forceDirectionChange = true;
                }
            }
            else if (curDirection != CurrentDirection.DOWN)
            {
                forceDirectionChange = true;
            }
        }

        private void ChooseDirection()
        {
            if (currentCount < 3 && !forceDirectionChange)
            {
                currentCount++;
            }
            else
            {
                bool chanceToChange = Mathf.FloorToInt(Random.value * 1.99f) == 0;

                if (chanceToChange || forceDirectionChange || currentCount > 7)
                {
                    currentCount = 0;
                    forceDirectionChange = false;
                    ChangeDirection();
                }

                currentCount++;
            }
        }

        private void ChangeDirection()
        {
            int dirValue = Mathf.FloorToInt(Random.value * 2.99f);

            if (dirValue == 0 && (curDirection == CurrentDirection.LEFT || curDirection == CurrentDirection.RIGHT))
            {
                if (curY - 1 >= 0 &&
                    tileData[curX, curY - 1].tileID == 0 &&
                    tileData[Mathf.Max(0, curX - 1), curY - 1].tileID == 0 &&
                    tileData[Mathf.Min(mapWidth - 1, curX + 1), curY - 1].tileID == 0)
                {
                    GoUp();
                    return;
                }
            }

            if (curDirection == CurrentDirection.LEFT)
            {
                UpdateMap(curX, curY, leftDown);
            }
            else if (curDirection == CurrentDirection.RIGHT)
            {
                UpdateMap(curX, curY, rightDown);
            }

            if (curDirection == CurrentDirection.LEFT || curDirection == CurrentDirection.RIGHT)
            {
                curY++;
                spriteToUse = downPath;
                curDirection = CurrentDirection.DOWN;
                return;
            }

            if ((curX - 1 > 0 && curX + 1 < mapWidth - 1) || continueLeft || continueRight)
            {
                if ((dirValue == 1 && !continueRight) || continueLeft)
                {
                    if (tileData[curX - 1, curY].tileID == 0)
                    {
                        spriteToUse = continueLeft ? rightDown : downLeft;
                        continueLeft = false;
                        curDirection = CurrentDirection.LEFT;
                    }
                }
                else
                {
                    if (tileData[curX + 1, curY].tileID == 0)
                    {
                        spriteToUse = continueRight ? leftDown : downRight;
                        continueRight = false;
                        curDirection = CurrentDirection.RIGHT;
                    }
                }
            }
            else if (curX - 1 > 0)
            {
                spriteToUse = downLeft;
                curDirection = CurrentDirection.LEFT;
            }
            else if (curX + 1 < mapWidth - 1)
            {
                spriteToUse = downRight;
                curDirection = CurrentDirection.RIGHT;
            }

            if (curDirection == CurrentDirection.LEFT)
            {
                GoLeft();
            }
            else if (curDirection == CurrentDirection.RIGHT)
            {
                GoRight();
            }
        }

        private void GoUp()
        {
            if (curDirection == CurrentDirection.LEFT)
            {
                UpdateMap(curX, curY, downRight);
                continueLeft = true;
            }
            else
            {
                UpdateMap(curX, curY, downLeft);
                continueRight = true;
            }
            curDirection = CurrentDirection.UP;
            curY--;
            spriteToUse = downPath;
        }

        private void GoLeft()
        {
            UpdateMap(curX, curY, spriteToUse);
            curX--;
            spriteToUse = leftRight;
        }

        private void GoRight()
        {
            UpdateMap(curX, curY, spriteToUse);
            curX++;
            spriteToUse = leftRight;
        }
    }
}
