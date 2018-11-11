using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace Paint2D
{
    class MyParabola:MyRectangle
    {
        public MyParabola() : base() { }
        public override void Draw(System.Drawing.Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //phải xét lại để trong quá trình MouseMove
            int width = Math.Abs(endPoint.X - startPoint.X);
            int height = Math.Abs(endPoint.Y - startPoint.Y);
            int spX = Math.Min(startPoint.X, endPoint.X);
            int spY = Math.Min(startPoint.Y, endPoint.Y);
            mRec = new Rectangle(spX, spY, Math.Min(width, height),Math.Max(width, height));

            List<Point> l = getPointParabola(true); //right
            g.DrawLines(p, l.ToArray());
            l.Clear();
            l = getPointParabola(false); //left
            g.DrawLines(p, l.ToArray());
            //show control point
            if (this.isSelect)
            {
                this.getListControlPoint();
                this.showControlPoint(g);
            }
        }
        public List<Point> getPointParabola(bool mode)
        {
            //y = x^2
            List<Point> listPointPara = new List<Point>();
            Point O = new Point(mRec.Width / 2 + startPoint.X, endPoint.Y);
            Double x = 0, y = 0;
            while (y < mRec.Height)
            {
                y = 0.02 *x * x;
                if(mode) listPointPara.Add(new Point(O.X+(int)x,O.Y-(int)y));
                if(!mode) listPointPara.Add(new Point(O.X-(int)x,O.Y-(int)y));
                x++;
            }
            return listPointPara;
        }
    }
}
