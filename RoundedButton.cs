﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace CourseworkFifteen
{
    class RoundedButton : Button
    {
        #region -- Свойства --

        [Description("Цвет обводки (границы) кнопки")]
        public Color BorderColor { get; set; } = Color.Tomato;

        [Description("Указывает, включено ли использование отдельного цвета обводки (границы) кнопки")]
        public bool BorderColorEnabled { get; set; } = false;

        [Description("Цвет обводки (границы) кнопки при наведении курсора")]
        public Color BorderColorOnHover { get; set; } = Color.Tomato;

        [Description("Указывает, включено ли использование отдельного цвета обводки (границы) кнопки при наведении курсора")]
        public bool BorderColorOnHoverEnabled { get; set; } = false;

        [Description("Дополнительный фоновый цвет кнопки используемый для создания градиента (При BackColorGradientEnabled = true)")]
        public Color BackColorAdditional { get; set; } = Color.Gray;

        [Description("Указывает, включен ли градинт кнопки")]
        public bool BackColorGradientEnabled { get; set; } = false;

        [Description("Определяет направление линейного градиента шапки")]
        public LinearGradientMode BackColorGradientMode { get; set; } = LinearGradientMode.Horizontal;

        [Description("Текст, отображаемый при наведении курсора")]
        public string TextHover { get; set; }

        private bool roundingEnable = false;
        [Description("Вкл/Выкл закругление объекта")]
        public bool RoundingEnable
        {
            get => roundingEnable;
            set
            {
                roundingEnable = value;
                Refresh();
            }
        }

        private int roundingPercent = 100;
        [DisplayName("Rounding [%]")]
        [DefaultValue(100)]
        [Description("Указывает радиус закругления объекта в процентном соотношении")]
        public int Rounding
        {
            get => roundingPercent;
            set
            {
                if (value >= 0 && value <= 100)
                {
                    roundingPercent = value;

                    Refresh();
                }
            }
        }

        [Description("Вкл/Выкл эффект волны по нажатию кнопки курсором.")]
        public bool UseRippleEffect { get; set; } = true;

        [Description("Цвет эффекта волны по нажатию кнопки курсором")]
        public Color RippleColor { get; set; } = Color.Black;

        [Description("Вкл/Выкл эффект нажатия кнопки.")]
        public bool UseDownPressEffectOnClick { get; set; }

        public bool UseZoomEffectOnHover { get; set; }

        public override string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                Invalidate();
            }
        }

        #endregion

        #region -- Переменные --

        private StringFormat SF = new StringFormat();

        private bool MouseEntered = false;
        private bool MousePressed = false;

        Point ClickLocation = new Point();

        #endregion

        public RoundedButton()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor |
                ControlStyles.UserPaint |
                ControlStyles.Opaque |
                ControlStyles.Selectable |
                ControlStyles.UserMouse |
                ControlStyles.EnableNotifyMessage,
                true);
            DoubleBuffered = true;

            Size = new Size(100, 30);

            Font = new Font("Verdana", 8.25F, FontStyle.Regular);

            Cursor = Cursors.Hand;

            BackColor = Color.Tomato;
            BorderColor = BackColor;
            ForeColor = Color.White;

            SF.Alignment = StringAlignment.Center;
            SF.LineAlignment = StringAlignment.Center;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics graph = e.Graphics;
            graph.SmoothingMode = SmoothingMode.HighQuality;
            graph.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graph.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graph.SmoothingMode = SmoothingMode.AntiAlias;

            graph.Clear(Parent.BackColor);

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);

            // Закругление
            float roundingValue = 0.1F;
            if (RoundingEnable && roundingPercent > 0)
            {
                roundingValue = Height / 100F * roundingPercent;
            }
            GraphicsPath rectPath = Drawer.RoundedRectangle(rect, roundingValue);

            //Rectangle regionRect = rect;
            //regionRect.Inflate(1, 1);
            //GraphicsPath regionPath = Drawer.RoundedRectangle(regionRect, roundingValue);
            //Region = new Region(regionPath);
            Region = new Region(rectPath);
            graph.Clear(Parent.BackColor);


            Brush headerBrush = new SolidBrush(BackColor);
            if (BackColorGradientEnabled)
            {
                if (rect.Width > 0 && rect.Height > 0)
                    headerBrush = new LinearGradientBrush(rect, BackColor, BackColorAdditional, BackColorGradientMode);
            }

            Brush borderBrush = headerBrush;
            if (BorderColorEnabled)
            {
                borderBrush = new SolidBrush(BorderColor);

                if (MouseEntered && BorderColorOnHoverEnabled)
                    borderBrush = new SolidBrush(BorderColorOnHover);
            }

            // Основной прямоугольник (Фон)
            graph.DrawPath(new Pen(borderBrush), rectPath);
            graph.FillPath(headerBrush, rectPath);

            graph.SetClip(rectPath);


            if (MousePressed)
            {
                graph.DrawRectangle(new Pen(Color.FromArgb(30, Color.Black)), rect);
                graph.FillRectangle(new SolidBrush(Color.FromArgb(30, Color.Black)), rect);
            }

            // Рисуем текст
            if (string.IsNullOrEmpty(TextHover))
            {
                graph.DrawString(Text, Font, new SolidBrush(ForeColor), rect, SF);
            }
            else
            {

            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            MouseEntered = true;

            if (UseZoomEffectOnHover)
            {
                Rectangle buttonRect = new Rectangle(Location, Size);
                buttonRect.Inflate(1, 1);
                Location = buttonRect.Location;
                Size = buttonRect.Size;
            }

        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            MouseEntered = false;

            if (UseZoomEffectOnHover)
            {
                Rectangle buttonRect = new Rectangle(Location, Size);
                buttonRect.Inflate(-1, -1);
                Location = buttonRect.Location;
                Size = buttonRect.Size;
            }

        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            MousePressed = true;

            ClickLocation = e.Location;
            //ButtonRippleAction();

            if (UseDownPressEffectOnClick) Location = new Point(Location.X, Location.Y + 2);

            Focus();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            MousePressed = false;

            Invalidate();

            if (UseDownPressEffectOnClick) Location = new Point(Location.X, Location.Y - 2);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            //Invalidate();
        }

        protected override void OnParentBackColorChanged(EventArgs e)
        {
            Invalidate();
            base.OnParentBackColorChanged(e);
        }
    }
}
