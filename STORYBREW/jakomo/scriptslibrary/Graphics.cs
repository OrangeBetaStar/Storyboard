using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using StorybrewScripts.ExtraFunctions;
using System.Drawing;

namespace StorybrewScripts
{
    namespace Graphics
    {
        public class Circle
        {
            public int radius;
            public List<Vector2> pointList;
            public List<double> alphaList;

            public void introMethod(int size)
            {
                radius = size;
                pointList = size == 0 ? new List<Vector2>(){ new Vector2(0, 0) } : HelperClass.LocationsInCircle(size, new Vector2(0, 0), 1);

                List<double> localList = new List<double>();

                var distBig = HelperClass.Distance(new Vector2(radius, 0), new Vector2(0, 0));
                if (distBig >= 1)
                {
                    foreach (var point in pointList)
                    {
                        var dist = HelperClass.Distance(point, new Vector2(0, 0));
                        if (dist > 0)
                            localList.Add(1 - dist / distBig);
                        else
                            localList.Add(1);
                    }
                }
                else
                    localList.Add(1);

                alphaList = localList;
            }
            public Circle(int size) { introMethod(size); }
            public Circle() { introMethod(0); }
            public int GetRadius() => radius;
            public List<Vector2> GetPointList() => pointList;
            public List<double> GetAlphaList() => alphaList;
        }
        public class BrushG
        {
            public Circle circ;
            public Library spLib;
            public Color4 color;
            public List<Vector2> pointList;

            public BrushG(int brushSize, string sprite, Color4 brushColor)
            {
                circ = new Circle(brushSize);
                spLib = new Library(sprite);
                color = brushColor;
                pointList = circ.GetPointList();
            }
            public BrushG(string sprite, Color4 brushColor)
            {
                circ = new Circle();
                spLib = new Library(sprite);
                color = brushColor;
                pointList = circ.GetPointList();
            }
            public BrushG(string sprite)
            {
                circ = new Circle();
                spLib = new Library(sprite);
                color = Color4.White;
                pointList = circ.GetPointList();
            }
            public BrushG(int brushSize, string sprite)
            {
                circ = new Circle(brushSize);
                spLib = new Library(sprite);
                color = Color4.White;
                pointList = circ.GetPointList();
            }
            public void SetColor(Color4 colorNew) => color = colorNew;
            public void SetColor(Color colorNew) => color = colorNew;
            public Color4 GetColor() => color;
            public string GetPath() => spLib.GetPath();
            public Bitmap DrawLine(Bitmap image, Vector2 point1, Vector2 point2)
            {
                var dist = HelperClass.Distance(point1, point2);

                for (int i = 0; i < dist; i++)
                {
                    Vector2 middle = Vector2.Lerp(point1, point2, (float)(i / dist));

                    for (int z = 0; z < pointList.Count; z++)
                    {
                        var pos = new Vector2((float)Math.Round(middle.X + pointList[z].X), (float)Math.Round(middle.Y + pointList[z].Y));
                        if (pos.X >= 0 && pos.Y >= 0 && pos.X <= image.Width - 1 && pos.Y <= image.Height - 1)
                            image.SetPixel((int)pos.X, (int)pos.Y, (Color)color);
                    }
                }
                
                return image;
            }
            public Bitmap DrawLine(Bitmap image, Vector2 point1, Vector2 point2, Circle c)
            {
                var pointListLocal = c.GetPointList();
                var alphaListLocal = c.GetAlphaList();
                var dist = HelperClass.Distance(point1, point2);

                for (int i = 0; i < dist; i++)
                {
                    Vector2 middle = Vector2.Lerp(point1, point2, (float)(i / dist));

                    for (int z = 0; z < pointListLocal.Count; z++)
                    {
                        var pos = new Vector2((float)Math.Round(middle.X + pointListLocal[z].X), (float)Math.Round(middle.Y + pointListLocal[z].Y));
                        if (pos.X >= 0 && pos.Y >= 0 && pos.X <= image.Width - 1 && pos.Y <= image.Height - 1)
                            image.SetPixel((int)pos.X, (int)pos.Y, (Color)color);
                            
                    }
                }
                
                return image;
            }
            public Bitmap DrawShape(Bitmap image, int width, int pointNumbs, double angleOffset, Vector2 origin)
            {
                List<Vector2> points = new List<Vector2>();
                for (int i = 0; i < pointNumbs; i++)
                {
                    var angle = (i + 0.5) / (float)pointNumbs * Math.PI * 2 + angleOffset;
                    var pos = HelperClass.CirclePos(angle, (int)Math.Round(width / 2f), origin);
                    
                    points.Add(pos);
                }

                for (int i = 0; i < points.Count; i++)
                {
                    if (i != points.Count - 1)
                        image = DrawLine(image, points[i], points[i + 1]);
                    else
                        image = DrawLine(image, points[i], points[0]);
                }
                return image;
            }
            public Bitmap DrawShapeFill(Bitmap image, int width, int pointNumbs, double angleOffset, Vector2 origin)
            {
                var c = new Circle();

                List<Vector2> points = new List<Vector2>();
                for (int i = 0; i < pointNumbs; i++)
                {
                    var angle = (i + 0.5) / (float)pointNumbs * Math.PI * 2 + angleOffset;
                    var pos = HelperClass.CirclePos(angle, (int)Math.Round(width / 2f), origin);
                    
                    points.Add(pos);
                }

                var allPoints = new List<Vector2>();
                for (int i = 0; i < points.Count; i++)
                {
                    var point1 = points[i];
                    var point2 = i == points.Count - 1 ? points[0] : points[i + 1];

                    var dist = HelperClass.Distance(point1, point2);
                    
                    for (int z = 0; z < dist; z++)
                    {
                        var v2 = Vector2.Lerp(point1, point2, (float)(z / dist));
                        allPoints.Add(new Vector2(v2.X, (float)Math.Round(v2.Y)));
                    }
                }
                
                while (allPoints.Count >= 1)
                {
                    var p1 = allPoints[0];
                    var p2 = allPoints[0];

                    for (int i = 1; i < allPoints.Count; i++)
                    {
                        if (allPoints[0].Y == allPoints[i].Y)
                        {
                            p2 = allPoints[i];
                            break;
                        }
                    }

                    image = DrawLine(image, p1, p2, c);

                    allPoints.RemoveAt(0);
                }
                
                return image;
            }
        }
    }
}