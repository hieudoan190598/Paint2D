using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Paint2D
{
    class MyHyperbola:MyRectangle
    {
        int a = 50, b = 20;
        Point center;
        List<Point>[] l; //list[4] containt 4 goc phan tu
        //
        public MyHyperbola() : base() { }
        public override void Draw(System.Drawing.Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //phải xét lại để trong quá trình MouseMove
            int width = Math.Abs(endPoint.X - startPoint.X);
            int height = Math.Abs(endPoint.Y - startPoint.Y);
            int spX = Math.Min(startPoint.X, endPoint.X);
            int spY = Math.Min(startPoint.Y, endPoint.Y);
            mRec = new Rectangle(spX, spY, width,height);
            center = new Point(mRec.Width / 2 + startPoint.X, mRec.Height / 2 + startPoint.Y);
            l = new List<Point>[4];
            for (int i = 0; i < 4; i++)
            {
                l[i] = new List<Point>();
            }
            DDA();
            for (int i = 0; i < 4; i++)
            {
                g.DrawLines(p, l[i].ToArray());
            }
                
        }
        
        public void hyperbolaPlotPoints( int x, int y)
        {
            int xCenter = center.X, yCenter = center.Y;
            
            l[0].Add(new Point(xCenter + x, yCenter + y));
            l[1].Add(new Point(xCenter - x, yCenter + y));
            l[2].Add(new Point(xCenter + x, yCenter - y));
            l[3].Add(new Point(xCenter - x, yCenter - y));
        }

        //DDA Algorithm
        public void DDA()
        {
            int xCenter = center.X, yCenter = center.Y;
            int x = a;
            int y = 0;
            hyperbolaPlotPoints(x, y);
            //region1
            while (a * a * y < b * b * x)
            {
                y++;
                x = (int)(Math.Sqrt((1 + 1.0 * y * y / (b * b)) * a * a) + 0.5);
                hyperbolaPlotPoints(x, y);
            }
            //Region 2
            while (x < mRec.Width/2)
            {
                x++;
                y = (int)(Math.Sqrt((1.0 * x * x / (a * a) - 1) * b * b) + 0.5);
                hyperbolaPlotPoints(x, y);
            }
        }
    }
}
