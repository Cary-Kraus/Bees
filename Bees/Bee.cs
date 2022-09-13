using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bees
{
    class Bee
    {
        int x1, y1; //начальные координаты пчелы
        int x2, y2; //конечные координаты пчелы
        public int x, y; //настоящие коодинаты пчелы

        public int time = 0;

        public Bee()
        {
            x = 30;
            y = 30;
        }

    }
}
