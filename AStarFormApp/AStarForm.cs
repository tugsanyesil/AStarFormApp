using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Accord.Math;
using System.Threading;

namespace AStarFormApp
{
    public partial class AStarForm : Form
    {
        public AStarForm()
        {
            InitializeComponent();
        }

        int[,] M;
        int[,] Distances;
        int MWidth;
        int MHeight;
        Size Msize => new Size(MWidth, MHeight);
        Bitmap MBitmap => new Bitmap(MWidth, MHeight);

        int Unit;
        int ArrayPictureBoxUnit;
        int SpinWait = 4000000;

        void RefreshBox()
        {
            var b = MBitmap;
            using (Graphics g = Graphics.FromImage(b))
            {
                g.Clear(Color.Black);
            }
            MapPictureBox.Image = b;
        }

        void PaintBox()
        {
            Bitmap Bitmap = MBitmap;
            Bitmap UnitBitmap = GetUnitBitmap();

            using (var g = Graphics.FromImage(Bitmap))
            using (var gu = Graphics.FromImage(UnitBitmap))
            {
                for (int i = 0; i < M.GetLength(0); i++)
                {
                    for (int j = 0; j < M.GetLength(1); j++)
                    {
                        gu.Clear(PointTypeToColor((PointType)M[i, j]));
                        g.DrawImage(UnitBitmap, PointScale(new Point(j, i), Unit));
                    }
                }
            }

            MapPictureBox.Image = Bitmap;
            MapPictureBox.Refresh();
        }

        List<Point> GetNeighbors(Point P, List<Point> Ns) => Ns.ConvertAll(p => PointSum(P, p)).Where(p => p.InBorder(M.GetLength(1), M.GetLength(0))).Where(p => M[p.Y, p.X] == (int)PointType.Free || M[p.Y, p.X] == (int)PointType.Start || M[p.Y, p.X] == (int)PointType.End).ToList();

        List<Point> GetTrip()
        {
            var Trip = M.Apply((m, i, j) => Convert.ToInt32(NAs.ConvertAll(N => N + new Size(i, j)).Where(N => N.InBorder(M.GetLength(0), M.GetLength(1)) && m != (int)PointType.Wall).ToList().Where(N => M[N.X, N.Y] == (int)PointType.Wall).Count() > 0));
            var TripP = new List<Point>();

            var start = M.Find(m => m == (int)PointType.Start, true)[0];
            int[] first_start = new int[2] { start[0], start[1] };

            var end = M.Find(m => m == (int)PointType.End, true)[0];
            M[end[0], end[1]] = (int)PointType.Free;

            var TripCount = Trip.Sum() - 1;
            Trip[start[0], start[1]] = 100;
            TripP.Add(new Point(start[1], start[0]));

            for (int t = 0; t < TripCount; t++)
            {
                var P = Trip.Apply((m, i, j) =>
                {
                    if (m == 1)
                    {
                        M[i, j] = (int)PointType.End;
                        var ret = GetPath(false).Count;
                        M[i, j] = (int)PointType.Free;
                        return ret;
                    }
                    else { return 10000; }
                });
                var Pmin = P.Min();
                var next = P.Find(p => p == Pmin, true)[0];

                Trip[next[0], next[1]] = Trip[start[0], start[1]] + 1;

                M[next[0], next[1]] = (int)PointType.End;
                var path = GetPath(false);
                M[next[0], next[1]] = (int)PointType.Free;

                path.RemoveAt(0);
                TripP.AddRange(path);

                M[start[0], start[1]] = (int)PointType.Free;
                start[0] = next[0];
                start[1] = next[1];
                M[start[0], start[1]] = (int)PointType.Start;
            }

            M[start[0], start[1]] = (int)PointType.Free;
            M[first_start[0], first_start[1]] = (int)PointType.Start;
            M[end[0], end[1]] = (int)PointType.End;

            return TripP;
        }

