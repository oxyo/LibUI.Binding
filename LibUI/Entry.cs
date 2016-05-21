﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LibUI
{
    /// <summary>
    /// Shared attributes between each Entry control type.
    /// </summary>
    public interface IEntry
    {
        string Text { get; set; }
        bool ReadOnly { get; set; }
        event EventHandler<EventArgs> Changed;
    }

    /// <summary>
    /// A text box where a single-line string can be entered.
    /// </summary>
    public class Entry : Control, IEntry
    {
        #region Interop
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern string uiEntryText(IntPtr control);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiEntrySetText(IntPtr control, string text);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern bool uiEntryReadOnly(IntPtr control);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiEntrySetReadOnly(IntPtr control, bool readOnly);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern IntPtr uiNewEntry(string text);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiEntryOnChanged(IntPtr c, uiEntryOnChangedDelegate f, IntPtr data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        protected delegate void uiEntryOnChangedDelegate(IntPtr c, IntPtr data);
        #endregion

        /// <summary>
        /// Creates an entry.
        /// </summary>
        /// <param name="text">
        /// The text to put into it, out of the box.
        /// </param>
        public Entry(string text = "")
        {
            Substrate = uiNewEntry(text);

            uiEntryOnChanged(Substrate, (b, f) =>
                { OnChanged(new EventArgs()); }, IntPtr.Zero);
        }

        /// <summary>
        /// Fired when the text in the entry is updated.
        /// </summary>
        public event EventHandler<EventArgs> Changed;

        protected virtual void OnChanged(EventArgs e)
        {
            Changed?.Invoke(this, e);
        }

        public string Text
        {
            get
            {
                return uiEntryText(Substrate);
            }
            set
            {
                uiEntrySetText(Substrate, value);
            }
        }

        public bool ReadOnly
        {
            get
            {
                return uiEntryReadOnly(Substrate);
            }
            set
            {
                uiEntrySetReadOnly(Substrate, value);
            }
        }
    }

    /// <summary>
    /// A text box where a multi-line text can be entered.
    /// </summary>
    public class MultilineEntry : Control, IEntry
    {
        #region Interop
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern string uiMultilineEntryText(IntPtr control);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiMultilineEntrySetText(IntPtr control, string text);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiMultilineEntryAppend(IntPtr control, string text);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern bool uiMultilineEntryReadOnly(IntPtr control);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiMultilineEntrySetReadOnly(IntPtr control, bool readOnly);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern IntPtr uiNewMultilineEntry(string text);
        [DllImport("libui.dll", CallingConvention = CallingConvention.Cdecl)]
        protected static extern void uiMultilineEntryOnChanged(IntPtr c, uiEntryOnChangedDelegate f, IntPtr data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        protected delegate void uiEntryOnChangedDelegate(IntPtr c, IntPtr data);
        #endregion

        /// <summary>
        /// Creates an entry with multiple lines.
        /// </summary>
        /// <param name="text">
        /// The text to put into it, out of the box.
        /// </param>
        public MultilineEntry(string text = "")
        {
            Substrate = uiNewMultilineEntry(text);

            uiMultilineEntryOnChanged(Substrate, (b, f) =>
                { OnChanged(new EventArgs()); }, IntPtr.Zero);
        }

        /// <summary>
        /// Fired when the text in the entry is updated.
        /// </summary>
        public event EventHandler<EventArgs> Changed;

        protected virtual void OnChanged(EventArgs e)
        {
            Changed?.Invoke(this, e);
        }

        public string Text
        {
            get
            {
                return uiMultilineEntryText(Substrate);
            }
            set
            {
                uiMultilineEntrySetText(Substrate, value);
            }
        }

        public bool ReadOnly
        {
            get
            {
                return uiMultilineEntryReadOnly(Substrate);
            }
            set
            {
                uiMultilineEntrySetReadOnly(Substrate, value);
            }
        }

        public void Append(string text)
        {
            uiMultilineEntryAppend(Substrate, text);
        }
    }
}