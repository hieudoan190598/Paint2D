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
    class MyPolyline:Shape
    {
        //attribute
        List<MyLine> listLine;
        MyLine curLine;
        public MyPolyline():base()
        {
            listLine = new List<MyLine>();
            //curLine = new MyLine();
        }
        public override void Draw(Graphics g)
        {
            foreach (MyLine l in listLine)
            {
                l.Draw(g);
            }
        }
        
       //Override setSelect()
        public override void setSelect()
        {
            //base.setSelect();
            foreach (MyLine l in listLine)
            {
                l.setSelect();
            }
        }
        public override void unSelect()
        {
            //base.unSelect();
            foreach (MyLine l in listLine)
            {
                l.unSelect();
            }
        }
        //
        bool firstTime = true;
        public override void mouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //ve
                if (this.isDraw)
                {
                    if (firstTime)
                    {
                        //first time
                        curLine = new MyLine();
                        curLine.mouseDown(e);
                        curLine.startPoint = e.Location;
                        listLine.Add(curLine);
                        firstTime = false;
                    }
                    curLine.endPoint = e.Location;
                }
                //move
                if (isMove)
                {
                    //only 1 time
                    this.anchor = e.Location;
                }

            }

        }
        public override void mouseMove( MouseEventArgs e)
        {
            curLine.mouseMove(e);
         
        }
        public override void mouseUp(MouseEventArgs e)
        {
            if (this.isDraw)
            {
                curLine.endPoint = e.Location;
                curLine = new MyLine();
                curLine.isDraw = true;
                listLine.Add(curLine);
                curLine.startPoint = e.Location;
                curLine.endPoint = e.Location;
            }
           
        }

        public override void mouseDoubleClick(int mode, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                isDraw = false;
                curLine.isDraw = false;
            }
        }

        public override bool isContain(Point location)
        {
            //return base.isContain(location);
            foreach (MyLine l in listLine)  
            {
                if (l.isContain(location))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
