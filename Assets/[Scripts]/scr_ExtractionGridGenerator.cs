using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_ExtractionGridGenerator : MonoBehaviour
{
    public int rows;
    public int columns;

    public int maxNumberOfRichOreTiles;
    public int richOreTilesValue;

    public GameObject[,] gridArray;

    public GameObject resourcePanelPrefab;

    void Start()
    {
        gameObject.GetComponent<GridLayoutGroup>().cellSize = new Vector2((480 / rows), (480 / columns));

       gridArray = new GameObject[rows, columns];

        MakePanels();
        
    }

    public void MakePanels()
    {

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                GameObject gridTile = (GameObject)Instantiate(resourcePanelPrefab, gameObject.transform);
                scr_ResourcePanel panelScript = gridTile.GetComponent<scr_ResourcePanel>();
                gridArray[r, c] = gridTile;
                panelScript.row = r;
                panelScript.column = c;
                panelScript.generatorScript = gameObject.GetComponent<scr_ExtractionGridGenerator>();
            }
        }
        foreach (GameObject tile in gridArray)
        {
            //      assign neighbours
            scr_ResourcePanel tileScript = tile.GetComponent<scr_ResourcePanel>();

            // above tile
            if (tileScript.row > 0)
            {
                tileScript.aboveTile = gridArray[tileScript.row - 1, tileScript.column];
            }

            // left tile
            if (tileScript.column > 0)
            {
                tileScript.leftTile = gridArray[tileScript.row, tileScript.column - 1];
            }

            // right tile
            if (tileScript.column < columns - 1)
            {
                tileScript.rightTile = gridArray[tileScript.row, tileScript.column + 1];

            }

            // below tile
            if (tileScript.row < rows - 1)
            {
                tileScript.belowTile = gridArray[tileScript.row + 1, tileScript.column];
            }
        }

        AssignOreValues();

             
    }

    public void AssignOreValues()
    {
      
        for (int i = 0; i < maxNumberOfRichOreTiles; i++)
        {

            SetOreValue();
        }

        void SetOreValue()
        {
            int randomRow = Random.Range(0, rows - 1);
            int randomColumn = Random.Range(0, columns - 1);

            scr_ResourcePanel gridScript = gridArray[randomRow, randomColumn].GetComponent<scr_ResourcePanel>();

            if (gridScript.oreValue == 0)
            {
                gridScript.oreValue = richOreTilesValue;
            }
            else
            {
                //SetOreValue();
                Debug.Log("Tile already used");
            }

            //***Set Half ore Values****
            
            // up left

                // up 

                // up right

                // left

                // right

                // down left

                // down

                // down right
        }

        

    }

    public void TileClicked(int rowClicked, int columnClicked)
    {
        Debug.Log("CLICKED ON TILE AT X:" + rowClicked + " Y:" + columnClicked);
        gridArray[rowClicked, columnClicked].GetComponent<Image>().color = Color.cyan;


        // above
        if (rowClicked > 0)
        {
            // up left
            if (columnClicked > 0)
            {
                gridArray[rowClicked - 1, columnClicked - 1].GetComponent<Image>().color = Color.yellow;
            }
            // up
            gridArray[rowClicked - 1, columnClicked].GetComponent<Image>().color = Color.yellow;
            
            // up right
            if (columnClicked < columns - 1)
            {
                gridArray[rowClicked - 1, columnClicked + 1].GetComponent<Image>().color = Color.yellow;
            }
        }
        // left
        if (columnClicked > 0)
        {
            gridArray[rowClicked, columnClicked - 1].GetComponent<Image>().color = Color.yellow;
        }

        // right
        if (columnClicked < columns - 1)
        {
            gridArray[rowClicked, columnClicked + 1].GetComponent<Image>().color = Color.yellow;
        }

        // below
        if (rowClicked < rows - 1)
        {
            // down left
            if (columnClicked > 0)
            {
                gridArray[rowClicked + 1, columnClicked - 1].GetComponent<Image>().color = Color.yellow;
            }
            // down
            gridArray[rowClicked + 1, columnClicked].GetComponent<Image>().color = Color.yellow;

            // down right
            if (columnClicked < columns - 1)
            {
                gridArray[rowClicked + 1, columnClicked + 1].GetComponent<Image>().color = Color.yellow;
            }
        }

    }



}
