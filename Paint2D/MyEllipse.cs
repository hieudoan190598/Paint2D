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
    class MyEllipse:MyRectangle
    {
        public MyEllipse() : base() { }
        public override void Draw(System.Drawing.Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //phải xét lại để trong quá trình MouseMove
            int width = Math.Abs(endPoint.X - startPoint.X);
            int height = Math.Abs(endPoint.Y - startPoint.Y);
            gp = new GraphicsPath();
            int spX = Math.Min(startPoint.X, endPoint.X);
            int spY = Math.Min(startPoint.Y, endPoint.Y);
            mRec = new Rectangle(spX, spY, width, height);
            gp.AddEllipse(mRec);
            g.DrawPath(p, gp);
            gp.Dispose();
            //show control point
            if (this.isSelect)
            {
                this.getListControlPoint();
                this.showControlPoint(g);
            }
        }
    }
}
