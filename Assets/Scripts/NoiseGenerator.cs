using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class NoiseGenerator : MonoBehaviour
{

    public int gridSize = 4;
    public int gridSizeX = 4;
    public int gridSizeY = 4;
    private void OnGUI()
    {
        // Make a background box
        GUI.Box(new Rect(10,10,230,90), "Noise Generator Controls");
    
        // Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
        if (GUI.Button(new Rect(20, 40, 200, 20), "Generate Random"))
        {
            GenerateTextureAndApply();
        }
        if (GUI.Button(new Rect(20, 70, 200, 20), "Generate Fixed"))
        {
            GenerateTextureAndApply2();
        }
    }
    
    float lerp(float a0, float a1, float w) {
        return (1f - w)*a0 + (w)*a1;
    }

    float DotGradient(int gX, int gY, float x, float y, Vector2[,] grid)
    {
        Vector2 gVector = grid[gX, gY];
        Vector2 dVector = new Vector2(x - (float)gX, y-(float)gY);
        float dotProduct = (gVector.x * dVector.x) + (gVector.y * dVector.y);
        return dotProduct;
    }
    
    
    void GenerateTextureAndApply()
    {
        Texture2D texture = new Texture2D(128, 128);
        //texture.filterMode = FilterMode.Point;
        GetComponent<Renderer>().material.mainTexture = texture;

        //int gridSizeX = gridSize;
        //int gridSizeY = gridSize;
        
        Vector2[,] grid = new Vector2[gridSizeX,gridSizeY];
        for (int x1 = 0; x1 < gridSizeX; x1++)
        {
            for (int y1 = 0; y1 < gridSizeY; y1++)
            {
                grid[x1, y1] = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
                //Debug.Log("GRID[" + x1 + "," + y1 + "] - X: " + grid[x1, y1].x + " Y: " + grid[x1, y1].y);
            }
        }
        Color[] colors = new Color[texture.width * texture.height];
        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                float xF = ((float)x / texture.width) * (gridSizeX - 1);
                float yF = ((float)y / texture.height) * (gridSizeY - 1);
                int x0 = (int) xF;
                int x1 = x0 + 1;
                int y0 = (int) yF;
                int y1 = y0 + 1;
                float sX = xF - x0;
                float sY = yF - y0;
                float ix0, ix1, n0, n1, value;
                n0 = DotGradient(x0, y0, xF, yF, grid);
                n1 = DotGradient(x1, y0, xF, yF, grid);
                ix0 = lerp(n0, n1, sX);
                n0 = DotGradient(x0, y1, xF, yF, grid);
                n1 = DotGradient(x1, y1, xF, yF, grid);
                ix1 = lerp(n0, n1, sX);
                value = lerp(ix0, ix1, sY);
                
                float xF2 = ((float)x / texture.width) * gridSize;
                float yF2 = ((float)y / texture.height) * gridSize;
                float perlin = Mathf.PerlinNoise(xF2, yF2);
                
                colors[y * (texture.width) + x] = Color.Lerp(Color.black, Color.white, value*perlin+perlin*value);
            }
        }
        texture.SetPixels(colors);
        texture.Apply();
    }
    
    void GenerateTextureAndApply2()
    {
        Texture2D texture = new Texture2D(128, 128);
        GetComponent<Renderer>().material.mainTexture = texture;
        Color[] colors = new Color[texture.width * texture.height];
        for (int y = 0; y < texture.height; y++)
        {
            //int yI = Mathf.RoundToInt((y / texture.height) * 4);
            //float yF = (y / texture.height) * 4;
            for (int x = 0; x < texture.width; x++)
            {
                float xF = ((float)x / texture.width) * gridSize;
                float yF = ((float)y / texture.height) * gridSize;
                float perlin = Mathf.PerlinNoise(xF, yF);
                //Color pixelColor = Color.Lerp(Color.black, Color.white, perlin);
                colors[y * (texture.width) + x] = Color.Lerp(Color.black, Color.white, perlin*perlin);
                //texture.SetPixel(x, y, pixelColor);
                //texture.Apply();
            }
        }
        texture.SetPixels(colors);
        texture.Apply();
    }

    void GenerateTextureAndApply3(float t)
    {
        Texture2D texture = new Texture2D(128, 128);
        GetComponent<Renderer>().material.mainTexture = texture;
        Color[] colors = new Color[texture.width * texture.height];
        for (int y = 0; y < texture.height; y++)
        {
            //int yI = Mathf.RoundToInt((y / texture.height) * 4);
            //float yF = (y / texture.height) * 4;
            for (int x = 0; x < texture.width; x++)
            {
                float xF = ((float)x / texture.width) * gridSize;
                float yF = ((float)y / texture.height) * gridSize;
                float perlin = Mathf.PerlinNoise(xF+t, yF+t);
                //Color pixelColor = Color.Lerp(Color.black, Color.white, perlin);
                colors[y * (texture.width) + x] = Color.Lerp(Color.black, Color.white, perlin*perlin);
                //texture.SetPixel(x, y, pixelColor);
                //texture.Apply();
            }
        }
        texture.SetPixels(colors);
        texture.Apply();
    }
    
    void Start()
    {
        GenerateTextureAndApply();
    }

    void Update()
    {
        float t = Time.time;
        //GenerateTextureAndApply3(t);
    }
    
    
}
                /*
                //int xI = Mathf.RoundToInt((x / texture.width) * 4);
                //float xF = (x / texture.width) * 4;
                //Color color = ((x & y) != 0 ? Color.white : Color.gray);
                //float dotProduct = (grid[xI, yI].x * xF) + (grid[xI, yI].y * yF);
                 
                 
                Vector2 currentPixel = new Vector2(x,y);
                int currentGridX = Mathf.RoundToInt(((float)x / texture.width)*(gridSizeX-1));
                int currentGridY = Mathf.RoundToInt(((float)y / texture.height)*(gridSizeY-1));
                
                int otherSideX;
                int otherSideY;
                if (currentGridX == gridSizeX - 1) { otherSideX = currentGridX - 1; }
                else { otherSideX = currentGridX + 1; }
                if (currentGridY == gridSizeY - 1) { otherSideY = currentGridY - 1; }
                else { otherSideY = currentGridY + 1; }
                
                Vector2 gridPoint1 = new Vector2(Mathf.RoundToInt(currentGridX*((float)texture.width/(gridSizeX-1))), Mathf.RoundToInt(currentGridY*((float)texture.height/(gridSizeX-1))));
                Vector2 gridPoint2 = new Vector2(Mathf.RoundToInt(currentGridX*((float)texture.width/(gridSizeX-1))), Mathf.RoundToInt(otherSideY*((float)texture.height/(gridSizeX-1))));
                Vector2 gridPoint3 = new Vector2(Mathf.RoundToInt(otherSideX*((float)texture.width/(gridSizeX-1))), Mathf.RoundToInt(currentGridY*((float)texture.height/(gridSizeX-1))));
                Vector2 gridPoint4 = new Vector2(Mathf.RoundToInt(otherSideX*((float)texture.width/(gridSizeX-1))), Mathf.RoundToInt(otherSideY*((float)texture.height/(gridSizeX-1))));
                
                Vector2 gridPoint = new Vector2(currentGridX, currentGridY);
                Vector2 distanceVector = (currentPixel - gridPoint);
                
                
                //Vector2 currentClosestGrid = grid[currentGridX, currentGridY];
                
                Debug.Log(gridPoint1 + " " + gridPoint2 + " " + gridPoint3 + " " + gridPoint4);
                Debug.Log("PIXEL[" + x + "," + y + "] " + "GRID[" + gridPoint.x + "," + gridPoint.y + "] - X: " + gridPoint.x*((float)texture.width/(gridSizeX-1)) + " Y: " + gridPoint.y*((float)texture.height/(gridSizeX-1)));
                ((float)currentGridY/(gridSizeY-1)+(float)currentGridX/(gridSizeY-1))/2
                */