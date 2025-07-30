using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowPiece : MonoBehaviour
{
    public int Color;
    public int StartColor;
    public int Row;
    public int Collumn;
    public bool isEmpty;
    public bool isTheEnd;
    public bool StartingPoint;
    public Sprite[] straight;
    public Sprite[] end;
    public Sprite[] turn;
    public int Connection;
    public FlowPiece tileAfter;
    public FlowPiece tileBefore;
    public SpriteRenderer Renderer;
    public FlowEndStart EndStartPoint;
    public bool isPortal;
    public int PortalIndex;
    void Awake()
    {
        Renderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        FlowManager.instance.checkCompletion.AddListener(Check);
    }
    void ODisable()
    {
         FlowManager.instance.checkCompletion.RemoveListener(Check);
    }
    public void Check()
    {
        if (StartingPoint)
        {
            if (Color == StartColor)
                FlowManager.instance.Counter++;
            else
                FlowManager.instance.Counter--;
        }
    }
    public void MakeStarter(int colorCode)
    {
        isEmpty = true;
        isTheEnd = true;
        StartingPoint = true;
        StartColor = colorCode;
        // Color = colorCode;
    }
    public void MakePortal(int index)
    {
        isPortal = true;
        isEmpty = true;
        PortalIndex = index;
    }
    public void MakeNormal()
    {
        Renderer.sprite = null;
        Connection = -1;
        Color = -1;
        tileAfter = null;
        tileBefore = null;
        if(!StartingPoint)
        {
            isEmpty = true;
            isTheEnd = false;
        }
        else
        {
            MakeStarter(StartColor);
        }
    }
    public void reset()
    {
        Renderer.sprite = null;
        Connection = -1;
        Color = -1;
        PortalIndex = -1;
        isEmpty = true;
        isTheEnd = false;
        isPortal = false;
        tileAfter = null;
        tileBefore = null;
    }
     
    void Update()
    {

    }
    void OnMouseEnter()
    {
        if(tileAfter != null && FlowManager.instance.isChanging)
        {
            if(tileAfter.isTheEnd)
            {
                if(StartingPoint)
                {
                    isEmpty = true;
                    Renderer.sprite = null;
                    isTheEnd = true;
                }
                tileAfter.RemoveColor();
                switch(Connection)
                {
                    case 1:
                        // Debug.Log("1");
                        Renderer.sprite = end[Color];
                        transform.localEulerAngles  = new Vector3(0,0,180);
                        isTheEnd = true;
                        break;
                    case 2:
                        // Debug.Log("2");
                        Renderer.sprite = end[Color];
                        transform.localEulerAngles  = new Vector3(0,0,0);
                        isTheEnd = true;
                        break;
                    case 3:
                        // Debug.Log("3");
                        Renderer.sprite = end[Color];
                        transform.localEulerAngles  = new Vector3(0,0,-90);
                        isTheEnd = true;
                        break;
                    case 4:
                        // Debug.Log("4");
                        Renderer.sprite = end[Color];
                        transform.localEulerAngles  = new Vector3(0,0,90);
                        isTheEnd = true;
                        break;
                }
                tileAfter = null;
                FlowManager.instance.CanChangeMore = true;
                FlowManager.instance.Color = Color;
            }
        }

        else if(FlowManager.instance.Color >= 0 && FlowManager.instance.CanChangeMore )
        {
            // Debug.Log("Add Color");
            AddColor();
        }
        if(StartingPoint || isPortal)
        {
            // Debug.Log("Hovering Enter");
            FlowManager.instance.isHoveringStartPoint = true;
            FlowManager.instance.Color = -1;
            FlowManager.instance.CanChangeMore = false;

        }
    }
    void OnMouseExit()
    {
        if(StartingPoint || isPortal)
        {
            // Debug.Log("Hovering Exit");
            FlowManager.instance.isHoveringStartPoint = false;
            if(StartingPoint && tileBefore != null)
            {
                FlowManager.instance.CanChangeMore = false;
            }
        }
    }
    void OnMouseDown()
    {
        if((isTheEnd || StartingPoint))
        {
            // Debug.Log("Starting");
            FlowManager.instance.isChanging = true;
            FlowManager.instance.Color = Color;
            if(StartingPoint && tileBefore == null)
            {
                FlowManager.instance.Counter++;
                FlowManager.instance.Color = StartColor;
                FlowManager.instance.CanChangeMore = true;
                Color = StartColor;
                isEmpty = false;
            }
            else if(StartingPoint && tileBefore != null)
            {
                FlowManager.instance.CanChangeMore = false;
                FlowManager.instance.isHoveringStartPoint = true;
            }
            else if(!StartingPoint)
            {
                FlowManager.instance.CanChangeMore = true;
            }
        }
    }
    void RemoveColor()
    {
        Color = -1;
        isEmpty = true;
        Renderer.sprite = null;
        isTheEnd = false;
    }
    void AddColor()
    {

        if(isEmpty)
        {
            if(!isPortal)
            {
                if(Row - 1 >= 0)
                {
                    if(FlowManager.instance.Collumn[Collumn].Row[Row-1].Color == FlowManager.instance.Color && FlowManager.instance.Collumn[Collumn].Row[Row-1].isTheEnd )
                    {
                        isEmpty = false;
                        Color = FlowManager.instance.Color;
                        // Debug.Log("Left");
                        //Kiri
                        Connection = 1;
                        Renderer.sprite = end[Color];
                        transform.localEulerAngles  = new Vector3(0,0,180);
                        isTheEnd = true;
                        FlowManager.instance.Collumn[Collumn].Row[Row-1].isTheEnd = false;
                        FlowManager.instance.Collumn[Collumn].Row[Row-1].tileAfter = this;
                        tileBefore = FlowManager.instance.Collumn[Collumn].Row[Row-1];
                        tileBefore.isEmpty = false;
                        switch(FlowManager.instance.Collumn[Collumn].Row[Row-1].Connection)
                        {
                            case 1:
                                FlowManager.instance.Collumn[Collumn].Row[Row-1].Renderer.sprite = straight[Color];
                                FlowManager.instance.Collumn[Collumn].Row[Row-1].gameObject.transform.localEulerAngles  = new Vector3(0,0,0);
                                break;
                            case 3:
                                FlowManager.instance.Collumn[Collumn].Row[Row-1].Renderer.sprite = turn[Color];
                                FlowManager.instance.Collumn[Collumn].Row[Row-1].gameObject.transform.localEulerAngles  = new Vector3(0,0,90);
                                break;
                            case 4:
                                FlowManager.instance.Collumn[Collumn].Row[Row-1].Renderer.sprite = turn[Color];
                                FlowManager.instance.Collumn[Collumn].Row[Row-1].gameObject.transform.localEulerAngles  = new Vector3(0,0,180);
                                break;
                            case -1:
                                FlowManager.instance.Collumn[Collumn].Row[Row-1].Renderer.sprite = end[Color];
                                FlowManager.instance.Collumn[Collumn].Row[Row-1].gameObject.transform.localEulerAngles  = new Vector3(0,0,0);
                                break;
                        }
                    }
                }
                if(Row + 1 <= 5)
                {
                    if( FlowManager.instance.Collumn[Collumn].Row[Row+1].Color == FlowManager.instance.Color && FlowManager.instance.Collumn[Collumn].Row[Row+1].isTheEnd )
                    {
                        isEmpty = false;
                        Color = FlowManager.instance.Color;
                        //Kanan
                        // Debug.Log("Right");
                        Connection = 2;
                        Renderer.sprite = end[Color];
                        transform.localEulerAngles  = new Vector3(0,0,0);
                        isTheEnd = true;
                        FlowManager.instance.Collumn[Collumn].Row[Row+1].isTheEnd = false;
                        FlowManager.instance.Collumn[Collumn].Row[Row+1].tileAfter = this;
                        tileBefore = FlowManager.instance.Collumn[Collumn].Row[Row+1];
                        tileBefore.isEmpty = false;
                        switch(FlowManager.instance.Collumn[Collumn].Row[Row+1].Connection)
                        {
                            case 2:
                                FlowManager.instance.Collumn[Collumn].Row[Row+1].Renderer.sprite = straight[Color];
                                FlowManager.instance.Collumn[Collumn].Row[Row+1].gameObject.transform.localEulerAngles  = new Vector3(0,0,0);
                                break;
                            case 3:
                                FlowManager.instance.Collumn[Collumn].Row[Row+1].Renderer.sprite = turn[Color];
                                FlowManager.instance.Collumn[Collumn].Row[Row+1].gameObject.transform.localEulerAngles  = new Vector3(0,0,0);
                                break;
                            case 4:
                                FlowManager.instance.Collumn[Collumn].Row[Row+1].Renderer.sprite = turn[Color];
                                FlowManager.instance.Collumn[Collumn].Row[Row+1].gameObject.transform.localEulerAngles  = new Vector3(0,0,-90);
                                break;
                            case -1:
                                FlowManager.instance.Collumn[Collumn].Row[Row+1].Renderer.sprite = end[Color];
                                FlowManager.instance.Collumn[Collumn].Row[Row+1].gameObject.transform.localEulerAngles  = new Vector3(0,0,180);
                                break;
                        }
                        
                    }
                }
                if(Collumn+1 <= 5)
                {
                    if( FlowManager.instance.Collumn[Collumn+1].Row[Row].Color == FlowManager.instance.Color && FlowManager.instance.Collumn[Collumn+1].Row[Row].isTheEnd && Collumn + 1 >= 0&& Collumn + 1 <= 5)
                    {
                        isEmpty = false;
                        Color = FlowManager.instance.Color;
                        // Debug.Log("Up");
                        Connection = 3;
                        Renderer.sprite = end[Color];
                        transform.localEulerAngles  = new Vector3(0,0,-90);
                        isTheEnd = true;
                        FlowManager.instance.Collumn[Collumn+1].Row[Row].isTheEnd = false;
                        FlowManager.instance.Collumn[Collumn+1].Row[Row].tileAfter = this;
                        tileBefore = FlowManager.instance.Collumn[Collumn+1].Row[Row];
                        tileBefore.isEmpty = false;
                        switch(FlowManager.instance.Collumn[Collumn+1].Row[Row].Connection)
                        {
                            case 1:
                                FlowManager.instance.Collumn[Collumn+1].Row[Row].Renderer.sprite = turn[Color];
                                FlowManager.instance.Collumn[Collumn+1].Row[Row].gameObject.transform.localEulerAngles  = new Vector3(0,0,-90);
                                break;
                            case 2:
                                FlowManager.instance.Collumn[Collumn+1].Row[Row].Renderer.sprite = turn[Color];
                                FlowManager.instance.Collumn[Collumn+1].Row[Row].gameObject.transform.localEulerAngles  = new Vector3(0,0,180);
                                break;
                            case 3:
                                FlowManager.instance.Collumn[Collumn+1].Row[Row].Renderer.sprite = straight[Color];
                                FlowManager.instance.Collumn[Collumn+1].Row[Row].gameObject.transform.localEulerAngles  = new Vector3(0,0,90);
                                break;
                            case -1:
                                FlowManager.instance.Collumn[Collumn+1].Row[Row].Renderer.sprite = end[Color];
                                FlowManager.instance.Collumn[Collumn+1].Row[Row].gameObject.transform.localEulerAngles  = new Vector3(0,0,90);
                                break;
                        }
                    }
                }
                if(Collumn - 1 >=0)
                {
                    if(FlowManager.instance.Collumn[Collumn-1].Row[Row].Color == FlowManager.instance.Color&& FlowManager.instance.Collumn[Collumn-1].Row[Row].isTheEnd && Collumn - 1 >= 0 && Collumn - 1 <= 5)
                    {
                        isEmpty = false;
                        Color = FlowManager.instance.Color;
                        //Bawah
                        // Debug.Log("Down");
                        Connection =  4;
                        Renderer.sprite = end[Color];
                        transform.localEulerAngles  = new Vector3(0,0,90);
                        isTheEnd = true;
                        FlowManager.instance.Collumn[Collumn-1].Row[Row].isTheEnd = false;
                        FlowManager.instance.Collumn[Collumn-1].Row[Row].tileAfter = this;
                        tileBefore = FlowManager.instance.Collumn[Collumn-1].Row[Row];
                        tileBefore.isEmpty = false;
                        switch(FlowManager.instance.Collumn[Collumn-1].Row[Row].Connection)
                        {
                            case -1:
                                FlowManager.instance.Collumn[Collumn-1].Row[Row].Renderer.sprite = end[Color];
                                FlowManager.instance.Collumn[Collumn-1].Row[Row].gameObject.transform.localEulerAngles  = new Vector3(0,0,-90);
                                break;
                            case 1:
                                FlowManager.instance.Collumn[Collumn-1].Row[Row].Renderer.sprite = turn[Color];
                                FlowManager.instance.Collumn[Collumn-1].Row[Row].gameObject.transform.localEulerAngles  = new Vector3(0,0,0);
                                break;
                            case 2:
                                FlowManager.instance.Collumn[Collumn-1].Row[Row].Renderer.sprite = turn[Color];
                                FlowManager.instance.Collumn[Collumn-1].Row[Row].gameObject.transform.localEulerAngles  = new Vector3(0,0,90);
                                break;
                            case 4:
                                FlowManager.instance.Collumn[Collumn-1].Row[Row].Renderer.sprite = straight[Color];
                                FlowManager.instance.Collumn[Collumn-1].Row[Row].gameObject.transform.localEulerAngles  = new Vector3(0,0,90);
                                break;
                        }
                    }
                }
            }







            else
            {
                int PairRow = -1;
                int PairCollumn = -1;
                for(int i=0;i<6;i++)
                {
                    for (int j=0;j<6;j++)
                    {
                        if(FlowManager.instance.Collumn[i].Row[j].PortalIndex == PortalIndex && FlowManager.instance.Collumn[i].Row[j] != this)
                        {
                            PairCollumn = i;
                            PairRow = j;
                            break;
                        }
                    }
                    if(PairRow != -1) break;
                }
                Debug.Log($"{PairCollumn} {PairRow}");
                if(Row - 1 >= 0)
                {
                    if(FlowManager.instance.Collumn[Collumn].Row[Row-1].Color == FlowManager.instance.Color && FlowManager.instance.Collumn[Collumn].Row[Row-1].isTheEnd )
                    {
                        if(PairRow + 1 <= 5)
                        {
                            if(FlowManager.instance.Collumn[PairCollumn].Row[PairRow + 1].isEmpty)
                            {
                                //Teleport left to right
                                //urus kiri
                                FlowManager.instance.Collumn[Collumn].Row[Row-1].isTheEnd = false;
                                switch(FlowManager.instance.Collumn[Collumn].Row[Row-1].Connection)
                                {
                                    case 1:
                                        FlowManager.instance.Collumn[Collumn].Row[Row-1].Renderer.sprite = straight[FlowManager.instance.Color];
                                        FlowManager.instance.Collumn[Collumn].Row[Row-1].gameObject.transform.localEulerAngles  = new Vector3(0,0,0);
                                        break;
                                    case 3:
                                        FlowManager.instance.Collumn[Collumn].Row[Row-1].Renderer.sprite = turn[FlowManager.instance.Color];
                                        FlowManager.instance.Collumn[Collumn].Row[Row-1].gameObject.transform.localEulerAngles  = new Vector3(0,0,90);
                                        break;
                                    case 4:
                                        FlowManager.instance.Collumn[Collumn].Row[Row-1].Renderer.sprite = turn[FlowManager.instance.Color];
                                        FlowManager.instance.Collumn[Collumn].Row[Row-1].gameObject.transform.localEulerAngles  = new Vector3(0,0,180);
                                        break;
                                    case -1:
                                        FlowManager.instance.Collumn[Collumn].Row[Row-1].Renderer.sprite = end[FlowManager.instance.Color];
                                        FlowManager.instance.Collumn[Collumn].Row[Row-1].gameObject.transform.localEulerAngles  = new Vector3(0,0,0);
                                        break;
                                }

                                //urus Kanan;
                                FlowManager.instance.Collumn[PairCollumn].Row[PairRow + 1].Renderer.sprite = end[FlowManager.instance.Color];
                                FlowManager.instance.Collumn[PairCollumn].Row[PairRow + 1].Color = FlowManager.instance.Color;
                                FlowManager.instance.Collumn[PairCollumn].Row[PairRow + 1].isTheEnd = true;
                                FlowManager.instance.Collumn[PairCollumn].Row[PairRow + 1].isEmpty = false ;
                                FlowManager.instance.Collumn[PairCollumn].Row[PairRow + 1].Connection = 1 ;
                                FlowManager.instance.Collumn[PairCollumn].Row[PairRow + 1].gameObject.transform.localEulerAngles  = new Vector3(0,0,180);
                                return;
                            }
                        }
                    }
                }
                if(Row + 1 <= 5)
                {
                    if( FlowManager.instance.Collumn[Collumn].Row[Row+1].Color == FlowManager.instance.Color && FlowManager.instance.Collumn[Collumn].Row[Row+1].isTheEnd )
                    {
                        if(PairRow - 1 >= 0)
                        {
                            if(FlowManager.instance.Collumn[PairCollumn].Row[PairRow - 1].isEmpty)
                            {
                                //Teleport left to right
                                //urus kiri
                                FlowManager.instance.Collumn[Collumn].Row[Row+1].isTheEnd = false;
                                switch(FlowManager.instance.Collumn[Collumn].Row[Row+1].Connection)
                                {
                                    case 2:
                                        FlowManager.instance.Collumn[Collumn].Row[Row+1].Renderer.sprite = straight[FlowManager.instance.Color];
                                        FlowManager.instance.Collumn[Collumn].Row[Row+1].gameObject.transform.localEulerAngles  = new Vector3(0,0,0);
                                        break;
                                    case 3:
                                        FlowManager.instance.Collumn[Collumn].Row[Row+1].Renderer.sprite = turn[FlowManager.instance.Color];
                                        FlowManager.instance.Collumn[Collumn].Row[Row+1].gameObject.transform.localEulerAngles  = new Vector3(0,0,0);
                                        break;
                                    case 4:
                                        FlowManager.instance.Collumn[Collumn].Row[Row+1].Renderer.sprite = turn[FlowManager.instance.Color];
                                        FlowManager.instance.Collumn[Collumn].Row[Row+1].gameObject.transform.localEulerAngles  = new Vector3(0,0,-90);
                                        break;
                                    case -1:
                                        FlowManager.instance.Collumn[Collumn].Row[Row+1].Renderer.sprite = end[FlowManager.instance.Color];
                                        FlowManager.instance.Collumn[Collumn].Row[Row+1].gameObject.transform.localEulerAngles  = new Vector3(0,0,180);
                                        break;
                                }

                                //urus Kanan;
                                FlowManager.instance.Collumn[PairCollumn].Row[PairRow - 1].Renderer.sprite = end[FlowManager.instance.Color];
                                FlowManager.instance.Collumn[PairCollumn].Row[PairRow - 1].Color = FlowManager.instance.Color;
                                FlowManager.instance.Collumn[PairCollumn].Row[PairRow - 1].isTheEnd = true;
                                FlowManager.instance.Collumn[PairCollumn].Row[PairRow - 1].isEmpty = false ;
                                FlowManager.instance.Collumn[PairCollumn].Row[PairRow - 1].Connection = 2 ;
                                FlowManager.instance.Collumn[PairCollumn].Row[PairRow - 1].gameObject.transform.localEulerAngles  = new Vector3(0,0,0);
                                return;
                            }
                        }
                    }
                }
                if(Collumn+1 <= 5)
                {
                    if( FlowManager.instance.Collumn[Collumn+1].Row[Row].Color == FlowManager.instance.Color && FlowManager.instance.Collumn[Collumn+1].Row[Row].isTheEnd)
                    {
                        if(PairCollumn - 1 >= 0)
                        {
                            if(FlowManager.instance.Collumn[PairCollumn - 1].Row[PairRow].isEmpty)
                            {
                                //Teleport left to right
                                //urus kiri
                                FlowManager.instance.Collumn[Collumn+1].Row[Row].isTheEnd = false;
                                switch(FlowManager.instance.Collumn[Collumn+1].Row[Row].Connection)
                                {
                                    case 1:
                                        FlowManager.instance.Collumn[Collumn+1].Row[Row].Renderer.sprite = turn[FlowManager.instance.Color];
                                        FlowManager.instance.Collumn[Collumn+1].Row[Row].gameObject.transform.localEulerAngles  = new Vector3(0,0,-90);
                                        break;
                                    case 2:
                                        FlowManager.instance.Collumn[Collumn+1].Row[Row].Renderer.sprite = turn[FlowManager.instance.Color];
                                        FlowManager.instance.Collumn[Collumn+1].Row[Row].gameObject.transform.localEulerAngles  = new Vector3(0,0,180);
                                        break;
                                    case 3:
                                        FlowManager.instance.Collumn[Collumn+1].Row[Row].Renderer.sprite = straight[FlowManager.instance.Color];
                                        FlowManager.instance.Collumn[Collumn+1].Row[Row].gameObject.transform.localEulerAngles  = new Vector3(0,0,90);
                                        break;
                                    case -1:
                                        FlowManager.instance.Collumn[Collumn+1].Row[Row].Renderer.sprite = end[FlowManager.instance.Color];
                                        FlowManager.instance.Collumn[Collumn+1].Row[Row].gameObject.transform.localEulerAngles  = new Vector3(0,0,90);
                                        break;
                                }
                                //urus Kanan;
                                FlowManager.instance.Collumn[PairCollumn - 1].Row[PairRow ].Renderer.sprite = end[FlowManager.instance.Color];
                                FlowManager.instance.Collumn[PairCollumn - 1].Row[PairRow ].Color = FlowManager.instance.Color;
                                FlowManager.instance.Collumn[PairCollumn - 1].Row[PairRow ].isTheEnd = true;
                                FlowManager.instance.Collumn[PairCollumn - 1].Row[PairRow ].isEmpty = false ;
                                FlowManager.instance.Collumn[PairCollumn - 1].Row[PairRow ].Connection = 3 ;
                                FlowManager.instance.Collumn[PairCollumn - 1].Row[PairRow ].gameObject.transform.localEulerAngles  = new Vector3(0,0,-90);
                                return;
                            }
                        }
                    }
                }
                if(Collumn - 1 >= 0)
                {
                    if(FlowManager.instance.Collumn[Collumn-1].Row[Row].Color == FlowManager.instance.Color&& FlowManager.instance.Collumn[Collumn-1].Row[Row].isTheEnd)
                    {
                        if(PairCollumn + 1 <= 5)
                        {
                            if(FlowManager.instance.Collumn[PairCollumn + 1 ].Row[PairRow].isEmpty)
                            {
                                //Teleport left to right
                                //urus kiri
                                FlowManager.instance.Collumn[Collumn-1].Row[Row].isTheEnd = false;
                                switch(FlowManager.instance.Collumn[Collumn-1].Row[Row].Connection)
                                {
                                    case -1:
                                        FlowManager.instance.Collumn[Collumn-1].Row[Row].Renderer.sprite = end[FlowManager.instance.Color];
                                        FlowManager.instance.Collumn[Collumn-1].Row[Row].gameObject.transform.localEulerAngles  = new Vector3(0,0,-90);
                                        break;
                                    case 1:
                                        FlowManager.instance.Collumn[Collumn-1].Row[Row].Renderer.sprite = turn[FlowManager.instance.Color];
                                        FlowManager.instance.Collumn[Collumn-1].Row[Row].gameObject.transform.localEulerAngles  = new Vector3(0,0,0);
                                        break;
                                    case 2:
                                        FlowManager.instance.Collumn[Collumn-1].Row[Row].Renderer.sprite = turn[FlowManager.instance.Color];
                                        FlowManager.instance.Collumn[Collumn-1].Row[Row].gameObject.transform.localEulerAngles  = new Vector3(0,0,90);
                                        break;
                                    case 4:
                                        FlowManager.instance.Collumn[Collumn-1].Row[Row].Renderer.sprite = straight[FlowManager.instance.Color];
                                        FlowManager.instance.Collumn[Collumn-1].Row[Row].gameObject.transform.localEulerAngles  = new Vector3(0,0,90);
                                        break;
                                }
                                //urus Kanan;
                                FlowManager.instance.Collumn[PairCollumn + 1].Row[PairRow ].Renderer.sprite = end[FlowManager.instance.Color];
                                FlowManager.instance.Collumn[PairCollumn + 1].Row[PairRow ].Color = FlowManager.instance.Color;
                                FlowManager.instance.Collumn[PairCollumn + 1].Row[PairRow ].isTheEnd = true;
                                FlowManager.instance.Collumn[PairCollumn + 1].Row[PairRow ].isEmpty = false ;
                                FlowManager.instance.Collumn[PairCollumn + 1].Row[PairRow ].Connection = 4 ;
                                FlowManager.instance.Collumn[PairCollumn + 1].Row[PairRow ].gameObject.transform.localEulerAngles  = new Vector3(0,0,90);
                                return;
                            }
                        }
                    }
                }
            }  
        }
    }
    




    bool IsConected()
    {
        if(Row >= 1 && Row <= 4  && Collumn >=1 && Collumn <=4)
        {
            return (FlowManager.instance.Collumn[Collumn].Row[Row-1].Color == FlowManager.instance.Color  ) ||
            (FlowManager.instance.Collumn[Collumn].Row[Row+1].Color == FlowManager.instance.Color  ) ||
            (FlowManager.instance.Collumn[Collumn+1].Row[Row].Color == FlowManager.instance.Color ) ||
            (FlowManager.instance.Collumn[Collumn-1].Row[Row].Color == FlowManager.instance.Color);
        }
        if(Row >= 1 && Row <= 4)
        {
            return (FlowManager.instance.Collumn[Collumn].Row[Row-1].Color == FlowManager.instance.Color  ) ||
            (FlowManager.instance.Collumn[Collumn].Row[Row+1].Color == FlowManager.instance.Color );
        }
        if(Collumn >=1 && Collumn <=4)
        {
            return (FlowManager.instance.Collumn[Collumn+1].Row[Row].Color == FlowManager.instance.Color ) ||
            (FlowManager.instance.Collumn[Collumn-1].Row[Row].Color == FlowManager.instance.Color);
        }
        return false;
    }

}
