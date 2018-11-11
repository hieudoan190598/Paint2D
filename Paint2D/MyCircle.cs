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
    class MyCircle:MyRectangle
    {
        public MyCircle():base(){ }
        public override void Draw(System.Drawing.Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //phải xét lại để trong quá trình MouseMove
            int width = Math.Abs(endPoint.X - startPoint.X);
            int height =Math.Abs(endPoint.Y - startPoint.Y);
            gp = new GraphicsPath();
            int spX = Math.Min(startPoint.X,endPoint.X);
            int spY = Math.Min(startPoint.Y,endPoint.Y);
            mRec = new Rectangle(spX, spY, Math.Max(width, height), Math.Max(width, height));
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
        public override void getListControlPoint()
        {
            Point topLeft = new Point(Math.Min(startPoint.X, endPoint.X), Math.Min(startPoint.Y, endPoint.Y));
            int DaiX = Math.Abs(endPoint.X - startPoint.X);
            int RongY = Math.Abs(endPoint.Y - startPoint.Y);
            DaiX = RongY = Math.Max(DaiX, RongY);
            //xoa listControlPoint cũ đi
            listControlPoint.Clear();
            this.listControlPoint.Add(new Rectangle(topLeft.X - 5, topLeft.Y - 5, 10, 10));
            // counter-lock thu tu theo chieu kim dong ho
            Point[] diem = new Point[8];
            diem[1] = new Point(topLeft.X + DaiX / 2, topLeft.Y);
            diem[2] = new Point(topLeft.X + DaiX, topLeft.Y);
            diem[3] = new Point(topLeft.X + DaiX, topLeft.Y + RongY / 2);
            diem[4] = new Point(topLeft.X + DaiX, topLeft.Y + RongY);
            diem[5] = new Point(topLeft.X + DaiX / 2, topLeft.Y + RongY);
            diem[6] = new Point(topLeft.X, topLeft.Y + RongY);
            diem[7] = new Point(topLeft.X, topLeft.Y + RongY / 2);
            for (int i = 1; i < 8; i++)
            {
                this.listControlPoint.Add(new Rectangle(diem[i].X - 5, diem[i].Y - 5, 10, 10));
            }
        }
    }
}
