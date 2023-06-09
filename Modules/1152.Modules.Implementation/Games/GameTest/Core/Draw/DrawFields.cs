﻿namespace _1152.Modules.Application.Games.GameTest.Core.Draw
{
    public static class DrawFields
    {
        private static object _locker = new object();
        private volatile static bool canDraw = true;
        public static void Draw(char[][] fields, int maxW, int maxH)
        {
            Console.CursorVisible = false;

            if (canDraw)
            {
                canDraw = false;
                for (int i = 0; i < maxH; i++)
                {
                    int k = i;
                    for (int j = 0; j < maxW; j++)
                    {
                        int w = j;
                        Task.Run(() =>
                        {
                            MoveAndDraw(fields, w, k);
                            if (k == (maxH - 1) && w == (maxW - 1))
                            {
                                canDraw = true;
                            }
                        });
                    }
                }
            }
        }

        private static void MoveAndDraw(char[][] fields, int i, int j)
        {
            lock (_locker)
            {
                Console.SetCursorPosition(i, j);
                Console.Write(fields[j][i]);
            }
        }
    }
}
