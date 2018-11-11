using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Paint2D
{
    class MyParallelogram:MyRectangle
    {
        //MyParallelogram Attribute Add 1 doan de scale
        protected int subtract; //pixel sau - pixel dau
        protected Rectangle mRec;
        //
        //Default Construtor
        public MyParallelogram():base(){
            subtract = 1;
        }
        //----
        public override void Draw(System.Drawing.Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            int width = Math.Abs(endPoint.X - startPoint.X);
            int spX = Math.Min(startPoint.X, endPoint.X);
            int spY = Math.Min(startPoint.Y, endPoint.Y);
            int epX = Math.Max(startPoint.X, endPoint.X);
            int epY = Math.Max(startPoint.Y, endPoint.Y);
            gp = new GraphicsPath();
            subtract = width / 3;
            Point B = new Point(epX - subtract, spY);
            Point D = new Point(spX + subtract, epY);
            gp.AddLine(spX, spY, B.X, B.Y);
            gp.AddLine(B.X, B.Y, epX, epY);
            gp.AddLine(epX, epY, D.X, D.Y);
            gp.AddLine(D.X, D.Y, spX, spY);
            mRegion = new Region(gp);
            g.DrawPath(p, gp);
            //gp.Dispose();
            //show control point
            if (this.isSelect)
            {
                this.getListControlPoint();
                this.showControlPoint(g);
            }
        }
        //Do trên Draw() Dispose() rồi nên ko được dùng, phải mở Dispose() ra
        public override bool isContain(Point location)
        {
            return gp.IsVisible(location) || gp.IsOutlineVisible(location, p);
        }

    }
}
