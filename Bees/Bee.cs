using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bees
{
    class Bee
    {
        /*int x1, y1; //начальные координаты пчелы
        int x2, y2; //конечные координаты пчелы
        public int x, y; //настоящие коодинаты пчелы*/
        static List<Bee> bees; //список всех пчел
        static int countBees = 0;
        public int time = 0;
        Image image;

        public Bee()
        {
            //x = 30;
           // y = 30;
            countBees++;
        }
        void Live()
        {

        }
        void Draw(Graphics g) //отрисовка
        {
            //g.DrawImage(image, p);
        }
        /*private Point GetCoords() //получить координаты пчелы
        {

        }
        static List<Bee> GetBees() //получить список всех пчел
        {

        }*/
    }
    class Queen : Bee //оплодотворение, выкладка яиц, питание, время воспроизведения потомства, сытость (да/нет), картинка
    {
        
    }
    class Drone : Bee //оплодотворение
    {

    }
    class Worker : Bee //двигаться, заметить цветок, сесть на цветок, взять мед из цветка, залететь в улей, положить мед в соты, вылететь из улья, наличие меда у пчелы, время жизни
    {

        void Smell(Flower f) //почуять запах
        {

        }
    }
}