        List<Point> GetPath(bool LookCorners)
        {
            Distances.Set(-1);
            var start = M.Find(m => m == (int)PointType.Start, true)[0];
            var end = M.Find(m => m == (int)PointType.End, true)[0];
            Point Start = new Point(start[1], start[0]);
            Point End = new Point(end[1], end[0]);
            Distances[Start.Y, Start.X] = 0;
            List<Point> Tagger = new List<Point>() { Start };
            List<Point> Path = new List<Point>() { End };
            bool whilebreak = false;

            while (Tagger.Count > 0)
            {
                var current = Tagger[0];
                Tagger.RemoveAt(0);
                foreach (Point n in GetNeighbors(current, Ns))
                {
                    if (Distances[n.Y, n.X] == -1)
                    {
                        Distances[n.Y, n.X] = Distances[current.Y, current.X] + 1;
                        Tagger.Add(n);
                        if (n == End) { Path.Add(current); whilebreak = true; break; }
                    }
                }
                if (whilebreak) { break; }
            }

            var sps = new List<Point>();
            var last = Path[Path.Count - 1];
            while (Distances[last.Y, last.X] != 0)
            {
                var ns = LookCorners ? GetNeighbors(last, NAs) : GetNeighbors(last, Ns);
                ns.RemoveAll(n => Distances[n.Y, n.X] == -1);

                var mnds = ns.Select(n => Distances[n.Y, n.X]).Min();
                ns.RemoveAll(n => Distances[n.Y, n.X] != mnds);


                if (ns.Count > 0) // due to momentum
                {
                    var nms = ns.ConvertAll(n => Distance(Path[Path.Count - 2], n));
                    var minms = nms.MaxIndex();
                    var mnms = nms[minms];
                    if (mnms != 4 && mnms != 8) // not flat
                    {
                        sps.Add(Path.Last());
                    }
                    Path.Add(ns[minms]);
                }
                else { break; }

                last = Path[Path.Count - 1];
            }
            Path.Reverse();
            return Path;
        }

        void ShowPath(Bitmap B, List<Point> Path, PictureBox p1, PictureBox p2)
        {
            List<PointF> ps = new List<PointF>();
            CubicSpline.FitParametric(Path.Select(p => (float)p.X).ToArray(), Path.Select(p => (float)p.Y).ToArray(), 100, out float[] xs, out float[] ys);
            for (int i = 0; i < xs.Length; i++)
            {
                ps.Add(new PointF(xs[i], ys[i]));
            }

            int c = 0;
            Bitmap Bitmap = (Bitmap)B.Clone();
            Bitmap UnitBitmap = GetUnitBitmap();
            using (var g = Graphics.FromImage(Bitmap))
            using (var gu = Graphics.FromImage(UnitBitmap))
            {
                Path.Where(s => !float.IsNaN(s.X) && !float.IsNaN(s.Y)).ToList().ForEach(s =>
                {
                    var f = (int)((255f * c++) / Path.Count);
                    gu.Clear(Color.FromArgb(255, f, 255 - f, 0));
                    g.DrawImage(UnitBitmap, PointScale(s, Unit));
                    p1.Image = Bitmap;
                    p1.Refresh();
                    Thread.SpinWait(SpinWait);
                });
            }


            Bitmap = (Bitmap)B.Clone();
            UnitBitmap = GetUnitBitmap(0.3f);

            using (var g = Graphics.FromImage(Bitmap))
            using (var gu = Graphics.FromImage(UnitBitmap))
            {
                gu.Clear(Color.LimeGreen);

                Path.Where(s => !float.IsNaN(s.X) && !float.IsNaN(s.Y)).ToList().ForEach(s =>
                {
                    g.DrawImage(UnitBitmap, PointScale(PointSum(s, 0.4f), Unit));
                    p2.Image = Bitmap;
                    p2.Refresh();
                    Thread.SpinWait(SpinWait);
                });
            }
        }

