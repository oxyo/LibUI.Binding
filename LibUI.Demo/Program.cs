﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LibUI.Demo
{
    class Program
    {
        static Slider s;
        static Spinbox n;
        static ProgressBar p;

        static long val = 0;

        static void ApplyValues(long v)
        {
            if (v >= 0 && v <= 100)
            {
                val = v;
                p.Value = val;
                s.Value = val;
                n.Value = val;
            }
        }

        [STAThread]
        static void Main(string[] args)
        {
            Application.Init();

            Menu fM = new Menu("File");
            CheckableMenuItem nI = fM.AppendCheckableItem("Hello world", true);
            MenuItem qI = fM.AppendQuitItem();

            Window w = new Window("Hello world", 300, 300, true);
            Button b = new Button("Increment me");
            s = new Slider(0, 100);
            n = new Spinbox(0, 100);
            p = new ProgressBar();
            Checkbox c = new Checkbox("Error");
            ColorButton cb = new ColorButton();
            EditableCombobox m = new EditableCombobox();
            m.Text = w.Title;
            VBox x = new VBox();

            b.Clicked += (o, e) =>
            {
                val++;
                ApplyValues(val);
            };
            EventHandler<EventArgs> handle = (o, e) =>
            {
                var v = ((INumInput)o).Value;
                ApplyValues(v);
            };
            n.Changed += handle;
            s.Changed += handle;
            cb.Changed += (o, e) =>
            {
                w.MessageBox("Color", String.Format("{0} {1} {2} {3}",
                    cb.Color.R, cb.Color.G, cb.Color.B, cb.Color.A));
            };
            nI.Clicked += (o, e) =>
            {
                w.MessageBox("result", nI.Checked.ToString());
            };
            w.Closing += (o, e) =>
            {
                w.MessageBox("Bye!", "See you later", c.Checked);
                w.Destroy();
                Application.Quit();
            };

            w.Margins = 5;
            w.Child = x;

            x.Padding = 5;
            x.Append(b);
            x.Append(s);
            x.Append(n);
            x.Append(p);
            x.Append(c);
            x.Append(cb);
            x.Append(m);
            w.Visible = true;

            Application.Main();
            Application.Finish();
        }
    }
}
