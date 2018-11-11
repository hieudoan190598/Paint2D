using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace Paint2D
{
    class MyLine:Shape
    {
        public MyLine():base(){}
        public MyLine(Point sp, Point ep):base()
        {
            startPoint = sp;
            endPoint = ep;
        }
        public override void Draw(System.Drawing.Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.DrawLine(this.p, this.startPoint, this.endPoint);
            //p.Dispose(); init o ngoai nen ko Dispose() duoc
            //show control point
            if (this.isSelect)
            {
                this.getListControlPoint();
                this.showControlPoint(g);
            }
        }
        public override void mouseDown( MouseEventArgs e) {
            if (e.Button == MouseButtons.Left)
            {
                //ve
                if (isDraw)
                {
                    this.startPoint = e.Location;
                    this.endPoint = e.Location;
                    isDraw = true;
                }
                //move
                if (isMove || this.isResize)
                {
                    //only 1 time
                    this.anchor = e.Location;
                }
                
            }
            
        }
        public override void mouseMove( MouseEventArgs e) {
            if (isDraw)
            {
                this.endPoint = e.Location;
            }
            //move
 
            if (isMove)
            {
                //MessageBox.Show("move");
                int deltaX = e.Location.X - anchor.X;
                int deltaY = e.Location.Y - anchor.Y;
                this.startPoint.X += deltaX;
                this.startPoint.Y += deltaY;
                this.endPoint.X += deltaX;
                this.endPoint.Y += deltaY;
                anchor = e.Location;
            }
            if (isResize)
            {
                moveFollowControlPoint(e);
            }
        }
        public void moveFollowControlPoint(MouseEventArgs e)
        {
            int deltaX = e.Location.X - anchor.X;
            int deltaY = e.Location.Y - anchor.Y;
            if (currentCpIndex == 0)
            {
                startPoint.X += deltaX;
                startPoint.Y += deltaY;
            }
            if (currentCpIndex == 1)
            {
                endPoint.X += deltaX;
                endPoint.Y += deltaY;
            }
            anchor = e.Location;

        }
        public override void mouseUp(MouseEventArgs e) {
            if (isDraw)
            {
                this.endPoint = e.Location;
                this.isDraw = false;
                //format2point();
            }
            //getListControlPoint();
            this.release();
        }
        public override void getListControlPoint()
        {
            listControlPoint.Clear();
            this.listControlPoint.Add(new Rectangle(startPoint.X - 5, startPoint.Y - 5,10, 10));
            this.listControlPoint.Add(new Rectangle(endPoint.X - 5, endPoint.Y - 5, 10, 10));
        }
        //Override isContain
        public override bool isContain(Point loc)
        {
            if (loc.X < Math.Min(startPoint.X, endPoint.X) ||
                loc.X > Math.Max(startPoint.X, endPoint.X) ||
                loc.Y < Math.Min(startPoint.Y, endPoint.Y) ||
                loc.Y > Math.Max(startPoint.Y, endPoint.Y))
                return false;
            Double m = 1.0 * (endPoint.Y - startPoint.Y) / (endPoint.X - startPoint.X);
            Double b = 1.0 * startPoint.Y - m * startPoint.X;
            if (Math.Abs(loc.Y - (loc.X * m + b)) <= 1)
            {
                return true;
            }
            else return false;
        }
        //---
        public void setLineRegion()
        {
            GraphicsPath gpTmp = new GraphicsPath();
            gpTmp.AddLine(startPoint.X, startPoint.Y - 3, endPoint.X, endPoint.Y - 3);
            gpTmp.AddLine(endPoint.X, endPoint.Y - 3, endPoint.X, endPoint.Y + 3);
            gpTmp.AddLine(endPoint.X, endPoint.Y + 3, startPoint.X, startPoint.Y + 3);
            gpTmp.AddLine(startPoint.X, startPoint.Y + 3, startPoint.X, startPoint.Y - 3);
            mRegion = new Region(gpTmp);
        }
    }
}