        private void AStarForm_Load(object sender, EventArgs e)
        {
            M = new int[,]
               {
                { 1,100,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
                { 2,2,2,2,2,2,2,2,2,2,2,2,2,1,1,1 },
                { 1,1,1,1,1,1,2,1,1,1,1,1,2,1,1,1 },
                { 1,1,1,1,1,1,2,1,2,1,1,1,2,1,2,1 },
                { 1,1,1,2,1,1,2,1,2,1,1,1,2,1,1,1 },
                { 1,1,1,2,1,1,2,1,2,1,1,1,2,2,1,1 },
                { 1,1,1,2,1,1,2,1,2,1,1,1,2,1,1,1 },
                { 1,1,1,2,1,1,2,1,1,1,1,1,1,1,1,1 },
                { 1,1,1,2,1,1,1,1,1,1,1,1,1,1,1,1 },
                { 1,1,50,2,1,1,1,1,1,1,1,1,1,1,1,1 },
               };

            Unit = 10;
            MWidth = M.GetLength(1) * Unit;
            MHeight = M.GetLength(0) * Unit;
            MapPictureBox.Size = N4_Path_PictureBox.Size = DotDot_N4_Path_PictureBox.Size = N8_Path_PictureBox.Size = DotDot_N8_Path_PictureBox.Size = Trip_PictureBox.Size = DotDot_Trip_PictureBox.Size = Msize;

            int Padding = 12;
            int X1 = Padding;
            int X2 = X1 + MWidth + Padding;
            int X3 = X2 + MWidth + Padding;

            MapPictureBox.Location = new Point(X1, Padding);
            N4_Path_PictureBox.Location = new Point(X1, MapPictureBox.Location.Y + MHeight + Padding);
            N8_Path_PictureBox.Location = new Point(X1, N4_Path_PictureBox.Location.Y + MHeight + Padding);
            Trip_PictureBox.Location = new Point(X1, N8_Path_PictureBox.Location.Y + MHeight + Padding);

            DotDot_N4_Path_PictureBox.Location = new Point(X2, N4_Path_PictureBox.Location.Y);
            DotDot_N8_Path_PictureBox.Location = new Point(X2, N8_Path_PictureBox.Location.Y);
            DotDot_Trip_PictureBox.Location = new Point(X2, Trip_PictureBox.Location.Y);

            ArrayPictureBoxUnit = 20;
            ArrayPictureBox.Location = new Point(X3, Padding);
            ArrayPictureBox.Size = new Size(M.GetLength(0) * ArrayPictureBoxUnit, M.GetLength(1) * ArrayPictureBoxUnit);
            GenerateButton.Location = new Point(X2 + ((MWidth - GenerateButton.Size.Width) / 2), Padding + ((MHeight - GenerateButton.Size.Height) / 2));
            ClientSize = new Size(X3 + ArrayPictureBox.Size.Width + Padding, Trip_PictureBox.Location.Y + MHeight + Padding);

            Distances = new int[M.GetLength(0), M.GetLength(1)];
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            Distances.Clear();
            RefreshBox();
            PaintBox();

            ShowPath((Bitmap)MapPictureBox.Image, GetPath(false), N4_Path_PictureBox, DotDot_N4_Path_PictureBox);
            ShowPath((Bitmap)MapPictureBox.Image, GetPath(true), N8_Path_PictureBox, DotDot_N8_Path_PictureBox);
            ShowPath((Bitmap)MapPictureBox.Image, GetTrip(), Trip_PictureBox, DotDot_Trip_PictureBox);

            var b = new Bitmap(ArrayPictureBox.Size.Width, ArrayPictureBox.Size.Height);
            using (Graphics g = Graphics.FromImage(b))
            {
                for (int i = 0; i < Distances.GetLength(0); i++)
                {
                    for (int j = 0; j < Distances.GetLength(1); j++)
                    {
                        g.DrawString(Distances[i, j].ToString(), DefaultFont, Brushes.Black, PointScale(new Point(j, i), ArrayPictureBoxUnit));
                    }
                }
            }
            ArrayPictureBox.Image = b;
        }

        enum PointType { Border = 0, Free = 1, Wall = 2, Start = 50, End = 100 }

        private Color PointTypeToColor(PointType type)
        {
            switch (type)
            {
                case PointType.Border: { return Color.Black; }
                case PointType.Free: { return Color.Aqua; }
                case PointType.Wall: { return Color.Brown; }
                case PointType.Start: { return Color.Green; }
                case PointType.End: { return Color.Red; }
                default: { return Color.White; }
            }
        }

        Point PointScale(Point p, int s) => new Point(p.X * s, p.Y * s);

        PointF PointScale(PointF p, int s) => new PointF(p.X * s, p.Y * s);

        Point PointSum(Point p1, Point p2) => new Point(p1.X + p2.X, p1.Y + p2.Y);

        PointF PointSum(PointF p, float f) => new PointF(p.X + f, p.Y + f);

        double Distance(Point p1, Point p2) => Math.Pow(p1.Y - p2.Y, 2) + Math.Pow(p1.X - p2.X, 2);

        Bitmap GetUnitBitmap(float ratio = 1f) => new Bitmap((int)(Unit * ratio), (int)(Unit * ratio));

        List<Point> Ns => new List<Point>() { new Point(0, -1), new Point(1, 0), new Point(0, 1), new Point(-1, 0) }.OrderBy(p => Distance(p, Point.Empty)).ToList();

        List<Point> NAs => new List<Point>() { new Point(-1, -1), new Point(0, -1), new Point(1, -1), new Point(1, 0), new Point(1, 1), new Point(0, 1), new Point(-1, 1), new Point(-1, 0) }.OrderBy(p => Distance(p, Point.Empty)).ToList();
    }

    public static class Exts
    {
        public static int MaxIndex(this List<double> Array) => Array.Select((value, index) => new { Value = value, Index = index }).Aggregate((a, b) => (a.Value > b.Value) ? a : b).Index;
        public static bool InBorder(this Point p, int X, int Y) => p.X >= 0 && p.X < X && p.Y >= 0 && p.Y < Y;
    }
}