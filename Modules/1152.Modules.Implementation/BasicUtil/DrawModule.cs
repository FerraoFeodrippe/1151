﻿using System.Drawing;
using System.Runtime.InteropServices;
using _1152.Modules.Implementation;
using _1151.Cross.DepedencyInjection.Helpers;
using System.Reflection;

namespace _1152.Modules.Application.BasicUtil
{
    public class DrawModule : BaseModule
    {
        private static CancellationTokenSource _tokenSource = new();
        private readonly object __locker = new ();

        public DrawModule(string[] args) : base(args)
        {
        }

        [DllImport("User32.dll")]
        private static extern IntPtr GetDC(IntPtr hwnd);
        [DllImport("User32.dll")]
        private static extern void ReleaseDC(IntPtr hwnd, IntPtr dc);

        /// <summary>
        /// Windows only for now
        /// </summary>
        public async void DrawStart()
        {
            DrawStop();
            var token = GetToken();

            await Task.Run(() =>
            {
                IntPtr desktopPtr = GetDC(IntPtr.Zero);
                Graphics g = Graphics.FromHdc(desktopPtr);

                try
                {
                    while (!token.IsCancellationRequested)
                    {
                        var path = Path.Combine(Assembly.GetEntryAssembly()?.Location ?? string.Empty, "..", "BasicUtil", "skull_picture.jpg");
                        Image img = Image.FromFile(path);


                        if (!File.Exists(path))
                        {
                            OutputsPrinter.Print("skull_picture not exists");
                            return;
                        }

                        g.DrawImage(img, 0, 0, 1920, 1080);
                    }
                }
                finally
                {
                    g.Dispose();
                    ReleaseDC(IntPtr.Zero, desktopPtr);
                    _tokenSource.Dispose();
                    _tokenSource = new();
                }

            }, _tokenSource.Token);
        }

        private CancellationToken GetToken()
        {
            CancellationToken result;

            lock (__locker)
            {
                result = _tokenSource.Token;
            }

            return result;
        }

        public void DrawStop()
        {
            lock(__locker)
            {
                _tokenSource.Cancel();
                _tokenSource.Dispose();
                _tokenSource = new();
            }
        }
    }
}
