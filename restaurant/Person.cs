using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restaurant
{
    public class Points
    {
        public int X, Y;
        public Points(int x, int y) { X = x; Y = y; }
    }
    public class Person
    {
        List<Points> Path;
        public Points ActualPoint, EndPoint;
        private Points OldEndPoint;

        public Person()
        {
            Path = new List<Points>();
            ActualPoint = new Points(1, 1);
            EndPoint = new Points(1, 1);
            OldEndPoint = new Points(1, 1);
        }

        public void Step(ref int[,] room)
        {
            room[ActualPoint.Y, ActualPoint.X] = 100;
            bool newPath = false;
            List<Points> Path = new List<Points>();
            if (!EqualPoints(EndPoint, OldEndPoint)) newPath = true;
            if (!EqualPoints(ActualPoint, EndPoint) && Path.Count == 0) newPath = true;
            if (Path.Count > 0)
            {
                if (room[Path[0].Y, Path[0].X] != 0) newPath = true;
            }
            if ((newPath) && (Path != null))
            {
                Path = FindPath(room, ActualPoint, EndPoint);
                if (Path != null)
                {
                    if (Path.Count > 0) Path.RemoveAt(0);
                    OldEndPoint.X = EndPoint.X;
                    OldEndPoint.Y = EndPoint.Y;
                } 
            }
            if (!EqualPoints(ActualPoint, EndPoint) && (Path != null))
            {
                room[ActualPoint.Y, ActualPoint.X] = 0;
                room[Path[0].Y, Path[0].X] = 100;
                ActualPoint.X = Path[0].X;
                ActualPoint.Y = Path[0].Y;
                Path.RemoveAt(0);
            }
        }
        private bool EqualPoints(Points A, Points B)
        {
            if ((A.X == B.X) && (A.Y == B.Y))
            {
                return true;
            }
            else
            {
                return false;
            }


        }
        private List<Points> FindPath(int[,] room, Points start, Points end)
        {
            int width = room.GetLength(1);
            int height = room.GetLength(0);

            bool[,] visited = new bool[height, width];
            Points[,] previous = new Points[height, width];

            int[] dx = new int[] { 0, 0, -1, 1 };
            int[] dy = new int[] { -1, 1, 0, 0 };

            Queue<Points> queue = new Queue<Points>();
            queue.Enqueue(start);
            visited[start.Y, start.X] = true;

            while (queue.Count > 0)
            {
                Points current = queue.Dequeue();

                if (current.X == end.X && current.Y == end.Y)
                {
                    // Cíl nalezen – sestav cestu
                    List<Points> path = new List<Points>();
                    Points p = end;
                    while (p != null)
                    {
                        path.Add(p);
                        p = previous[p.Y, p.X];
                    }
                    path.Reverse();
                    return path;
                }

                // Projdi sousedy
                for (int i = 0; i < 4; i++)
                {
                    int newX = current.X + dx[i];
                    int newY = current.Y + dy[i];

                    if (newX >= 0 && newX < width &&
                        newY >= 0 && newY < height &&
                        room[newY, newX] == 0 &&
                        !visited[newY, newX])
                    {
                        visited[newY, newX] = true;
                        previous[newY, newX] = current;
                        queue.Enqueue(new Points(newX, newY));
                    }
                }
            }

            // Cesta neexistuje
            return null;
        }
    }
}

