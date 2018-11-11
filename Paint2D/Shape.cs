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
    class Shape
    {
        //position of shape
        public Point startPoint;
        public Point endPoint;
        //Pen and Brush
        public Pen p;
        public Brush br;
        //Control Point
        public List<Rectangle> listControlPoint;
        public Rectangle currentCp;
        public int currentCpIndex;
        //GraphicsPath and Region
        public Region mRegion;
        public GraphicsPath gp;
        
        //Anchor & Check variable
        public Point anchor;
        public bool isFill;
        public bool isSelect;
        public bool isDraw;
        public bool isMove;
        public bool isResize;

       
        //default constructor
        public Shape()
        {
            startPoint = new Point(0, 0);
            endPoint = new Point(0, 0);

            p = new Pen(Color.Black, 1.0F);
            br = new SolidBrush(Color.White);

            mRegion = new Region();

            listControlPoint = new List<Rectangle>();
            isFill = false;
            isSelect = false;
            isDraw = false;
            isMove = false;
            isResize = false;
        }
        //check variable
        public virtual void unSelect()
        {
            this.isSelect = false;
        }
        public virtual void setSelect(){
            this.isSelect=true;
        }
        //Method
        public virtual void Draw(Graphics g) { }
        public virtual void mouseDown(MouseEventArgs e) { }
        public virtual void mouseMove(MouseEventArgs e) { }
        public virtual void mouseUp(MouseEventArgs e) { }
        public virtual bool isContain(Point location) {
            //return mRegion.IsVisible(location);
            return false;
        }
        public virtual void fill(Graphics g) { }
        //control point
        public virtual void showControlPoint(Graphics g)
        {
            Pen p = new Pen(Color.Red, 1.0F);
            Brush b = new SolidBrush(Color.Purple);
            foreach (Rectangle r in listControlPoint)
            {
                g.DrawRectangle(p, r);
                g.FillRectangle(b, r);
            }
        }
        public virtual void getListControlPoint() { }
        //--------
        public virtual void format2point()
        {
            int spX = Math.Min(startPoint.X, endPoint.X);
            int spY = Math.Min(startPoint.Y, endPoint.Y);
            int epX = Math.Max(startPoint.X, endPoint.X);
            int epY = Math.Max(startPoint.Y, endPoint.Y);
            //reformat 2 point
            startPoint = new Point(spX, spY);
            endPoint = new Point(epX, epY);
        }
        public virtual void mouseDoubleClick(int mode, MouseEventArgs e) { }
        // Kiểm tra vị trí tương đối của 1 điểm và 1 đối tượng
        // - 1 : Nằm ngoài đối tượng
        // 0   : Trong đối tượng
        // > 1 : Điểm điều khiển 
        public virtual int HitTest(Point point)
        {
            for (int i = 0; i < this.listControlPoint.Count; i++)
            {
                if (listControlPoint[i].Contains(point))
                    return i+1;
            }
            if (this.isContain(point))
                return 0;
            return -1;
        }
        public virtual void release()
        {
            this.isDraw = false;
            this.isMove = false;
            //this.isSelect = false;
            //this.isFill = false;
            this.isResize = false;
        }
    }
}
