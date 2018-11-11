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
    class MyRectangle:Shape
    {
        //Rectangle Attribute
        protected Rectangle mRec;
        //----------------------

        //Default Construtor
        public MyRectangle():base(){}
        //---------------

        public override void Draw(System.Drawing.Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //phải xét lại để trong quá trình MouseMove
            int width = Math.Abs(endPoint.X - startPoint.X);
            int height =Math.Abs(endPoint.Y - startPoint.Y);
            gp = new GraphicsPath();
            int spX = Math.Min(startPoint.X,endPoint.X);
            int spY = Math.Min(startPoint.Y,endPoint.Y);
            mRec= new Rectangle(spX, spY, width, height);
            gp.AddRectangle(mRec);
            g.DrawPath(p, gp);
            gp.Dispose();
            //ve xong roi reformat
            //mRegion = new Region(mRec);
            //show control point
            if (this.isSelect)
            {
                this.getListControlPoint();
                this.showControlPoint(g);
            }
        }
        //Override isContain
        public override bool isContain(System.Drawing.Point location)
        {
            return mRec.Contains(location);
        }
        public override void mouseDown(MouseEventArgs e) {
            //ve
            if (this.isDraw)
            {
                this.startPoint = e.Location;
                this.endPoint = e.Location;
            }
            //move or resize
            if (this.isMove || this.isResize)
            {
                this.anchor = e.Location;
            }
            
        }
        public override void mouseMove(MouseEventArgs e) {
            if (isDraw)
            {
                this.endPoint = e.Location;
            }
            //move
            if (isMove && e.Button==MouseButtons.Left)
            {
                int deltaX = e.Location.X - anchor.X;
                int deltaY = e.Location.Y - anchor.Y;
                this.startPoint.X += deltaX;
                this.startPoint.Y += deltaY;
                this.endPoint.X += deltaX;
                this.endPoint.Y += deltaY;
                anchor = e.Location;
            }
            if (isResize /*&& e.Button == MouseButtons.Left*/)
            {
                moveFollowControlPoint(e);
                //Update CurrentCp: Vi no chi duoc khoi tao luc Down thoi, nay la Move (mấy tiếng đồng hồ của bố) :'(

            }

        }
        
        public override void mouseUp(MouseEventArgs e) {
            if (isDraw)
            {
                this.endPoint = e.Location;
                //format2point();
                mRegion = new Region(mRec); //chuan bi sau nay thoi
            }
            this.release(); //isMove = false & isDraw = false & isReszie=false
        }
        public override void getListControlPoint()
        {
            Point topLeft = new Point(Math.Min(startPoint.X, endPoint.X), Math.Min(startPoint.Y, endPoint.Y));
            int DaiX = Math.Abs(endPoint.X - startPoint.X);
            int RongY = Math.Abs(endPoint.Y - startPoint.Y);
            //xoa listControlPoint cũ đi
            listControlPoint.Clear();
            this.listControlPoint.Add(new Rectangle(topLeft.X - 5, topLeft.Y - 5, 10, 10));
            // counter-lock thu tu theo chieu kim dong ho
            Point[] diem = new Point[8];
            diem[1] = new Point(topLeft.X + DaiX / 2, topLeft.Y);
            diem[2] = new Point(topLeft.X + DaiX, topLeft.Y);
            diem[3] = new Point(topLeft.X + DaiX, topLeft.Y+RongY/2);
            diem[4] = new Point(topLeft.X + DaiX, topLeft.Y+RongY);
            diem[5] = new Point(topLeft.X + DaiX / 2, topLeft.Y+RongY);
            diem[6] = new Point(topLeft.X, topLeft.Y + RongY);
            diem[7] = new Point(topLeft.X , topLeft.Y + RongY/2);
            for (int i = 1; i < 8; i++)
            {
                this.listControlPoint.Add(new Rectangle(diem[i].X - 5, diem[i].Y - 5, 10, 10));
            }
        }
        
        //fill rectangle
        public override void fill(Graphics g) {
            //this.fillColor = Color.Red;
            g.FillRectangle(br, mRec);
            
        }
        public void moveFollowControlPoint(MouseEventArgs e)
        {
            int deltaX = e.Location.X - anchor.X;
            int deltaY = e.Location.Y - anchor.Y;
            //
            //endPoint.X += deltaX;

            //
            if (currentCpIndex==0)
            {
                startPoint.X += deltaX;
                startPoint.Y += deltaY;
            }
            if (currentCpIndex==2)
            {
                startPoint.Y += deltaY;
                endPoint.X += deltaX;
            }
            if (currentCpIndex==4)
            {
                endPoint.X += deltaX;
                endPoint.Y += deltaY;
            }
            if (currentCpIndex==6)
            {
                startPoint.X += deltaX;
                endPoint.Y += deltaY;
            }
            if (currentCpIndex==1)
            {
                startPoint.Y += deltaY;
            }
            if (currentCpIndex==3)
            {
                endPoint.X += deltaX;
            }
            if (currentCpIndex==5)
            {
                endPoint.Y += deltaY;
            }
            if (currentCpIndex == 7)
            {
                startPoint.X += deltaX;
            }
            anchor = e.Location;
            //getListControlPoint();

        }
    }
}
