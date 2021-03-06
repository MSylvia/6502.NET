﻿/* Copyright (c) 2009 Joseph Robert. All rights reserved.
 *
 * This file is part of 6502.NET.
 * 
 * 6502.NET is free software; you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as
 * published by the Free Software Foundation; either version 3.0  of 
 * the License, or (at your option) any later version.
 * 
 * 6502.NET is distributed in the hope that it will be useful, but 
 * WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU 
 * Lesser General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License 
 * along with 6502.NET.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Threading;
using System.Collections.Generic;

namespace Emulator
{
    class Program
    {
        static void ExecuteThread(object memory)
        {
            Interpreter.Processor processor = new Interpreter.Processor((Interpreter.Memory)memory, 0x600);

            //int interval = 500000;
            //System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            //stopwatch.Start();
            for (; ; )
                processor.Execute();
            //stopwatch.Stop();
            //Console.WriteLine((stopwatch.ElapsedMilliseconds / 1000.0) + " seconds");
            //Console.WriteLine(4 * interval + " cycles");
            //Console.WriteLine(4.0 * interval / (stopwatch.ElapsedMilliseconds / 1000.0) + " hertz");

        }

        static void Main(string[] args)
        {
            // Console Initialization
            Display.Text.TextDisplay.Setup();

            Interpreter.Memory memory = new Interpreter.Memory(0x1000);
            Display.Text.TextDisplay display = new Display.Text.TextDisplay(memory);

            // Launch Execute Thread
            Thread executeThread = new Thread(new ParameterizedThreadStart(ExecuteThread));
            executeThread.Start(memory);

            // alive.asm
            memory.Set(0x600, new List<byte> { 0xa9, 0xf, 0x85, 0x0, 0x85, 0x1, 0xa5, 0xfe, 0x29, 0x3, 0xc9, 0x0, 0xf0, 0x2f, 0xc9, 0x1, 0xf0, 0x30, 0xc9, 0x2, 0xf0, 0x22, 0xc6, 0x1, 0xa5, 0x1, 0x29, 0x1f, 0xa, 0xaa, 0xbd, 0x47, 0x6, 0x85, 0x2, 0xe8, 0xbd, 0x47, 0x6, 0x85, 0x3, 0xa5, 0x0, 0x29, 0x1f, 0xa8, 0xb1, 0x2, 0xaa, 0xe8, 0x8a, 0x91, 0x2, 0x4c, 0x6, 0x6, 0xe6, 0x1, 0x4c, 0x18, 0x6, 0xc6, 0x0, 0x4c, 0x18, 0x6, 0xe6, 0x0, 0x4c, 0x18, 0x6, 0x0, 0x2, 0x20, 0x2, 0x40, 0x2, 0x60, 0x2, 0x80, 0x2, 0xa0, 0x2, 0xc0, 0x2, 0xe0, 0x2, 0x0, 0x3, 0x20, 0x3, 0x40, 0x3, 0x60, 0x3, 0x80, 0x3, 0xa0, 0x3, 0xc0, 0x3, 0xe0, 0x3, 0x0, 0x4, 0x20, 0x4, 0x40, 0x4, 0x60, 0x4, 0x80, 0x4, 0xa0, 0x4, 0xc0, 0x4, 0xe0, 0x4, 0x0, 0x5, 0x20, 0x5, 0x40, 0x5, 0x60, 0x5, 0x80, 0x5, 0xa0, 0x5, 0xc0, 0x5, 0xe0, 0x5 });

            // disco.asm
            //memory.Set(0x600, new List<byte> { 0xe8, 0x8a, 0x99, 0x0, 0x2, 0x99, 0x0, 0x3, 0x99, 0x0, 0x4, 0x99, 0x0, 0x5, 0xc8, 0x98, 0xc5, 0x10, 0xd0, 0x4, 0xc8, 0x4c, 0x0, 0x6, 0xc8, 0xc8, 0xc8, 0xc8, 0x4c, 0x0, 0x6 });

            for (; ; )
            {
                display.Render();
                if (Interpreter.DisplaySettings.LimitGraphicsSpeed)
                    Thread.Sleep(1000 / Interpreter.DisplaySettings.RenderFrequency);
            }
        }
    }
}
