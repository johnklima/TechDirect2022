using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GOL : MonoBehaviour
{

    const int MAX_ROWS = 128;
    const int MAX_COLUMNS = 128;

   
   
    public Transform cube;

    //2d array of ints (alive or dead)
    int[,] cells;
    //2d array of boxes - 3d objects on a plane
    Transform[,] cells3d;

    // Use this for initialization
    void Start()
    {
       
        //redimension our array
        cells = new int[MAX_ROWS, MAX_COLUMNS];
        cells3d = new Transform[MAX_ROWS, MAX_COLUMNS];

        //initialize the cells
        initCells();

    }

    // Update is called once per frame
    void Update()
    {
        generateGOL();

    }   

    //set up the cells array with zeros or random 0,1
    void initCells()
    {
        for (int row = 0; row < MAX_ROWS; row++)
        {
            for (int col = 0; col < MAX_COLUMNS; col++)
            {
               
                    cells[row, col] = Random.Range(0, 2);
                

                //now position the boxes
                Transform newBox = Instantiate(cube, transform);
                newBox.position = new Vector3(col, 0, row);
                cells3d[row, col] = newBox;
            }
        }      

    }

    
    
    

    //draws our current array of cells on the GUI canvas
    
    //next generation game of life
    void generateGOL()
    {
        int[,] next = new int[MAX_ROWS, MAX_COLUMNS];

        // Loop through every spot in our 2D array and check spots neighbors
        for (int row = 1; row < MAX_ROWS - 1; row++)
        {
            for (int col = 1; col < MAX_COLUMNS - 1; col++)
            {

                // Add up all the states in a 3x3 surrounding grid, not including where i am now
                int neighbors = 0;

                neighbors += cells[row - 1, col];
                neighbors += cells[row + 1, col];
                neighbors += cells[row, col - 1];
                neighbors += cells[row, col + 1];
                neighbors += cells[row + 1, col + 1];
                neighbors += cells[row + 1, col - 1];
                neighbors += cells[row - 1, col + 1];
                neighbors += cells[row - 1, col - 1];


                // Rules of Life
                if ((cells[row, col] == 1) && (neighbors < 2))				// Loneliness 
                    next[row, col] = 0;
                else if ((cells[row, col] == 1) && (neighbors > 3))     // Overpopulation
                    next[row, col] = 0;
                else if ((cells[row, col] == 0) && (neighbors == 3))        // Reproduction 
                    next[row, col] = 1;
                else                                                        // Stasis         
                    next[row, col] = cells[row, col];
            }
        }

        //now swap new values for old
        for (int row = 1; row < MAX_ROWS - 1; row++)
        {
            for (int col = 1; col < MAX_COLUMNS - 1; col++)
            {

                cells[row, col] = next[row, col];

                if (next[row, col] == 1)
                {
                    cells3d[row, col].gameObject.SetActive(true);
                }
                else
                {
                    cells3d[row, col].gameObject.SetActive(false);
                }


            }
        }

    }

}
